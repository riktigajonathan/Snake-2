using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Snake_2;

internal class Food
{
    public SubGame subGame;
    public Tile tile;

    public Food(Vector2 pos)
    {
        Map map = Game.currentSubGame.GetMap();

        this.tile = new Tile(pos * Settings.tileSize, map.GetScale(), Settings.foodColor);
    }

    public void Update()
    {
        if (subGame != null)
        {
            subGame.Update();
        }
    }

    public void Draw(Vector2 offset)
    {
        if (subGame != null)
        {
            
            subGame.Draw();
        }
        else if (tile != null)
        {
            tile.Draw(offset);
        }
    }
}
