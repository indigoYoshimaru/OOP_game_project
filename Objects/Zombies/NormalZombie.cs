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
    public class NormalZombie:Zombie
    {   
        private Vector2 _Position;
        private float _DamageFactor = 1;
        private Tile _ZombieTile;

        private Plant MeetPlant()
        {
            if (_ZombieTile == null)
                return null;
            return _ZombieTile.Plant;
        }


        public override void Update()
        {
            _ZombieTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);
            ObjectTile = _ZombieTile;
            base.Update();
            var p = MeetPlant();
            if (p != null) Attack(p);

            else Move();

         }
        
        public override void Attack(Plant p)
        {
            p.Damaged(_DamageFactor);
        }

        public override void Damaged(float dam)
        {
            Health -= dam;
        }

        public override void Move()
        {
            
            _Position.X = Position.X-Speed;
            _Position.Y = Position.Y;

            this.Position = _Position;
        }

        public NormalZombie():base()
        {
            _Position = Position;
            _ZombieTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);
            ObjectTile = _ZombieTile;
            Speed = 0.4f;
            Score = 5;
        }
    }
}
