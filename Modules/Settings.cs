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

    public static Color bgColor = new(124, 144, 255);
    public static Color borderColor = new(255, 166, 124);
    public static Color primaryMapColor = new(178, 255, 168);
    public static Color secondaryMapColor = new(172, 239, 153);
    public static Color snakeColor = new(255, 148, 142);

    public static Vector2 mapSize = new Vector2(10, 9);
    public static Vector2 tileSize = new Vector2(10, 10);
}
