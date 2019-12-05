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
        //private Texture2D _Image = Content.Load<Texture2D>("Texture/NormalZombie");
        
        private Vector2 _Position;
        private float _DamageFactor = 1;
        private Tile _ZombieTile;

        private Plant MeetPlant()
        {
            
            foreach (var p in PVZGame.Game.Plants)
            {

                if (_ZombieTile.Contains(p.Position))
                    return p;
            }
            
            return null;
        }


        public override void Update()
        {
            base.Update();
            _ZombieTile = PVZGame.Game.GameMap.GetTileAt(_Position);
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
            _ZombieTile = PVZGame.Game.GameMap.GetTileAt(_Position);
            Speed = 0.1f;
        }
    }
}
