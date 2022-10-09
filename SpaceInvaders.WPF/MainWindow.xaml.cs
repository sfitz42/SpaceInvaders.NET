using SpaceInvaders.Core;
using System;
using System.Windows;
using System.Windows.Threading;

namespace SpaceInvaders.WPF
{
    /// <summary>
    /// Interaction \ Initialisation logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double ScreenRefreshRate = 16.666667;

        private readonly ArcadeMachine _arcadeMachine = new();
        private readonly DispatcherTimer _timer;

        public MainWindow()
        {
            _timer = new()
            {
                Interval = TimeSpan.FromMilliseconds(ScreenRefreshRate)
            };

            _timer.Tick += UpdateScreen;

            Loaded += OnLoad;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            LoadSounds();

            InitializeComponent();
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            /* All elements will default to 0 (black) */
            var pixels = new int[ArcadeMachine.ScreenWidth * ArcadeMachine.ScreenHeight];

            DrawPixels(pixels);

            _timer.Start();
            _arcadeMachine.Run();
        }
    }
}
