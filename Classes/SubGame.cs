using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Snake_2;

internal class SubGame
{
    public Dictionary<KeyboardKey, Action> keybinds = new();
    public SubGame? parent = null;
    public int depth = 0;
    public bool paused = false;

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

        if (snake != null && Game.currentSubGame == this)
        {
            snake.Draw(offset);
        }

        for (int i = 0; i < food.Count; i++)
        {
            food[i].Draw(offset);
        }
    }

    public void Update()
    {
        if (!paused)
        {
            int pressedKey = Raylib.GetKeyPressed();
            if (pressedKey != 0 && !paused)
            {
                KeyboardKey key = (KeyboardKey)pressedKey;

                if (keybinds.TryGetValue(key, out Action? action))
                {
                    action.Invoke();
                }
            }

            snake.Update();

            var successful = true;
            while (food.Count < Settings.appleCount && successful)
            {
                successful = SpawnFood();
                if (successful)
                {
                    break;
                }
            }
        }
        //if (Game.currentSubGame != this) return;

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

    void Exit(bool won)
    {
        SubGame current = this;

        Game.subGames.Remove(current);
        Game.lastSubGameWon = won;

        Game.ChangeSubGameTo(current.parent);
    }

    public void Win()
    {
        Exit(true);
    }

    public void Die()
    {
        Exit(false);
    }

    public bool SpawnFood()
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

        if (spawnablePos.Count == 0)
        {
            Win();
            return false;
        }

        for (int i = 0; i < food.Count; i++)
        {
            spawnablePos.Remove(food[i].GetPos());
        }
        
        if (spawnablePos.Count > 0)
        {
            var newFood = new Food(spawnablePos[Settings.rng.Next(0, spawnablePos.Count)]);
            food.Add(newFood);
        } 
        else
        {
            return false;
        }

        return true;
    }

    public void Grow()
    {
        snake.Grow();
    }

    public void SetPos(Vector2 newPos)
    {
        offset = newPos;
    }

    public Vector2 GetPos() => offset;
    public Map GetMap() => map;
    public List<Food> GetFood() => food;
    public bool MapOccupied(Vector2 pos) => GetMap().OccupiedAt(pos);
}
