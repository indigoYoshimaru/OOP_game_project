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
        public PlayerManagement management;
        public SpriteFont gameFont;
        public HashSet<GameObject> ManagedObjects;
        public HashSet<Plant> Plants;
        public HashSet<Zombie> Zombies;
        //public HashSet<Tile> Tiles;
        public HashSet<Bullet> Bullets;
        private Dictionary<String, Texture2D> _TextureAssets = new Dictionary<string, Texture2D>();
        public Dictionary<String, Texture2D> TextureAssets
        {
            get
            {
                return _TextureAssets;
            }

        }
        float _TimeSinceLastSpawn;

        private Texture2D _Background;
        private Texture2D normalMouse;
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
            management = new PlayerManagement();
            Plants = new HashSet<Plant>();
            Zombies = new HashSet<Zombie>();
            //Tiles = new HashSet<Tile>();
            Bullets = new HashSet<Bullet>();
            _TimeSinceLastSpawn = 0f;
            SpawnZombie();
            //this.IsMouseVisible = true;
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
            gameFont = Content.Load<SpriteFont>("Texture/galleryFont");
            normalMouse = Content.Load<Texture2D>("Texture/normalmouse");
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
            if (_TimeSinceLastSpawn >= 5f)
            {
                SpawnZombie();
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
                SpawnPlant(_CurrentMouseState.X, _CurrentMouseState.Y);

            }
            _OldMouseState = _CurrentMouseState;

            management.Controller();
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

            _SpriteBatch.DrawString(gameFont, "Score: " + management.GetScore().ToString(), new Vector2(0, _Graphic.PreferredBackBufferHeight - 30), Color.White); //display score at the bottom left

            switch (Game.management.GetMouseIcon())
            {
                case PlayerManagement.MouseIcon.NORMAL:
                    _SpriteBatch.Draw(normalMouse, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
                    break;
                case PlayerManagement.MouseIcon.PEASHOOTER:
                    _SpriteBatch.Draw(_TextureAssets["PeaShooter"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
                    break;
                case PlayerManagement.MouseIcon.SUNFLOWER:
                    _SpriteBatch.Draw(_TextureAssets["SunFlower"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), null, Color.White, 0f, Vector2.Zero, _ScaleFact, SpriteEffects.None, 0f);
                    break;
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