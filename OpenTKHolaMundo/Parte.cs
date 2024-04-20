using OpenTK.Graphics.OpenGL4;

namespace OpenTKHolaMundo
{
    internal class Parte
    {
        private float[] vertices;
        private uint[] indices;

        private int VertexBufferObject;

        private int ElementBufferObject;

        private int VertexArrayObject;
        public Parte(float[] vertices, uint[] indices, float x, float y, float z)
        {
            this.vertices = vertices;
            this.indices = indices;

            this.MoverAlPunto(x, y, z);
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

        public void MoverAlPunto(float x, float y, float z)
        {
            for (int i = 0; i < this.vertices.Length; i += 6)
            {
                this.vertices[i] = this.vertices[i] + x;
                this.vertices[i + 1] = this.vertices[i + 1] + y;
                this.vertices[i + 2] = this.vertices[i + 2] + z;
            }
        }
    }
}
