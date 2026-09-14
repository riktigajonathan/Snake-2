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
    
    public static float cameraZoom = 1.0f;
    public static Vector2 cameraPosition = new Vector2(128, 72);

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
            Settings.gameScreenWidth / 2f,
            Settings.gameScreenHeight / 2f
        );
    }

    public static Color bgColor = new(124, 144, 255);
    public static Color borderColor = new(255, 166, 124);
    public static Color primaryMapColor = new(178, 255, 168);
    public static Color secondaryMapColor = new(172, 239, 153);
    public static Color snakeColor = new(178, 100, 237);

    public static Vector2 mapSize = new Vector2(10, 9);
    public static Vector2 tileSize = new Vector2(10, 10);

    public static float moveDelay = 0.01f;
    public static float moveTransition = 0f;
    public static int startLength = 5;

    public static bool autoplay = true;
    public static int autoplayIndex = 0;
    public static Vector2[] autoplayQueue =
    [
        // (0,0) -> (9,0)
        new(1,0), new(1,0), new(1,0), new(1,0), new(1,0),
        new(1,0), new(1,0), new(1,0), new(1,0),

        // (9,0) -> (9,8)
        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1), new(0,1), new(0,1),

        // (9,8) -> (8,8)
        new(-1,0),

        // Column 8: row 8 -> row 1
        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1), new(0,-1),

        // Row 1: col 8 -> col 7
        new(-1,0),

        // Column 7: row 1 -> row 7
        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1),

        // Row 7: col 7 -> col 6
        new(-1,0),

        // Column 6: row 7 -> row 1
        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1),

        // Row 1: col 6 -> col 5
        new(-1,0),

        // Column 5: row 1 -> row 7
        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1),

        // Row 7: col 5 -> col 4
        new(-1,0),

        // Column 4: row 7 -> row 1
        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1),

        // Row 1: col 4 -> col 3
        new(-1,0),

        // Column 3: row 1 -> row 7
        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1),

        // Row 7: col 3 -> col 2
        new(-1,0),

        // Column 2: row 7 -> row 1
        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1),

        // Row 1: col 2 -> col 1
        new(-1,0),

        // Column 1: row 1 -> row 8
        new(0,1), new(0,1), new(0,1), new(0,1),
        new(0,1), new(0,1), new(0,1),

        // (1,8) -> (0,8)
        new(-1,0),

        // (0,8) -> (0,0), closing the loop
        new(0,-1), new(0,-1), new(0,-1), new(0,-1),
        new(0,-1), new(0,-1), new(0,-1), new(0,-1)
    ];
}
