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

        public override void Update()
        {
            
            base.Update();
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

        private Zombie MeetZombie()
        {
            
            foreach (var z in PVZGame.Game.LogicManager.Zombies)
            {
                if (ObjectTile.Y == z.ObjectTile.Y && ObjectTile.X <= z.ObjectTile.X && z.ObjectTile.X <= ObjectTile.X + 5)
                    return z;
            }
            return null;

        }

        public PeaShooter(Vector2 _Position)
        {
            Position = _Position;
            ObjectTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);
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
