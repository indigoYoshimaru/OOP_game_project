using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class PeaShooter:Plant
    {
        
        private float _TimeSinceLastSpawn=0f;
        private Zombie _CurrentZombie;
        private Tile _ShooterTile;

        public override void Update()
        {
            base.Update();

            // if the Zombie passes the tile but haven't been check
            if (_CurrentZombie!=null&&_CurrentZombie.Health>0)
            {
                _TimeSinceLastSpawn += (float)PVZGame.Game.CurrentGameTime.ElapsedGameTime.TotalSeconds;
                if (_TimeSinceLastSpawn >= 5f)
                {
                    Attack();
                    _TimeSinceLastSpawn = 0f;
                }
            }

            else
                _CurrentZombie = MeetZombie();

        }


        // Idea: check if the tile that is 3 cell from the plant has a zombie in it --> attack if it does

        public Zombie MeetZombie()
        {
            
            foreach (var z in PVZGame.Game.Zombies)
            {
                if (_ShooterTile.Contains(z.Position))
                    return z;
            }
            return null;

        }

        public PeaShooter(Vector2 _Position)
        {
            Position = _Position;
            _ShooterTile = PVZGame.Game.GameMap.GetTileAt(_Position).GetRelativeTile(4,0);
            
        }

        public override void Attack()
        {
            //throw new NotImplementedException();
            // bulletfire
            // if is only for testing, will be updated after tile is built
            PVZGame.Game.Spawner.SpawnBullet(this);

        }

        public override void Damaged(float dam)
        {
            Health -= dam;
        }
    }
}
