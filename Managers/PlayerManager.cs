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
        private int _PlayerScore= 0;
        private int _HighScore;
        private int _TotalSun = 200;
        public enum MouseIcon { NORMAL, SUNFLOWER, PEASHOOTER, CARNIVOROUSPLANT };    //icon of the mouse
        private MouseIcon _MIcon;
        private MouseState _CurrentMouseState;
        private MouseState _OldMouseState;
        private Tile _MouseTile;

        public String IconState { get; set; } = "NormalMouse";

        public int GetScore()
        {
            return _PlayerScore;
        }

        public MouseIcon GetMouseIcon()
        {
            return _MIcon;
        }

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
                _MIcon = MouseIcon.NORMAL;
                IconState = "NormalMouse";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _MIcon = MouseIcon.SUNFLOWER;
                IconState = "SunFlower";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _MIcon = MouseIcon.PEASHOOTER;
                IconState = "PeaShooter";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                _MIcon = MouseIcon.CARNIVOROUSPLANT;
                IconState = "CarnivorousPlant";
            }

        }
        public void UpdateControl()
        {
            Controller();
            _CurrentMouseState = Mouse.GetState();
            if (_MIcon != MouseIcon.NORMAL && _CurrentMouseState.LeftButton == ButtonState.Pressed && _OldMouseState.LeftButton == ButtonState.Released)
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

        // this one also needs fixing
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
            if (_MIcon != MouseIcon.NORMAL)
                return;

            if (Mouse.GetState().LeftButton != ButtonState.Pressed)
                return;

            foreach (GameObject o in PVZGame.Game.LogicManager.ManagedObjects.ToList())
            {
                // don't use is Sun
                if (o is Sun)
                    _TotalSun += ((Sun)o).Collect(Mouse.GetState().X, Mouse.GetState().Y);
            }
        }

        public int GetTotalSun()
        {
            return _TotalSun;
        }

        public int GetHighScore()
        {
            return _HighScore;
        }

        public void SetHighScore(int value)
        {
            _HighScore = value;
        }

        public void UpdateHighScore()
        {
            if (_PlayerScore > _HighScore)
            {
                SetHighScore(_PlayerScore);
            }
        }

        public void LoadHighScore()
        {
            try
            {
                var text = File.ReadAllText("Content/highscore.txt", Encoding.UTF8);
                if (!Int32.TryParse(text, out _HighScore))
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
                StreamWriter sw = new StreamWriter("Content/highscore.txt", false, Encoding.UTF8); //false means rewrite the file

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
