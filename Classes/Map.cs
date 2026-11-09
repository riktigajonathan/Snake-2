using Raylib_cs;
using System.Numerics;

namespace Snake_2;

internal class Map
{
    public Tile[,] tiles;
    public Vector2 size;

    Vector2 tileSize;
    Vector2 pos;

    public Map()
    {
        this.size = Settings.mapSize;
        this.tileSize = Settings.tileSize;
        this.pos = new Vector2(0, 0);
        
        tiles = new Tile[(int)size.X, (int)size.Y];

        CreateMap();
    }

    public void CreateMap()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                Color c = (x + y) % 2 == 0
                    ? Settings.primaryMapColor
                    : Settings.secondaryMapColor;

                tiles[x, y] = new Tile(new Vector2(x * tileSize.X + pos.X, y * tileSize.Y + pos.Y), tileSize, c);
            }
        }
    }

    public void Draw()
    {
        if (Game.currentSubGame.GetMap() == this)
        {
            Raylib.DrawRectangle(
                (int)((pos.X - tileSize.X + 6) * Tile.globalScale.X + Tile.globalOffset.X),
                (int)((pos.Y - tileSize.Y + 6) * Tile.globalScale.Y + Tile.globalOffset.Y),
                (int)(tileSize.X * (size.X + 2) * Tile.globalScale.X),
                (int)(tileSize.Y * (size.Y + 2) * Tile.globalScale.Y),
                new Color(0, 0, 0, 20)
            );

            Raylib.DrawRectangle(
                (int)((pos.X - tileSize.X) * Tile.globalScale.X + Tile.globalOffset.X),
                (int)((pos.Y - tileSize.Y) * Tile.globalScale.Y + Tile.globalOffset.Y),
                (int)(tileSize.X * (size.X + 2) * Tile.globalScale.X),
                (int)(tileSize.Y * (size.Y + 2) * Tile.globalScale.Y),
                Settings.borderColor
            );

            Raylib.BeginBlendMode(BlendMode.Additive);
            Raylib.DrawRectangle(
                (int)((pos.X - tileSize.X + 2) * Tile.globalScale.X + Tile.globalOffset.X),
                (int)((pos.Y - tileSize.Y + 2) * Tile.globalScale.Y + Tile.globalOffset.Y),
                (int)((tileSize.X * (size.X + 2) - 4) * Tile.globalScale.X),
                (int)((tileSize.Y * (size.Y + 2)-4) * Tile.globalScale.Y),
                new Color(255, 255, 255, 10)
            );
            Raylib.EndBlendMode();
        }

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                tiles[x, y].Draw();
            }
        }
    }

    public bool OccupiedAt(Vector2 pos)
    {
        pos /= tileSize;
        return pos.X < 0 || pos.Y < 0 || pos.X >= size.X || pos.Y >= size.Y;
    }

    public void SetPos(Vector2 newPos)
    {
        this.pos = newPos;
        CreateMap();
    }

    public void SetScale(Vector2 tileSize)
    {
        this.tileSize = tileSize;
        CreateMap();
    }

    public Vector2 GetPos() => pos;
    public Vector2 GetScale() => tileSize;
}