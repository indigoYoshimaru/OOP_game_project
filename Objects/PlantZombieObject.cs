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
        public Tile ObjectTile { get; set; }
        public float Health { get; set; } = 100;

        public override void Update()
        {
            if (Health <= 0)
                Die();
        }

        public abstract void Damaged(float dam);
    }
}
