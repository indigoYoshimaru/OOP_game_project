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
    public class LaneJumpingZombie:Zombie
    {
        
        private Vector2 _Position;
        private float _DamageFactor = 10;
        Random rand = new Random();

        float yDes;
        float xMinus = 0f;

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

        //public override void Attack(Plant p)
        //{
        //p.Damaged();
        //}

        public override void Update()
        {
            base.Update();
            var p = MeetPlant();
            if (p != null) Attack(p);

            else Move();

        }

        public override void Attack(Plant p)
        {
            p.Damaged(_DamageFactor);
        }

        

        public override void Damaged(float dam)
        {
            Health -= dam;
        }

        public override void Move()
        {
            _Position.X -= Speed;

            xMinus += Speed;
            float yCheck;
            
                if (Math.Abs(_Position.Y - yDes) < 1 && xMinus >= 20)
                {
                    int r = rand.Next(0, 3) - 1;
                    yCheck = yDes + (r * 75);
                    if ((yCheck >= 30) && (yCheck <= 380))
                    {
                        yDes = yCheck;

                    }
                    xMinus = 0;
                }
            _Position.Y = Lerp(_Position.Y, yDes, 0.02f);
            this.Position = _Position;
           
        }

        float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public LaneJumpingZombie()
        {
            _Position.X = 700;
            int i = rand.Next(0,5);
            _Position.Y = i * 75 + 20;
            Position = _Position;
            yDes = Position.Y;
            Speed = 0.2f;
        }
    }
}
