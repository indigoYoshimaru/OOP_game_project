using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class PeaShooter:Plant
    {
        
        private float _TimeSinceLastSpawn=5f;
        private Zombie _CurrentZombie;
        private Tile _ShooterTile;

        public override void Update()
        {
            ObjectTile = _ShooterTile;
            base.Update();

            //if (_CurrentZombie!=null&&_CurrentZombie.Health>0)
            //{
            //    _TimeSinceLastSpawn += (float)PVZGame.Game.CurrentGameTime.ElapsedGameTime.TotalSeconds;
            //    if (_TimeSinceLastSpawn >= 5f)
            //    {
            //        Attack(null);
            //        _TimeSinceLastSpawn = 0f;
            //    }
            //}

            //else
            //    _CurrentZombie = MeetZombie();
            if (MeetZombie() != null)
            {
                _TimeSinceLastSpawn += (float)PVZGame.Game.CurrentGameTime.ElapsedGameTime.TotalSeconds;
                if (_TimeSinceLastSpawn >= 5f)
                {
                    Attack(null);
                    _TimeSinceLastSpawn = 0f;
                }
            }
            

        }


        // Idea: check if the tile that is 3 cell from the plant has a zombie in it --> attack if it does

        public Zombie MeetZombie()
        {
            
            foreach (var z in PVZGame.Game.LogicManager.Zombies)
            {

                //if (_ShooterTile.Contains(z.Position))
                //    return z;

                if (_ShooterTile.Y == z.ObjectTile.Y && _ShooterTile.X <= z.ObjectTile.X && z.ObjectTile.X <= _ShooterTile.X + 5)
                    return z;
            }
            return null;

        }

        public PeaShooter(Vector2 _Position)
        {
            Position = _Position;
            _ShooterTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);//.GetRelativeTile(4,0);
        
        }

        public override void Attack(Zombie z)
        {
            PVZGame.Game.LogicManager.Spawner.SpawnBullet(this);

        }

        public override void Damaged(float dam)
        {
            Health -= dam;
        }
    }
}
