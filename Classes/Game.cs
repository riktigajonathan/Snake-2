using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_2;

internal class Game
{
    List<SubGame> subGames = new();

    public Game()
    {
        subGames.Add(new SubGame());
    }

    public void Draw()
    {
        for (int i = 0; i < subGames.Count; i++)
        {
            subGames[i].Draw();
        }
    }
}
