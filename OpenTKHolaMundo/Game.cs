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



        private Shader shader;
             
        private double time;

        private Matrix4 view, projection;

        private Monitor monitor1;
        private Monitor monitor2;
        private Monitor monitor3;
        private Monitor monitor4;

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
        }
        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.4f, 0.0f, 0.6f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            

            shader = new Shader("./shader.vert", "./shader.frag");
            shader.Use();

            monitor1 = new Monitor(0.3f, 0.1f, -0.1f);
            monitor2 = new Monitor(-0.3f, 0.1f, -0.6f);
            monitor3 = new Monitor(0.0f, -0.3f, -0.9f);
            monitor4 = new Monitor(0.4f, -0.1f, -0.4f);

            monitor1.Cargar();
            monitor2.Cargar();
            monitor3.Cargar();
            monitor4.Cargar();

            // Configurar las matrices antes de usarlas

            view = Matrix4.CreateTranslation(0.0f, -0.15f, -1.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);

            
        }

        protected override void OnUnload()
        {
            base.OnUnload();

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            time += 8.0 * args.Time;

            // Limpiar el color y el buffer de profundidad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);



            // Usar el shader y configurar las matrices
            shader.Use();

            Matrix4 model = Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(time));

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);

            // Dibujar el objeto
            
            monitor1.Dibujar();
            monitor2.Dibujar();
            monitor3.Dibujar();
            monitor4.Dibujar();

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