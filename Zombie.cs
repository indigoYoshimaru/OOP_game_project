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
        public abstract void Move();

        public Zombie()
        {
            
        }
        public abstract void Attack(Plant p);

        public override void Update()
        {
            base.Update();
            if (Position.X < 0)
                PVZGame.Game.EndGame();
        }
    }
}
