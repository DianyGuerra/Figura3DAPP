using System;
using System.Drawing;
using static Motor3D.Motor3DCore;

namespace Motor3D
{
    /// <summary>
    /// Fábrica para crear figuras 3D predefinidas.
    /// Genera geometría para cubos, esferas, cilindros, conos y pirámides.
    /// </summary>
    public static class Figuras3DFactory
    {
        /// <summary>
        /// Crea un cubo centrado en el origen.
        /// </summary>
        public static Figura3D CrearCubo(float tamaño = 1.0f)
        {
            var cubo = new Figura3D { Nombre = "Cubo", Color = Color.Blue };
            float t = tamaño / 2f;

            // 8 vértices del cubo
            cubo.Vertices.Add(new Vector3D(-t, -t, -t)); // 0
            cubo.Vertices.Add(new Vector3D(t, -t, -t));  // 1
            cubo.Vertices.Add(new Vector3D(t, t, -t));   // 2
            cubo.Vertices.Add(new Vector3D(-t, t, -t));  // 3
            cubo.Vertices.Add(new Vector3D(-t, -t, t));  // 4
            cubo.Vertices.Add(new Vector3D(t, -t, t));   // 5
            cubo.Vertices.Add(new Vector3D(t, t, t));    // 6
            cubo.Vertices.Add(new Vector3D(-t, t, t));   // 7

            // 6 caras (cada una con 4 vértices)
            cubo.Caras.Add(new int[] { 0, 1, 2, 3 }); // Frente
            cubo.Caras.Add(new int[] { 5, 4, 7, 6 }); // Atrás
            cubo.Caras.Add(new int[] { 4, 0, 3, 7 }); // Izquierda
            cubo.Caras.Add(new int[] { 1, 5, 6, 2 }); // Derecha
            cubo.Caras.Add(new int[] { 3, 2, 6, 7 }); // Arriba
            cubo.Caras.Add(new int[] { 4, 5, 1, 0 }); // Abajo

            return cubo;
        }

        /// <summary>
        /// Crea una esfera usando subdivisión icosaédrica.
        /// </summary>
        public static Figura3D CrearEsfera(float radio = 1.0f, int subdivisiones = 2)
        {
            var esfera = new Figura3D { Nombre = "Esfera", Color = Color.Red };

            int stacks = Math.Max(6, subdivisiones * 8);
            int slices = stacks * 2;

            // Polo norte
            esfera.Vertices.Add(new Vector3D(0, radio, 0));
            int idxTop = 0;

            // Anillos (sin polos)
            for (int i = 1; i < stacks; i++)
            {
                float theta = (float)Math.PI * i / stacks;      // 0..PI
                float y = radio * (float)Math.Cos(theta);
                float r = radio * (float)Math.Sin(theta);

                for (int j = 0; j < slices; j++)
                {
                    float phi = 2f * (float)Math.PI * j / slices; // 0..2PI
                    float x = r * (float)Math.Cos(phi);
                    float z = r * (float)Math.Sin(phi);
                    esfera.Vertices.Add(new Vector3D(x, y, z));
                }
            }

            // Polo sur
            esfera.Vertices.Add(new Vector3D(0, -radio, 0));
            int idxBottom = esfera.Vertices.Count - 1;

            int RingStart(int ring) => 1 + (ring * slices); // ring 0 = primer anillo (i=1)

            // Tapas (triángulos)
            // Norte: top -> ring0[j] -> ring0[j+1]
            for (int j = 0; j < slices; j++)
            {
                int a = idxTop;
                int b = RingStart(0) + j;
                int c = RingStart(0) + (j + 1) % slices;
                esfera.Caras.Add(new[] { a, b, c });
            }

            // Cuerpo (quads)
            for (int ring = 0; ring < stacks - 2; ring++)
            {
                int r0 = RingStart(ring);
                int r1 = RingStart(ring + 1);

                for (int j = 0; j < slices; j++)
                {
                    int a = r0 + j;
                    int b = r1 + j;
                    int c = r1 + (j + 1) % slices;
                    int d = r0 + (j + 1) % slices;
                    esfera.Caras.Add(new[] { a, b, c, d });
                }
            }

            // Sur: bottom -> lastRing[j+1] -> lastRing[j]
            int lastRingStart = RingStart(stacks - 2);
            for (int j = 0; j < slices; j++)
            {
                int a = idxBottom;
                int b = lastRingStart + (j + 1) % slices;
                int c = lastRingStart + j;
                esfera.Caras.Add(new[] { a, b, c });
            }

            return esfera;
        }


