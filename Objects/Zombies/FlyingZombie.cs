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
        private int _Counter = 0;
        private Tile _ZombieTile;
        private Plant MeetPlant()
        {
            foreach (var p in PVZGame.Game.LogicManager.Plants)
            {
                if (_ZombieTile.Contains(p.Position))
                    return p;
            }
            return null;
        }

        public override void Update()
        {
            _ZombieTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);
            ObjectTile = _ZombieTile;
            base.Update();
            var p = MeetPlant();
            if (p != null)
            {
                if (_Counter == 0)
                {
                    _Counter++;
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

        private void MoveByCell()
        {
            var jumpTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position).GetRelativeTile(-1,0);
            _Position.X = jumpTile.GetCenter().X;
            _Position.Y = Position.Y;
            Position = _Position;
        }

        public FlyingZombie() : base()
        {
            _Position = Position;
            Speed = 0.3f;
            DamagedState = false;
            _ZombieTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);
            ObjectTile = _ZombieTile;
            Score = 10;
        }
    }
}
