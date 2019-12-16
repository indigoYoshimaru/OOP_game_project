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
        public SpriteFont GameFont;
        public GraphicsDeviceManager Graphics;
        public StartMenu StartMenu;
        public EndMenu EndMenu;
        private MouseState _CurrentMouseState;
        private MouseState _OldMouseState;

        public enum GameState { START_MENU, PLAYING, END_MENU };

        public float TimeSinceLastSpawn { get; set; }

        public Dictionary<string, Texture2D> TextureAssets { get; } = new Dictionary<string, Texture2D>();
        public float TimeManager { get; set; }

        public PlayerManager Player { get; set; }
        public SpawnManager Spawner { get; set; }
        public GameTime CurrentGameTime { get; private set; }
        public SpriteBatch SpriteBatch { get; set; }
        public GameState State { get; set; }

        private PVZGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        public static readonly PVZGame Game = new PVZGame();


        public int MouseX()
        {
            return Mouse.GetState().X;
        }

        public int MouseY()
        {
            return Mouse.GetState().Y;
        }

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

            StartMenu = new StartMenu(this);
            EndMenu = new EndMenu(this);
            Components.Add(StartMenu);
            Components.Add(EndMenu);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            TextureAssets["EndMenuBG"] = Content.Load<Texture2D>("Texture/Background/EndMenuBG");
            TextureAssets["StartMenuBG"] = Content.Load<Texture2D>("Texture/Background/StartMenuBG");
            TextureAssets["Background"] = Content.Load<Texture2D>("Texture/Background/Lawn");
            TextureAssets["NormalZombie"] = Content.Load<Texture2D>("Texture/Zombies/NormalZombie");
            TextureAssets["PeaShooter"] = Content.Load<Texture2D>("Texture/Plants/PeaShooter");
            TextureAssets["SunFlower"] = Content.Load<Texture2D>("Texture/Plants/SunFlower");
            TextureAssets["CarnivorousPlant"] = Content.Load<Texture2D>("Texture/Plants/CarnivorousPlant");
            TextureAssets["Bullet"] = Content.Load<Texture2D>("Texture/Miscellaneous/Bullet");
            TextureAssets["Sun"] = Content.Load<Texture2D>("Texture/Miscellaneous/Sun");
            TextureAssets["FlyingZombie"] = Content.Load<Texture2D>("Texture/Zombies/FlyingZombie");
            TextureAssets["LaneJumpingZombie"] = Content.Load<Texture2D>("Texture/Zombies/LaneJumpingZombie");
            TextureAssets["NormalMouse"] = Content.Load<Texture2D>("Texture/Miscellaneous/NormalMouse");
            GameFont = Content.Load<SpriteFont>("Texture/Miscellaneous/GalleryFont");
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
            DisplayManager.Displayer.Display(gameTime);
        }

        public void ToStartMenu()
        {
            State = GameState.START_MENU;
        }

        public void EnterGame()
        {
            State = GameState.PLAYING;
            Console.WriteLine(Graphics.PreferredBackBufferWidth);
            Console.WriteLine(Graphics.PreferredBackBufferHeight);
        }

        public void ToEndMenu()
        {
            State = GameState.END_MENU;
        }

        public void EndGame()
        {
            //Exit(); //temporary
        }

    }
}