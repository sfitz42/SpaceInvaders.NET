namespace SpaceInvaders.OpenTK;

internal class Program
{
    static void Main(string[] args)
    {
        var displayScale = 1;

        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            if (arg == "--displayScale" || arg == "-s")
            {
                if (!int.TryParse(args[++i], out displayScale))
                    throw new ArgumentException("displayScale", "displayScale not valid");
            }
        }

        if (displayScale < 1)
            throw new ArgumentOutOfRangeException("displayScale", displayScale, "Display scale cannot be less than 1X");

        var app = new SpaceInvaders(displayScale);

        app.Run();
    }
}
