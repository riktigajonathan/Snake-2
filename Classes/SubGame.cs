using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace Snake_2;

internal class SubGame
{
    Map map;
    Snake snake;

    public SubGame()
    {
        map = new();

        Vector2 center = Settings.GetCenter();
        Vector2 scale = map.GetScale();
        map.SetPos(new Vector2(
            center.X - (map.size.X * scale.X) / 2f,
            center.Y - (map.size.Y * scale.Y) / 2f
        ));

        snake = new(Vector2.Zero);
    }

    public void Draw()
    {
        map.Draw();
        snake.Draw(map.GetPos());
    }

    public void Update()
    {
        snake.Update();
    }
}
