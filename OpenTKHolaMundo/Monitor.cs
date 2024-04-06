using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenTKHolaMundo
{
    internal class Monitor
    {
        private int VertexBufferObject;

        private int ElementBufferObject;

        private int VertexArrayObject;


        float x, y, z;

        float[] vertices = new float[]
            {
                // Vértices de la pantalla (x, y, z, r, g, b)
                    // Cara frontal
                     0.25f,  0.3f, -0.05f, 0.0f, 0.0f, 0.0f, // Vértice 0
                     0.25f,  0.3f, 0.05f, 0.0f, 0.0f, 0.0f, // Vértice 1
                     0.25f,  0.0f, -0.05f, 0.3f, 0.3f, 0.3f, // Vértice 2
                     0.25f,  0.0f, 0.05f, 0.3f, 0.3f, 0.3f, // Vértice 3

                     -0.25f,  0.3f, -0.05f, 0.0f, 0.0f, 0.0f, // Vértice 4
                     -0.25f,  0.3f, 0.05f, 0.0f, 0.0f, 0.0f, // Vértice 5
                     -0.25f,  0.0f, -0.05f, 0.0f, 0.0f, 0.0f, // Vértice 6
                     -0.25f,  0.0f, 0.05f, 0.0f, 0.0f, 0.0f, // Vértice 7

                 //vertices del soporte (x, y, z, r, g, b)

                     0.03f, 0.0f, -0.03f, 0.3f, 0.3f, 0.3f, // Vértice 8
                     0.03f, 0.0f, 0.03f, 0.3f, 0.3f, 0.3f, // Vértice 9
                     -0.03f, 0.0f, -0.03f, 0.3f, 0.3f, 0.3f, // Vértice 10
                     -0.03f, 0.0f, 0.03f, 0.3f, 0.3f, 0.3f, // Vértice 11

                     0.03f, -0.04f, -0.03f, 0.3f, 0.3f, 0.3f, // Vértice 12
                     0.03f, -0.04f, 0.03f, 0.3f, 0.3f, 0.3f, // Vértice 13
                     -0.03f, -0.04f, -0.03f, 0.3f, 0.3f, 0.3f, // Vértice 14
                     -0.03f, -0.04f, 0.03f, 0.3f, 0.3f, 0.3f, // Vértice 15

                    0.06f, -0.04f, -0.04f, 0.3f, 0.3f, 0.3f, // Vértice 16
                     0.06f, -0.04f, 0.04f, 0.3f, 0.3f, 0.3f, // Vértice 17
                     -0.06f, -0.04f, -0.04f, 0.3f, 0.3f, 0.3f, // Vértice 18
                     -0.06f, -0.04f, 0.04f, 0.3f, 0.3f, 0.3f, // Vértice 19

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
                    10, 14, 15,
                //Base
                    16,17,18,
                    19,18,17

            };

       public Monitor(float x, float y, float z)
        {

            for (int i = 0; i < this.vertices.Length; i+=6) {
                this.vertices[i] = this.vertices[i] + x;
                this.vertices[i+1]  = this.vertices[i+1] + y;
                this.vertices[i+2] = this.vertices[i+2] + z;           
            }
        }

        public void Cargar()
        {
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);


            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        }

        public void Dibujar()
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
