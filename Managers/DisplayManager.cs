using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantvsZombie
{
    public class DisplayManager
    {
        

        public void LoadContent()
        {
            
        }

        public void Display(GameTime gameTime)
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
            PVZGame.Game.SpriteBatch.Draw(PVZGame.Game.TextureAssets[backgroundName], rec, Color.White);
        }

        private void DrawGameplay()
        {
            var currentObjects = new HashSet<GameObject>(PVZGame.Game.LogicManager.ManagedObjects);

            foreach (var ob in currentObjects)
            {
                string objectClassName = ob.GetType().Name;
                float size = PVZGame.Game.LogicManager.GameMap.TileSize.X;
                if (objectClassName != null)
                    DrawCenter(PVZGame.Game.TextureAssets[objectClassName], ob.Position, size, size);
            }
            PVZGame.Game.SpriteBatch.DrawString(PVZGame.Game.GameFont, "Score: " + PVZGame.Game.LogicManager.Player.GetScore().ToString(),
                new Vector2(0, PVZGame.Game.Graphic.PreferredBackBufferHeight - 30),
                Color.White);
            PVZGame.Game.SpriteBatch.DrawString(PVZGame.Game.GameFont, "HIGHSCORE: " + PVZGame.Game.LogicManager.Player.GetHighScore().ToString(), new Vector2(480, PVZGame.Game.Graphic.PreferredBackBufferHeight - 40), Color.White);
            PVZGame.Game.SpriteBatch.DrawString(PVZGame.Game.GameFont, "SUN: " + PVZGame.Game.LogicManager.Player.GetTotalSun().ToString(), new Vector2(10, 10), Color.White);//display score at the bottom left
        }

        private void DrawMouse()
        {
            Vector2 pos = new Vector2(PVZGame.Game.MouseX(), PVZGame.Game.MouseY());
            float size = PVZGame.Game.LogicManager.GameMap.TileSize.X;

            string mouseState = PVZGame.Game.LogicManager.Player.IconState;

            switch (mouseState)
            {
                case "NormalMouse":
                    PVZGame.Game.SpriteBatch.Draw(PVZGame.Game.TextureAssets[mouseState], pos, Color.White);
                    break;
                default:
                    DrawCenter(PVZGame.Game.TextureAssets[mouseState], pos, size, size);
                    break;
            }
        }

        private void DrawCenter(Texture2D texture, Vector2 center, float width, float height)
        {
            int x = (int)(center.X - width / 2);
            int y = (int)(center.Y - height / 2);
            PVZGame.Game.SpriteBatch.Draw(texture, new Rectangle(x, y, (int)width, (int)height), Color.White);
        }
    }
}