using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlantvsZombie
{
    public class Tile
    {
        private readonly Map map;
        public int X { get; }
        public int Y { get; }

        public Tile(int x, int y, Map map)
        {
            this.map = map;
            X = x;
            Y = y;
        }

        public Plant Plant { get; set; }

        public bool HasPlant()
        {
            return Plant != null;
        }

        public Vector2 GetCenter()
        {
            return new Vector2((X + 0.5f) * map.TileSize.X + map.TopLeft.X, (Y + 0.5f) * map.TileSize.Y + map.TopLeft.Y);
        }

        public bool Contains(Vector2 location)
        {
            return map.GetTileAt(location) == this;
        }

        public Tile GetRelativeTile(int xr, int yr)
        {
            return map.GetTile(X + xr, Y + yr);
        }

    }
}
