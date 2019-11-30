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
        private PlayerManagement _Player;
        private SpriteFont _GameFont;
        private Texture2D _NormalMouse;
        public HashSet<GameObject> ManagedObjects;
        public HashSet<Plant> Plants;
        public HashSet<Zombie> Zombies;
        public HashSet<Bullet> Bullets;
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
        private float _TimeManager;


        private Texture2D _Background;
        private Vector2 _ObjectPosition;
        private String _ObjectClassName;
        private MouseState _CurrentMouseState;
        private MouseState _OldMouseState;
        private Random _Rand= new Random();

        private Vector2 _PlantPosition;
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
            //Tiles = new HashSet<Tile>();
            Bullets = new HashSet<Bullet>();
            ZombieTypes = new List<string>();
            ZombieTypes.Add("NormalZombie");
            ZombieTypes.Add("FlyingZombie");
            ZombieTypes.Add("LaneJumpingZombie");
            _TimeSinceLastSpawn = 0f;
            _TimeManager = 0f;
            _Player = new PlayerManagement();
           
            SpawnZombie();
            
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

            _TimeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _TimeManager += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_TimeSinceLastSpawn >= 5f)
            {
                SpawnZombie();
            }
            _CurrentMouseState = Mouse.GetState();
            if (_CurrentMouseState.LeftButton == ButtonState.Pressed&& _OldMouseState.LeftButton==ButtonState.Released)
            {
                //checking  before Spawn
                //foreach (var t:Tiles)
                //{
                //    if (t.BoundingRectangle.Contains(_CurrentMouseState.Position){
                //        SpawnPlant(_CurrentMouseState.X, _CurrentMouseState.Y);
                //    }
                        
                //}
                SpawnPlant(_CurrentMouseState.X, _CurrentMouseState.Y);
                
            }
            _OldMouseState = _CurrentMouseState;

            _Player.Controller();
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
                _ObjectPosition = ob.Position;
                _ObjectClassName = ob.GetType().Name;
                
                if (_ObjectClassName != null)
                    //_SpriteBatch.Draw(_TextureAssets[_ObjectClassName], _ObjectPosition, null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
                    Utility.DrawCenter(_SpriteBatch, _TextureAssets[_ObjectClassName], _ObjectPosition, GameMap.TileSize.X , GameMap.TileSize.X);
            }
            _SpriteBatch.DrawString(_GameFont, "Score: " + _Player.GetScore().ToString(), new Vector2(0, _Graphic.PreferredBackBufferHeight - 30), Color.White); //display score at the bottom left

            
            switch (_Player.GetMouseIcon())
            {
                case PlayerManagement.MouseIcon.NORMAL:
                    _SpriteBatch.Draw(_NormalMouse, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
                    break;
                case PlayerManagement.MouseIcon.PEASHOOTER:
                    _SpriteBatch.Draw(_TextureAssets["PeaShooter"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
                    break;
                case PlayerManagement.MouseIcon.SUNFLOWER:
                    _SpriteBatch.Draw(_TextureAssets["SunFlower"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
                    break;
            }

            _SpriteBatch.End();
            base.Draw(gameTime);
        }


        public void SpawnZombie()
        {
            Zombie z= null; 

            if (_TimeManager<=20f)
            {
                z=new NormalZombie();
                ManagedObjects.Add(z);
                Zombies.Add(z);
                z.Died+=HandleDeadZombie;
                _TimeSinceLastSpawn=0f;

            }

            else if (_TimeManager>=40f)
            {
                int num = _Rand.Next(ZombieTypes.Count);
                
                switch(ZombieTypes[num]){
                    case "NormalZombie":
                        z=new NormalZombie();
                        break;
                    case "FlyingZombie":
                        z=new FlyingZombie();
                        break;
                    case "LaneJumpingZombie":
                        z=new LaneJumpingZombie();
                        break;
                }

                z.Died+=HandleDeadZombie;
                ManagedObjects.Add(z);
                Zombies.Add(z);
                _TimeSinceLastSpawn=0f;
                
            }

            else if (_TimeManager>=90f)
            {
                _TimeManager=0f;
                _TimeSinceLastSpawn=0f;
            }

            else
            {
                int num = _Rand.Next(ZombieTypes.Count-1);
                
                switch(ZombieTypes[num]){
                    case "NormalZombie":
                        z=new NormalZombie();
                        break;
                    case "FlyingZombie":
                        z=new FlyingZombie();
                        break;
                }

                z.Died+=HandleDeadZombie;
                ManagedObjects.Add(z);
                Zombies.Add(z);
                _TimeSinceLastSpawn=0f;

            }

        }

        private void HandleDeadZombie(object self)
        {
            ManagedObjects.Remove((GameObject)self);
            Zombies.Remove((Zombie)self);
        }

        public void SpawnPlant(int _X, int _Y)
        {
            _PlantPosition.X = _X;
            _PlantPosition.Y = _Y;
            Plant pl = new PeaShooter(_PlantPosition);
            pl.Died += HandleDeadPlantObject;
            ManagedObjects.Add(pl);
            Plants.Add(pl);
        }

        private void HandleDeadPlantObject(object self)
        {
            ManagedObjects.Remove((GameObject)self);
            Plants.Remove((Plant)self);
        }

        public void SpawnBullet(PeaShooter p)
        {
            Bullet bul = new Bullet(p);
            bul.Died += HandleDeadBullet;
            ManagedObjects.Add(bul);
            Bullets.Add(bul);
        }

        private void HandleDeadBullet(object self)
        {
            ManagedObjects.Remove((GameObject)self);
            Bullets.Remove((Bullet)self);
        }

        public void EndGame()
        {
            Exit(); //temporary
        }

    }
}