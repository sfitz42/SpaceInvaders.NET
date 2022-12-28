using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using SpaceInvaders.Core;

namespace SpaceInvaders.OpenTK;

public class SpaceInvaders
{
    private readonly ArcadeMachine _arcadeMachine = new();
    private readonly Thread _machineThread;

    private readonly int _displayWidth = ArcadeMachine.ScreenHeight;
    private readonly int _displayHeight = ArcadeMachine.ScreenWidth;

    public SpaceInvaders()
    {
        var soundControler = new SoundController(_arcadeMachine);

        _machineThread = new Thread(() => _arcadeMachine.Run());
    }

    public SpaceInvaders(int displayScale) : this()
    {
        _displayHeight *= displayScale;
        _displayWidth *= displayScale;
    }

    public void Run()
    {
        _machineThread.Start();

        var nativeWindowSettings = new NativeWindowSettings()
        {
            Size = new Vector2i(_displayWidth, _displayHeight),
            Title = "SpaceInvaders.NET",
            WindowBorder = WindowBorder.Fixed,
            Flags = ContextFlags.ForwardCompatible
        };

        using Game game = new(_arcadeMachine, GameWindowSettings.Default, nativeWindowSettings);
        game.Run();
    }
}
