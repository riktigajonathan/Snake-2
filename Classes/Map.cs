using Raylib_cs;
using System.Numerics;

namespace Snake_2;

internal class Map
{
    public Tile[,] tiles;

    public Map(Vector2 size, Vector2? _pos = null)
    {
        Vector2 pos = Vector2.Zero;
        if (_pos != null) pos = (Vector2)_pos;

        tiles = new Tile[(int)size.X, (int)size.Y];

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                tiles[x, y] = new Tile(new Vector2(x * Settings.tileSize.X, y * Settings.tileSize.Y), Settings.tileSize, Settings.primaryMapColor);
            }
        }
    }

    public void Draw()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                tiles[x, y].Draw();
            }
        }
    }
}