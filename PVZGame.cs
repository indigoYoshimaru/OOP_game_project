using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Collections;

namespace PlantvsZombie
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PVZGame : Game
    {
        GraphicsDeviceManager _Graphic;
        SpriteBatch _SpriteBatch;
        public HashSet<GameObject> ManagedObjects;
        public HashSet<Plant> Plants;
        public HashSet<Zombie> Zombies;
        //public HashSet<Tile> Tiles;
        public HashSet<Bullet> Bullets;
        public HashSet<Sun> Suns;
        public ArrayList sunList = new ArrayList();
        public int checkClick = 0;
        private Dictionary<String, Texture2D> _TextureAssets = new Dictionary<string, Texture2D>();
        public Dictionary<String, Texture2D> TextureAssets
        {
            get
            {
                return _TextureAssets;
            }

        }
        float _TimeSinceLastSpawn;
        float _TimeSpawnSkySun;
        private Texture2D _Background;
        private Vector2 _ObjectPosition;
        private String _ObjectClassName;
        private MouseState _CurrentMouseState;
        private MouseState _OldMouseState;

        private Vector2 _PlantPosition;
        public const float Side = 50;
        private float _ScaleFact = 0.2f;
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

            ManagedObjects = new HashSet<GameObject>();
            Plants = new HashSet<Plant>();
            Zombies = new HashSet<Zombie>();
            //Tiles = new HashSet<Tile>();
            Bullets = new HashSet<Bullet>();
            Suns = new HashSet<Sun>();
            _TimeSinceLastSpawn = 0f;
            SpawnZombie();
            this.IsMouseVisible = true;
            _OldMouseState = Mouse.GetState();
            _TimeSpawnSkySun = 0f;
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
            _TextureAssets["Bullet"] = Content.Load<Texture2D>("Texture/Miscellaneous/Bullet");
            _TextureAssets["FlyingZombie"] = Content.Load<Texture2D>("Texture/Zombies/FlyingZombie");
            _TextureAssets["LaneJumpingZombie"] = Content.Load<Texture2D>("Texture/Zombies/LaneJumpingZombie");
            _TextureAssets["Sun"] = Content.Load<Texture2D>("Texture/Miscellaneous/Sun");
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
            _TimeSpawnSkySun += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_TimeSpawnSkySun > 5f)
            {
                SpawnSun();
                _TimeSpawnSkySun = 0;

            }
            if (_TimeSinceLastSpawn >= 5f)
            {
                //SpawnZombie();
            }
            _CurrentMouseState = Mouse.GetState();
            if (_CurrentMouseState.LeftButton == ButtonState.Pressed && _OldMouseState.LeftButton == ButtonState.Released)
            {
                //checking  before Spawn
                //foreach (var t:Tiles)
                //{
                //    if (t.BoundingRectangle.Contains(_CurrentMouseState.Position){
                //        SpawnPlant(_CurrentMouseState.X, _CurrentMouseState.Y);
                //    }
                //}
                CollectSun(_CurrentMouseState.X - 50, _CurrentMouseState.Y - 50);
                if (checkClick == 0)
                {
                    SpawnPlant(_CurrentMouseState.X - 50, _CurrentMouseState.Y - 50);
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
                _ObjectPosition = ob.Position;
                _ObjectClassName = ob.GetType().Name;

                if (_ObjectClassName != null)
                    _SpriteBatch.Draw(_TextureAssets[_ObjectClassName], _ObjectPosition, null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
            }
            // TODO: Add your drawing code here
            _SpriteBatch.End();
            base.Draw(gameTime);
        }


        public void SpawnZombie()
        {
            Zombie z = new NormalZombie();
            Zombie z1 = new FlyingZombie();
            Zombie z2 = new LaneJumpingZombie();

            z2.Died += HandleDeadZombie;
            z1.Died += HandleDeadZombie;
            z.Died += HandleDeadZombie;

            ManagedObjects.Add(z);
            ManagedObjects.Add(z1);
            ManagedObjects.Add(z2);

            Zombies.Add(z);
            Zombies.Add(z1);
            Zombies.Add(z2);
            _TimeSinceLastSpawn = 0f;
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

        public void SpawnBullet(Plant p)
        {
            Bullet bul = new Bullet(p);
            bul.Died += HandleDeadBullet;
            ManagedObjects.Add(bul);
            Bullets.Add(bul);
        }

        public void SpawnSun(Plant p)
        {
            Sun sun = new Sun(p);
            sunList.Add(sun);
            sun.Died += HandleDeadSun;
            ManagedObjects.Add(sun);
            Suns.Add(sun);  
        }
        public void SpawnSun()
        {
            Sun sun = new Sun();
            sunList.Add(sun);
            sun.Died += HandleDeadSun;
            ManagedObjects.Add(sun);
            Suns.Add(sun);
        }
        private void HandleDeadBullet(object self)
        {
            ManagedObjects.Remove((GameObject)self);
            Bullets.Remove((Bullet)self);
        }

        private void HandleDeadSun(object self)
        {
            ManagedObjects.Remove((GameObject)self);
            Suns.Remove((Sun)self);
        }
        public void CollectSun(int _X, int _Y)
        {
            {
                foreach (Sun sun in sunList)
                {
                    if (_X > sun.Position.X - 50 && _X < sun.Position.X + 50 && _Y > sun.Position.Y - 60 && _Y < sun.Position.Y + 60)
                    {
                        sun.Die();
                        checkClick = 1;
                    }
                    else checkClick = 0;
                }
            }
        }
        public void EndGame()
        {
            Exit(); //temporary
        }

    }
}