        /// <summary>
        /// Crea un cilindro con tapas.
        /// </summary>
        public static Figura3D CrearCilindro(float radio = 0.5f, float altura = 2.0f, int segmentos = 20)
        {
            var cilindro = new Figura3D { Nombre = "Cilindro", Color = Color.Green };
            float mitadAltura = altura / 2f;

            // Vértices del círculo superior
            for (int i = 0; i < segmentos; i++)
            {
                float angulo = 2 * (float)Math.PI * i / segmentos;
                float x = radio * (float)Math.Cos(angulo);
                float z = radio * (float)Math.Sin(angulo);
                cilindro.Vertices.Add(new Vector3D(x, mitadAltura, z));
            }

            // Vértices del círculo inferior
            for (int i = 0; i < segmentos; i++)
            {
                float angulo = 2 * (float)Math.PI * i / segmentos;
                float x = radio * (float)Math.Cos(angulo);
                float z = radio * (float)Math.Sin(angulo);
                cilindro.Vertices.Add(new Vector3D(x, -mitadAltura, z));
            }

            // Vértices centrales para las tapas
            int centroSuperior = cilindro.Vertices.Count;
            cilindro.Vertices.Add(new Vector3D(0, mitadAltura, 0));
            int centroInferior = cilindro.Vertices.Count;
            cilindro.Vertices.Add(new Vector3D(0, -mitadAltura, 0));

            // Caras laterales
            for (int i = 0; i < segmentos; i++)
            {
                int siguiente = (i + 1) % segmentos;
                cilindro.Caras.Add(new int[] { i, siguiente, siguiente + segmentos, i + segmentos });
            }

            // Tapa superior
            for (int i = 0; i < segmentos; i++)
            {
                int siguiente = (i + 1) % segmentos;
                cilindro.Caras.Add(new int[] { centroSuperior, siguiente, i });
            }

            // Tapa inferior
            for (int i = 0; i < segmentos; i++)
            {
                int siguiente = (i + 1) % segmentos;
                cilindro.Caras.Add(new int[] { centroInferior, i + segmentos, siguiente + segmentos });
            }

            return cilindro;
        }

        /// <summary>
        /// Crea un cono con base circular.
        /// </summary>
        public static Figura3D CrearCono(float radio = 0.5f, float altura = 2.0f, int segmentos = 20)
        {
            var cono = new Figura3D { Nombre = "Cono", Color = Color.Yellow };

            // Vértice superior (punta del cono)
            int verticeApice = 0;
            cono.Vertices.Add(new Vector3D(0, altura / 2f, 0));

            // Vértices de la base
            for (int i = 0; i < segmentos; i++)
            {
                float angulo = 2 * (float)Math.PI * i / segmentos;
                float x = radio * (float)Math.Cos(angulo);
                float z = radio * (float)Math.Sin(angulo);
                cono.Vertices.Add(new Vector3D(x, -altura / 2f, z));
            }

            // Vértice central de la base
            int centroBase = cono.Vertices.Count;
            cono.Vertices.Add(new Vector3D(0, -altura / 2f, 0));

            // Caras laterales (triángulos)
            for (int i = 0; i < segmentos; i++)
            {
                int siguiente = (i % (segmentos - 1)) + 1;
                if (i == segmentos - 1) siguiente = 1;
                cono.Caras.Add(new int[] { verticeApice, siguiente, i + 1 });
            }

            // Base (triángulos desde el centro)
            for (int i = 1; i <= segmentos; i++)
            {
                int siguiente = (i % segmentos) + 1;
                cono.Caras.Add(new int[] { centroBase, i, siguiente });
            }

            return cono;
        }

        /// <summary>
        /// Crea una pirámide cuadrada.
        /// </summary>
        public static Figura3D CrearPiramide(float base_tamaño = 1.0f, float altura = 1.5f)
        {
            var piramide = new Figura3D { Nombre = "Pirámide", Color = Color.Orange };
            float b = base_tamaño / 2f;
            float h = altura / 2f;

            // Vértice superior (ápice)
            piramide.Vertices.Add(new Vector3D(0, h, 0)); // 0

            // Vértices de la base
            piramide.Vertices.Add(new Vector3D(-b, -h, -b)); // 1
            piramide.Vertices.Add(new Vector3D(b, -h, -b));  // 2
            piramide.Vertices.Add(new Vector3D(b, -h, b));   // 3
            piramide.Vertices.Add(new Vector3D(-b, -h, b));  // 4

            // Caras laterales (triángulos)
            piramide.Caras.Add(new int[] { 0, 2, 1 }); // Frente
            piramide.Caras.Add(new int[] { 0, 3, 2 }); // Derecha
            piramide.Caras.Add(new int[] { 0, 4, 3 }); // Atrás
            piramide.Caras.Add(new int[] { 0, 1, 4 }); // Izquierda

            // Base (cuadrado)
            piramide.Caras.Add(new int[] { 1, 2, 3, 4 });

            return piramide;
        }

        /// <summary>
        /// Crea un toro (dona).
        /// </summary>
        public static Figura3D CrearToro(float radioMayor = 1.0f, float radioMenor = 0.3f, int segmentos = 16, int lados = 12)
        {
            var toro = new Figura3D { Nombre = "Toro", Color = Color.Purple };

            for (int i = 0; i < segmentos; i++)
            {
                float u = 2 * (float)Math.PI * i / segmentos;
                float cosU = (float)Math.Cos(u);
                float sinU = (float)Math.Sin(u);

                for (int j = 0; j < lados; j++)
                {
                    float v = 2 * (float)Math.PI * j / lados;
                    float cosV = (float)Math.Cos(v);
                    float sinV = (float)Math.Sin(v);

                    float x = (radioMayor + radioMenor * cosV) * cosU;
                    float y = radioMenor * sinV;
                    float z = (radioMayor + radioMenor * cosV) * sinU;

                    toro.Vertices.Add(new Vector3D(x, y, z));
                }
            }

            // Crear caras
            for (int i = 0; i < segmentos; i++)
            {
                int nextI = (i + 1) % segmentos;
                for (int j = 0; j < lados; j++)
                {
                    int nextJ = (j + 1) % lados;

                    int v1 = i * lados + j;
                    int v2 = nextI * lados + j;
                    int v3 = nextI * lados + nextJ;
                    int v4 = i * lados + nextJ;

                    toro.Caras.Add(new int[] { v1, v2, v3, v4 });
                }
            }

            return toro;
        }
    }
}