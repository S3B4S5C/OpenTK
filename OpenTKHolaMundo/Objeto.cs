using OpenTK.Graphics.OpenGL4;

namespace OpenTKHolaMundo
{
    internal class Objeto
    {
        private Dictionary<int, Parte> carasDict;

        public Objeto(Dictionary<float[], uint[]> vertices, float x, float y, float z)
        {
            carasDict = new Dictionary<int, Parte>();
            int i = 1;
            foreach (var vertex in vertices)
            {
                carasDict.Add(i, this.CrearCara(vertex.Key, vertex.Value, x, y, z));
                i++;
            }   
        }

        public Parte CrearCara(float[] vertices, uint[] indices, float x, float y, float z)
        {
            Parte cara = new Parte(vertices, indices, x, y ,z);
            return cara;
        }

        public void CargarObjeto()
        {
            foreach (var cara in carasDict.Values)
            {
                cara.Cargar();
            }
        }

        public void DibujarObjeto()
        {
            foreach (var cara in carasDict.Values)
            {
                cara.Dibujar();
            }
        }
        


        
    }      
}
