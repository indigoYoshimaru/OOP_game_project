using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlantvsZombie
{
    public static class Utility
    {
        public static int GetCell(float x,float a)
        {
            return Convert.ToInt32(Math.Ceiling(x / a));
        }

        public static void DrawCenter(SpriteBatch sb, Texture2D texture, Vector2 center, float width, float height)
        {
            int x = (int)(center.X - width / 2);
            int y = (int)(center.Y - height / 2);
            sb.Draw(texture, new Rectangle(x, y, (int) width, (int) height), Color.White);
        }
    }
}
