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
        //private int highScore;
        public int _TotalSun { get; private set; } = 200;
        public enum MouseIcon { NORMAL, SUNFLOWER, PEASHOOTER, CARNIVOROUSPLANT };    //icon of the mouse
        private MouseIcon mIcon;
        private MouseState _CurrentMouseState;
        private MouseState _OldMouseState;
        private Tile _MouseTile;

        public String IconState { get; set; } = "NormalMouse";

        public int _PlayerScore { get; private set; } = 0;
        //public int HighScore { get => highScore; set => highScore = value; }
        public int _HighScore { get; private set; } = 0;

        //open-close principle this is not open for extension
        public void UpdateScore(Zombie z)
        {
            _PlayerScore += z.Score;
        }

        //send signal to plant the correct type
        public void Controller()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.X) || (Mouse.GetState().RightButton == ButtonState.Pressed))
            {
                mIcon = MouseIcon.NORMAL;
                IconState = "NormalMouse";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                mIcon = MouseIcon.SUNFLOWER;
                IconState = "SunFlower";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mIcon = MouseIcon.PEASHOOTER;
                IconState = "PeaShooter";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                mIcon = MouseIcon.CARNIVOROUSPLANT;
                IconState = "CarnivorousPlant";
            }

        }
        public void UpdateControl()
        {
            Controller();
            _CurrentMouseState = Mouse.GetState();
            if (mIcon != MouseIcon.NORMAL && _CurrentMouseState.LeftButton == ButtonState.Pressed && _OldMouseState.LeftButton == ButtonState.Released)
            {

                _MouseTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_CurrentMouseState.Position.ToVector2());
                if (_MouseTile!=null&&!_MouseTile.HasPlant())
                {
                    if (SpendSun())
                        PVZGame.Game.LogicManager.Spawner.SpawnPlant(_MouseTile);
                        

                }
            }
            _OldMouseState = _CurrentMouseState;
        }

        public bool SpendSun()
        {
            int sunSpend = 0;
            switch (IconState)
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

            if (_TotalSun >= sunSpend)
            {
                _TotalSun -= sunSpend;
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

            foreach (GameObject o in PVZGame.Game.LogicManager.ManagedObjects.ToList())
            {
                if (o is Sun)
                    _TotalSun += ((Sun)o).Collect(Mouse.GetState().X, Mouse.GetState().Y);
            }
        }




        public void UpdateHighScore()
        {
            if (_PlayerScore > _HighScore)
            {
                _HighScore = _PlayerScore;
            }
        }

        public void LoadHighScore()
        {
            try
            {
                int temp;
                var text = File.ReadAllText("Content/highscore.txt", Encoding.UTF8);
                if (!Int32.TryParse(text, out temp))
                {
                    _HighScore = 0;
                }
                else
                {
                    _HighScore = temp;
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
                StreamWriter sw = new StreamWriter("Content/highscore.txt", false, Encoding.UTF8); //false means rewrite the file

                sw.WriteLine(_HighScore);

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
            //call Update()
            UpdateControl();

            //update highscore
            UpdateHighScore();

            //update sun
            CheckSun();

        }
    }
}
