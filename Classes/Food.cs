using System;
using System.Collections.Generic;
using System.Drawing;
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
        Vector2 mapScale = Game.currentSubGame.GetMap().GetScale();

        if (Game.currentSubGame.depth >= Settings.gameDepth) 
        { 
            this.tile = new Tile(pos * mapScale, mapScale, Settings.foodColor); 
        } 
        else
        {
            subGame = new(pos * mapScale, Game.currentSubGame);
            subGame.GetMap().SetPos(Vector2.Zero);

            Game.subGames.Add(subGame);
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
        else if (subGame != null)
        {
            Map parentMap = Game.currentSubGame.GetMap();
            Map subMap = subGame.GetMap();

            Vector2 parentTileSize = parentMap.GetScale();
            Vector2 subMapTotalSize = subMap.size * subMap.GetScale();

            Tile.globalScale = parentTileSize / subMapTotalSize;
            Tile.globalOffset = subGame.GetPos() + offset;

            subGame.Draw();

            Tile.globalScale = Vector2.One;
            Tile.globalOffset = Vector2.Zero;
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
            Game.Center();
        }

        eaten = true;
    }

    public Vector2 GetPos()
    {
        if (tile != null)
        {
            return tile.pos / tile.size;
        }
        else if (subGame != null)
        {
            return subGame.GetPos() / subGame.GetMap().GetScale();
        }

        return Vector2.One * -1;
    }
}
