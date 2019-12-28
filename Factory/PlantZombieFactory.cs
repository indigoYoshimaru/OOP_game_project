using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class PlantZombieFactory:IFactory
    {
        
        Zombie IFactory.ZombieFactory()
        {
            Random _Rand = new Random();

            if (PVZGame.Game.LogicManager.TimeManager <= 20f)
            {
                return new NormalZombie();
            }

            else if (PVZGame.Game.LogicManager.TimeManager >= 40f)
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

            else if (PVZGame.Game.LogicManager.TimeManager >= 90f)
            {
                PVZGame.Game.LogicManager.TimeManager = 40f;
                PVZGame.Game.LogicManager.TimeSpawnRange -= 2f;
            }

            else
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
            return null;
        }



        Plant IFactory.PlantFactory(string plantState, Vector2 plantPosition)
        {
            switch (plantState)
            {
                case "PeaShooter":
                    return new PeaShooter(plantPosition);
                case "SunFlower":
                    return new SunFlower(plantPosition);
                case "CarnivorousPlant":
                    return new CarnivorousPlant(plantPosition);
                default:
                    return null;
            }
        }
    }
}
