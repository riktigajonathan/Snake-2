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
    public static bool lastSubGameWon = false;

    public static void AddSubGame(Vector2 pos)
    {
        var subGame = new SubGame(pos);
        subGames.Add(subGame);
        currentSubGame = subGame;
    }

    public static void ChangeSubGameTo(SubGame subgame)
    {
        currentSubGame = subgame;

        if (currentSubGame != null)
            currentSubGame.paused = false;

        if (lastSubGameWon)
        {
            if (currentSubGame == null)
            {
                Win();
            }
        }
    }

    public static void Draw()
    {
        if (currentSubGame != null)
            currentSubGame.Draw();
    }

    public static void Update()
    {
        if (subGames.Count <= 0) return;

        if (currentSubGame != null)
        {
            currentSubGame.Update();
        }
    }

    public static void Center()
    {
        Vector2 center = Settings.GetCenter();
        var gamePos = new Vector2(
            center.X - (Settings.mapSize.X * Settings.tileSize.X) / 2f,
            center.Y - (Settings.mapSize.Y * Settings.tileSize.Y) / 2f
        );
        currentSubGame.SetPos(gamePos);
        currentSubGame.GetMap().SetPos(gamePos);
    }

    public static void Win()
    {
        Console.WriteLine("you won!");
        Program.WindowShouldClose = true;
    }
}
