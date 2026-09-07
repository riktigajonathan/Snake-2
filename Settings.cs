using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_2;

internal class Settings
{
    public const int screenWidth = 1280;
    public const int screenHeight = 720;
    public static int gameScreenWidth = 256;
    public static int gameScreenHeight = 144;

    public static float GetScale()
    {
        return Math.Min((float)Raylib.GetScreenWidth() / Settings.gameScreenWidth, (float)Raylib.GetScreenHeight() / Settings.gameScreenHeight);
    }
}
