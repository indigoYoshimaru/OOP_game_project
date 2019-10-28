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
    public class BlueZombieMale:Zombie
    {
        //private Texture2D _Image = Content.Load<Texture2D>("Texture/BlueZombieMale");
        private float _ScaleFactor = .3f;
        private Vector2 _Position;

        private Plant MeetPlant()
        {
            foreach (var p in PVZGame.Game.Plants)
                if (p.Position.Y == this.Position.Y && p.Position.X < this.Position.X) // Position.Y or Position.X - the side of a square
                    return p;

            return null;
        }

        //public override void Attack(Plant p)
        //{
        //p.Damaged();
        //}

        public override void Update()
        {
            var p = MeetPlant();
            if (p != null) Attack();

            else Move();

            base.Update();

        }
        
        public override void Attack()
        {
            throw new NotImplementedException();
        }

        public override float GetScaleFactor()
        {
            return _ScaleFactor;
        }

        public override void Move()
        {
            
            _Position.X = Position.X-Speed;
            _Position.Y = Position.Y;

            this.Position = _Position;
        }

        public BlueZombieMale()
        {
            _Position.X = 700;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            _Position.Y = r.Next(0, 300);
            Position = _Position;
            Speed = 0.2f;
        }
    }
}
