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
            
            foreach (var p in PVZGame.Game.Plants)
            {
                try
                {
                    if (_ZombieTile.Contains(p.Position))
                        return p;
                }
                catch (Exception e)
                {
                    PVZGame.Game.Exit();
                    // get to GameOver Screen
                }
                
            }
            
            return null;
        }


        public override void Update()
        {
            base.Update();
            try
            {
                _ZombieTile = PVZGame.Game.GameMap.GetTileAt(_Position);
            }
            catch (Exception e)
            {
                PVZGame.Game.Exit();
            }
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
            Speed = 0.2f;
        }
    }
}
