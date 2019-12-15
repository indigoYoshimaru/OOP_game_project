using Microsoft.Xna.Framework;
using MonoGame.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantvsZombie
{
    public class EndMenu : ControlManager
    {
        public EndMenu(PVZGame game) : base(game)
        {

        }
        public override void InitializeComponent()
        {

            var btn1 = new Button()
            {
                Text = "Play again",
                Size = new Vector2(200, 50),
                Location = new Vector2(300, 150),
                BackgroundColor = Color.DarkGreen
            };

            btn1.Clicked += Btn1_Clicked;
            Controls.Add(btn1);
            var btn2 = new Button()
            {
                Text = "Quit",
                Size = new Vector2(200, 50),
                Location = new Vector2(300, 220),
                BackgroundColor = Color.DarkGreen
            };
            btn2.Clicked += Btn2_Clicked;
            Controls.Add(btn2);


        }

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ((PVZGame)Game).EndGame();
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ((PVZGame)Game).EnterGame();
        }
        public override void Draw(GameTime gameTime)
        {
            //_SpriteBatch.Draw(_Background, rec, Color.White);
            base.Draw(gameTime);
        }
    }
}