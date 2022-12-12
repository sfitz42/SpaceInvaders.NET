using SpaceInvaders.Core;
using System.Windows;

namespace SpaceInvaders.WPF
{
    /// <summary>
    /// Interaction \ Initialisation logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int DisplayScale = 2;
        private const int DisplayWidth = ArcadeMachine.ScreenHeight * DisplayScale;
        private const int DisplayHeight = ArcadeMachine.ScreenWidth * DisplayScale;

        private readonly ArcadeMachine _arcadeMachine = new();

        public MainWindow()
        {
            _arcadeMachine.DisplayUpdated += UpdateScreen;

            Loaded += OnLoad;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            LoadSounds();

            InitializeComponent();
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            InvadersDisplay.Width = DisplayWidth;
            InvadersDisplay.Height = DisplayHeight;
            MainView.Width = DisplayWidth;
            MainView.Height = DisplayHeight;

            /* All elements will default to 0 (black) */
            var pixels = new int[ArcadeMachine.ScreenWidth * ArcadeMachine.ScreenHeight];

            DrawPixels(pixels);

            _arcadeMachine.Run();
        }
    }
}
