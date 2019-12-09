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
    public class FlyingZombie : Zombie
    {
        
        private Vector2 _Position;
        private float _DamageFactor = 2;
        private int _CounterOrder = 0;
        private Tile _ZombieTile;
        private Tile _FirstPlantTile;
        private Plant MeetPlant()
        {
            foreach (var p in PVZGame.Game.Plants)
            {
                if (_ZombieTile.Contains(p.Position))
                {
                    if (_CounterOrder == 0)
                    {
                        _FirstPlantTile = _ZombieTile;
                        _CounterOrder = 1;
                    }
                    return p;
                }     
            }
            return null;
        }



        public override void Update()
        {
            base.Update();
            _ZombieTile = PVZGame.Game.GameMap.GetTileAt(_Position);
            var p = MeetPlant();
            if (p != null)
            {
                if (_FirstPlantTile.Contains(p.Position))
                {
                    DamagedState = true;
                    MoveByCell();
                }
                else
                {
                    Attack(p);
                }
            } 
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
            _Position.X = Position.X - Speed;
            _Position.Y = Position.Y;
            this.Position = _Position;
        }

        public void MoveByCell()
        {
            _Position.X = Position.X - Speed;
            _Position.Y = Position.Y;
            Position = _Position;
        }

        public FlyingZombie() : base()
        {
            _Position = Position;
            Speed = 0.4f;
            DamagedState = false;
            _ZombieTile = PVZGame.Game.GameMap.GetTileAt(_Position);
        }
    }
}
