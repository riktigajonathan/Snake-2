using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Snake_2;

internal class Snake
{
    public List<Tile> body = new();
    public Vector2 dir = new Vector2(1, 0);

    Vector2 pos;
    Vector2 tileSize;
    Color color;

    List<Vector2> moveQueue = new();
    float moveTimer = 0;

    public Snake(Vector2 pos)
    {
        this.pos = pos;
        this.tileSize = Settings.tileSize;
        this.color = Settings.snakeColor;

        CreateSnake(Settings.startLength);
        InitKeybinds();
    }

    public void CreateSnake(int length = 3)
    {
        body.Clear();

        Vector2 headPixelPos = new Vector2(pos.X * (tileSize.X + 1), pos.Y * (tileSize.Y + 1));

        for (int i = 0; i <= length; i++)
        {
            Vector2 spawnPos = new Vector2(pos.X * tileSize.X + pos.X, pos.Y * tileSize.Y + pos.Y);
            var tile = new Tile(spawnPos, tileSize, color);
            body.Add(tile);
        }
    }

    public void InitKeybinds()
    {
        Game.keybinds.Add(KeyboardKey.Up, () => QueueDirection(new Vector2(0, -1)));
        Game.keybinds.Add(KeyboardKey.Down, () => QueueDirection(new Vector2(0, 1)));
        Game.keybinds.Add(KeyboardKey.Left, () => QueueDirection(new Vector2(-1, 0)));
        Game.keybinds.Add(KeyboardKey.Right, () => QueueDirection(new Vector2(1, 0)));
    }

    private void QueueDirection(Vector2 newDir)
    {
        Vector2 lastDir = moveQueue.Count > 0 ? moveQueue[moveQueue.Count - 1] : dir;

        if (lastDir + newDir != Vector2.Zero && moveQueue.Count < 3)
        {
            moveQueue.Add(newDir);
        }
    }

    public void Update()
    {
        float dt = Raylib.GetFrameTime();

        moveTimer -= dt;
        if (moveTimer <= 0)
        {
            Move();
        }

        for (int i = 0; i < body.Count; i++)
        {
            body[i].UpdateTween(dt);
        }
    }

    public void Move()
    {
        moveTimer = Settings.moveDelay;

        if (moveQueue.Count > 0)
        {
            dir = moveQueue[0];
            moveQueue.RemoveAt(0);
        }

        Vector2 newHeadPos = body[0].pos + (dir * tileSize);
        if (MapOccupied(newHeadPos / tileSize))
        {
            Game.Die();
        }

        for (int i = body.Count - 1; i >= 1; i--)
        {
            body[i].Move(body[i - 1].pos);
            
            if (body[i].pos == newHeadPos)
            {
                Game.Die();
            }
        }

        body[0].Move(newHeadPos);
    }

    bool MapOccupied(Vector2 pos)
    {
        return Game.subGames[Game.currentSubGame].GetMap().OccupiedAt(pos);
    }

    public void Draw(Vector2 offset)
    {
        for (int i = 0; i < body.Count; i++)
        {
            body[i].Draw(offset);
        }
    }

    public void SetPos(Vector2 newPos)
    {
        this.pos = newPos;
        CreateSnake(body.Count > 0 ? body.Count : Settings.startLength);
    }

    public void SetScale(Vector2 tileSize)
    {
        this.tileSize = tileSize;
        CreateSnake(body.Count > 0 ? body.Count : Settings.startLength);
    }

    public Vector2 GetPos() => pos;
    public Vector2 GetScale() => tileSize;
}