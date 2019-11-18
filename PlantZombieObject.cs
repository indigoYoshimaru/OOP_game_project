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
    //the class for common att. btw plants vs zombies
    public abstract class PlantZombieObject:GameObject
    {
        private float _Health;
        public float Health
        {
            get
            {
                return _Health;
            }
            set
            {
                _Health = value;
            }
        }

        public override void Update()
        {
            if (_Health <= 0)
                Die();
        }
        
    }
}
