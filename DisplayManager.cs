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
            _game.SpriteBatch.Begin();
            DrawBackground();
            DisplayGameObjects();
            _game.SpriteBatch.End();

            _game.StartMenu.Draw(gameTime);

            _game.SpriteBatch.Begin();
            DrawMouse();
            _game.SpriteBatch.End();
        }

        private void DrawBackground()
        {
            Rectangle rec = new Rectangle(0, 0, 800, 480);
            _game.SpriteBatch.Draw(_game.TextureAssets["Background"], rec, Color.White);
        }

        private void DisplayGameObjects()
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
                new Vector2(0, _game.Graphic.PreferredBackBufferHeight - 30), 
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

        public void DrawCenter(Texture2D texture, Vector2 center, float width, float height)
        {
            int x = (int)(center.X - width / 2);
            int y = (int)(center.Y - height / 2);
            _game.SpriteBatch.Draw(texture, new Rectangle(x, y, (int)width, (int)height), Color.White);
        }
    }
}
