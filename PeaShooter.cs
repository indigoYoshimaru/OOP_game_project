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
    class PeaShooter:Plant
    {
        
        public override void Update()
        {
            base.Update();
        }

        public PeaShooter(Vector2 _Position)
        {
            Position = _Position;
        }

        public override void Attack(Zombie z)
        {
            throw new NotImplementedException();
            // bulletfire
        }

        public override void Damaged(float dam)
        {
            Health -= dam;
        }
    }
}
