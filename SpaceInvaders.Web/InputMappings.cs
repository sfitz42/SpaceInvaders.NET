using System.Collections.Generic;
using SpaceInvaders.Core.Ports.Input;

namespace SpaceInvaders.Web;

internal class InputMappings
{
    public static readonly Dictionary<string, InputPort0.Input> InputPort0Mapping = new()
    {
        { "Left", InputPort0.Input.Left },
        { "Right", InputPort0.Input.Right },
        { "Fire", InputPort0.Input.Fire }
    };

    public static readonly Dictionary<string, InputPort1.Input> InputPort1Mapping = new()
    {
        { "Credit", InputPort1.Input.Credit },
        { "1UP", InputPort1.Input.OnePlayerStart },
        { "2UP", InputPort1.Input.TwoPlayerStart },
        { "Left", InputPort1.Input.PlayerOneLeft },
        { "Right", InputPort1.Input.PlayerOneRight },
        { "Fire", InputPort1.Input.PlayerOneFire }
    };

    public static readonly Dictionary<string, InputPort2.Input> InputPort2Mapping = new()
    {
        { "Left", InputPort2.Input.PlayerTwoLeft },
        { "Right", InputPort2.Input.PlayerTwoRight },
        { "Fire", InputPort2.Input.PlayerTwoFire }
    };
}
