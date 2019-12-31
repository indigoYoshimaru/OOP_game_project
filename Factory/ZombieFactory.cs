using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class ZombieFactory:Factory
    {

        public override GameObject FactoryMethod(String State, Vector2 Position)
        {
            return ZombieConcreteFactory();
        }

        private Zombie ZombieConcreteFactory()
        {
            Random _Rand = new Random();

            if (PVZGame.Game.LogicManager.TimeManager <= 20f)
            {
                return new NormalZombie();
            }

            else if (PVZGame.Game.LogicManager.TimeManager <= 40f)
            {
                int num = _Rand.Next(PVZGame.Game.LogicManager.ZombieTypes.Count - 1);

                switch (PVZGame.Game.LogicManager.ZombieTypes[num])
                {
                    case "NormalZombie":
                        return new NormalZombie();
                    case "FlyingZombie":
                        return new FlyingZombie();
                    default:
                        return null;
                }

            }

            else if (PVZGame.Game.LogicManager.TimeManager <= 90f)
            {
                int num = _Rand.Next(PVZGame.Game.LogicManager.ZombieTypes.Count);

                switch (PVZGame.Game.LogicManager.ZombieTypes[num])
                {
                    case "NormalZombie":
                        return new NormalZombie();
                    case "FlyingZombie":
                        return new FlyingZombie();
                    case "LaneJumpingZombie":
                        return new LaneJumpingZombie();
                }
            }

            else
            {
                PVZGame.Game.LogicManager.TimeManager = 40f;
                float timeSpawnRange=PVZGame.Game.LogicManager.TimeSpawnRange-=1.5f;
                if (timeSpawnRange <= 0f)
                    PVZGame.Game.LogicManager.TimeSpawnRange = 8f;

            }
            return null;
        }

    }
}
