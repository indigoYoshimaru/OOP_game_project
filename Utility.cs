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
    }
}
