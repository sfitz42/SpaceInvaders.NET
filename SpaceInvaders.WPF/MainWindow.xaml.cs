using SpaceInvaders.Core;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SpaceInvaders.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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

            InitializeComponent();

            Loaded += OnLoad;
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            /* All elements will default to 0 (black) */
            var pixels = new byte[ArcadeMachine.ScreenWidth * ArcadeMachine.ScreenHeight / 8];

            DrawPixels(pixels);

            _timer.Start();
            _arcadeMachine.Run();
        }

        public void UpdateScreen(object? sender, EventArgs e)
        {
            var vram = _arcadeMachine.Memory.ReadVRAM();

            Array.Reverse(vram);

            DrawPixels(vram);
        }

        private void DrawPixels(byte[] pixels)
        {
            var bmp = BitmapSource.Create(
                ArcadeMachine.ScreenWidth,
                ArcadeMachine.ScreenHeight,
                0,
                0,
                PixelFormats.BlackWhite,
                null,
                pixels,
                ArcadeMachine.ScreenWidth / 8
            );

            var rotated = new TransformedBitmap(bmp, new RotateTransform(90));

            MainView.Source = rotated;
        }
    }
}
