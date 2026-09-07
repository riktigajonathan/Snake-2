using Raylib_cs;
using System.Numerics;

namespace Snake_2;

internal static class Program
{
    [System.STAThread]
    public static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.VSyncHint);
        Raylib.InitWindow(Settings.screenWidth, Settings.screenHeight, "Snake^2");

        RenderTexture2D target = Raylib.LoadRenderTexture(Settings.gameScreenWidth, Settings.gameScreenHeight);
        Raylib.SetTextureFilter(target.Texture, TextureFilter.Point);

        Raylib.SetTargetFPS(-1);

        while (!Raylib.WindowShouldClose())
        {
            // Update
            //----------------------------------------------------------------------------------
            // Compute required framebuffer scaling

            Mouse.Update();
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            // Draw everything in the render texture, note this will not be rendered on screen, yet
            Raylib.BeginTextureMode(target);
                Raylib.ClearBackground(Color.White);  // Clear render texture background color
            Raylib.EndTextureMode();

            Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black); // Clear screen background

            float scale = Settings.GetScale();
                // Draw render texture to screen, properly scaled
                Raylib.DrawTexturePro(target.Texture, new Rectangle(0.0f, 0.0f, (float)target.Texture.Width, (float)-target.Texture.Height ),
                               new Rectangle(
                    (Raylib.GetScreenWidth() - ((float)Settings.gameScreenWidth * scale)) * 0.5f, (Raylib.GetScreenHeight() - ((float)Settings.gameScreenHeight * scale)) * 0.5f,
                               (float)Settings.gameScreenWidth * scale, (float)Settings.gameScreenHeight * scale ), new Vector2(0, 0), 0.0f, Color.White);
            Raylib.EndDrawing();
            //--------------------------------------------------------------------------------------
        }
        Raylib.UnloadRenderTexture(target);

        Raylib.CloseWindow();
    }
}