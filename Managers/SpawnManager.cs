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

            if (PVZGame.Game.TimeManager <= 20f)
            {
                z = new NormalZombie();

            }

            else if (PVZGame.Game.TimeManager >= 40f)
            {
                
                int num = _Rand.Next(PVZGame.Game.ZombieTypes.Count);

                switch (PVZGame.Game.ZombieTypes[num])
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

            else if (PVZGame.Game.TimeManager >= 90f)
            {
                PVZGame.Game.TimeManager = 0f;
            }

            else
            {
                int num = _Rand.Next(PVZGame.Game.ZombieTypes.Count - 1);

                switch (PVZGame.Game.ZombieTypes[num])
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
            PVZGame.Game.ManagedObjects.Add(z);
            PVZGame.Game.Zombies.Add(z);

        }

        private void HandleScore(object self)
        {
            PVZGame.Game.Player.UpdateScore((Zombie)self);
        }

        private void HandleDeadZombie(object self)
        {
            PVZGame.Game.ManagedObjects.Remove((GameObject)self);
            PVZGame.Game.Zombies.Remove((Zombie)self);
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
            
            Plant pl = PlantFactory(PVZGame.Game.Player.PlantState, mouseTile.GetCenter());
            if (pl != null)
            {
                pl.Died += HandleDeadPlantObject;
                PVZGame.Game.ManagedObjects.Add(pl);
                PVZGame.Game.Plants.Add(pl);
            }

        }

        private void HandleDeadPlantObject(object self)
        {
            PVZGame.Game.ManagedObjects.Remove((GameObject)self);
            PVZGame.Game.Plants.Remove((Plant)self);
        }

        public void SpawnBullet(PeaShooter p)
        {
            Bullet bul = new Bullet(p);
            bul.Died += HandleDeadObject;
            PVZGame.Game.ManagedObjects.Add(bul);
        }

        private void HandleDeadObject(object self)
        {
            PVZGame.Game.ManagedObjects.Remove((GameObject)self);
        }

        public void SpawnSun(SunFlower sunFlower)
        {
            Sun sun = new Sun(sunFlower);
            sun.Died += HandleDeadObject;
            PVZGame.Game.ManagedObjects.Add(sun);
        }

        public void SpawnSun()
        {
            Sun sun = new Sun();
            sun.Died += HandleDeadObject;
            PVZGame.Game.ManagedObjects.Add(sun);
        }
    }
}
