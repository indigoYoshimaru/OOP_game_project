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
    public class LaneJumpingZombie:Zombie
    {
        
        private Vector2 _Position;
        private float _DamageFactor = 2;
        private Tile _ZombieTile;
        Random rand = new Random();

        float yDes;
        float xMinus = 0f;

        private Plant MeetPlant()
        {
            
            foreach (var p in PVZGame.Game.LogicManager.Plants)
            {
                if (_ZombieTile != null)
                {
                    if (_ZombieTile.Contains(p.Position)) // Position.Y or Position.X - the side of a square
                        return p;
                }
            }

            return null;
        }

        public override void Update()
        {
            base.Update();
            _ZombieTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);
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
            _Position.X -= Speed;   //just let the zombie move in horizontal axis normally

            xMinus += Speed;        //this will capture the distance the zombie has travelled
            Tile tileCheck;
            
            //when the zombie has reached to the destination tile and the horizontal distance the zombie has travelled 
            //is two times greater than horizontal size Tile
            if (Math.Abs(_Position.Y - yDes) < 1 && xMinus >= 2*PVZGame.Game.LogicManager.GameMap.TileSize.X)
            {
                int r = rand.Next(-1,2);    //random for next reletive tile
                tileCheck = _ZombieTile.GetRelativeTile(0, r); //get the position of the relative tile
                
                if (tileCheck != null)  //check if that tile is valid
                {
                    //System.Diagnostics.Debug.WriteLine(tileCheck.Y);  //this use for debug
                    yDes = tileCheck.GetCenter().Y; //get the y value of the next tile
                }
                xMinus = 0;
            }
            _Position.Y = Lerp(_Position.Y, yDes, 0.02f);   //lerp from the old y value the new value.
            this.Position = _Position;  //assign new position
           
        }

        //Lerp: Linear interpolation, this will return the value between first float and second float
        float Lerp(float firstFloat, float secondFloat, float by)  
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public LaneJumpingZombie() : base()
        {
            _Position = Position;
            _ZombieTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position);
            yDes = Position.Y;
            Speed = 0.4f;
            Score = 15;
        }
    }
}