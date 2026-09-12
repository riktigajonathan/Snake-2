using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace Snake_2;

internal class SubGame
{
    public Dictionary<KeyboardKey, Action> keybinds = new();
    Map map;
    Snake snake;

    public SubGame(Vector2 pos)
    {
        map = new();
        map.SetPos(pos);

        snake = new(Vector2.Zero);
        InitKeybinds();
    }

    public void InitKeybinds()
    {
        keybinds.Add(KeyboardKey.Up, () => snake.QueueDirection(new Vector2(0, -1)));
        keybinds.Add(KeyboardKey.Down, () => snake.QueueDirection(new Vector2(0, 1)));
        keybinds.Add(KeyboardKey.Left, () => snake.QueueDirection(new Vector2(-1, 0)));
        keybinds.Add(KeyboardKey.Right, () => snake.QueueDirection(new Vector2(1, 0)));
    }

    public void Draw()
    {
        map.Draw();

        if (snake != null)
        {
            snake.Draw(map.GetPos());
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

        snake.Update();
    }

    public Map GetMap() => map;
}
