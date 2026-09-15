using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_2;

internal class Food
{
    SubGame subGame;
    Tile tile;

    public void Update()
    {
        if (subGame != null)
        {
            subGame.Update();
        }
    }

    public void Draw()
    {
        if (subGame != null)
        {
            subGame.Draw();
        }
        else if (tile != null)
        {
            tile.Draw();
        }
    }
}
