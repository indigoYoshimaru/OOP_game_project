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
        GraphicsDeviceManager _Graphic;
        SpriteBatch _SpriteBatch;
        public Map GameMap;
        
        public PlayerManager Player { get; set; }
        public SpawnManager Spawner { get; set; }
        private SpriteFont _GameFont;
        private Texture2D _NormalMouse;
        public HashSet<GameObject> ManagedObjects;
        public HashSet<Plant> Plants;
        public HashSet<Zombie> Zombies;
        public List<String> ZombieTypes;
        private Dictionary<String, Texture2D> _TextureAssets = new Dictionary<string, Texture2D>();
        public Dictionary<String, Texture2D> TextureAssets
        {
            get
            {
                return _TextureAssets;
            }

        }
        private float _TimeSinceLastSpawn;
        public float TimeSinceLastSpawn
        {
            get { return _TimeSinceLastSpawn; }
            set { _TimeSinceLastSpawn = value; }
        }
        private float _TimeManager;
        public float TimeManager
        {
            get { return _TimeManager; }
            set { _TimeManager = value;  }
        }

        private Texture2D _Background;
        private MouseState _CurrentMouseState;
        private MouseState _OldMouseState;
        private Tile _MouseTile;

        public const float Side=50;
        private float _ScaleFact = 0.1f;
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
            _TimeSinceLastSpawn = 0f;
            _TimeManager = 0f;
            Player = new PlayerManager();
            Spawner = new SpawnManager();
            
           
            Spawner.SpawnZombie();
            
            _OldMouseState = Mouse.GetState();

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
            _TextureAssets["NormalZombie"] = Content.Load<Texture2D>("Texture/Zombies/NormalZombie");
            _TextureAssets["PeaShooter"] = Content.Load<Texture2D>("Texture/Plants/PeaShooter");
            _TextureAssets["SunFlower"] = Content.Load<Texture2D>("Texture/Plants/SunFlower");
            _TextureAssets["Bullet"] = Content.Load<Texture2D>("Texture/Miscellaneous/Bullet");
            _TextureAssets["Sun"] = Content.Load<Texture2D>("Texture/Miscellaneous/Sun");
            _TextureAssets["FlyingZombie"] = Content.Load<Texture2D>("Texture/Zombies/FlyingZombie");
            _TextureAssets["LaneJumpingZombie"] = Content.Load<Texture2D>("Texture/Zombies/LaneJumpingZombie");
            _NormalMouse = Content.Load<Texture2D>("Texture/Miscellaneous/NormalMouse");
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

            var currentObjects = new HashSet<GameObject>(ManagedObjects);

            foreach (var ob in currentObjects)
            {
                ob.Update();

            }

            TimeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeManager += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TimeSinceLastSpawn >= 6f)
            {
                Spawner.SpawnZombie();
                Spawner.SpawnSun();
            }

            Player.Controller();
            _CurrentMouseState = Mouse.GetState();
            if (_CurrentMouseState.LeftButton == ButtonState.Pressed&& _OldMouseState.LeftButton==ButtonState.Released)
            {
                
                _MouseTile = GameMap.GetTileAt(_CurrentMouseState.Position.ToVector2());

                Spawner.SpawnPlant(_MouseTile);
                
            }
            _OldMouseState = _CurrentMouseState;

            //Player.Controller();
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
            Rectangle rec = new Rectangle(0, 0,800, 480);
            _SpriteBatch.Draw(_Background, rec, Color.White);
            var currentObjects = new HashSet<GameObject>(ManagedObjects);

            foreach (var ob in currentObjects)
            {
                ob.Update();
                String objectClassName = ob.GetType().Name;
                
                if (objectClassName != null)
                    //_SpriteBatch.Draw(_TextureAssets[objectClassName], _ObjectPosition, null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
                    Utility.DrawCenter(_SpriteBatch, _TextureAssets[objectClassName], ob.Position, GameMap.TileSize.X , GameMap.TileSize.X);
            }
            _SpriteBatch.DrawString(_GameFont, "Score: " + Player.GetScore().ToString(), new Vector2(0, _Graphic.PreferredBackBufferHeight - 30), Color.White); //display score at the bottom left

            
            switch (Player.GetMouseIcon())
            {
                case PlayerManager.MouseIcon.NORMAL:
                    _SpriteBatch.Draw(_NormalMouse, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
                    break;
                case PlayerManager.MouseIcon.PEASHOOTER:
                    _SpriteBatch.Draw(_TextureAssets["PeaShooter"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
                    break;
                case PlayerManager.MouseIcon.SUNFLOWER:
                    _SpriteBatch.Draw(_TextureAssets["SunFlower"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
                    break;
            }

            _SpriteBatch.End();
            base.Draw(gameTime);
        }

        public void EndGame()
        {
            Exit(); //temporary
        }

    }
}