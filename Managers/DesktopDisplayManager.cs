﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantvsZombie
{
    public class DesktopDisplayManager:IDisplay
    {
        private Dictionary<String, Texture2D> TextureAssets = new Dictionary<string, Texture2D>();
        private SpriteFont GameFont;

        void IDisplay.LoadContent()
        {
            
            TextureAssets["EndMenuBG"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Background/EndMenuBG");
            TextureAssets["StartMenuBG"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Background/StartMenuBG");
            TextureAssets["Lawn"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Background/Lawn");
            TextureAssets["NormalZombie"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Zombies/NormalZombie");
            TextureAssets["PeaShooter"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Plants/PeaShooter");
            TextureAssets["SunFlower"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Plants/SunFlower");
            TextureAssets["CarnivorousPlant"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Plants/CarnivorousPlant");
            TextureAssets["Bullet"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Miscellaneous/Bullet");
            TextureAssets["Sun"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Miscellaneous/Sun");
            TextureAssets["FlyingZombie"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Zombies/FlyingZombie");
            TextureAssets["LaneJumpingZombie"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Zombies/LaneJumpingZombie");
            TextureAssets["NormalMouse"] = PVZGame.Game.Content.Load<Texture2D>("Texture/Miscellaneous/NormalMouse");
            GameFont = PVZGame.Game.Content.Load<SpriteFont>("Texture/Miscellaneous/galleryFont");
        }

        void IDisplay.Display(GameTime gameTime)
        {
            switch (PVZGame.Game.State)
            {
                case PVZGame.GameState.START_MENU:
                    PVZGame.Game.SpriteBatch.Begin();
                    DrawBackground("StartMenuBG");
                    PVZGame.Game.SpriteBatch.End();
                    PVZGame.Game.StartMenu.Draw(gameTime);
                    break;
                case PVZGame.GameState.PLAYING:
                    PVZGame.Game.SpriteBatch.Begin();
                    DrawBackground("Lawn");
                    DrawGameplay();
                    PVZGame.Game.SpriteBatch.End();
                    break;
                case PVZGame.GameState.END_MENU:
                    PVZGame.Game.SpriteBatch.Begin();
                    DrawBackground("EndMenuBG");
                    PVZGame.Game.SpriteBatch.End();
                    PVZGame.Game.EndMenu.Draw(gameTime);
                    break;
            }


            PVZGame.Game.SpriteBatch.Begin();
            DrawMouse();
            PVZGame.Game.SpriteBatch.End();
        }

        private void DrawBackground(string backgroundName)
        {
            Rectangle rec = new Rectangle(0, 0, 800, 480);
            PVZGame.Game.SpriteBatch.Draw(TextureAssets[backgroundName], rec, Color.White);
        }

        private void DrawGameplay()
        {
            var currentObjects = new HashSet<GameObject>(PVZGame.Game.LogicManager.ManagedObjects);

            foreach (var ob in currentObjects)
            {
                string objectClassName = ob.GetType().Name;
                float size = PVZGame.Game.LogicManager.GameMap.TileSize.X;
                if (objectClassName != null)
                    DrawCenter(TextureAssets[objectClassName], ob.Position, size, size);
            }
            PVZGame.Game.SpriteBatch.DrawString(GameFont, "Score: " + PVZGame.Game.LogicManager.Player.GetScore().ToString(),
                new Vector2(10, PVZGame.Game.Graphic.PreferredBackBufferHeight - 40),
                Color.White);
            PVZGame.Game.SpriteBatch.DrawString(GameFont, "HIGHSCORE: " + PVZGame.Game.LogicManager.Player.GetHighScore().ToString(), new Vector2(PVZGame.Game.Graphic.PreferredBackBufferWidth - 300, PVZGame.Game.Graphic.PreferredBackBufferHeight - 40), Color.White);
            PVZGame.Game.SpriteBatch.DrawString(GameFont, "SUN: " + PVZGame.Game.LogicManager.Player.GetTotalSun().ToString(), new Vector2(10, 10), Color.White);
        }

        private void DrawMouse()
        {
            PVZGame g = PVZGame.Game;

            Vector2 pos = new Vector2(g.MouseX(), g.MouseY());
            float size = g.LogicManager.GameMap.TileSize.X;

            string mouseState = g.LogicManager.Player.IconState;

            if (g.State != PVZGame.GameState.PLAYING || mouseState == "NormalMouse")
                g.SpriteBatch.Draw(TextureAssets["NormalMouse"], pos, Color.White);
            else
                DrawCenter(TextureAssets[mouseState], pos, size, size);
        }

        private void DrawCenter(Texture2D texture, Vector2 center, float width, float height)
        {
            int x = (int)(center.X - width / 2);
            int y = (int)(center.Y - height / 2);
            PVZGame.Game.SpriteBatch.Draw(texture, new Rectangle(x, y, (int)width, (int)height), Color.White);
        }
    }
}