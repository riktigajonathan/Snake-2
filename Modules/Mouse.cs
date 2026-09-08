using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Snake_2;

internal class Mouse
{
    public static Vector2 Pos = new Vector2(0, 0);

    public static void Update()
    {
        float scale = Math.Min((float)Raylib.GetScreenWidth() / Settings.gameScreenWidth, (float)Raylib.GetScreenHeight() / Settings.gameScreenHeight);
        
        Vector2 mouse = Raylib.GetMousePosition();
        Vector2 virtualMouse = Vector2.Zero;
        virtualMouse.X = (mouse.X - (Raylib.GetScreenWidth() - (Settings.gameScreenWidth * scale)) * 0.5f) / scale;
        virtualMouse.Y = (mouse.Y - (Raylib.GetScreenHeight() - (Settings.gameScreenHeight * scale)) * 0.5f) / scale;
        virtualMouse = Vector2.Clamp(virtualMouse, Vector2.Zero, new Vector2((float)Settings.gameScreenWidth, (float)Settings.gameScreenHeight));
        
        Pos = virtualMouse;
    }
}
