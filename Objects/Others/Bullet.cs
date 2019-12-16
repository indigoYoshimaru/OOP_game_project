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
    public class Bullet:GameObject
    {
        private Vector2 _Position;
        private int _DamageFactor=20;
        private Tile _BulletTile;
    
        public Bullet(PeaShooter p)
        {
            _Position = p.Position;
            this.Position = _Position;
            _BulletTile=PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);
            Speed = 0.4f;
        }

        public Zombie MeetZombie()
        {

            foreach (var z in PVZGame.Game.LogicManager.Zombies)
            {
                if (_BulletTile.Contains(z.Position))
                    return z;
            }
            return null;

        }

        public override void Update()
        {
            _BulletTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);
            if (_BulletTile != null)
            {
                var z = MeetZombie();
                if (z != null) Attack(z);
                else
                    Move();
            }
            else Die();
           
        }

        public void Move()
        {
            _Position.X = Position.X + Speed;
            _Position.Y = Position.Y;

            this.Position = _Position;
        }
        
        
        public void Attack(Zombie z)
        {
            
            if (z.DamagedState)
            {
                z.Damaged(_DamageFactor);
                Die();
            }
            else Move();
            
        }

    }
}
