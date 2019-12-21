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
    public abstract class Zombie:PlantZombieObject
    {
        public bool DamagedState { get; set; }
        public abstract void Move();
        public abstract void Attack(Plant p);

        public override void Update()
        {
            base.Update();
            if (ObjectTile==null)
                PVZGame.Game.ToEndMenu();
        }

        public Zombie()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int y = r.Next(0, 4);
            DamagedState = true;
            Position = PVZGame.Game.LogicManager.GameMap.GetTile(9, y).GetCenter();

        }
    }
}
