using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Snake_2;

internal class Food
{
    public Tile? tile;
    public SubGame? subGame;

    public bool eaten = false;
    public bool queuedDeletion = false;

    float zoomTimer = 0;
    bool zoomActive = false;
    Vector2 targetOffset = Vector2.Zero;

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
            targetOffset = pos * mapScale;
            subGame = new(targetOffset, Game.currentSubGame);
            subGame.GetMap().SetPos(Vector2.Zero);

            targetOffset = targetOffset-(Settings.tileSize * Settings.mapSize)/2;

            targetOffset -= new Vector2(
                (Settings.gameScreenWidth / Settings.tileSize.X/2) - Settings.tileSize.X / 2,
                (Settings.gameScreenHeight / Settings.tileSize.X/2) - Settings.tileSize.X / 2
            );

            Game.subGames.Add(subGame);
        }
    }

    public void Update()
    {
        if (zoomActive)
        {
            zoomTimer += Raylib.GetFrameTime();

            Program.camera.Offset = Vector2.Lerp(Program.camera.Offset, -targetOffset*Settings.tileSize, zoomTimer / Settings.zoomTransition);
            Program.camera.Zoom = float.Lerp(Program.camera.Zoom, Settings.tileSize.X, zoomTimer / Settings.zoomTransition);

            if (zoomTimer > Settings.zoomTransition / 1000)
            {
                Game.currentSubGame.cameraOffset = Program.camera.Offset;
                Game.currentSubGame.cameraZoom = Program.camera.Zoom;

                Program.camera.Zoom = Settings.defaultZoom;
                Program.camera.Offset = Settings.cameraPosition;

                EnterSubGame();
            }
        }
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
            Game.currentSubGame.paused = true;
            zoomActive = true;
        }

        eaten = true;
    }

    void EnterSubGame()
    {
        Program.flashTimer = Settings.flashTime;
        Game.lastEntered = true;
        queuedDeletion = true;
        Game.ChangeSubGameTo(subGame);
        Game.Center();
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
