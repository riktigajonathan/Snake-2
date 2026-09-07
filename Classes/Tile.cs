using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Snake_2;

internal class Tile
{
    public Vector2 pos;
    public Vector2 size;
    public Color color;

    public Tile(Vector2 pos, Vector2 size, Color color)
    {
        this.pos = pos;
        this.size = size;
        this.color = color;
    }

    public void Draw(Vector2? _offset = null) 
    {
        Vector2 offset = Vector2.Zero;
        if (_offset != null) offset = (Vector2)_offset;

        Raylib.DrawRectangle((int)(pos.X+offset.X), (int)(pos.Y+offset.Y), (int)size.X, (int)size.Y, color);
    }
}
