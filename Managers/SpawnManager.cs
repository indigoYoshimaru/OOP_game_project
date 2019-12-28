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
                PVZGame.Game.LogicManager.ManagedObjects.Add(z);
                PVZGame.Game.LogicManager.Zombies.Add(z);
            }
            
        }

        private void HandleDeadZombie(object self)
        {
            PVZGame.Game.LogicManager.ManagedObjects.Remove((GameObject)self);
            PVZGame.Game.LogicManager.Zombies.Remove((Zombie)self);
            PVZGame.Game.LogicManager.Player.UpdateScore((Zombie)self);
        }

        public void SpawnPlant(Tile mouseTile)
        {
            Plant pl = _Factory.PlantFactory(PVZGame.Game.LogicManager.Player.IconState, mouseTile.GetCenter());
            if (pl != null)
            {
                pl.Died += HandleDeadPlant;
                PVZGame.Game.LogicManager.ManagedObjects.Add(pl);
                PVZGame.Game.LogicManager.Plants.Add(pl);
                mouseTile.Plant = pl;
            }

        }

        private void HandleDeadPlant(object self)
        {
            PVZGame.Game.LogicManager.ManagedObjects.Remove((GameObject)self);
            PVZGame.Game.LogicManager.Plants.Remove((Plant)self);
            Tile tile = PVZGame.Game.LogicManager.GameMap.GetTileAt(((GameObject)self).Position);
            if(tile != null)
                tile.Plant = null;
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
