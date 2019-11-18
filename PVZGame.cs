using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Timers;
using System;

namespace PlantvsZombie
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PVZGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public HashSet<GameObject> ManagedObjects;
        public HashSet<Plant> Plants;
        public HashSet<Zombie> Zombies;
        private Dictionary<String, Texture2D> _TextureAssets = new Dictionary<string, Texture2D>();
        public Dictionary<String, Texture2D> TextureAssets
        {
            get
            {
                return _TextureAssets;
            }

        }
        float timeSinceLastSpawn;

        private Texture2D background;
        private Vector2 _ObjectPosition;
        private String _ObjectClassName;
        private MouseState mouseState;
        private Vector2 _PlantPosition;
        public const float Side=50;

        private PVZGame()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            ManagedObjects = new HashSet<GameObject>();
            Plants = new HashSet<Plant>();
            Zombies = new HashSet<Zombie>();
            timeSinceLastSpawn = 0f;
            SpawnZombie();
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Texture/Frontyard");
            _TextureAssets["BlueZombieMale"] = Content.Load<Texture2D>("Texture/BlueZombieMale");
            _TextureAssets["HeartShooter"] = Content.Load<Texture2D>("Texture/HeartShooter");

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            foreach (var ob in ManagedObjects)
            {
                ob.Update();

            }

            timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastSpawn >= 5f)
            {
                SpawnZombie();
            }
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //checking  before Spawn
                SpawnPlant(mouseState.X, mouseState.Y);
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
            spriteBatch.Begin();
            Rectangle rec = new Rectangle(0, 0, 800, 480);
            spriteBatch.Draw(background, rec, Color.White);
            foreach (var ob in ManagedObjects)
            {
                ob.Update();
                _ObjectPosition = ob.Position;
                _ObjectClassName = ob.GetType().Name;
                //ScaleFactor!
                if (_ObjectClassName != null)
                    spriteBatch.Draw(_TextureAssets[_ObjectClassName], _ObjectPosition, null, Color.White, 0f, Vector2.Zero, 0.3f, SpriteEffects.None, 0f);
            }
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void SpawnZombie()
        {
            Zombie z = new BlueZombieMale();
            ManagedObjects.Add(z);
            Zombies.Add(z);
            timeSinceLastSpawn = 0f;
        }

        public void SpawnPlant(int _X, int _Y)
        {
            _PlantPosition.X = _X;
            _PlantPosition.Y = _Y;
            Plant pl = new HeartShooter(_PlantPosition);
            ManagedObjects.Add(pl);
            Plants.Add(pl);
        }
        
        public void EndGame()
        {
            Exit(); //temporary
        }

    }
}