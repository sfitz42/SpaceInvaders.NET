using System.Collections.Generic;
using SpaceInvaders.Core.Ports.Input;

namespace SpaceInvaders.Web;

internal class InputMappings
{
    public static readonly Dictionary<int, InputPort0.Input> InputPort0Mapping = new()
    {
        { 0x25, InputPort0.Input.Left },
        { 0x27, InputPort0.Input.Right },
        { 0x20, InputPort0.Input.Fire }
    };

    public static readonly Dictionary<int, InputPort1.Input> InputPort1Mapping = new()
    {
        { 0x43, InputPort1.Input.Credit },
        { 0x0D, InputPort1.Input.OnePlayerStart },
        { 0x08, InputPort1.Input.TwoPlayerStart },
        { 0x25, InputPort1.Input.PlayerOneLeft },
        { 0x27, InputPort1.Input.PlayerOneRight },
        { 0x20, InputPort1.Input.PlayerOneFire }
    };

    public static readonly Dictionary<int, InputPort2.Input> InputPort2Mapping = new()
    {
        { 0x25, InputPort2.Input.PlayerTwoLeft },
        { 0x27, InputPort2.Input.PlayerTwoRight },
        { 0x20, InputPort2.Input.PlayerTwoFire }
    };
}
