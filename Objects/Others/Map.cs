using Microsoft.Xna.Framework;
using System;

namespace PlantvsZombie
{
    public class Map
    {
        public Vector2 TopLeft { get; }
        public Vector2 TileSize { get; }
        public float Width { get; }
        public float Height { get; }

        private readonly Tile[,] tiles;

        public Map(Rectangle windowBound)
        {
            // TopLeft and TileSize are calculated based on the original sizes and locations of the background texture
            TopLeft = new Vector2(95.0f / 900 * windowBound.Width, 125.0f / 600 * windowBound.Height);
            TileSize = new Vector2(80.0f / 900 * windowBound.Width, 80.0f / 600 * windowBound.Height);

            int rows = 5;
            int collumns = 10;
            Width = TileSize.X * collumns;
            Height = TileSize.Y * rows;

            tiles = new Tile[collumns, rows];
            for (int i = 0; i < collumns; i++)
                for (int j = 0; j < rows; j++)
                {
                    tiles[i, j] = new Tile(i, j, this);
                }
        }

        public Tile GetTileAt(Vector2 position)
        {
            Vector2 relativePos = Vector2.Subtract(position, TopLeft);
            if (relativePos.X < 0 || relativePos.X >= Width ||
               relativePos.Y < 0 || relativePos.Y >= Height)
                return null;
            int x = (int)(relativePos.X / TileSize.X);
            int y = (int)(relativePos.Y / TileSize.Y);
            return tiles[x, y];
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1))
                return null;
            return tiles[x, y];
        }

        public int getDimensionX()
        {
            return tiles.GetLength(0);
        }

        public int getDimensionY()
        {
            return tiles.GetLength(1);
        }
    }
}
