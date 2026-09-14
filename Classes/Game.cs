using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Snake_2;

internal static class Game
{
    public static List<SubGame> subGames = new();
    public static SubGame currentSubGame = null;

    public static void AddSubGame(Vector2 pos)
    {
        var subGame = new SubGame(pos);
        subGames.Add(subGame);
        currentSubGame = subGame;
    }

    public static void Draw()
    {
        if (subGames.Count <= 0) return;

        for (int i = 0; i < subGames.Count; i++) // todo: only draw visible
        {
            subGames[i].Draw();
        }
    }

    public static void Update()
    {
        if (subGames.Count <= 0) return;

        currentSubGame.Update();
    }

    public static void Die()
    {
        currentSubGame.Die();
    }
}
