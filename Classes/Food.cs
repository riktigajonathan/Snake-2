using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Snake_2;

internal class Food
{
    public Tile? tile;
    public SubGame? subGame;

    public bool eaten = false;
    public bool queuedDeletion = false;

    public Food(Vector2 pos)
    {
        if (Game.currentSubGame == null) return;
        Map map = Game.currentSubGame.GetMap();

        if (Game.currentSubGame.depth >= Settings.gameDepth) 
        { 
            this.tile = new Tile(pos * map.GetScale(), map.GetScale(), Settings.foodColor); 
        } 
        else
        {
            SubGame foodSubGame = new(pos, Game.currentSubGame); // temp pos

            Game.subGames.Add(foodSubGame);
        }
    }

    public void Update()
    {
        // animations
    }

    public void Draw(Vector2 offset)
    {
        if (tile != null)
        {
            tile.Draw(offset);
        }
    }

    public void Eaten()
    {
        if (eaten) return;

        if (tile != null)
        {
            queuedDeletion = true;
            if (Game.currentSubGame != null)
                Game.currentSubGame.Grow();
        }
        else if (subGame != null)
        {
            queuedDeletion = true;
            Game.ChangeSubGameTo(subGame);
        }

        eaten = true;
    }

    public Vector2 GetPos()
    {
        if (tile != null)
        {
            return tile.pos / tile.size;
        }

        return Vector2.One * -1;
    }
}
