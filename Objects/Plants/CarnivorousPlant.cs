using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
namespace PlantvsZombie
{
    public class CarnivorousPlant:Plant
    {
        private float _DamageFactor = 100;
        private float _TimeSinceLastEat = 20f;
        private Zombie MeetZombie()
        {

            foreach (var z in PVZGame.Game.LogicManager.Zombies)
            {
                if (z.Position == null)
                    continue;
                if (ObjectTile.Contains(z.Position))
                    return z;
            }
            return null;

        }
        public CarnivorousPlant(Vector2 _Position)
        {
            Position = _Position;
            ObjectTile = PVZGame.Game.LogicManager.GameMap.GetTileAt(_Position).GetRelativeTile(1, 0); //check if 1 cell from the plant has a zombie in it
        }

        public override void Update()
        {
            base.Update();

            _TimeSinceLastEat += (float)PVZGame.Game.CurrentGameTime.ElapsedGameTime.TotalSeconds;
            var z = MeetZombie();
            if (z != null&&_TimeSinceLastEat >= 20f)
            {
                Attack(z);
                _TimeSinceLastEat = 0f;
                
            }
        }
        
        public override void Attack(Zombie z)
        {

            z.Damaged(_DamageFactor);
        }

        public override void Damaged(float dam)
        {
            Health -= dam;
        }



    }
}
