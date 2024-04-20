using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKHolaMundo
{
    internal class Escenario
    {

        public Objeto[] partes;

        public Dictionary<string, Objeto> objetosDict;

        public Escenario(Dictionary<string, Dictionary<float[], uint[]>> vertices, float x, float y, float z)
        {
            objetosDict = new Dictionary<string, Objeto>();
            foreach (var v in vertices)
            {
                objetosDict.Add(v.Key, this.CrearObjeto(v.Value, x, y, z));
            }
        }

        public Objeto CrearObjeto(Dictionary<float[], uint[]> vertices, float x, float y, float z)
        {
            Objeto objeto = new Objeto(vertices, x, y, z);
            return objeto;
        }

        public void CargarEscenario()
        {
            foreach (var objeto in objetosDict.Values)
            {
                objeto.CargarObjeto();
            }
        }

        public void DibujarEscenario()
        {
            foreach (var objeto in objetosDict.Values)
            {
                objeto.DibujarObjeto();
            }
        }
    }
}

 

//    internal class Vaso : Escenario
//    {
//        float[] vertices_vaso = new float[]
//            {
//                     0.03f, 0.04f, -0.03f, 0.9f, 0.7f, 0.9f, // Vértice 8
//                     0.03f, 0.04f, 0.03f, 0.9f, 0.7f, 0.9f, // Vértice 9
//                     -0.03f, 0.04f, -0.03f, 0.9f, 0.7f, 0.9f, // Vértice 10
//                     -0.03f, 0.04f, 0.03f, 0.9f, 0.7f, 0.9f,// Vértice 11

//                     0.03f, -0.04f, -0.03f, 0.9f, 0.7f, 0.9f, // Vértice 12
//                     0.03f, -0.04f, 0.03f, 0.9f, 0.7f, 0.9f,// Vértice 13
//                     -0.03f, -0.04f, -0.03f, 0.9f, 0.7f, 0.9f, // Vértice 14
//                     -0.03f, -0.04f, 0.03f, 0.9f, 0.7f, 0.9f, // Vértice 15

//            };

//        uint[] indices_vaso = new uint[]
//            {
//                //Vaso
//                     // Cara derecha
//                    0, 1, 3,
//                    0, 2, 3,

//                    // Cara izquierda
//                    4, 5, 7,
//                    4, 6, 7,

//                    // Cara trasera
//                    1, 5, 7,
//                    1, 3, 7,

//                    // Cara frontal
//                    0, 2, 6,
//                    0, 4, 6,

//                    // Cara inferior
//                    2, 3, 7,
//                    2, 6, 7,

//            };
//        Objeto vaso;


//        public Vaso(float x, float y, float z)
//        {
//            vaso = new Parte(vertices_vaso, indices_vaso);

//            partes = new Objeto[] {
//                vaso
//            };

//            this.xc = x;
//            this.yc = y;
//            this.zc = z;
//        }
//    }

//    internal class Mesa : Escenario
//    {

//        float[] vertices_mesa = new float[]
//            {
//                // Vértices de la pantalla (x, y, z, r, g, b)
//                    // Cara frontal
//                     0.3f,  0.03f, -0.25f, 0.9f, 0.7f, 0.6f, // Vértice 0
//                     0.3f,  0.03f, 0.25f, 0.9f, 0.7f, 0.6f, // Vértice 1
//                     0.3f,  0.0f, -0.25f, 0.4f, 0.2f, 0.1f, // Vértice 2
//                     0.3f,  0.0f, 0.25f, 0.4f, 0.2f, 0.1f, // Vértice 3

//                    -0.3f,  0.03f, -0.25f, 0.9f, 0.5f, 0.4f, // Vértice 4
//                     -0.3f,  0.03f, 0.25f, 0.9f, 0.5f, 0.4f, // Vértice 5
//                     -0.3f,  0.0f, -0.25f, 0.4f, 0.2f, 0.1f, // Vértice 6
//                     -0.3f,  0.0f, 0.25f, 0.4f, 0.2f, 0.1f, // Vértice 7         
//            };
//        float[] vertices_pata1 = new float[]
//            {
//                     0.29f, 0.0f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 8
//                     0.29f, 0.0f, 0.24f, 0.4f, 0.2f, 0.1f, // Vértice 9
//                     0.25f, 0.0f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 10
//                     0.25f, 0.0f, 0.24f, 0.4f, 0.2f, 0.1f, // Vértice 11

