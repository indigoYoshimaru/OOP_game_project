using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlantvsZombie
{
    public class Sun:PlantZombieObject
    {
        private Vector2 _Position;
        private float _TimeSinceLastSpawned = 0f;
        private float stop;
        private float stop2;
        private int kind;
        public Sun(Plant p)
        {
            kind = 0;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            _Position.X = p.Position.X + r.Next(-50,50);
            _Position.Y = p.Position.Y + r.Next(10,50);
            stop = _Position.Y;
            this.Position = _Position;
            Speed = 0.4f;
        }
        public Sun()
        {
            kind = 1;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            _Position.X = r.Next(0, 700);
            _Position.Y = 50;
            stop2 = r.Next(100, 500);
            this.Position = _Position;
            Speed = 0.4f;
        }
        public override void Update()
        {
            base.Update();
            if (kind == 0)
            {
                FallFromPlant();
            }

            else
            {
                FallFromSky();
            }
            _TimeSinceLastSpawned += (float)PVZGame.Game.CurrentGameTime.ElapsedGameTime.TotalSeconds;
            if (_TimeSinceLastSpawned >= 20f)
            {
                Die();
            }
        }

        public void FallFromPlant()
        {
            _Position.X = Position.X;
            if (_Position.Y > stop + 20 || _Position.Y == stop2)
            {
                _Position.Y = Position.Y;
            }
            else { _Position.Y = Position.Y + Speed; }
            this.Position = _Position;
        }
        public void FallFromSky()
        {
            _Position.X = Position.X;
            if (_Position.Y > stop2)
            {
                _Position.Y = stop2;
            }
            else
            {
                _Position.Y = Position.Y + Speed;
            }
            this.Position = _Position;
        }
        public Boolean CollectSun(int _X, int _Y)
        {
            if (_X > Position.X - 50 && _X < Position.X + 50 && _Y > Position.Y - 60 && _Y < Position.Y + 60)
            {
                this.Die();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
