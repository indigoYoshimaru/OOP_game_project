using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using MonoGame.UI.Forms;
using Microsoft.Xna.Framework.Content;

namespace PlantvsZombie
{
    public class StartMenu : ControlManager
    {

        SpriteBatch _SpriteBatch;
        public StartMenu(PVZGame game) : base(game)
        {
            _SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
        public override void InitializeComponent()
        {
            var button1 = new Button()
            {
                Text = "Start Game",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.DarkGreen,
                Location = new Vector2(300, 150)
            };
            button1.Clicked += button1_Clicked;
            Controls.Add(button1);

            var button2 = new Button()
            {
                Text = "Quit",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.DarkGreen,
                Location = new Vector2(300, 250)
            };
            button2.Clicked += button2_Clicked;
            Controls.Add(button2);
        }
        public void button1_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //state = play
        }
        public void button2_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //state = end
        }
        public override void Draw(GameTime gameTime)
        {
            //_SpriteBatch.Draw(_Background, rec, Color.White);
            base.Draw(gameTime);
        }
    }
}

