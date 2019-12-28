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
        public GraphicsDeviceManager Graphic { get; set; }
        public SpriteBatch SpriteBatch { get; set; }   

        public LogicManager LogicManager { get; set; }
        private IDisplay DisplayManager;
        public GameTime CurrentGameTime { get; private set; }

        public enum GameState { START_MENU, PLAYING, END_MENU };
        public GameState State { get; set; }

        public StartMenu StartMenu;
        public EndMenu EndMenu;

        private PVZGame()
        {
            Graphic = new GraphicsDeviceManager(this);
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
            
            DisplayManager = new DesktopDisplayManager();
            LogicManager = new LogicManager();
            StartMenu = new StartMenu(this);
            EndMenu = new EndMenu(this);
            Components.Add(StartMenu);
            Components.Add(EndMenu);

            LogicManager.Initialize();
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
            DisplayManager.LoadContent();
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

            LogicManager.Update(gameTime);
            
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            DisplayManager.Display(gameTime);

        }

        public void ToStartMenu()
        {
            State = GameState.START_MENU;
        }

        public void EnterGame()
        {
            State = GameState.PLAYING; 
            
        }

        public void ToEndMenu()
        {
            State = GameState.END_MENU;
        }

        public int MouseX()
        {
            return Mouse.GetState().X;
        }

        public int MouseY()
        {
            return Mouse.GetState().Y;
        }
    }
}