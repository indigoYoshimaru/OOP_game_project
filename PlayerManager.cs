﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantvsZombie
{
    public class PlayerManager
    {
        private int score = 0; 
        public enum MouseIcon { NORMAL, SUNFLOWER, PEASHOOTER };    //icon of the mouse
        private  MouseIcon mIcon;
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
    }
}