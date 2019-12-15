using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PVZGame:Game
    {
        GraphicsDeviceManager _Graphic;
        SpriteBatch _SpriteBatch;
        public Map GameMap;
        
        public PlayerManager Player { get; set; }
        public SpawnManager Spawner { get; set; }
        private SpriteFont _GameFont;
        public HashSet<GameObject> ManagedObjects;
        public HashSet<Plant> Plants;
        public HashSet<Zombie> Zombies;
        public List<String> ZombieTypes;

        public Dictionary<String, Texture2D> TextureAssets { get; set; } = new Dictionary<string, Texture2D>();
        public float TimeManager { get; set; }
        public float TimeSinceLastSpawn { get; set; }
        public GameTime CurrentGameTime { get; private set; }
        private int _UIScreen = 1; //display UIScreen

        private PVZGame()
        {
            _Graphic = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        public static readonly PVZGame Game = new PVZGame();

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameMap = new Map(GraphicsDevice.PresentationParameters.Bounds);
            ManagedObjects = new HashSet<GameObject>();
            Plants = new HashSet<Plant>();
            Zombies = new HashSet<Zombie>();
            ZombieTypes = new List<string>();
            ZombieTypes.Add("NormalZombie");
            ZombieTypes.Add("FlyingZombie");
            ZombieTypes.Add("LaneJumpingZombie");
            TimeSinceLastSpawn = 0f;
            TimeManager = 0f;
            Player = new PlayerManager();
            Spawner = new SpawnManager();

            Player.LoadHighScore();
            Spawner.SpawnZombie();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _SpriteBatch = new SpriteBatch(GraphicsDevice);
            TextureAssets["Lawn"] = Content.Load<Texture2D>("Texture/Background/Lawn");
            TextureAssets["NormalZombie"] = Content.Load<Texture2D>("Texture/Zombies/NormalZombie");
            TextureAssets["PeaShooter"] = Content.Load<Texture2D>("Texture/Plants/PeaShooter");
            TextureAssets["SunFlower"] = Content.Load<Texture2D>("Texture/Plants/SunFlower");
            TextureAssets["CarnivorousPlant"] = Content.Load<Texture2D>("Texture/Plants/CarnivorousPlant");
            TextureAssets["Bullet"] = Content.Load<Texture2D>("Texture/Miscellaneous/Bullet");
            TextureAssets["Sun"] = Content.Load<Texture2D>("Texture/Miscellaneous/Sun");
            TextureAssets["FlyingZombie"] = Content.Load<Texture2D>("Texture/Zombies/FlyingZombie");
            TextureAssets["LaneJumpingZombie"] = Content.Load<Texture2D>("Texture/Zombies/LaneJumpingZombie");
            TextureAssets["NormalMouse"]=Content.Load<Texture2D>("Texture/Miscellaneous/NormalMouse");
            _GameFont = Content.Load<SpriteFont>("Texture/Miscellaneous/GalleryFont");
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CurrentGameTime = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            /*if (_UIScreen == 1)
            {
                UI.Update();
            }*/

            //else
            {
                var currentObjects = new HashSet<GameObject>(ManagedObjects);

                foreach (var ob in currentObjects)
                {
                    try
                    {
                        ob.Update();
                    }
                    catch (Exception exc)
                    {
                        continue;
                    }
                }

                TimeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
                TimeManager += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimeSinceLastSpawn >= 5f)
                {
                    Spawner.SpawnZombie();
                    Spawner.SpawnSun();
                    TimeSinceLastSpawn = 0f;
                }

                Player.Update();
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _SpriteBatch.Begin();

            /*if (_UIScreen == 1)
              {
                 UI.Draw();
              }*/

            //else
            {
                Rectangle rec = new Rectangle(0, 0, 800, 480);
                _SpriteBatch.Draw(TextureAssets["Lawn"], rec, Color.White);
                var currentObjects = new HashSet<GameObject>(ManagedObjects);

                foreach (var ob in currentObjects)
                {
                    ob.Update();
                    String objectClassName = ob.GetType().Name;

                    if (objectClassName != null)
                        Utility.DrawCenter(_SpriteBatch, TextureAssets[objectClassName], ob.Position, GameMap.TileSize.X, GameMap.TileSize.X);
                }
                _SpriteBatch.DrawString(_GameFont, "SCORE: " + Player.GetScore().ToString(), new Vector2(5, _Graphic.PreferredBackBufferHeight - 40), Color.White); //display score at the bottom left
                _SpriteBatch.DrawString(_GameFont, "HIGHSCORE: " + Player.GetHighScore().ToString(), new Vector2(480, _Graphic.PreferredBackBufferHeight - 40), Color.White);
                _SpriteBatch.DrawString(_GameFont, "SUN: " + Player.GetTotalSun().ToString(), new Vector2(10, 10), Color.White);

                switch (Player.GetMouseIcon())
                {
                    case PlayerManager.MouseIcon.NORMAL:
                        _SpriteBatch.Draw(TextureAssets["NormalMouse"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
                        break;
                    case PlayerManager.MouseIcon.PEASHOOTER:
                        Utility.DrawCenter(_SpriteBatch, TextureAssets["PeaShooter"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), GameMap.TileSize.X, GameMap.TileSize.X);
                        break;
                    case PlayerManager.MouseIcon.SUNFLOWER:
                        Utility.DrawCenter(_SpriteBatch, TextureAssets["SunFlower"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), GameMap.TileSize.X, GameMap.TileSize.X);
                        break;
                    case PlayerManager.MouseIcon.CARNIVOROUSPLANT:
                        Utility.DrawCenter(_SpriteBatch, TextureAssets["CarnivorousPlant"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), GameMap.TileSize.X, GameMap.TileSize.X);
                        break;
                }
            }
            _SpriteBatch.End();
            base.Draw(gameTime);
        }

        public void EndGame()
        {
            //UI.ShowEndScreen();
            Player.SaveHighScore();
        }

    }
}