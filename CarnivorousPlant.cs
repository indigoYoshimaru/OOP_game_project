using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
namespace PlantvsZombie
{
    class CarnivorousPlant:Plant
    {
        private Vector2 _Position;
        private float _DamageFactor = 100;
        private float _TimeSinceLastSpawn = 0f;
        private Tile_EatTile;
         public Zombie MeetZombie()
        {

            foreach (var z in PVZGame.Game.Zombies)
            {
                if (_EatTile.Contains(z.Position))
                    return z;
            }
            return null;

        }
        public CarnivorousPlant(Vector2 _Position)
        {
            Position = _Position;
            _EatTile = PVZGame.Game.GameMap.GetTileAt(_Position).GetRelativeTile(2, 0); //check if 1 cell from the plant has a zombie in it

        }

        public override void Update()
        {
            base.Update();
            _TimeSinceLastSpawn += (float)PVZGame.Game.CurrentGameTime.ElapsedGameTime.TotalSeconds;
            var z = MeetZombie();
            if (z != null&& _TimeSinceLastSpawn > =5f)     //set time pending between each attack
            {
                Attack(z);
                _TimeSinceLastSpawn = 0;
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
