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
    public class PVZGame : Game
    {
        public Map GameMap;
        public HashSet<GameObject> ManagedObjects;
        public HashSet<Plant> Plants;
        public HashSet<Zombie> Zombies;
        public List<string> ZombieTypes;

        GraphicsDeviceManager _Graphic;
        SpriteBatch _SpriteBatch;

        private GameState _State;
        private SpriteFont _GameFont;
        private Texture2D _Background;
        private MouseState _CurrentMouseState;
        private MouseState _OldMouseState;
        private StartMenu _StartMenu;
        private EndMenu _EndMenu;

        public enum GameState { START_MENU, PLAYING, END_MENU };

        public float TimeSinceLastSpawn { get; set; }

        public Dictionary<string, Texture2D> TextureAssets { get; } = new Dictionary<string, Texture2D>();
        public float TimeManager { get; set; }

        public PlayerManager Player { get; set; }
        public SpawnManager Spawner { get; set; }

        public GameTime CurrentGameTime { get; private set; }

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
            _OldMouseState = Mouse.GetState();

            _StartMenu = new StartMenu(this);
            _EndMenu = new EndMenu(this);
            Components.Add(_StartMenu);
            Components.Add(_EndMenu);

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

            _Background = Content.Load<Texture2D>("Texture/Background/Lawn");
            TextureAssets["NormalZombie"] = Content.Load<Texture2D>("Texture/Zombies/NormalZombie");
            TextureAssets["PeaShooter"] = Content.Load<Texture2D>("Texture/Plants/PeaShooter");
            TextureAssets["SunFlower"] = Content.Load<Texture2D>("Texture/Plants/SunFlower");
            TextureAssets["CarnivorousPlant"] = Content.Load<Texture2D>("Texture/Plants/CarnivorousPlant");
            TextureAssets["Bullet"] = Content.Load<Texture2D>("Texture/Miscellaneous/Bullet");
            TextureAssets["Sun"] = Content.Load<Texture2D>("Texture/Miscellaneous/Sun");
            TextureAssets["FlyingZombie"] = Content.Load<Texture2D>("Texture/Zombies/FlyingZombie");
            TextureAssets["LaneJumpingZombie"] = Content.Load<Texture2D>("Texture/Zombies/LaneJumpingZombie");
            TextureAssets["NormalMouse"] = Content.Load<Texture2D>("Texture/Miscellaneous/NormalMouse");
            _GameFont = Content.Load<SpriteFont>("Texture/Miscellaneous/GalleryFont");
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

            var currentObjects = new HashSet<GameObject>(ManagedObjects);

            foreach (var ob in currentObjects)
            {
                ob.Update();

            }

            TimeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeManager += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TimeSinceLastSpawn >= 5f)
            {
                Spawner.SpawnZombie();
                Spawner.SpawnSun();
            }

            //call player Update()
            Player.Controller();
            _CurrentMouseState = Mouse.GetState();
            if (_CurrentMouseState.LeftButton == ButtonState.Pressed && _OldMouseState.LeftButton == ButtonState.Released)
            {

                Tile tile = GameMap.GetTileAt(_CurrentMouseState.Position.ToVector2());
                {
                    Spawner.SpawnPlant(tile);
                }
            }
            _OldMouseState = _CurrentMouseState;


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
            Rectangle rec = new Rectangle(0, 0, 800, 480);
            _SpriteBatch.Draw(_Background, rec, Color.White);
            var currentObjects = new HashSet<GameObject>(ManagedObjects);

            foreach (var ob in currentObjects)
            {
                ob.Update();
                string objectClassName = ob.GetType().Name;

                if (objectClassName != null)
                    //_SpriteBatch.Draw(_TextureAssets[objectClassName], _ObjectPosition, null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
                    Utility.DrawCenter(_SpriteBatch, TextureAssets[objectClassName], ob.Position, GameMap.TileSize.X, GameMap.TileSize.X);
            }
            _SpriteBatch.DrawString(_GameFont, "Score: " + Player.GetScore().ToString(), new Vector2(0, _Graphic.PreferredBackBufferHeight - 30), Color.White); //display score at the bottom left


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

            _SpriteBatch.End();
            base.Draw(gameTime);
        }

        public void StartMenu()
        {
            _State = GameState.START_MENU;
            _StartMenu.Visible = true;
        }

        public void StartGame()
        {
            _State = GameState.PLAYING;
            _StartMenu.Visible = false;
            _EndMenu.Visible = false;
        }

        public void EndMenu()
        {
            _State = GameState.END_MENU;
            _EndMenu.Visible = true;
        }

        public void EndGame()
        {
            Exit(); //temporary
        }

    }
}