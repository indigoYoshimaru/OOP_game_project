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
    class HeartShooter:Plant
    {
        
        public override void Update()
        {
            
        }

        public HeartShooter(Vector2 _Position)
        {
            Position = _Position;
        }

        public override void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
