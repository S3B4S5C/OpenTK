using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
namespace OpenTKHolaMundo
{
    public class Game : GameWindow
    {
        private int vertexBuffer;
        private int indexBufferHandle;
        private int shaderProgramHandle;
        private int vertexArrayHandle;
        Vector3 Position = new Vector3(0.0f, 0.0f, 3.0f);
        public Game() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {

        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Viewport(0, 0, Size.X, Size.Y);

            float[] vertices = new float[]
            {
                -0.5f, 0.5f, 0.0f, 1f, 0f, 0f, 1f,
                 0.5f, 0.5f, 0.0f, 0f, 1f, 0f, 1f,
                 0.5f, -0.5f, 0.0f, 0f, 0f, 1f, 1f,
                -0.5f, -0.5f, 0.0f, 1f, 1f, 0f, 1f,

                -0.5f, 0.5f, 0.5f, 1f, 0f, 0f, 1f,
                 0.5f, 1f, 0.5f, 0f, 1f, 0f, 1f,
                 0.5f, -0.5f, 0.5f, 0f, 0f, 1f, 1f,
                -0.5f, -0.5f, 0.5f, 1f, 1f, 0f, 1f,
            };
            int[] indices = new int[]
            {
                0, 1, 2,
                0, 3, 2,
                0, 1, 5,
                0, 4, 5
            };

            this.vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer,this.vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            this.indexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indexBufferHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            this.vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(this.vertexArrayHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBuffer);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);

            

            string vertexShaderCode =
                @"
                 #version 330 core
                 
                 layout (location = 0) in vec3 aPosition;
                 layout (location = 1) in vec4 aColor;

                 out vec4 vColor;

                 void main(){
                    vColor = aColor;
                    gl_Position = vec4(aPosition, 1f);
                 }
                ";
            string fragmentShaderCode =
                @"
                 #version 330 core
                 in vec4 vColor;
                 out vec4 pixelColor;

                 void main(){
                    pixelColor = vColor;
                 }
                ";

            int vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle, vertexShaderCode);
            GL.CompileShader(vertexShaderHandle);

            

            int fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderHandle, fragmentShaderCode);
            GL.CompileShader(fragmentShaderHandle);

            this.shaderProgramHandle = GL.CreateProgram();
            GL.AttachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(this.shaderProgramHandle, fragmentShaderHandle);

            GL.LinkProgram(this.shaderProgramHandle);

            GL.DetachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.DetachShader(this.shaderProgramHandle, fragmentShaderHandle);

            GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(fragmentShaderHandle);


            base.OnLoad();

        }
        protected override void OnUnload()
        {
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(this.vertexArrayHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(this.indexBufferHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(this.vertexBuffer);

            GL.UseProgram(0);
            GL.DeleteProgram(this.shaderProgramHandle);

            base.OnUnload();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(new Color4(0.2f, 0.3f, 0.3f, 1.0f));

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.UseProgram(this.shaderProgramHandle);
            GL.BindVertexArray(this.vertexArrayHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indexBufferHandle);
            GL.DrawElements(PrimitiveType.Triangles, 12, DrawElementsType.UnsignedInt, 0);

            this.Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
