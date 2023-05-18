using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SpaceInvaders.Core;

namespace SpaceInvaders.OpenTK
{
    public class Game : GameWindow
    {
        private readonly ArcadeMachine _arcadeMachine;

        private int _vertexBuffer;
        private int _vertexArray;
        private int _elementBuffer;

        private DisplayShader _shader = null!;
        private DisplayTexture _texture = null!;

        private readonly float[] _displayVertices =
        {
            1.0f, 1.0f, 0.0f, 1.0f, 1.0f,
            1.0f, -1.0f, 0.0f, 1.0f, 0.0f,
            -1.0f, -1.0f, 0.0f, 0.0f, 0.0f,
            -1.0f, 1.0f, 0.0f, 0.0f, 1.0f
        };

        private readonly uint[] _vertIndices = new uint[]
        {
            0, 1, 3,
            1, 2, 3
        };

        public Game(ArcadeMachine arcadeMachine, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            _arcadeMachine = arcadeMachine;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            CheckKeys();

            base.OnUpdateFrame(args);
        }

        private void CheckKeys()
        {
            var state = KeyboardState;

            if (state.IsKeyDown(Keys.Escape))
                Close();

            var inputDevice = _arcadeMachine.InputDevice;

            foreach (Keys key in InputMappings.InputPort0Mapping.Keys)
            {
                var port = inputDevice.InputPort0;
                var value = InputMappings.InputPort0Mapping[key];

                if (state.IsKeyDown(key))
                    port.HandleInput(value, true);
                else if (state.IsKeyReleased(key))
                    port.HandleInput(value, false);
            }

            foreach (Keys key in InputMappings.InputPort1Mapping.Keys)
            {
                var port = inputDevice.InputPort1;
                var value = InputMappings.InputPort1Mapping[key];

                if (state.IsKeyDown(key))
                    port.HandleInput(value, true);
                else if (state.IsKeyReleased(key))
                    port.HandleInput(value, false);
            }

            foreach (Keys key in InputMappings.InputPort2Mapping.Keys)
            {
                var port = inputDevice.InputPort2;
                var value = InputMappings.InputPort2Mapping[key];

                if (state.IsKeyDown(key))
                    port.HandleInput(value, true);
                else if (state.IsKeyReleased(key))
                    port.HandleInput(value, false);
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            _vertexArray = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArray);

            // Generate and store data in vertex buffer object (VBO)
            _vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, _displayVertices.Length * sizeof(float), _displayVertices, BufferUsageHint.StaticDraw);

            // Generate element buffer
            _elementBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _vertIndices.Length * sizeof(uint), _vertIndices, BufferUsageHint.StaticDraw);

            _shader = new DisplayShader();

            // Load vertex position into loc 0
            var vertexLocation = 0;
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            // Load texture coords into loc 1
            var texCoordLocation = 1;
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            _texture = new DisplayTexture();
            _texture.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _texture.UpdateDisplay(_arcadeMachine.Memory.ReadVRAM());

            _texture.Use();
            _shader.Use();

            GL.BindVertexArray(_vertexArray);

            GL.DrawElements(PrimitiveType.Triangles, _vertIndices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }
    }
}
