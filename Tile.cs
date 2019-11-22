using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    class Tile
    {
        private Point _Location;
        private Point _Size;

        public Rectangle BoundingRectangle
        {
            get { return new Rectangle(_Location, _Size); } // need someone to build the map
        }
    }
}
