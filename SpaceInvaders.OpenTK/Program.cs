using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using SpaceInvaders.Core;

namespace SpaceInvaders.OpenTK
{
    class Program
    {
        private const int DisplayWidth = ArcadeMachine.ScreenHeight;
        private const int DisplayHeight = ArcadeMachine.ScreenWidth;

        static void Main()
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(DisplayWidth, DisplayHeight),
                Title = "SpaceInvaders.NET",
                WindowBorder = WindowBorder.Fixed,
                Flags = ContextFlags.ForwardCompatible
            };

            using (Game game = new(GameWindowSettings.Default, nativeWindowSettings))
            {
                game.Run();
            }
        }
    }
}