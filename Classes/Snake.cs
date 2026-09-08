using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Snake_2;

internal class Snake
{
    List<Tile> body = new();
    Vector2 dir = new Vector2(1,0);

    Vector2 pos;
    Vector2 tileSize;
    Color color;

    float moveDelay = 0;

    public Snake(Vector2 pos, int length = 2)
    {
        this.pos = pos;
        this.tileSize = Settings.tileSize;
        this.color = Settings.snakeColor;

        CreateSnake(length);
    }

    public void CreateSnake(int length = 0)
    {
        if (length == 0) { length = body.Count; }

        for (int i = 0; i <= length; i++)
        {
            var tile = new Tile(new Vector2(pos.X * tileSize.X + pos.X, pos.Y * tileSize.Y + pos.Y), tileSize, color);

            body.Add(tile);
        }
    }

    public void Update()
    {
        if (moveDelay <= 0)
        {
            Move(dir);
            moveDelay = Settings.moveDelay;
        }
        else
        {
            moveDelay -= Raylib.GetFrameTime();
        }
    }

    public void Move(Vector2 dir)
    {
        body[0].Move(dir);

        if (body.Count < 1) return;

        for (int i = body.Count - 1; i >= 1; i--)
        {
            body[i].pos = body[i - 1].pos;
        }
    }

    public void Draw(Vector2 offset)
    {
        for (int i = 0;i < body.Count; i++)
        {
            body[i].Draw(offset);
        }
    }

    public void SetPos(Vector2 newPos)
    {
        this.pos = newPos;
        CreateSnake();
    }

    public void SetScale(Vector2 tileSize)
    {
        this.tileSize = tileSize;
        CreateSnake();
    }

    public Vector2 GetPos()
    {
        return pos;
    }

    public Vector2 GetScale()
    {
        return tileSize;
    }
}
