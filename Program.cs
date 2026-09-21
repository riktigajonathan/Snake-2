using Raylib_cs;
using System.Numerics;

namespace Snake_2;

internal static class Program
{
    [System.STAThread]
    public static void Main()
    {
        Raylib.SetConfigFlags(
            ConfigFlags.ResizableWindow
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

        Camera2D camera = new Camera2D
        {
            Target = Settings.cameraPosition,

            Offset = new Vector2(
                Settings.gameScreenWidth / 2f,
                Settings.gameScreenHeight / 2f
            ),

            Zoom = Settings.cameraZoom
        };

        Vector2 center = Settings.GetCenter();
        var gamePos = new Vector2(
            center.X - (Settings.mapSize.X * Settings.tileSize.X) / 2f,
            center.Y - (Settings.mapSize.Y * Settings.tileSize.Y) / 2f
        );
        Game.AddSubGame(gamePos);

        while (!Raylib.WindowShouldClose())
        {
            // ---------- update ----------

            Mouse.Update();
            Game.Update();

            // ---------- end update ----------

            Raylib.BeginTextureMode(target);
            Raylib.BeginMode2D(camera);
            Raylib.ClearBackground(Settings.bgColor);

            // ---------- draw ----------

            Game.Draw();
            Raylib.DrawText(
                $"FPS: {Raylib.GetFPS()}",
                0,
                0,
                10,
                Color.White
            );

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
