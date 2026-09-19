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

        if (snake != null && Game.currentSubGame == this)
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

        snake.Update();

        if (food.Count < Settings.appleCount)
        {
            SpawnFood();
        }

        for (int i = 0; i < food.Count; i++)
        {
            food[i].Update();

            if (snake.GetPos() == food[i].GetPos())
            {
                food[i].Eaten();
            }

            if (food[i].queuedDeletion)
            {
                food.RemoveAt(i);
                i--;
            }
        }
    }

    static void Exit(bool won)
    {
        SubGame current = Game.currentSubGame;

        Game.subGames.Remove(current);
        Game.lastSubGameWon = won;
        Game.ChangeSubGameTo(current.parent);
    }

    public static void Win()
    {
        SubGame.Exit(true);
    }

    public static void Die()
    {
        SubGame.Exit(false);
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

        if (spawnablePos.Count == 1)
        {
            SubGame.Win();
        }

        for (int i = 0; i < food.Count; i++)
        {
            spawnablePos.Remove(food[i].GetPos());
        }
        
        if (spawnablePos.Count-1 > 0)
        {
            var newFood = new Food(spawnablePos[Settings.rng.Next(0, spawnablePos.Count)]);
            food.Add(newFood);
        } 
    }

    public void Grow()
    {
        snake.Grow();
    }

    public void SetPos(Vector2 newPos)
    {
        offset = newPos;
    }

    public Map GetMap() => map;
    public List<Food> GetFood() => food;
    public bool MapOccupied(Vector2 pos) => GetMap().OccupiedAt(pos);
}
