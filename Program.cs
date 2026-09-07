using Raylib_cs;
using System.Numerics;

namespace Snake_2;

internal static class Program
{
    [System.STAThread]
    public static void Main()
    {
        Raylib.SetConfigFlags(
            ConfigFlags.ResizableWindow |
            ConfigFlags.VSyncHint
        );

        Raylib.InitWindow(
            Settings.screenWidth,
            Settings.screenHeight,
            "Snake^2"
        );

        RenderTexture2D target = Raylib.LoadRenderTexture(
            Settings.gameScreenWidth,
            Settings.gameScreenHeight
        );

        Raylib.SetTextureFilter(
            target.Texture,
            TextureFilter.Point
        );

        Raylib.SetTargetFPS(-1);

        Camera2D camera = new Camera2D
        {
            Target = Settings.cameraPosition,

            Offset = new Vector2(
                Settings.gameScreenWidth / 2f,
                Settings.gameScreenHeight / 2f
            ),

            Zoom = Settings.cameraZoom
        };

        while (!Raylib.WindowShouldClose())
        {
            // ---------- update ----------

            Mouse.Update();

            // ---------- end update ----------

            Raylib.BeginTextureMode(target);
            Raylib.BeginMode2D(camera);
            Raylib.ClearBackground(Settings.bgColor);

            // ---------- draw ----------

            Raylib.DrawRectangle((int)(Settings.gameScreenWidth / 2f) - 32, (int)(Settings.gameScreenHeight / 2f) - 24, 64, 48, Settings.borderColor);

            // ---------- end draw ----------

            Raylib.EndMode2D();
            Raylib.EndTextureMode();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            float scale = Settings.GetScale();

            Raylib.DrawTexturePro(
                target.Texture,

                new Rectangle(
                    0.0f,
                    0.0f,
                    target.Texture.Width,
                    -target.Texture.Height
                ),

                new Rectangle(
                    (Raylib.GetScreenWidth() -
                        Settings.gameScreenWidth * scale) * 0.5f,

                    (Raylib.GetScreenHeight() -
                        Settings.gameScreenHeight * scale) * 0.5f,

                    Settings.gameScreenWidth * scale,
                    Settings.gameScreenHeight * scale
                ),

                new Vector2(0, 0),
                0.0f,
                Color.White
            );

            Raylib.EndDrawing();
        }

        Raylib.UnloadRenderTexture(target);
        Raylib.CloseWindow();
    }
}
