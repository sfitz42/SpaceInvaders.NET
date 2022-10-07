using SpaceInvaders.Core;
using SpaceInvaders.Core.Ports.Input;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

            InitializeComponent();
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            /* All elements will default to 0 (black) */
            var pixels = new byte[ArcadeMachine.ScreenWidth * ArcadeMachine.ScreenHeight / 8];

            DrawPixels(pixels);

            _timer.Start();
            _arcadeMachine.Run();
        }
    }
}
