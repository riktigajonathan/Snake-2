using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Snake_2;

internal static class Game
{
    public static List<SubGame> subGames = new();
    public static int currentSubGame = 0;
        
    public static void AddSubGame(Vector2 pos)
    {
        subGames.Add(new SubGame(pos));
    }

    public static void Draw()
    {
        if (subGames.Count <= 0) return;

        for (int i = 0; i < subGames.Count; i++)
        {
            subGames[i].Draw();
        }
    }

    public static void Update()
    {
        if (subGames.Count <= 0) return;

        subGames[currentSubGame].Update();
    }

    public static void Die()
    {
        subGames.RemoveAt(currentSubGame);
        currentSubGame -= 1;
    }
}
