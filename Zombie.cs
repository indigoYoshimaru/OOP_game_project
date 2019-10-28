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
    public abstract class Zombie:PlantZombieObject
    {
        private Vector2 _Position;
        public abstract void Move();

        public Zombie()
        {
            
        }

        public override void Attack()
        {
            throw new NotImplementedException();
        }

        public override float GetScaleFactor()
        {
            throw new NotImplementedException();
        }
    }
}
