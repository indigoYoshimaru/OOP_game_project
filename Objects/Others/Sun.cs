using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlantvsZombie
{
    public class Sun:GameObject
    {
        private Vector2 _Position;
        private float _TimeSinceLastSpawned = 0f;
        private float _Stop;
        private float _Stop2;
        private int _Kind;

        public Sun(Plant p)
        {
            _Kind = 0;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            _Position.X = p.Position.X + r.Next(-50,50);
            _Position.Y = p.Position.Y + r.Next(10,50);
            _Stop = _Position.Y;
            this.Position = _Position;
            Speed = 0.4f;
        }
        public Sun()
        {
            _Kind = 1;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            _Position.X = r.Next(0, 700);
            _Position.Y = 50;
            _Stop2 = r.Next(100, 500);
            this.Position = _Position;
            Speed = 0.4f;
        }
        public override void Update()
        {
            
            if (_Kind == 0)
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
            if (_Position.Y > _Stop + 20 || _Position.Y == _Stop2)
            {
                _Position.Y = Position.Y;
            }
            else { _Position.Y = Position.Y + Speed; }
            this.Position = _Position;
        }
        public void FallFromSky()
        {
            _Position.X = Position.X;
            if (_Position.Y > _Stop2)
            {
                _Position.Y = _Stop2;
            }
            else
            {
                _Position.Y = Position.Y + Speed;
            }
            this.Position = _Position;
        }

        public int Collect(int x, int y)
        {
            if (x > Position.X - 50 && x < Position.X + 50 && y > Position.Y - 60 && y < Position.Y + 60)
            {
                this.Die();
                return 25;
            }
            else
            {
                return 0;
            }
        }

    }
}
