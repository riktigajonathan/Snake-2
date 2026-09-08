using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Snake_2;

internal class Snake
{
    public List<Tile> body = new();
    public Vector2 dir = new Vector2(1,0);

    Vector2 pos;
    Vector2 tileSize;
    Color color;

    bool inputThisFrame = false;
    float moveTimer = 0;

    public Snake(Vector2 pos, int length = 2)
    {
        this.pos = pos;
        this.tileSize = Settings.tileSize;
        this.color = Settings.snakeColor;

        CreateSnake(length);
        InitKeybinds();
    }

    public void CreateSnake(int length = 0)
    {
        if (length == 0) { length = body.Count; }

        for (int i = 0; i <= length; i++)
        {
            Vector2 spawnPos = new Vector2(pos.X * tileSize.X + pos.X, pos.Y * tileSize.Y + pos.Y);
            var tile = new Tile(spawnPos, tileSize, color);

            body.Add(tile);
        }
    }

    public void InitKeybinds()
    {
        Game.keybinds.Add(KeyboardKey.Up, () => { if (inputThisFrame) return; dir = dir == new Vector2(0, 1) ? dir : new Vector2(0, -1); inputThisFrame = true; });
        Game.keybinds.Add(KeyboardKey.Down, () => { if (inputThisFrame) return; dir = dir == new Vector2(0, -1) ? dir : new Vector2(0, 1); inputThisFrame = true; });
        Game.keybinds.Add(KeyboardKey.Right, () => { if (inputThisFrame) return; dir = dir == new Vector2(-1, 0) ? dir : new Vector2(1, 0); inputThisFrame = true; });
        Game.keybinds.Add(KeyboardKey.Left, () => { if (inputThisFrame) return; dir = dir == new Vector2(1, 0) ? dir : new Vector2(-1, 0); inputThisFrame = true; });
    }

    public void Update()
    {
        float dt = Raylib.GetFrameTime();

        if (moveTimer <= 0)
        {
            Move(dir);
        }
        else
        {
            moveTimer -= dt;
        }

        for (int i = 0; i < body.Count; i++)
        {
            body[i].UpdateTween(dt);
        }
    }

    public void Move(Vector2 dir)
    {
        moveTimer = Settings.moveDelay;
        inputThisFrame = false;

        for (int i = body.Count - 1; i >= 1; i--)
        {
            body[i].pos = body[i - 1].pos;
            body[i].Move(body[i - 1].pos);
        }

        Vector2 newHeadPos = body[0].pos + dir * body[0].size;
        body[0].Move(newHeadPos);
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

    public Vector2 GetPos() => pos;
    public Vector2 GetScale() => tileSize;
}