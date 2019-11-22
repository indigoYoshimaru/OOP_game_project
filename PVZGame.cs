﻿using Microsoft.Xna.Framework;
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
        private float scalefact = 0.2f;

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
            _TextureAssets["NormalZombie"] = Content.Load<Texture2D>("Texture/NormalZombie");
            _TextureAssets["PeaShooter"] = Content.Load<Texture2D>("Texture/PeaShooter");
            _TextureAssets["FlyingZombie"] = Content.Load<Texture2D>("Texture/FlyingZombie");
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

            var currentObjects = new HashSet<GameObject>(ManagedObjects);

            foreach (var ob in currentObjects)
            {
                ob.Update();

            }

            timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastSpawn >= 8f)
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
            var currentObjects = new HashSet<GameObject>(ManagedObjects);

            foreach (var ob in currentObjects)
            {
                ob.Update();
                _ObjectPosition = ob.Position;
                _ObjectClassName = ob.GetType().Name;
                //ScaleFactor!
                if (_ObjectClassName != null)
                    spriteBatch.Draw(_TextureAssets[_ObjectClassName], _ObjectPosition, null, Color.White, 0f, Vector2.Zero, scalefact, SpriteEffects.None, 0f);
            }
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void SpawnZombie()
        {
            Zombie z = new NormalZombie();
            Zombie f = new FlyingZombie();
            z.Died += HandleDeadZombie;
            f.Died += HandleDeadZombie;
            ManagedObjects.Add(z);
            ManagedObjects.Add(f);
            Zombies.Add(z);
            Zombies.Add(f);
            timeSinceLastSpawn = 0f;
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

        public void EndGame()
        {
            Exit(); //temporary
        }

    }
}