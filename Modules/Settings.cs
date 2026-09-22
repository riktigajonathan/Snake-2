using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Snake_2;

internal class Settings
{
    public const int screenWidth = 1280;
    public const int screenHeight = 720;
    public static int gameScreenWidth = 256;
    public static int gameScreenHeight = 144;
    
    public static Vector2 cameraPosition = new Vector2(gameScreenWidth/2, gameScreenHeight/2);
    public static float defaultZoom = 1f;
    public static float zoomTransition = 1400;

    public static float GetScale()
    {
        return Math.Min(
            (float)Raylib.GetScreenWidth() / gameScreenWidth,
            (float)Raylib.GetScreenHeight() / gameScreenHeight
        );
    }
    public static Vector2 GetCenter()
    {
        return new Vector2(
            gameScreenWidth / 2f,
            gameScreenHeight / 2f
        );
    }

    public static Color bgColor = new(124, 144, 255);
    public static Color borderColor = new(255, 166, 124);
    public static Color primaryMapColor = new(178, 255, 168);
    public static Color secondaryMapColor = new(172, 239, 153);
    public static Color snakeColor = new(178, 100, 237);
    public static Color foodColor = new(255, 148, 142);

    public static Vector2 mapSize = new Vector2(10, 10);
    public static Vector2 tileSize = new Vector2(10, 10);

    public static float moveDelay = 0.01f; // 0.1f;
    public static float moveTransition = 0; // 0.11f;

    public static int startLength = 3;
    public static int appleCount = 3;
    public static int gameDepth = 100;

    public static readonly Random rng = new Random();

    public static bool autoplay = true;
    public static Vector2[] autoplayQueue = [
        new(1,0), new(1,0), new(1,0), new(1,0), new(1,0),
        new(1,0), new(1,0), new(1,0), new(1,0),

        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1), new(0,1), new(0,1), new(0,1),

        new(-1,0),

        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1), new(0,-1), new(0,-1),

        new(-1,0),

        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1), new(0,1), new(0,1),

        new(-1,0),

        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1), new(0,-1), new(0,-1),

        new(-1,0),

        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1), new(0,1), new(0,1),

        new(-1,0),

        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1), new(0,-1), new(0,-1),

        new(-1,0),

        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1), new(0,1), new(0,1),

        new(-1,0),

        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1), new(0,-1), new(0,-1),

        new(-1,0),

        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1), new(0,1), new(0,1), 

        new(-1,0),

        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1), new(0,-1), new(0,-1), new(0,-1)
    ];
}
