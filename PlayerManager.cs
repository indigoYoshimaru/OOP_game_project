using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PlantvsZombie
{
    public class PlayerManager
    {
        private int score = 0;
        private int highscore = 0;
        public enum MouseIcon { NORMAL, SUNFLOWER, PEASHOOTER };    //icon of the mouse
        private  MouseIcon mIcon;
        private MouseState _CurrentMouseState;
        private MouseState _OldMouseState;
        private Tile _MouseTile;

        public String PlantState { get; set; } = "";

        public int GetScore()
        { 
            return score;
        }

        public MouseIcon GetMouseIcon()
        {
            return mIcon;
        }

        
        public void UpdateScore(Zombie z)
        {
            if (z is NormalZombie )
            {
                score += 5;
            }
            else if (z is FlyingZombie)
            {
                score += 10;
            }
            else if (z is LaneJumpingZombie)
            {
                score += 15;
            }
        }

        public int GetHighScore()
        {
            return highscore;
        }

        public void SetHighScore(int value)
        {
            highscore = value;
        }

        public void UpdateHighScore()
        {
            if (score > highscore)
            {
                SetHighScore(score);
            }
        }

        public void LoadHighScore()
        {
            try
            {
                var text = File.ReadAllText("highscore.txt", Encoding.UTF8);
                if (!Int32.TryParse(text, out highscore))
                {
                    SetHighScore(-1);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
            }
        }

        public void SaveHighScore()
        {
            try
            {
                //Open the File
                StreamWriter sw = new StreamWriter("highscore.txt", false, Encoding.UTF8);

                sw.WriteLine(GetHighScore());

                //close the file
                sw.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
            }
        }

        //send signal to plant the correct type
        public void Controller()
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.X) || (Mouse.GetState().RightButton == ButtonState.Pressed))
            {
                mIcon = MouseIcon.NORMAL;
                PlantState = "";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mIcon = MouseIcon.SUNFLOWER;
                PlantState = "SunFlower";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                mIcon = MouseIcon.PEASHOOTER;
                PlantState = "PeaShooter";
            }
            
        }

        public void UpdateControl()
        {
            Controller();
            _CurrentMouseState = Mouse.GetState();
            if (_CurrentMouseState.LeftButton == ButtonState.Pressed && _OldMouseState.LeftButton == ButtonState.Released)
            {

                _MouseTile = PVZGame.Game.GameMap.GetTileAt(_CurrentMouseState.Position.ToVector2());
                {
                    PVZGame.Game.Spawner.SpawnPlant(_MouseTile);
                }
            }
            _OldMouseState = _CurrentMouseState;
        }
    }
}
