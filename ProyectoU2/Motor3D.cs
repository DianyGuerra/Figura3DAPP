using System;
using System.Drawing;
using System.Collections.Generic;

namespace Motor3D
{
    /// <summary>
    /// Motor gráfico 3D que maneja transformaciones, proyecciones y renderizado.
    /// Implementa pipeline de gráficos 3D completo: transformaciones → proyección → rasterización.
    /// </summary>
    public class Motor3DCore
    {
        #region Estructuras Básicas

        /// <summary>
        /// Vector 3D con operaciones matemáticas básicas.
        /// </summary>
        public struct Vector3D
        {
            public float X, Y, Z;

            public Vector3D(float x, float y, float z)
            {
                X = x; Y = y; Z = z;
            }

            public static Vector3D operator +(Vector3D a, Vector3D b) =>
                new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

            public static Vector3D operator -(Vector3D a, Vector3D b) =>
                new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

            public static Vector3D operator *(Vector3D v, float s) =>
                new Vector3D(v.X * s, v.Y * s, v.Z * s);

            public float Magnitud() =>
                (float)Math.Sqrt(X * X + Y * Y + Z * Z);

            public Vector3D Normalizar()
            {
                float mag = Magnitud();
                return mag > 0 ? new Vector3D(X / mag, Y / mag, Z / mag) : this;
            }

            public static float ProductoPunto(Vector3D a, Vector3D b) =>
                a.X * b.X + a.Y * b.Y + a.Z * b.Z;

            public static Vector3D ProductoCruz(Vector3D a, Vector3D b) =>
                new Vector3D(
                    a.Y * b.Z - a.Z * b.Y,
                    a.Z * b.X - a.X * b.Z,
                    a.X * b.Y - a.Y * b.X
                );
        }

        /// <summary>
        /// Matriz 4x4 para transformaciones 3D homogéneas.
        /// </summary>
        public class Matrix4x4
        {
            public float[,] m = new float[4, 4];

            public Matrix4x4()
            {
                Identidad();
            }

            public void Identidad()
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        m[i, j] = (i == j) ? 1 : 0;
            }

            public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
            {
                Matrix4x4 result = new Matrix4x4();
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        result.m[i, j] = 0;
                        for (int k = 0; k < 4; k++)
                            result.m[i, j] += a.m[i, k] * b.m[k, j];
                    }
                return result;
            }

