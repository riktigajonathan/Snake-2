using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Snake_2;

internal class Tile
{
    public static Vector2 globalOffset = Vector2.Zero;
    public static Vector2 globalScale = Vector2.One;

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
            (int)(((visualPos.X+offset.X) * globalScale.X) + globalOffset.X + 0.5f), 
            (int)(((visualPos.Y+offset.Y) * globalScale.Y) + globalOffset.Y + 0.5f), 
            (int)(size.X * globalScale.X), 
            (int)(size.Y * globalScale.Y), 
            color
        );

        //Raylib.DrawRectangle((int)Math.Round(visualPos.X + offset.X + size.X * 0.2f), (int)Math.Round(visualPos.Y + offset.Y + size.Y * 0.2f), (int)(size.X * 0.6f), (int)(size.Y * 0.6f), new Color((byte)color.R-6, (byte)color.G-6, (byte)color.B-6, (byte)255));
    }

    public void Move(Vector2 newPos)
    {
        pos = newPos;
        timer = Settings.moveTransition;
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
