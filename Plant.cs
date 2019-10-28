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
    public abstract class Plant:PlantZombieObject
    {
       
        public override float GetScaleFactor()
        {
            throw new NotImplementedException();

        }
        public override void Attack()
        {
            throw new NotImplementedException();    
        }

    }
}
