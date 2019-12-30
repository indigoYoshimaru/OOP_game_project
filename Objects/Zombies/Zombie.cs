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
        public int Score { get; set; }
        public abstract void Move();
        public abstract void Attack(Plant p);

        public override void Update()
        {
            base.Update();
            
            if (Position.X <= 0)
                PVZGame.Game.LogicManager.EndGame();
        }

        public Zombie()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            Map map = PVZGame.Game.LogicManager.GameMap;
            int y = r.Next(0, map.GetDimensionY());
            Vector2 pos = map.GetTile(map.GetDimensionX() - 1, y).GetCenter();
            pos.X += map.TileSize.X;
            Position = pos;
            DamagedState = true;
        }
    }
}