//                     0.29f, -0.54f, 0.2f, 0.9f, 0.7f, 0.6f, // Vértice 12
//                     0.29f, -0.54f, 0.24f, 0.9f, 0.7f, 0.6f, // Vértice 13
//                     0.25f, -0.54f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 14
//                     0.25f, -0.54f, 0.24f, 0.9f, 0.7f, 0.6f, // Vértice 15
//            };
//        float[] vertices_pata2 = new float[]
//            {
//                     0.29f, 0.0f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 8
//                     0.29f, 0.0f, -0.2f, 0.4f, 0.2f, 0.1f, // Vértice 9
//                     0.25f, 0.0f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 10
//                     0.25f, 0.0f, -0.2f, 0.4f, 0.2f, 0.1f, // Vértice 11

//                     0.29f, -0.54f, -0.24f, 0.9f, 0.7f, 0.6f, // Vértice 12
//                     0.29f, -0.54f, -0.2f, 0.9f, 0.7f, 0.6f, // Vértice 13
//                     0.25f, -0.54f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 14
//                     0.25f, -0.54f, -0.2f, 0.9f, 0.7f, 0.6f, // Vértice 15
//            };
//        float[] vertices_pata3 = new float[]
//            {
//                     -0.29f, 0.0f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 8
//                     -0.29f, 0.0f, -0.2f, 0.4f, 0.2f, 0.1f, // Vértice 9
//                     -0.25f, 0.0f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 10
//                     -0.25f, 0.0f, -0.2f, 0.4f, 0.2f, 0.1f, // Vértice 11

//                     -0.29f, -0.54f, -0.24f, 0.9f, 0.7f, 0.6f, // Vértice 12
//                     -0.29f, -0.54f, -0.2f, 0.9f, 0.7f, 0.6f, // Vértice 13
//                     -0.25f, -0.54f, -0.24f, 0.4f, 0.2f, 0.1f, // Vértice 14
//                     -0.25f, -0.54f, -0.2f, 0.9f, 0.7f, 0.6f, // Vértice 15
//            };
//        float[] vertices_pata4 = new float[]
//            {
//                     -0.29f, 0.0f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 8
//                     -0.29f, 0.0f, 0.24f, 0.4f, 0.2f, 0.1f, // Vértice 9
//                     -0.25f, 0.0f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 10
//                     -0.25f, 0.0f, 0.24f, 0.4f, 0.2f, 0.1f, // Vértice 11

//                     -0.29f, -0.54f, 0.2f, 0.9f, 0.7f, 0.6f, // Vértice 12
//                     -0.29f, -0.54f, 0.24f, 0.9f, 0.7f, 0.6f, // Vértice 13
//                     -0.25f, -0.54f, 0.2f, 0.4f, 0.2f, 0.1f, // Vértice 14
//                     -0.25f, -0.54f, 0.24f, 0.9f, 0.7f, 0.6f, // Vértice 15
//            };

//        uint[] indices_mesa = new uint[]
//            {
//                //Pantalla
//                    // Cara derecha
//                    0, 1, 3,
//                    0, 2, 3,

//                    // Cara izquierda
//                    4, 5, 7,
//                    4, 6, 7,

//                    // Cara trasera
//                    1, 5, 7,
//                    1, 3, 7,

//                    // Cara frontal
//                    0, 2, 6,
//                    0, 4, 6,

//                    // Cara superior
//                    1, 0, 4,
//                    1, 5, 4,

//                    // Cara inferior
//                    2, 3, 7,
//                    2, 6, 7,
//            };
//        uint[] indices_patas = new uint[]
//            {
//                //Soporte
//                     // Cara derecha
//                    0, 1, 3,
//                    0, 2, 3,

//                    // Cara izquierda
//                    4, 5, 7,
//                    4, 6, 7,

//                    // Cara trasera
//                    1, 5, 7,
//                    1, 3, 7,

//                    // Cara frontal
//                    0, 2, 6,
//                    0, 4, 6,

//                    // Cara superior
//                    1, 0, 4,
//                    1, 5, 4,

//                    // Cara inferior
//                    2, 3, 7,
//                    2, 6, 7,

//            };

//        Objeto pantalla, pata1, pata2, pata3, pata4;


//        public Mesa(float x, float y, float z)
//        {

//            pantalla = new Parte(vertices_mesa, indices_mesa);
//            pata1 = new Parte(vertices_pata1, indices_patas);
//            pata2 = new Parte(vertices_pata2, indices_patas);
//            pata3 = new Parte(vertices_pata3, indices_patas);
//            pata4 = new Parte(vertices_pata4, indices_patas);

//            partes = new Objeto[] {
//                pantalla, pata1, pata2, pata3, pata4
//            };

//            this.xc = x;
//            this.yc = y;
//            this.zc = z;
//        }



//    }
//}