            public Vector3D TransformarPunto(Vector3D v)
            {
                float w = m[3, 0] * v.X + m[3, 1] * v.Y + m[3, 2] * v.Z + m[3, 3];
                if (Math.Abs(w) < 0.0001f) w = 1;

                return new Vector3D(
                    (m[0, 0] * v.X + m[0, 1] * v.Y + m[0, 2] * v.Z + m[0, 3]) / w,
                    (m[1, 0] * v.X + m[1, 1] * v.Y + m[1, 2] * v.Z + m[1, 3]) / w,
                    (m[2, 0] * v.X + m[2, 1] * v.Y + m[2, 2] * v.Z + m[2, 3]) / w
                );
            }
        }

        #endregion

        #region Transformaciones

        /// <summary>
        /// Crea matriz de traslación.
        /// </summary>
        public static Matrix4x4 Traslacion(float x, float y, float z)
        {
            Matrix4x4 m = new Matrix4x4();
            m.m[0, 3] = x;
            m.m[1, 3] = y;
            m.m[2, 3] = z;
            return m;
        }

        /// <summary>
        /// Crea matriz de escalamiento.
        /// </summary>
        public static Matrix4x4 Escalamiento(float sx, float sy, float sz)
        {
            Matrix4x4 m = new Matrix4x4();
            m.m[0, 0] = sx;
            m.m[1, 1] = sy;
            m.m[2, 2] = sz;
            return m;
        }

        /// <summary>
        /// Crea matriz de rotación en eje X.
        /// </summary>
        public static Matrix4x4 RotacionX(float angulo)
        {
            float rad = angulo * (float)Math.PI / 180f;
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);

            Matrix4x4 m = new Matrix4x4();
            m.m[1, 1] = cos;
            m.m[1, 2] = -sin;
            m.m[2, 1] = sin;
            m.m[2, 2] = cos;
            return m;
        }

        /// <summary>
        /// Crea matriz de rotación en eje Y.
        /// </summary>
        public static Matrix4x4 RotacionY(float angulo)
        {
            float rad = angulo * (float)Math.PI / 180f;
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);

            Matrix4x4 m = new Matrix4x4();
            m.m[0, 0] = cos;
            m.m[0, 2] = sin;
            m.m[2, 0] = -sin;
            m.m[2, 2] = cos;
            return m;
        }

        /// <summary>
        /// Crea matriz de rotación en eje Z.
        /// </summary>
        public static Matrix4x4 RotacionZ(float angulo)
        {
            float rad = angulo * (float)Math.PI / 180f;
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);

            Matrix4x4 m = new Matrix4x4();
            m.m[0, 0] = cos;
            m.m[0, 1] = -sin;
            m.m[1, 0] = sin;
            m.m[1, 1] = cos;
            return m;
        }

        #endregion

        #region Proyección y Cámara

        /// <summary>
        /// Crea matriz de proyección perspectiva.
        /// </summary>
        public static Matrix4x4 ProyeccionPerspectiva(float fov, float aspecto, float cerca, float lejos)
        {
            float fovRad = fov * (float)Math.PI / 180f;
            float tanHalfFov = (float)Math.Tan(fovRad / 2f);

            Matrix4x4 m = new Matrix4x4();
            m.m[0, 0] = 1f / (aspecto * tanHalfFov);
            m.m[1, 1] = 1f / tanHalfFov;
            m.m[2, 2] = -(lejos + cerca) / (lejos - cerca);
            m.m[2, 3] = -(2f * lejos * cerca) / (lejos - cerca);
            m.m[3, 2] = -1f;
            m.m[3, 3] = 0f;
            return m;
        }

        /// <summary>
        /// Crea matriz LookAt para posicionar la cámara.
        /// </summary>
        public static Matrix4x4 LookAt(Vector3D posicion, Vector3D objetivo, Vector3D arriba)
        {
            Vector3D z = (posicion - objetivo).Normalizar();
            Vector3D x = Vector3D.ProductoCruz(arriba, z).Normalizar();
            Vector3D y = Vector3D.ProductoCruz(z, x);

            Matrix4x4 m = new Matrix4x4();
            m.m[0, 0] = x.X; m.m[0, 1] = x.Y; m.m[0, 2] = x.Z;
            m.m[1, 0] = y.X; m.m[1, 1] = y.Y; m.m[1, 2] = y.Z;
            m.m[2, 0] = z.X; m.m[2, 1] = z.Y; m.m[2, 2] = z.Z;

            m.m[0, 3] = -Vector3D.ProductoPunto(x, posicion);
            m.m[1, 3] = -Vector3D.ProductoPunto(y, posicion);
            m.m[2, 3] = -Vector3D.ProductoPunto(z, posicion);

            return m;
        }

        #endregion

        #region Iluminación

        /// <summary>
        /// Calcula iluminación Phong simplificada.
        /// </summary>
        public static Color CalcularIluminacion(Vector3D normal, Vector3D luz, Color colorBase, float intensidad)
        {
            normal = normal.Normalizar();
            luz = luz.Normalizar();

            float difuso = Math.Max(0, Vector3D.ProductoPunto(normal, luz));
            float ambiente = 0.2f;
            float total = Math.Min(1.0f, ambiente + difuso * intensidad);

            return Color.FromArgb(
                (int)(colorBase.R * total),
                (int)(colorBase.G * total),
                (int)(colorBase.B * total)
            );
        }

        #endregion

        #region Conversión de Coordenadas

        /// <summary>
        /// Convierte coordenadas 3D a 2D en pantalla.
        /// </summary>
        public static PointF A2D(Vector3D v, int ancho, int alto)
        {
            return new PointF(
                (v.X + 1) * ancho / 2f,
                (1 - v.Y) * alto / 2f
            );
        }

        #endregion
    }

    /// <summary>
    /// Clase que representa una figura 3D con geometría y propiedades.
    /// </summary>
    public class Figura3D
    {
        public string Nombre { get; set; }
        public List<Motor3DCore.Vector3D> Vertices { get; set; }
        public List<int[]> Caras { get; set; }
        public Color Color { get; set; }

        // Transformaciones
        public Motor3DCore.Vector3D Posicion { get; set; }
        public Motor3DCore.Vector3D Rotacion { get; set; }
        public Motor3DCore.Vector3D Escala { get; set; }

        public Figura3D()
        {
            Vertices = new List<Motor3DCore.Vector3D>();
            Caras = new List<int[]>();
            Color = Color.White;
            Posicion = new Motor3DCore.Vector3D(0, 0, 0);
            Rotacion = new Motor3DCore.Vector3D(0, 0, 0);
            Escala = new Motor3DCore.Vector3D(1, 1, 1);
        }

        /// <summary>
        /// Obtiene la matriz de transformación completa del objeto.
        /// </summary>
        public Motor3DCore.Matrix4x4 ObtenerMatrizTransformacion()
        {
            var escala = Motor3DCore.Escalamiento(Escala.X, Escala.Y, Escala.Z);
            var rotX = Motor3DCore.RotacionX(Rotacion.X);
            var rotY = Motor3DCore.RotacionY(Rotacion.Y);
            var rotZ = Motor3DCore.RotacionZ(Rotacion.Z);
            var traslacion = Motor3DCore.Traslacion(Posicion.X, Posicion.Y, Posicion.Z);

            return traslacion * rotY * rotX * rotZ * escala;
        }

        /// <summary>
        /// Calcula la normal de una cara para iluminación.
        /// </summary>
        public Motor3DCore.Vector3D CalcularNormal(int[] cara)
        {
            if (cara.Length < 3) return new Motor3DCore.Vector3D(0, 0, 1);

            var v1 = Vertices[cara[1]] - Vertices[cara[0]];
            var v2 = Vertices[cara[2]] - Vertices[cara[0]];

            return Motor3DCore.Vector3D.ProductoCruz(v1, v2).Normalizar();
        }
    }
}
