using SpaceInvaders.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceInvaders.WPF
{
    /// <summary>
    /// Graphics update logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
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
