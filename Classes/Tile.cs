using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Snake_2;

internal class Tile
{
    public Vector2 pos;
    public Vector2 visualPos;
    public Vector2 size;
    public Color color;
    public float timer = 0;

    public Tile(Vector2 pos, Vector2 size, Color color)
    {
        this.pos = pos;
        this.visualPos = pos;
        this.size = size;
        this.color = color;
    }

    public void Draw(Vector2? _offset = null) 
    {
        Vector2 offset;
        if (_offset == null)
        {
            offset = Vector2.Zero;
        }
        else
        {
            offset = (Vector2)_offset;
        }

        Raylib.DrawRectangle(
            (int)Math.Round(visualPos.X+offset.X), 
            (int)Math.Round(visualPos.Y+offset.Y), 
            (int)size.X, 
            (int)size.Y, 
            color
        );
    }

    public void Move(Vector2 newPos)
    {
        pos = newPos;
        timer = Settings.moveDelay;
    }

    public void UpdateTween(float dt)
    {
        if (timer > 0)
        {
            visualPos = Vector2.Lerp(visualPos, pos, dt / timer);

            timer -= dt;
        }
        else
        {
            visualPos = pos;
        }
    }
}
