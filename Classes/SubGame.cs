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

    public SubGame(Vector2 pos)
    {
        map = new();
        map.SetPos(pos);

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

    public Map GetMap() => map;
}
