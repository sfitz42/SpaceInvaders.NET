using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using SpaceInvaders.Core;

namespace SpaceInvaders.OpenTK;

public class SpaceInvaders
{
    private const int DisplayWidth = ArcadeMachine.ScreenHeight * 2;
    private const int DisplayHeight = ArcadeMachine.ScreenWidth * 2;

    private readonly ArcadeMachine _arcadeMachine = new();
    private readonly Thread _machineThread;

    public SpaceInvaders()
    {
        var soundControler = new SoundController(_arcadeMachine);

        _machineThread = new Thread(() => _arcadeMachine.Run());
        _machineThread.Start();

        var nativeWindowSettings = new NativeWindowSettings()
        {
            Size = new Vector2i(DisplayWidth, DisplayHeight),
            Title = "SpaceInvaders.NET",
            WindowBorder = WindowBorder.Fixed,
            Flags = ContextFlags.ForwardCompatible
        };

        using Game game = new(_arcadeMachine, GameWindowSettings.Default, nativeWindowSettings);
        game.Run();
    }
}
