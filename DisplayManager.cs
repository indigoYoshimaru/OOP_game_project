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
        PVZGame _game;

        public DisplayManager(PVZGame game)
        {
            _game = game;
        }

        public void Display(GameTime gameTime)
        {
            switch (_game.State)
            {
                case PVZGame.GameState.START_MENU:
                    _game.SpriteBatch.Begin();
                    DrawBackground("StartMenuBG");
                    _game.SpriteBatch.End();
                    _game.StartMenu.Draw(gameTime);
                    break;
                case PVZGame.GameState.PLAYING:
                    _game.SpriteBatch.Begin();
                    DrawBackground("Background");
                    DrawGameplay();
                    _game.SpriteBatch.End();
                    break;
                case PVZGame.GameState.END_MENU:
                    _game.SpriteBatch.Begin();
                    DrawBackground("EndMenuBG");
                    _game.SpriteBatch.End();
                    _game.EndMenu.Draw(gameTime);
                    break;
            }


            _game.SpriteBatch.Begin();
            DrawMouse();
            _game.SpriteBatch.End();
        }

        private void DrawBackground(string backgroundName)
        {
            Rectangle rec = new Rectangle(0, 0, 800, 480);
            _game.SpriteBatch.Draw(_game.TextureAssets[backgroundName], rec, Color.White);
        }

        private void DrawGameplay()
        {
            var currentObjects = new HashSet<GameObject>(_game.ManagedObjects);

            foreach (var ob in currentObjects)
            {
                string objectClassName = ob.GetType().Name;
                float size = _game.GameMap.TileSize.X;
                if (objectClassName != null)
                    DrawCenter(_game.TextureAssets[objectClassName], ob.Position, size, size);
            }
            _game.SpriteBatch.DrawString(_game.GameFont, "Score: " + _game.Player.GetScore().ToString(), 
                new Vector2(0, _game.Graphics.PreferredBackBufferHeight - 30), 
                Color.White); //display score at the bottom left
        }

        private void DrawMouse()
        {
            Vector2 pos = new Vector2(_game.MouseX(), _game.MouseY());
            float size = _game.GameMap.TileSize.X;

            switch (_game.Player.GetMouseIcon())
            {
                case PlayerManager.MouseIcon.NORMAL:
                    _game.SpriteBatch.Draw(_game.TextureAssets["NormalMouse"], pos, Color.White);
                    break;
                case PlayerManager.MouseIcon.PEASHOOTER:
                    DrawCenter(_game.TextureAssets["PeaShooter"], pos, size, size);
                    break;
                case PlayerManager.MouseIcon.SUNFLOWER:
                    DrawCenter(_game.TextureAssets["SunFlower"], pos, size, size);
                    break;
                case PlayerManager.MouseIcon.CARNIVOROUSPLANT:
                    DrawCenter(_game.TextureAssets["CarnivorousPlant"], pos, size, size);
                    break;
            }
        }
        private void DrawCenter(Texture2D texture, Vector2 center, float width, float height)
        {
            int x = (int)(center.X - width / 2);
            int y = (int)(center.Y - height / 2);
            _game.SpriteBatch.Draw(texture, new Rectangle(x, y, (int)width, (int)height), Color.White);
        }
    }
}
