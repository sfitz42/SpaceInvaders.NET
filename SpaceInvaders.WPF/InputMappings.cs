using SpaceInvaders.Core.Ports.Input;
using System.Collections.Generic;
using System.Windows.Input;

namespace SpaceInvaders.WPF
{
    internal class InputMappings
    {
        public static readonly Dictionary<Key, InputPort0.Input> InputPort0Mapping = new()
        {
            { Key.Left,  InputPort0.Input.Left },
            { Key.Right, InputPort0.Input.Right },
            { Key.Space, InputPort0.Input.Fire }
        };

        public static readonly Dictionary<Key, InputPort1.Input> InputPort1Mapping = new()
        {
            { Key.C,     InputPort1.Input.Credit },
            { Key.Enter, InputPort1.Input.OnePlayerStart },
            { Key.Back,  InputPort1.Input.TwoPlayerStart },
            { Key.Left,  InputPort1.Input.PlayerOneLeft },
            { Key.Right, InputPort1.Input.PlayerOneRight },
            { Key.Space, InputPort1.Input.PlayerOneFire }
        };

        public static readonly Dictionary<Key, InputPort2.Input> InputPort2Mapping = new()
        {
            { Key.Left,  InputPort2.Input.PlayerTwoLeft },
            { Key.Right, InputPort2.Input.PlayerTwoRight },
            { Key.Space, InputPort2.Input.PlayerTwoFire }
        };
    }
}
