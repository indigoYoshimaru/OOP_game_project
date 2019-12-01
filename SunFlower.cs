using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;


namespace PlantvsZombie
{
    public class SunFlower:Plant
    {
        private float _currentTime = 0f;
        public override void Update() 
        {
            _currentTime += (float)PVZGame.Game.CurrentGameTime.ElapsedGameTime.TotalSeconds;
            base.Update();
            if(_currentTime >=10f)
            {
                PVZGame.Game.Spawner.SpawnSun(this);
                _currentTime = 0f;
            }
        }
        public SunFlower(Vector2 _Position)
        {
            Position = _Position;
        }
        public override void Damaged(float dam)
        {
            Health -= dam;
        }

        public override void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
