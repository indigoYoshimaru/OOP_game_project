using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class PeaShooter : Plant
    {

        private float _TimeSinceLastSpawn = 0f;

        public override void Update()
        {
            base.Update();

            _TimeSinceLastSpawn += (float)PVZGame.Game.CurrentGameTime.ElapsedGameTime.TotalSeconds;
            if (_TimeSinceLastSpawn >= 5f)
            {
                Money();
                Attack();
                _TimeSinceLastSpawn = 0f;
            }

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
            PVZGame.Game.SpawnBullet(this);

        }

        public override void Damaged(float dam)
        {
            Health -= dam;
        }

        public override void Money()
        {
            PVZGame.Game.SpawnSun(this);
 
        }
    }
}
