using OpenTK.Graphics.OpenGL4;
using SpaceInvaders.Core;

namespace SpaceInvaders.OpenTK;

 public class DisplayTexture
{
    public int Handle { get; }

    private readonly int[,] _pixelData = new int[ArcadeMachine.ScreenWidth, ArcadeMachine.ScreenHeight];

    public DisplayTexture()
    {
        Handle = GL.GenTexture();

        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, Handle);

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, ArcadeMachine.ScreenHeight, ArcadeMachine.ScreenWidth, 0, PixelFormat.Rgba, PixelType.UnsignedByte, _pixelData);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
    }

    public void UpdateDisplay(byte[] vram)
    {
        var totalPixels = ArcadeMachine.ScreenHeight * ArcadeMachine.ScreenWidth / 8;

        for (int i = 0; i < totalPixels; i++)
        {
            var currByte = vram[i];

            var x = i * 8 % ArcadeMachine.ScreenWidth;
            var y = i * 8 / ArcadeMachine.ScreenWidth;

            for (int j = 0; j < 8; j++)
            {
                var pixelSet = (currByte & (0x01 << j)) != 0;

                var rgb = 0x000000;

                if (pixelSet)
                {
                    // Set default pixel colour (white)
                    rgb = 0xFFFFFF;

                    // Set pixel colour to red
                    if (x >= 192 && x < 224)
                        rgb = 0x0000FF;

                    // Set pixel colour to green
                    if ((x >= 16 && x <= 72) || (x <= 16 && y >= 16 && y <= 134))
                        rgb = 0x00FF00;
                }

                _pixelData[x + j, y] = rgb;
            }
        }

        GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, ArcadeMachine.ScreenHeight, ArcadeMachine.ScreenWidth, PixelFormat.Rgba, PixelType.UnsignedByte, _pixelData);
    }

    public void Use()
    {
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }
}