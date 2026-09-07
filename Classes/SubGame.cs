using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_2;

internal class SubGame
{
    Map map;

    public SubGame()
    {
        map = new(Settings.mapSize);
    }

    public void Draw()
    {
        map.Draw();
    }
}
