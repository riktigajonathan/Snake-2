using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_2;

internal static class Game
{
    public static Dictionary<KeyboardKey, Action> keybinds = new();
    public static List<SubGame> subGames = new();
    public static int currentSubGame = 0;
        
    public static void AddSubGame()
    {
        subGames.Add(new SubGame());
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

        int pressedKey = Raylib.GetKeyPressed();
        if (pressedKey != 0)
        {
            KeyboardKey key = (KeyboardKey)pressedKey;

            if (keybinds.TryGetValue(key, out Action? action))
            {
                action.Invoke();
            }
        }

        subGames[currentSubGame].Update();
    }

    public static void Die()
    {
        subGames.RemoveAt(currentSubGame);
        currentSubGame -= 1;
    }
}
