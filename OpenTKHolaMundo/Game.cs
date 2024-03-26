using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
namespace OpenTKHolaMundo
{
    public class Game : GameWindow
    {

        private int VertexBufferObject;

        private int ElementBufferObject;

        private int VertexArrayObject;

        private Shader shader;
             
        private double time;

        private Matrix4 view, projection;


        float[] vertices = new float[]
            {
                // Vértices de la pantalla (x, y, z, r, g, b)
                    // Cara frontal
                     0.25f,  0.3f, -0.05f, 1.0f, 0.0f, 0.0f, // Vértice 0
                     0.25f,  0.3f, 0.05f, 0.0f, 1.0f, 0.0f, // Vértice 1
                     0.25f,  0.0f, -0.05f, 0.0f, 0.0f, 1.0f, // Vértice 2
                     0.25f,  0.0f, 0.05f, 1.0f, 1.0f, 0.0f, // Vértice 3

                     -0.25f,  0.3f, -0.05f, 1.0f, 0.0f, 1.0f, // Vértice 4
                     -0.25f,  0.3f, 0.05f, 0.0f, 1.0f, 0.0f, // Vértice 5
                     -0.25f,  0.0f, -0.05f, 1.0f, 0.0f, 1.0f, // Vértice 6
                     -0.25f,  0.0f, 0.05f, 1.0f, 0.0f, 1.0f, // Vértice 7

                 //vertices del soporte (x, y, z, r, g, b)

                     0.03f, 0.0f, -0.03f, 1.0f, 0.0f, 1.0f, // Vértice 8
                     0.03f, 0.0f, 0.03f, 0.0f, 1.0f, 0.0f, // Vértice 9
                     -0.03f, 0.0f, -0.03f, 0.0f, 0.0f, 1.0f, // Vértice 10
                     -0.03f, 0.0f, 0.03f, 1.0f, 0.0f, 0.0f, // Vértice 11

                     0.03f, -0.04f, -0.03f, 1.0f, 0.0f, 1.0f, // Vértice 12
                     0.03f, -0.04f, 0.03f, 0.0f, 1.0f, 0.0f, // Vértice 13
                     -0.03f, -0.04f, -0.03f, 0.0f, 0.0f, 1.0f, // Vértice 14
                     -0.03f, -0.04f, 0.03f, 1.0f, 0.0f, 0.0f // Vértice 15

            };

        uint[] indices = new uint[]
            {
                //Pantalla
                    // Cara derecha
                    0, 1, 3,
                    0, 2, 3,

                    // Cara izquierda
                    4, 5, 7,
                    4, 6, 7,

                    // Cara trasera
                    1, 5, 7,
                    1, 3, 7,

                    // Cara frontal
                    0, 2, 6,
                    0, 4, 6,

                    // Cara superior
                    1, 0, 4,
                    1, 5, 4,

                    // Cara inferior
                    2, 3, 7,
                    2, 6, 7,
                //Soporte
                    // Cara derecha
                    8, 9, 11,
                    8, 10, 11,

                    // Cara izquierda
                    12, 13, 15,
                    12, 14, 15,

                    // Cara trasera
                    9, 13, 15,
                    9, 11, 15,

                    // Cara frontal
                    8, 10, 14,
                    8, 12, 14,

                    // Cara superior
                    9, 8, 12,
                    9, 13, 12,

                    // Cara inferior
                    10, 11, 15,
                    10, 14, 15


            };

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
        }
        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.4f, 0.0f, 0.6f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            shader = new Shader("./shader.vert", "./shader.frag");
            shader.Use();

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            

            // Configurar las matrices antes de usarlas
            
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -2.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);

            
        }

        protected override void OnUnload()
        {
            base.OnUnload();

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            time += 4.0 * args.Time;

            // Limpiar el color y el buffer de profundidad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(VertexArrayObject);

            // Usar el shader y configurar las matrices
            shader.Use();

            Matrix4 model = Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(time));

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);

            // Dibujar el objeto
            
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            // Intercambiar los buffers
            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }
    }
    public class Shader
    {
        public readonly int Handle;

        private readonly Dictionary<string, int> _uniformLocations;

        public Shader(string vertPath, string fragPath)
        {
            var shaderSource = File.ReadAllText(vertPath);

            var vertexShader = GL.CreateShader(ShaderType.VertexShader);


            GL.ShaderSource(vertexShader, shaderSource);


            CompileShader(vertexShader);


            shaderSource = File.ReadAllText(fragPath);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);


            Handle = GL.CreateProgram();


            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);


            LinkProgram(Handle);


            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);


            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);


            _uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(Handle, i, out _, out _);

                var location = GL.GetUniformLocation(Handle, key);

                _uniformLocations.Add(key, location);
            }
        }

        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }


        public void Use()
        {
            GL.UseProgram(Handle);
        }
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }


        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }


        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(_uniformLocations[name], data);
        }
    }
}