using SpaceInvaders.Core;
using SpaceInvaders.Web;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

public partial class Program
{
    private static readonly ArcadeMachine _machine = new();

    public static async Task Main()
    {
        if (!OperatingSystem.IsBrowser())
        {
            throw new PlatformNotSupportedException("This demo is expected to run on browser platform");
        }

        await JSHost.ImportAsync("webgl", "./webgl.js");

        Console.WriteLine("Ready!");
    }

    [JSExport]
    internal static void LoadRom(string romName, byte[] data)
    {
        Enum.TryParse(romName, true, out RomBank bank);

        _machine.Memory.LoadRomFromArray(bank, data);

        Console.WriteLine($"ROM: {bank} Length: {data.Length}");
    }

    [JSExport]
    internal static void StartGame()
    {
        _machine.DisplayUpdated += TriggerUpdate;
        _machine.Run();
    }

    [JSExport]
    internal static void SetKey(int key, bool pressed)
    {
        var port0Mapping = InputMappings.InputPort0Mapping;
        var port1Mapping = InputMappings.InputPort1Mapping;
        var port2Mapping = InputMappings.InputPort2Mapping;

        if (port0Mapping.ContainsKey(key))
            _machine.InputPort0.HandleInput(port0Mapping[key], pressed);

        if (port1Mapping.ContainsKey(key))
            _machine.InputPort1.HandleInput(port1Mapping[key], pressed);

        if (port2Mapping.ContainsKey(key))
            _machine.InputPort2.HandleInput(port2Mapping[key], pressed);
    }

    [JSImport("draw", "webgl")]
    internal static partial void _draw(byte[] vram);

    private static void TriggerUpdate(object sender, EventArgs e) => _draw(_machine.Memory.ReadVRAM());
}
