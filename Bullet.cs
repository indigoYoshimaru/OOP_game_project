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
    public class Bullet:GameObject
    {
        private Vector2 _Position;
        private int _DamageFactor=20;
    
        public Bullet(PeaShooter p)
        {
            _Position = p.Position;
            this.Position = _Position;
            Speed = 0.4f;
        }

        public Zombie MeetZombie()
        {
            int bi, bj, zi, zj;
            float a = PVZGame.Side;
            foreach (var z in PVZGame.Game.Zombies)
            {
                //fixed after Title class finish, otherwise the zombie dies after two hits
                //since the bullet travel through the cell in a considerable amount of time
                zi = Utility.GetCell(z.Position.X, a);
                zj = Utility.GetCell(z.Position.Y, a);
                bi = Utility.GetCell(Position.X, a);
                bj = Utility.GetCell(Position.Y, a);
                if (bi == zi && bj == zj) // Position.Y or Position.X - the side of a square
                    return z;
            }

            return null;
        }

        public override void Update()
        {
            var z = MeetZombie();
            if (z != null) Attack(z);

            else Move();
        }

        public void Move()
        {
            _Position.X = Position.X + Speed;
            _Position.Y = Position.Y;

            this.Position = _Position;
        }
        
        
        public void Attack(Zombie z)
        {
            
            if (z.DamagedState)
            {
                z.Damaged(_DamageFactor);
                Die();
            }
            else Move();
            
        }

    }
}
