using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class SpawnManager
    {
        public void SpawnZombie()
        {
            Zombie z = null;
            Random _Rand = new Random();

            if (PVZGame.Game.LogicManager.TimeManager <= 20f)
            {
                z = new NormalZombie();

            }

            else if (PVZGame.Game.LogicManager.TimeManager >= 40f)
            {
                
                int num = _Rand.Next(PVZGame.Game.LogicManager.ZombieTypes.Count);

                switch (PVZGame.Game.LogicManager.ZombieTypes[num])
                {
                    case "NormalZombie":
                        z = new NormalZombie();
                        break;
                    case "FlyingZombie":
                        z = new FlyingZombie();
                        break;
                    case "LaneJumpingZombie":
                        z = new LaneJumpingZombie();
                        break;
                }

            }

            else if (PVZGame.Game.LogicManager.TimeManager >= 90f)
            {
                PVZGame.Game.LogicManager.TimeManager = 0f;
            }

            else
            {
                int num = _Rand.Next(PVZGame.Game.LogicManager.ZombieTypes.Count - 1);

                switch (PVZGame.Game.LogicManager.ZombieTypes[num])
                {
                    case "NormalZombie":
                        z = new NormalZombie();
                        break;
                    case "FlyingZombie":
                        z = new FlyingZombie();
                        break;
                    case "":
                        break;
                }

            }

            z.Died += HandleDeadZombie;
            z.Died += HandleScore;
            PVZGame.Game.LogicManager.ManagedObjects.Add(z);
            PVZGame.Game.LogicManager.Zombies.Add(z);

        }

        private void HandleScore(object self)
        {
            PVZGame.Game.LogicManager.Player.UpdateScore((Zombie)self);
        }

        private void HandleDeadZombie(object self)
        {
            PVZGame.Game.LogicManager.ManagedObjects.Remove((GameObject)self);
            PVZGame.Game.LogicManager.Zombies.Remove((Zombie)self);
        }

        private Plant PlantFactory(String plantState,Vector2 plantPosition)
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

        public void SpawnPlant(Tile mouseTile)
        {
            
            Plant pl = PlantFactory(PVZGame.Game.LogicManager.Player.PlantState, mouseTile.GetCenter());
            if (pl != null)
            {
                pl.Died += HandleDeadPlantObject;
                PVZGame.Game.LogicManager.ManagedObjects.Add(pl);
                PVZGame.Game.LogicManager.Plants.Add(pl);
            }

        }

        private void HandleDeadPlantObject(object self)
        {
            PVZGame.Game.LogicManager.ManagedObjects.Remove((GameObject)self);
            PVZGame.Game.LogicManager.Plants.Remove((Plant)self);
        }

        public void SpawnBullet(PeaShooter p)
        {
            Bullet bul = new Bullet(p);
            bul.Died += HandleDeadObject;
            PVZGame.Game.LogicManager.ManagedObjects.Add(bul);
        }

        private void HandleDeadObject(object self)
        {
            PVZGame.Game.LogicManager.ManagedObjects.Remove((GameObject)self);
        }

        public void SpawnSun(SunFlower sunFlower)
        {
            Sun sun = new Sun(sunFlower);
            sun.Died += HandleDeadObject;
            PVZGame.Game.LogicManager.ManagedObjects.Add(sun);
        }

        public void SpawnSun()
        {
            Sun sun = new Sun();
            sun.Died += HandleDeadObject;
            PVZGame.Game.LogicManager.ManagedObjects.Add(sun);
        }
    }
}
