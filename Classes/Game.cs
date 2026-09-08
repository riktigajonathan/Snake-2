using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_2;

internal class Game
{
    public static Dictionary<KeyboardKey, Action> keybinds = new();
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

    public void Update()
    {
        int pressedKey = Raylib.GetKeyPressed();

        if (pressedKey != 0)
        {
            KeyboardKey key = (KeyboardKey)pressedKey;

            if (keybinds.TryGetValue(key, out Action? action))
            {
                action.Invoke();
            }
        }


        for (int i = 0; i < subGames.Count; i++)
        {
            subGames[i].Update();
        }
    }
}
