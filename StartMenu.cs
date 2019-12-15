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
        public StartMenu(PVZGame game) : base(game)
        {
        }
        public override void InitializeComponent()
        {
            var startButton = new Button()
            {
                Text = "Start Game",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.DarkGreen,
                Location = new Vector2(300, 150)
            };
            startButton.Clicked += StartButton_Clicked;
            Controls.Add(startButton);

            var quitButton = new Button()
            {
                Text = "Quit",
                Size = new Vector2(200, 50),
                BackgroundColor = Color.DarkGreen,
                Location = new Vector2(300, 250)
            };
            quitButton.Clicked += QuitButton_Clicked;
            Controls.Add(quitButton);
        }
        public void StartButton_Clicked(object sender, EventArgs e)
        {
            ((PVZGame)Game).EnterGame();
        }
        public void QuitButton_Clicked(object sender, EventArgs e)
        {
            ((PVZGame)Game).EndGame();
        }
    }
}