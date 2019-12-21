﻿using System;
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
       
        public abstract void Attack(Zombie z);
        public override void Update()
        {
            if (ObjectTile == null)
            {
                //do something
            }
            base.Update();
        }

                
    }
}
