using Intel8080.Emulator.IO;
using SpaceInvaders.Core.Ports.Input;

namespace SpaceInvaders.Core;

public class InputDevice
{
    public InputPort0 InputPort0 { get; } = new();

    public InputPort1 InputPort1 { get; } = new();
    
    public InputPort2 InputPort2 { get; } = new();
}