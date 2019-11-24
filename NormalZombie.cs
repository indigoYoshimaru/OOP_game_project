﻿using System;
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
        private float _DamageFactor = 10;
        

        private Plant MeetPlant()
        {
            int pi, zi, pj, zj;
            float a = PVZGame.Side;

           
            foreach (var p in PVZGame.Game.Plants)
            {
                //fixed after Title class finish
                pi = Utility.GetCell(p.Position.X,a);
                pj = Utility.GetCell(p.Position.Y,a);
                zi = Utility.GetCell(Position.X,a);
                zj = Utility.GetCell(Position.Y, a);
                if (pi==zi&&pj==zj) // Position.Y or Position.X - the side of a square
                    return p;
            }
            
            return null;
        }


        public override void Update()
        {
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

        public NormalZombie()
        {
            _Position.X = 900;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            _Position.Y = r.Next(30, 300);
            Position = _Position;
            Speed = 0.2f;
        }
    }
}
