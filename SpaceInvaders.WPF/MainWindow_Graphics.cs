using SpaceInvaders.Core;
using System;
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

            var pixels = new int[ArcadeMachine.ScreenHeight * ArcadeMachine.ScreenWidth];

            var totalPixels = ArcadeMachine.ScreenHeight * ArcadeMachine.ScreenWidth / 8;

            for (int i = 0; i < totalPixels; i++)
            {
                var currByte = vram[i];

                var x = i * 8 % ArcadeMachine.ScreenWidth;
                var y = i * 8 / ArcadeMachine.ScreenWidth;

                for (int j = 0; j < 8; j++)
                {
                    var pixelSet = (currByte & (0x01 << j)) != 0;

                    if (pixelSet)
                    {
                        // Set default pixel colour (white)
                        var rgb = 0xFFFFFF;

                        // Set pixel colour to red
                        if (x >= 192 && x < 224)
                            rgb = 0xFF0000;

                        // Set pixel colour to green
                        if ((x >= 16 && x <= 72) || (x <= 16 && y >= 16 && y <= 134))
                            rgb = 0x00FF00;

                        pixels[(i * 8) + j] = rgb;
                    }
                }
            }

            DrawPixels(pixels);
        }

        private void DrawPixels(int[] pixels)
        {
            var bmp = BitmapSource.Create(
                ArcadeMachine.ScreenWidth,
                ArcadeMachine.ScreenHeight,
                0,
                0,
                PixelFormats.Bgr32,
                null,
                pixels,
                ArcadeMachine.ScreenWidth * 4
            );

            var rotated = new TransformedBitmap(bmp, new RotateTransform(-90));
            var resized = new TransformedBitmap(rotated, new ScaleTransform(DisplayScale, DisplayScale));

            MainView.Source = resized;
        }
    }
}
