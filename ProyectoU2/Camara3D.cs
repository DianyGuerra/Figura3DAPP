using System;
using static Motor3D.Motor3DCore;

namespace Motor3D
{
    /// <summary>
    /// Sistema de cámara 3D con modos orbital, libre y fija.
    /// </summary>
    public class Camara3D
    {
        public enum ModoCamara
        {
            Orbital,    // Gira alrededor de un punto objetivo
            Libre,      // Movimiento libre en todas direcciones
            Fija        // Cámara estática
        }

        // Propiedades de la cámara
        public Vector3D Posicion { get; set; }
        public Vector3D Objetivo { get; set; }
        public Vector3D Arriba { get; set; }
        public ModoCamara Modo { get; set; }

        // Parámetros de cámara orbital
        public float Distancia { get; set; }
        public float AnguloAzimutal { get; set; }  // Rotación horizontal (grados)
        public float AnguloPolar { get; set; }      // Rotación vertical (grados)

        // Parámetros de proyección
        public float CampoVision { get; set; }     // FOV en grados
        public float PlanoConjunto { get; set; }
        public float PlanoLejano { get; set; }

        public Camara3D()
        {
            // Inicialización por defecto
            Posicion = new Vector3D(0, 2, 5);
            Objetivo = new Vector3D(0, 0, 0);
            Arriba = new Vector3D(0, 1, 0);
            Modo = ModoCamara.Orbital;

            Distancia = 5f;
            AnguloAzimutal = 0f;
            AnguloPolar = 30f;

            CampoVision = 60f;
            PlanoConjunto = 0.1f;
            PlanoLejano = 100f;

            ActualizarPosicionOrbital();
        }

        /// <summary>
        /// Actualiza la posición de la cámara en modo orbital.
        /// </summary>
        public void ActualizarPosicionOrbital()
        {
            if (Modo != ModoCamara.Orbital) return;

            // Limitar ángulo polar para evitar gimbal lock
            AnguloPolar = Math.Max(1f, Math.Min(179f, AnguloPolar));

            float radAz = AnguloAzimutal * (float)Math.PI / 180f;
            float radPol = AnguloPolar * (float)Math.PI / 180f;

            float x = Distancia * (float)Math.Sin(radPol) * (float)Math.Cos(radAz);
            float y = Distancia * (float)Math.Cos(radPol);
            float z = Distancia * (float)Math.Sin(radPol) * (float)Math.Sin(radAz);

            Posicion = new Vector3D(
                Objetivo.X + x,
                Objetivo.Y + y,
                Objetivo.Z + z
            );
        }

        /// <summary>
        /// Rota la cámara orbital.
        /// </summary>
        public void RotarOrbital(float deltaAzimutal, float deltaPolar)
        {
            if (Modo != ModoCamara.Orbital) return;

            AnguloAzimutal += deltaAzimutal;
            AnguloPolar += deltaPolar;

            // Normalizar ángulo azimutal
            while (AnguloAzimutal > 360f) AnguloAzimutal -= 360f;
            while (AnguloAzimutal < 0f) AnguloAzimutal += 360f;

            ActualizarPosicionOrbital();
        }

        /// <summary>
        /// Ajusta la distancia de la cámara orbital (zoom).
        /// </summary>
        public void Zoom(float delta)
        {
            if (Modo != ModoCamara.Orbital) return;

            Distancia += delta;
            Distancia = Math.Max(1f, Math.Min(50f, Distancia));
            ActualizarPosicionOrbital();
        }

        /// <summary>
        /// Mueve la cámara en modo libre.
        /// </summary>
        public void MoverLibre(float adelante, float derecha, float arriba)
        {
            if (Modo != ModoCamara.Libre) return;

            // Calcular vectores de dirección
            Vector3D direccion = (Objetivo - Posicion).Normalizar();
            Vector3D lateral = Vector3D.ProductoCruz(direccion, Arriba).Normalizar();
            Vector3D vertical = Vector3D.ProductoCruz(lateral, direccion).Normalizar();

            // Aplicar movimiento
            Posicion = Posicion + direccion * adelante + lateral * derecha + vertical * arriba;
            Objetivo = Objetivo + direccion * adelante + lateral * derecha + vertical * arriba;
        }

