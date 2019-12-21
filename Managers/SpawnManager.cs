using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class SpawnManager
    {

        private IFactory _Factory = new PlantZombieFactory();
        public void SpawnZombie()
        {
            Zombie z = _Factory.ZombieFactory();
            if (z != null)
            {
                z.Died += HandleDeadZombie;
                z.Died += HandleScore;
                PVZGame.Game.LogicManager.ManagedObjects.Add(z);
                PVZGame.Game.LogicManager.Zombies.Add(z);
            }
            
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

        public void SpawnPlant(Tile mouseTile)
        {
            
            Plant pl = _Factory.PlantFactory(PVZGame.Game.LogicManager.Player.IconState, mouseTile.GetCenter());
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
