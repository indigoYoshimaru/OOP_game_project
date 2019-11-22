using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class PeaShooter:Plant
    {
        private int bullNumber=0;
        private float timeSinceLastSpawn=0;
        private GameTime gameTime = new GameTime();

        public override void Update()
        {
            base.Update();
            
            //timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if (timeSinceLastSpawn >= 2f)
            //{
                Attack();
            //    timeSinceLastSpawn = 0f;
            //}
               
        }


        // Idea: check if the tile that is 3 cell from the plant has a zombie in it --> attack if it does

        public Zombie MeetZombie()
        {          
            throw new NotImplementedException();

        }

        public PeaShooter(Vector2 _Position)
        {
            Position = _Position;
        }

        public override void Attack()
        {
            //throw new NotImplementedException();
            // bulletfire
            // if is only for testing, will be updated after tile is built
            if (bullNumber == 0)
            {
                PVZGame.Game.SpawnBullet(this);
                bullNumber++;
            }
            

        }

        public override void Damaged(float dam)
        {
            Health -= dam;
        }
    }
}