        /// <summary>
        /// Rota la cámara en modo libre.
        /// </summary>
        public void RotarLibre(float horizontal, float vertical)
        {
            if (Modo != ModoCamara.Libre) return;

            Vector3D direccion = (Objetivo - Posicion).Normalizar();
            float distancia = (Objetivo - Posicion).Magnitud();

            // Rotación horizontal
            Matrix4x4 rotY = RotacionY(horizontal);
            direccion = rotY.TransformarPunto(direccion);

            // Rotación vertical
            Vector3D lateral = Vector3D.ProductoCruz(direccion, Arriba).Normalizar();
            float radV = vertical * (float)Math.PI / 180f;
            float cosV = (float)Math.Cos(radV);
            float sinV = (float)Math.Sin(radV);

            Vector3D nuevaDireccion = direccion * cosV + Arriba * sinV;
            direccion = nuevaDireccion.Normalizar();

            Objetivo = Posicion + direccion * distancia;
        }

        /// <summary>
        /// Obtiene la matriz de vista de la cámara.
        /// </summary>
        public Matrix4x4 ObtenerMatrizVista()
        {
            return LookAt(Posicion, Objetivo, Arriba);
        }

        /// <summary>
        /// Obtiene la matriz de proyección.
        /// </summary>
        public Matrix4x4 ObtenerMatrizProyeccion(float aspectRatio)
        {
            return ProyeccionPerspectiva(CampoVision, aspectRatio, PlanoConjunto, PlanoLejano);
        }

        /// <summary>
        /// Reinicia la cámara a valores por defecto según el modo.
        /// </summary>
        public void Reiniciar()
        {
            Objetivo = new Vector3D(0, 0, 0);
            Arriba = new Vector3D(0, 1, 0);

            switch (Modo)
            {
                case ModoCamara.Orbital:
                    Distancia = 5f;
                    AnguloAzimutal = 0f;
                    AnguloPolar = 30f;
                    ActualizarPosicionOrbital();
                    break;

                case ModoCamara.Libre:
                    Posicion = new Vector3D(0, 2, 5);
                    break;

                case ModoCamara.Fija:
                    Posicion = new Vector3D(0, 3, 8);
                    break;
            }
        }

        /// <summary>
        /// Cambia el modo de la cámara.
        /// </summary>
        public void CambiarModo(ModoCamara nuevoModo)
        {
            Modo = nuevoModo;
            Reiniciar();
        }

        /// <summary>
        /// Establece el objetivo de la cámara (útil para enfocar objetos).
        /// </summary>
        public void EnfocarObjetivo(Vector3D nuevoObjetivo)
        {
            Objetivo = nuevoObjetivo;

            if (Modo == ModoCamara.Orbital)
            {
                ActualizarPosicionOrbital();
            }
        }

        /// <summary>
        /// Obtiene la dirección de la cámara (vector adelante).
        /// </summary>
        public Vector3D ObtenerDireccion()
        {
            return (Objetivo - Posicion).Normalizar();
        }

        /// <summary>
        /// Obtiene información de la cámara para UI.
        /// </summary>
        public string ObtenerInfo()
        {
            string info = $"Modo: {Modo}\n";
            info += $"Posición: ({Posicion.X:F2}, {Posicion.Y:F2}, {Posicion.Z:F2})\n";
            info += $"Objetivo: ({Objetivo.X:F2}, {Objetivo.Y:F2}, {Objetivo.Z:F2})\n";

            if (Modo == ModoCamara.Orbital)
            {
                info += $"Distancia: {Distancia:F2}\n";
                info += $"Ángulos: Az={AnguloAzimutal:F1}° Pol={AnguloPolar:F1}°";
            }

            return info;
        }
    }
}