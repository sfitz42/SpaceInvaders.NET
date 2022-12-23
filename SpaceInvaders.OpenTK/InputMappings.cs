using OpenTK.Windowing.GraphicsLibraryFramework;
using SpaceInvaders.Core.Ports.Input;

namespace SpaceInvaders.OpenTK;

public class InputMappings
{
    public static readonly Dictionary<Keys, InputPort0.Input> InputPort0Mapping = new()
    {
        { Keys.Left,  InputPort0.Input.Left },
        { Keys.Right, InputPort0.Input.Right },
        { Keys.Space, InputPort0.Input.Fire }
    };

    public static readonly Dictionary<Keys, InputPort1.Input> InputPort1Mapping = new()
    {
        { Keys.C,     InputPort1.Input.Credit },
        { Keys.Enter, InputPort1.Input.OnePlayerStart },
        { Keys.Backspace,  InputPort1.Input.TwoPlayerStart },
        { Keys.Left,  InputPort1.Input.PlayerOneLeft },
        { Keys.Right, InputPort1.Input.PlayerOneRight },
        { Keys.Space, InputPort1.Input.PlayerOneFire }
    };

    public static readonly Dictionary<Keys, InputPort2.Input> InputPort2Mapping = new()
    {
        { Keys.Left,  InputPort2.Input.PlayerTwoLeft },
        { Keys.Right, InputPort2.Input.PlayerTwoRight },
        { Keys.Space, InputPort2.Input.PlayerTwoFire }
    };
}
