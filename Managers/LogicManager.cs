using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class LogicManager
    {
        public HashSet<GameObject> ManagedObjects;
        public HashSet<Plant> Plants;
        public HashSet<Zombie> Zombies;
        public List<String> ZombieTypes;
        public float TimeManager { get; set; }
        public float TimeSinceLastSpawn { get; set; }
        public PlayerManager Player { get; set; }
        public SpawnManager Spawner { get; set; }
        public Map GameMap;

        public void Initialize()
        {
            ManagedObjects = new HashSet<GameObject>();
            Plants = new HashSet<Plant>();
            Zombies = new HashSet<Zombie>();
            ZombieTypes = new List<string>();
            ZombieTypes.Add("NormalZombie");
            ZombieTypes.Add("FlyingZombie");
            ZombieTypes.Add("LaneJumpingZombie");
            TimeSinceLastSpawn = 0f;
            TimeManager = 0f;
            GameMap = new Map(PVZGame.Game.GraphicsDevice.PresentationParameters.Bounds);

            Player = new PlayerManager();
            Spawner = new SpawnManager();
            Player.LoadHighScore();
            Spawner.SpawnZombie();
 
        }

        public void Update(GameTime gameTime)
        {
            switch (PVZGame.Game.State)
            {
                case PVZGame.GameState.PLAYING:
                    var currentObjects = new HashSet<GameObject>(ManagedObjects);
                    foreach (var ob in currentObjects)
                    {
                        try
                        {
                            ob.Update();
                        }
                        catch (Exception exc)
                        {
                            continue;
                        }
                    }
                    TimeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    TimeManager += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (TimeSinceLastSpawn >= 5f)
                    {
                        Spawner.SpawnZombie();
                        Spawner.SpawnSun();
                        TimeSinceLastSpawn = 0f;
                    }
                    Player.Update();
                    break;
                case PVZGame.GameState.START_MENU:
                    PVZGame.Game.StartMenu.Update(gameTime);
                    break;
                case PVZGame.GameState.END_MENU:
                    PVZGame.Game.EndMenu.Update(gameTime);
                    break;
                default:
                    break;
            }
        }

        public void EndGame()
        {
            Player.SaveHighScore();
        }

        
      
    }
}
