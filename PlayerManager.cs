using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantvsZombie
{
    public class PlayerManager
    {
        private int score = 0;
        private int highScore;
        private int totalSun = 200;
        public enum MouseIcon { NORMAL, SUNFLOWER, PEASHOOTER, CARNIVOROUSPLANT };    //icon of the mouse
        private MouseIcon mIcon;
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
            if (z is NormalZombie)
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

        //send signal to plant the correct type
        public void Controller()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.X) || (Mouse.GetState().RightButton == ButtonState.Pressed))
            {
                mIcon = MouseIcon.NORMAL;
                PlantState = "";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                mIcon = MouseIcon.SUNFLOWER;
                PlantState = "SunFlower";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mIcon = MouseIcon.PEASHOOTER;
                PlantState = "PeaShooter";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                mIcon = MouseIcon.CARNIVOROUSPLANT;
                PlantState = "CarnivorousPlant";
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
                    if(SpendSun())
                        PVZGame.Game.Spawner.SpawnPlant(_MouseTile);
                    
                }
            }
            _OldMouseState = _CurrentMouseState;
        }

        public bool SpendSun()
        {
            int sunSpend = 0;
            switch (PlantState)
            {
                case "SunFlower":
                    sunSpend = 50;
                    break;
                case "PeaShooter":
                    sunSpend = 100;
                    break;
                case "CarnivorousPlant":
                    sunSpend = 150;
                    break;
            }

            if (totalSun >= sunSpend)
            {
                totalSun -= sunSpend;
                return true;
            }

            return false;
        }

        public void CheckSun()
        {
            if (mIcon != MouseIcon.NORMAL)
                return;

            if (Mouse.GetState().LeftButton != ButtonState.Pressed)
                return;

            foreach(GameObject o in PVZGame.Game.ManagedObjects.ToList())
            {
                if(o is Sun)
                    totalSun += ((Sun) o).Collect(Mouse.GetState().X, Mouse.GetState().Y);
            }
        }

        public int GetTotalSun()
        {
            return totalSun;
        }

        public int GetHighScore()
        {
            return highScore;
        }

        public void SetHighScore(int value)
        {
            highScore = value;
        }

        public void UpdateHighScore()
        {
            if (score > highScore)
            {
                SetHighScore(score);
            }
        }

        public void LoadHighScore()
        {
            try
            {
                var text = File.ReadAllText("highscore.txt", Encoding.UTF8);
                if (!Int32.TryParse(text, out highScore))
                {
                    SetHighScore(0);
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
                StreamWriter sw = new StreamWriter("highscore.txt", false, Encoding.UTF8); //false means rewrite the file

                sw.WriteLine(GetHighScore());

                //close the file
                sw.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
            }
        }

        public void Update()
        {
            //call player Update()
            UpdateControl();

            //update highscore
            UpdateHighScore();

            //update sun
            CheckSun();

        }
    }
}
