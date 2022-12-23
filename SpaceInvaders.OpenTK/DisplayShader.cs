using OpenTK.Graphics.OpenGL4;

namespace SpaceInvaders.OpenTK;

public class DisplayShader
{
    private const string VertexShaderSource = @"
        #version 330 core

        layout(location = 0) in vec3 aPosition;
        layout(location = 1) in vec2 aTexCoord;

        out vec2 texCoord;

        void main(void)
        {   
            texCoord = aTexCoord;

            gl_Position = vec4(aPosition, 1.0);
        }
    ";

    private const string FragShaderSource = @"
        #version 330

        out vec4 outputColor;
        in vec2 texCoord;

        uniform sampler2D texture0;

        void main()
        {
            outputColor = texture(texture0, texCoord);
        }
    ";

    private readonly int _handle = GL.CreateProgram();

    public DisplayShader()
    {
        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, VertexShaderSource);

        var fragShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragShader, FragShaderSource);

        CompileShader(vertexShader);
        CompileShader(fragShader);

        GL.LinkProgram(_handle);

        GL.DetachShader(_handle, vertexShader);
        GL.DetachShader(_handle, fragShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragShader);
    }

    public void Use()
    {
        GL.UseProgram(_handle);
    }

    private void CompileShader(int shader)
    {
        GL.CompileShader(shader);
        GL.AttachShader(_handle, shader);
    }
}
