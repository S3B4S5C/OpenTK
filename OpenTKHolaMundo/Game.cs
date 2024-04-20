using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OpenTKHolaMundo
{
    public class Game : GameWindow
    {
        private Shader shader;

        private Matrix4 view, projection;
        private double time;


        //Vertices:

        float[] vertices_pantalla;
        float[] vertices_soporte;
        float[] vertices_base;

        uint[] indices_pan_sop;
        uint[] indices_base;


        float[] vertices_mesa;
        float[] vertices_pata1;
        float[] vertices_pata2;
        float[] vertices_pata3;
        float[] vertices_pata4;

        uint[] indices_mesa;
        uint[] indices_patas;


        float[] vertices_vaso;
        uint[] indices_vaso;


        private Escenario escenario1;

        Dictionary<float[], uint[]> partes;
        Dictionary<string, Dictionary<float[], uint[]>> objetos;


        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
            //Monitor
            vertices_pantalla = new float[]
           {
                // Vértices de la pantalla (x, y, z, r, g, b)
                    // Cara frontal
                     0.25f,  0.37f, -0.05f, 0.0f, 0.0f, 0.0f, // Vértice 0
                     0.25f,  0.37f, 0.05f, 0.0f, 0.0f, 0.0f, // Vértice 1
                     0.25f,  0.07f, -0.05f, 0.3f, 0.3f, 0.3f, // Vértice 2
                     0.25f,  0.07f, 0.05f, 0.3f, 0.3f, 0.3f, // Vértice 3

                     -0.25f,  0.37f, -0.05f, 0.0f, 0.0f, 0.0f, // Vértice 4
                     -0.25f,  0.37f, 0.05f, 0.0f, 0.0f, 0.0f, // Vértice 5
                     -0.25f,  0.07f, -0.05f, 0.0f, 0.0f, 0.0f, // Vértice 6
                     -0.25f,  0.07f, 0.05f, 0.0f, 0.0f, 0.0f, // Vértice 7

           };
            vertices_soporte = new float[]
            {
                     0.03f, 0.07f, -0.03f, 0.3f, 0.3f, 0.3f, // Vértice 8
                     0.03f, 0.07f, 0.03f, 0.3f, 0.3f, 0.3f, // Vértice 9
                     -0.03f, 0.07f, -0.03f, 0.3f, 0.3f, 0.3f, // Vértice 10
                     -0.03f, 0.07f, 0.03f, 0.3f, 0.3f, 0.3f, // Vértice 11

                     0.03f, 0.04f, -0.03f, 0.3f, 0.3f, 0.3f, // Vértice 12
                     0.03f, 0.04f, 0.03f, 0.3f, 0.3f, 0.3f, // Vértice 13
                     -0.03f, 0.04f, -0.03f, 0.3f, 0.3f, 0.3f, // Vértice 14
                     -0.03f, 0.04f, 0.03f, 0.3f, 0.3f, 0.3f, // Vértice 15

            };
            vertices_base = new float[]{
                     0.06f, 0.04f, -0.04f, 0.3f, 0.3f, 0.3f, // Vértice 16
                     0.06f, 0.04f, 0.04f, 0.3f, 0.3f, 0.3f, // Vértice 17
                     -0.06f, 0.04f, -0.04f, 0.3f, 0.3f, 0.3f, // Vértice 18
                     -0.06f, 0.04f, 0.04f, 0.3f, 0.3f, 0.3f, // Vértice 19
        };

            indices_pan_sop = new uint[]
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
            };
            indices_base = new uint[]
        {
                0, 1, 2,
                3, 2, 1
        };

            //Mesa
            vertices_mesa = new float[]
            {
                // Vértices de la pantalla (x, y, z, r, g, b)
                    // Cara frontal
                     0.3f,  0.03f, -0.25f, 0.9f, 0.7f, 0.6f, // Vértice 0
                     0.3f,  0.03f, 0.25f, 0.9f, 0.7f, 0.6f, // Vértice 1
                     0.3f,  0.0f, -0.25f, 0.4f, 0.2f, 0.1f, // Vértice 2
                     0.3f,  0.0f, 0.25f, 0.4f, 0.2f, 0.1f, // Vértice 3

                                -0.3f,  0.03f, -0.25f, 0.9f, 0.5f, 0.4f, // Vértice 4
                                 -0.3f,  0.03f, 0.25f, 0.9f, 0.5f, 0.4f, // Vértice 5
                                 -0.3f,  0.0f, -0.25f, 0.4f, 0.2f, 0.1f, // Vértice 6
                                 -0.3f,  0.0f, 0.25f, 0.4f, 0.2f, 0.1f, // Vértice 7         
             };
            vertices_pata1 = new float[]
                {
                                 0.29f, 0.0f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 8
                                 0.29f, 0.0f, 0.24f, 0.4f, 0.2f, 0.1f, // Vértice 9
                                 0.25f, 0.0f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 10
                                 0.25f, 0.0f, 0.24f, 0.4f, 0.2f, 0.1f, // Vértice 11

                                 0.29f, -0.54f, 0.2f, 0.9f, 0.7f, 0.6f, // Vértice 12
                                 0.29f, -0.54f, 0.24f, 0.9f, 0.7f, 0.6f, // Vértice 13
                                 0.25f, -0.54f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 14
                                 0.25f, -0.54f, 0.24f, 0.9f, 0.7f, 0.6f, // Vértice 15
                };
            vertices_pata2 = new float[]
                {
                                 0.29f, 0.0f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 8
                                 0.29f, 0.0f, -0.2f, 0.4f, 0.2f, 0.1f, // Vértice 9
                                 0.25f, 0.0f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 10
                                 0.25f, 0.0f, -0.2f, 0.4f, 0.2f, 0.1f, // Vértice 11

                                 0.29f, -0.54f, -0.24f, 0.9f, 0.7f, 0.6f, // Vértice 12
                                 0.29f, -0.54f, -0.2f, 0.9f, 0.7f, 0.6f, // Vértice 13
                                 0.25f, -0.54f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 14
                                 0.25f, -0.54f, -0.2f, 0.9f, 0.7f, 0.6f, // Vértice 15
                };
            vertices_pata3 = new float[]
                {
                                 -0.29f, 0.0f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 8
                                 -0.29f, 0.0f, -0.2f, 0.4f, 0.2f, 0.1f, // Vértice 9
                                 -0.25f, 0.0f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 10
                                 -0.25f, 0.0f, -0.2f, 0.4f, 0.2f, 0.1f, // Vértice 11

                                 -0.29f, -0.54f, -0.24f, 0.9f, 0.7f, 0.6f, // Vértice 12
                                 -0.29f, -0.54f, -0.2f, 0.9f, 0.7f, 0.6f, // Vértice 13
                                 -0.25f, -0.54f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 14
                                 -0.25f, -0.54f, -0.2f, 0.9f, 0.7f, 0.6f, // Vértice 15
                };
            vertices_pata4 = new float[]
                {
                                 -0.29f, 0.0f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 8
                                 -0.29f, 0.0f, 0.24f, 0.4f, 0.2f, 0.1f, // Vértice 9
                                 -0.25f, 0.0f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 10
                                 -0.25f, 0.0f, 0.24f, 0.4f, 0.2f, 0.1f, // Vértice 11

                                 -0.29f, -0.54f, 0.2f, 0.9f, 0.7f, 0.6f, // Vértice 12
                                 -0.29f, -0.54f, 0.24f, 0.9f, 0.7f, 0.6f, // Vértice 13
                                 -0.25f, -0.54f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 14
                                 -0.25f, -0.54f, 0.24f, 0.9f, 0.7f, 0.6f, // Vértice 15
                };

            indices_mesa = new uint[]
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
                };
            indices_patas = new uint[]
                {
                            //Soporte
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

                };

            //Vaso
            vertices_vaso = new float[]
            {
                     0.03f, 0.11f, -0.03f, 0.9f, 0.7f, 0.9f, // Vértice 8
                     0.03f, 0.11f, 0.03f, 0.9f, 0.7f, 0.9f, // Vértice 9
                     -0.03f, 0.11f, -0.03f, 0.9f, 0.7f, 0.9f, // Vértice 10
                     -0.03f, 0.11f, 0.03f, 0.9f, 0.7f, 0.9f,// Vértice 11

                     0.03f, 0.04f, -0.03f, 0.9f, 0.7f, 0.9f, // Vértice 12
                     0.03f, 0.04f, 0.03f, 0.9f, 0.7f, 0.9f,// Vértice 13
                     -0.03f, 0.04f, -0.03f, 0.9f, 0.7f, 0.9f, // Vértice 14
                     -0.03f, 0.04f, 0.03f, 0.9f, 0.7f, 0.9f, // Vértice 15

                        };

            indices_vaso = new uint[]
                {
                            //Vaso
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

                                // Cara inferior
                                2, 3, 7,
                                2, 6, 7,

                };


            Dictionary<float[], uint[]> Monitor = new Dictionary<float[], uint[]>()
            {
                { vertices_pantalla, indices_pan_sop }, { vertices_soporte, indices_pan_sop }, { vertices_base, indices_base }
            };
            Dictionary<float[], uint[]> Mesa = new Dictionary<float[], uint[]>()
            {
                { vertices_mesa, indices_mesa }, { vertices_pata1, indices_patas }, { vertices_pata2, indices_patas }, { vertices_pata3, indices_patas }, { vertices_pata4, indices_patas }
            };
            Dictionary<float[], uint[]> Vaso = new Dictionary<float[], uint[]>()
            {
                { vertices_vaso, indices_vaso }
            };

            objetos = new Dictionary<string, Dictionary<float[], uint[]>>()
            {
                { "Monitor", Monitor },
                { "Mesa", Mesa },
                { "Vaso", Vaso }
            };


            SerializarDic(Monitor, "Monitor");
            SerializarDic(Mesa, "Mesa");
            SerializarDic(Vaso, "Vaso");

        }
        protected override void OnLoad()
        {
            base.OnLoad();
            

            GL.ClearColor(0.4f, 0.0f, 0.6f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            shader = new Shader("./shader.vert", "./shader.frag");
            shader.Use();

            


            escenario1 = new Escenario(objetos, 0, 0, 0);

            Console.Write(escenario1);
            escenario1.CargarEscenario();

            view = Matrix4.CreateTranslation(0.0f, -0.15f, -1.5f);
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
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 model = Matrix4.CreateRotationY(MathHelper.DegreesToRadians((float)time));

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);


            escenario1.DibujarEscenario();

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

        public void SerializarDic(Dictionary<float[], uint[]> dictionary, string name)
        {
            string jsonObjeto = "";
            int i = 1;
            foreach (var kvp in dictionary)
            {
                Console.Write(dictionary.Count);
                string vertices = JsonConvert.SerializeObject(kvp.Key, Formatting.Indented);
                string indices = JsonConvert.SerializeObject(kvp.Value, Formatting.Indented);
                string json = "{ \"vertices\": "+ vertices + ", \"indices\":" + indices + "}";
                jsonObjeto = jsonObjeto + json;
                 if ( i < dictionary.Count)
                    jsonObjeto = jsonObjeto + ", \"" + i.ToString() + "\": ";
                i++;

            }

            jsonObjeto = "{ \"0\": " + jsonObjeto + " }";
            File.WriteAllText("./jsons/"+name + ".json", jsonObjeto);
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