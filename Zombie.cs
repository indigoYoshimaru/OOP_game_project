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
        public bool DamagedState { get; set; }

        public abstract void Move();

        public Zombie()
        {
            DamagedState = true;
        }
        public abstract void Attack(Plant p);

        public abstract void Damaged(float dam);
        

        public override void Update()
        {

            if (Health <= 0)
            {
                PVZGame.Game.management.UpdateScore(this);
                Die();
            }
            if (Position.X < 0)
                PVZGame.Game.EndGame();
        }
    }
}
