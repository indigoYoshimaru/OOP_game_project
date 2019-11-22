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
    public class FlyingZombie : Zombie
    {
        //private Texture2D _Image = Content.Load<Texture2D>("Texture/NormalZombie");
        private float _ScaleFactor = .3f;
        private Vector2 _Position;
        private float _DamageFactor = 20;
        private int counter = 0;
        private int state = 0;
        private Plant MeetPlant()
        {
            int pi, zi, pj, zj;
            float a = PVZGame.Side;
            foreach (var p in PVZGame.Game.Plants)
            {
                pi = Utility.GetCell(p.Position.X, a);
                pj = Utility.GetCell(p.Position.Y, a);
                zi = Utility.GetCell(Position.X, a);
                zj = Utility.GetCell(Position.Y, a);

                if (pi == zi && pj == zj) // Position.Y or Position.X - the side of a square
                    return p;
            } 
            return null;
        }

        

        public override void Update()
        {
            base.Update();
            var p = MeetPlant();
            if (p != null)
            {
                Console.WriteLine(counter);
                if (counter == 0)
                {
                    counter++;
                    float a = PVZGame.Side;
                    MoveByCell(Utility.GetCell(p.Position.X, a), Utility.GetCell(p.Position.Y, a));
                }
                else
                {
                    state = 1;
                    Attack(p);
                }
            } 
            else Move();
        }

        public override void Attack(Plant p)
        {
            p.Damaged(_DamageFactor);
        }

        public override float GetScaleFactor()
        {
            return _ScaleFactor;
        }

        public override void Damaged(float dam)
        {
            if (state == 1)
                Health -= dam;
        }

        public override void Move()
        {
            _Position.X = Position.X - Speed;
            _Position.Y = Position.Y;
            this.Position = _Position;
        }

        public void MoveByCell(int zx, int zy)
        {
            _Position.X = Position.X-PVZGame.Side;
            _Position.Y = Position.Y;
            Position = _Position;
        }

        public FlyingZombie()
        {
            _Position.X = 700;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            _Position.Y = r.Next(0, 300);
            Position = _Position;
            Speed = 0.2f;
        }
    }
}
