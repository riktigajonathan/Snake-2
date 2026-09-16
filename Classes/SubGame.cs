using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Snake_2;

internal class SubGame
{
    public Dictionary<KeyboardKey, Action> keybinds = new();
    public SubGame parent;
    public int depth = 0;

    Map map;
    Snake snake;
    List<Food> food;

    Vector2 offset = Vector2.Zero;

    public SubGame(Vector2 pos, SubGame parent = null)
    {
        this.parent = parent;
        this.map = new();
        this.snake = new(Vector2.Zero);
        this.food = new();

        if (parent != null)
        {
            depth = parent.depth + 1;
        }

        map.SetPos(pos);
        this.offset = map.GetPos();

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

        for (int i = 0; i < food.Count; i++)
        {
            food[i].Draw(offset);
        }

        if (snake != null)
        {
            snake.Draw(offset);
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

        for (int i = 0;i < food.Count;i++)
        {
            food[i].Update();
        }

        snake.Update();
        
        SpawnFood();
    }

    public void Die()
    {
        Game.subGames.Remove(this);
        Game.currentSubGame = parent;
    }

    public void SpawnFood()
    {
        List<Vector2> spawnablePos = new();

        for (int i = 0; i < map.size.X; i++)
        {
            for (int j = 0; j < map.size.Y; j++)
            {
                var pos = new Vector2(i, j);
                if (!snake.BodyAt(pos * snake.GetScale()))
                {
                    spawnablePos.Add(pos);
                }
            }
        }

        for (int i = 0; i < food.Count; i++)
        {
            spawnablePos.Remove(food[i].tile.pos / food[i].tile.size);
        }

        if (spawnablePos.Count > 0)
        {
            var newFood = new Food(spawnablePos[Settings.rng.Next(0, spawnablePos.Count)]);
            food.Add(newFood);
        }
    }

    public void SetPos(Vector2 newPos) => offset = newPos;
    public Map GetMap() => map;
    public bool MapOccupied(Vector2 pos) => GetMap().OccupiedAt(pos);
}
