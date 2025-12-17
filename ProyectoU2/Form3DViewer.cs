using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Motor3D.Motor3DCore;

namespace Motor3D
{
    /// <summary>
    /// Formulario principal del visor 3D con renderizado y controles interactivos.
    /// </summary>
    public partial class Form3DViewer : Form
    {
        // Buffer de renderizado
        private Bitmap bufferRenderizado;
        private Graphics graphicsBuffer;

        // Sistema 3D
        private Camara3D camara;
        private List<Figura3D> escena;
        private Figura3D figuraSeleccionada;
        private Vector3D direccionLuz;

        // Estado de interacción
        private Point ultimoMousePos;
        private bool arrastrando = false;
        private bool iluminacionActiva = true;
        private bool mostrarGrid = true;
        private bool modoWireframe = false;

        // Timer para animación/actualización
        private Timer timerActualizacion;

        public Form3DViewer()
        {
            InitializeComponent();
            InicializarSistema3D();
        }

        /// <summary>
        /// Inicializa el sistema 3D completo.
        /// </summary>
        private void InicializarSistema3D()
        {
            try
            {
                // Configurar viewport
                if (pictureBoxViewport.Width > 0 && pictureBoxViewport.Height > 0)
                {
                    bufferRenderizado = new Bitmap(pictureBoxViewport.Width, pictureBoxViewport.Height);
                    graphicsBuffer = Graphics.FromImage(bufferRenderizado);
                    graphicsBuffer.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                }

                // Inicializar cámara
                camara = new Camara3D();
                camara.CambiarModo(Camara3D.ModoCamara.Orbital);

                // Inicializar escena
                escena = new List<Figura3D>();
                direccionLuz = new Vector3D(-1, -1, -1).Normalizar();

                // Agregar figuras iniciales
                AgregarFiguraInicial();

                // Configurar timer de actualización
                timerActualizacion = new Timer();
                timerActualizacion.Interval = 16; // ~60 FPS
                timerActualizacion.Tick += TimerActualizacion_Tick;
                timerActualizacion.Start();

                // Actualizar UI
                ActualizarListaFiguras();
                ActualizarInfoCamara();

                // Renderizado inicial
                RenderizarEscena();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Agrega una figura inicial a la escena.
        /// </summary>
        private void AgregarFiguraInicial()
        {
            var cubo = Figuras3DFactory.CrearCubo(1.5f);
            cubo.Color = Color.DeepSkyBlue;
            AgregarFigura(cubo);
        }

        /// <summary>
        /// Timer para actualización continua.
        /// </summary>
        private void TimerActualizacion_Tick(object sender, EventArgs e)
        {
            ActualizarInfoCamara();
        }

        /// <summary>
        /// Agrega una figura a la escena.
        /// </summary>
        private void AgregarFigura(Figura3D figura)
        {
            if (figura == null) return;

            escena.Add(figura);
            ActualizarListaFiguras();

            if (figuraSeleccionada == null && escena.Count > 0)
            {
                SeleccionarFigura(0);
            }

            RenderizarEscena();
        }

        /// <summary>
        /// Elimina la figura seleccionada.
        /// </summary>
        private void EliminarFiguraSeleccionada()
        {
            if (figuraSeleccionada == null) return;

            escena.Remove(figuraSeleccionada);
            figuraSeleccionada = null;

            ActualizarListaFiguras();

            if (escena.Count > 0)
            {
                SeleccionarFigura(0);
            }
            else
            {
                DeshabilitarControlesTransformacion();
            }

            RenderizarEscena();
        }

        /// <summary>
        /// Selecciona una figura por índice.
        /// </summary>
        private void SeleccionarFigura(int indice)
        {
            if (indice < 0 || indice >= escena.Count) return;

            figuraSeleccionada = escena[indice];
            listBoxFiguras.SelectedIndex = indice;

            ActualizarControlesTransformacion();
            RenderizarEscena();
        }

        /// <summary>
        /// Actualiza la lista de figuras en la UI.
        /// </summary>
        private void ActualizarListaFiguras()
        {
            listBoxFiguras.Items.Clear();

            for (int i = 0; i < escena.Count; i++)
            {
                listBoxFiguras.Items.Add($"{i + 1}. {escena[i].Nombre}");
            }
        }

        /// <summary>
        /// Actualiza los controles de transformación con los valores de la figura seleccionada.
        /// </summary>
        private void ActualizarControlesTransformacion()
        {
            if (figuraSeleccionada == null)
            {
                DeshabilitarControlesTransformacion();
                return;
            }

            HabilitarControlesTransformacion();

            // Posición
            numericPosX.Value = (decimal)figuraSeleccionada.Posicion.X;
            numericPosY.Value = (decimal)figuraSeleccionada.Posicion.Y;
            numericPosZ.Value = (decimal)figuraSeleccionada.Posicion.Z;

            // Rotación
            trackBarRotX.Value = (int)figuraSeleccionada.Rotacion.X;
            trackBarRotY.Value = (int)figuraSeleccionada.Rotacion.Y;
            trackBarRotZ.Value = (int)figuraSeleccionada.Rotacion.Z;

            lblRotX.Text = $"X: {(int)figuraSeleccionada.Rotacion.X}°";
            lblRotY.Text = $"Y: {(int)figuraSeleccionada.Rotacion.Y}°";
            lblRotZ.Text = $"Z: {(int)figuraSeleccionada.Rotacion.Z}°";

            // Escala
            numericEscalaX.Value = (decimal)figuraSeleccionada.Escala.X;
            numericEscalaY.Value = (decimal)figuraSeleccionada.Escala.Y;
            numericEscalaZ.Value = (decimal)figuraSeleccionada.Escala.Z;

            // Color
            btnColor.BackColor = figuraSeleccionada.Color;
        }

        private void HabilitarControlesTransformacion()
        {
            numericPosX.Enabled = numericPosY.Enabled = numericPosZ.Enabled = true;
            trackBarRotX.Enabled = trackBarRotY.Enabled = trackBarRotZ.Enabled = true;
            numericEscalaX.Enabled = numericEscalaY.Enabled = numericEscalaZ.Enabled = true;
            btnColor.Enabled = true;
        }

        private void DeshabilitarControlesTransformacion()
        {
            numericPosX.Enabled = numericPosY.Enabled = numericPosZ.Enabled = false;
            trackBarRotX.Enabled = trackBarRotY.Enabled = trackBarRotZ.Enabled = false;
            numericEscalaX.Enabled = numericEscalaY.Enabled = numericEscalaZ.Enabled = false;
            btnColor.Enabled = false;
        }

        /// <summary>
        /// Actualiza la información de la cámara.
        /// </summary>
        private void ActualizarInfoCamara()
        {
            if (lblInfoCamara == null || camara == null) return;

            lblInfoCamara.Text = camara.ObtenerInfo();
        }

        /// <summary>
        /// Renderiza toda la escena 3D.
        /// </summary>
        private void RenderizarEscena()
        {
            if (graphicsBuffer == null || bufferRenderizado == null) return;

            try
            {
                // Limpiar buffer
                graphicsBuffer.Clear(Color.FromArgb(20, 20, 30));

                // Dibujar grid si está activo
                if (mostrarGrid)
                {
                    DibujarGrid();
                }

                // Obtener matrices de transformación
                var matrizVista = camara.ObtenerMatrizVista();
                float aspectRatio = (float)pictureBoxViewport.Width / pictureBoxViewport.Height;
                var matrizProyeccion = camara.ObtenerMatrizProyeccion(aspectRatio);

                // Renderizar cada figura
                foreach (var figura in escena)
                {
                    DibujarFigura(figura, matrizVista, matrizProyeccion);
                }

                // Resaltar figura seleccionada
                if (figuraSeleccionada != null)
                {
                    DibujarBoundingBox(figuraSeleccionada, matrizVista, matrizProyeccion);
                }

                // Copiar buffer a pantalla
                pictureBoxViewport.Image = (Bitmap)bufferRenderizado.Clone();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en renderizado: {ex.Message}");
            }
        }

        /// <summary>
        /// Dibuja una figura 3D.
        /// </summary>
        private void DibujarFigura(Figura3D figura, Matrix4x4 matrizVista, Matrix4x4 matrizProyeccion)
        {
            var matrizModelo = figura.ObtenerMatrizTransformacion();
            var matrizCompleta = matrizProyeccion * matrizVista * matrizModelo;

            // Transformar vértices
            var verticesTransformados = figura.Vertices
                .Select(v => matrizCompleta.TransformarPunto(v))
                .ToList();

            // Ordenar caras por profundidad (Painter's algorithm)
            var carasOrdenadas = figura.Caras
                .Select(cara => new
                {
                    Cara = cara,
                    Profundidad = cara.Take(3).Average(i => verticesTransformados[i].Z)
                })
                .OrderByDescending(x => x.Profundidad)
                .Select(x => x.Cara);

            // Dibujar cada cara
            foreach (var cara in carasOrdenadas)
            {
                // Back-face culling
                var normal = figura.CalcularNormal(cara);
                var normalTransformada = matrizVista.TransformarPunto(normal);

                if (normalTransformada.Z <= 0) continue; // No dibujar caras traseras

                // Calcular color con iluminación
                Color colorFinal = figura.Color;
                if (iluminacionActiva)
                {
                    colorFinal = CalcularIluminacion(normal, direccionLuz, figura.Color, 0.8f);
                }

                // Convertir a coordenadas de pantalla
                var puntos2D = cara
                    .Select(i => A2D(verticesTransformados[i], pictureBoxViewport.Width, pictureBoxViewport.Height))
                    .ToArray();

                // Dibujar cara
                if (modoWireframe)
                {
                    using (Pen pen = new Pen(colorFinal, 1))
                    {
                        for (int i = 0; i < puntos2D.Length; i++)
                        {
                            var p1 = puntos2D[i];
                            var p2 = puntos2D[(i + 1) % puntos2D.Length];
                            graphicsBuffer.DrawLine(pen, p1, p2);
                        }
                    }
                }
                else
                {
                    using (Brush brush = new SolidBrush(colorFinal))
                    using (Pen pen = new Pen(Color.FromArgb(100, colorFinal), 1))
                    {
                        graphicsBuffer.FillPolygon(brush, puntos2D);
                        graphicsBuffer.DrawPolygon(pen, puntos2D);
                    }
                }
            }
        }

        /// <summary>
        /// Dibuja una caja delimitadora alrededor de la figura seleccionada.
        /// </summary>
        private void DibujarBoundingBox(Figura3D figura, Matrix4x4 matrizVista, Matrix4x4 matrizProyeccion)
        {
            // Calcular AABB (Axis-Aligned Bounding Box)
            float minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;

            var matrizModelo = figura.ObtenerMatrizTransformacion();

            foreach (var v in figura.Vertices)
            {
                var vTransformado = matrizModelo.TransformarPunto(v);
                minX = Math.Min(minX, vTransformado.X);
                minY = Math.Min(minY, vTransformado.Y);
                minZ = Math.Min(minZ, vTransformado.Z);
                maxX = Math.Max(maxX, vTransformado.X);
                maxY = Math.Max(maxY, vTransformado.Y);
                maxZ = Math.Max(maxZ, vTransformado.Z);
            }

            // 8 vértices de la caja
            Vector3D[] vertices = {
                new Vector3D(minX, minY, minZ),
                new Vector3D(maxX, minY, minZ),
                new Vector3D(maxX, maxY, minZ),
                new Vector3D(minX, maxY, minZ),
                new Vector3D(minX, minY, maxZ),
                new Vector3D(maxX, minY, maxZ),
                new Vector3D(maxX, maxY, maxZ),
                new Vector3D(minX, maxY, maxZ)
            };

            var matrizCompleta = matrizProyeccion * matrizVista;
            var verticesProyectados = vertices.Select(v => {
                var vProyectado = matrizCompleta.TransformarPunto(v);
                return A2D(vProyectado, pictureBoxViewport.Width, pictureBoxViewport.Height);
            }).ToArray();

            // Dibujar aristas de la caja
            int[][] aristas = {
                new[] {0,1}, new[] {1,2}, new[] {2,3}, new[] {3,0},
                new[] {4,5}, new[] {5,6}, new[] {6,7}, new[] {7,4},
                new[] {0,4}, new[] {1,5}, new[] {2,6}, new[] {3,7}
            };

            using (Pen pen = new Pen(Color.Yellow, 2))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                foreach (var arista in aristas)
                {
                    graphicsBuffer.DrawLine(pen, verticesProyectados[arista[0]], verticesProyectados[arista[1]]);
                }
            }
        }

        /// <summary>
        /// Dibuja un grid de referencia en el suelo.
        /// </summary>
        private void DibujarGrid()
        {
            int tamaño = 10;
            float paso = 1f;

            var matrizVista = camara.ObtenerMatrizVista();
            float aspectRatio = (float)pictureBoxViewport.Width / pictureBoxViewport.Height;
            var matrizProyeccion = camara.ObtenerMatrizProyeccion(aspectRatio);
            var matrizCompleta = matrizProyeccion * matrizVista;

            using (Pen penGrid = new Pen(Color.FromArgb(50, Color.Gray), 1))
            {
                for (int i = -tamaño; i <= tamaño; i++)
                {
                    // Líneas paralelas al eje X
                    var p1 = matrizCompleta.TransformarPunto(new Vector3D(-tamaño * paso, 0, i * paso));
                    var p2 = matrizCompleta.TransformarPunto(new Vector3D(tamaño * paso, 0, i * paso));

                    var p1_2d = A2D(p1, pictureBoxViewport.Width, pictureBoxViewport.Height);
                    var p2_2d = A2D(p2, pictureBoxViewport.Width, pictureBoxViewport.Height);

                    graphicsBuffer.DrawLine(penGrid, p1_2d, p2_2d);

                    // Líneas paralelas al eje Z
                    var p3 = matrizCompleta.TransformarPunto(new Vector3D(i * paso, 0, -tamaño * paso));
                    var p4 = matrizCompleta.TransformarPunto(new Vector3D(i * paso, 0, tamaño * paso));

                    var p3_2d = A2D(p3, pictureBoxViewport.Width, pictureBoxViewport.Height);
                    var p4_2d = A2D(p4, pictureBoxViewport.Width, pictureBoxViewport.Height);

                    graphicsBuffer.DrawLine(penGrid, p3_2d, p4_2d);
                }
            }
        }

        // EVENTOS DE UI

        private void listBoxFiguras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFiguras.SelectedIndex >= 0)
            {
                SeleccionarFigura(listBoxFiguras.SelectedIndex);
            }
        }

        private void btnAgregarFigura_Click(object sender, EventArgs e)
        {
            var formAgregar = new FormAgregarFigura();
            if (formAgregar.ShowDialog() == DialogResult.OK)
            {
                AgregarFigura(formAgregar.FiguraCreada);
            }
        }

        private void btnEliminarFigura_Click(object sender, EventArgs e)
        {
            EliminarFiguraSeleccionada();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;

            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = figuraSeleccionada.Color;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    figuraSeleccionada.Color = dlg.Color;
                    btnColor.BackColor = dlg.Color;
                    RenderizarEscena();
                }
            }
        }

        // Eventos de transformación
        private void numericPosX_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Posicion = new Vector3D((float)numericPosX.Value, figuraSeleccionada.Posicion.Y, figuraSeleccionada.Posicion.Z);
            RenderizarEscena();
        }

        private void numericPosY_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Posicion = new Vector3D(figuraSeleccionada.Posicion.X, (float)numericPosY.Value, figuraSeleccionada.Posicion.Z);
            RenderizarEscena();
        }

        private void numericPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Posicion = new Vector3D(figuraSeleccionada.Posicion.X, figuraSeleccionada.Posicion.Y, (float)numericPosZ.Value);
            RenderizarEscena();
        }

        private void trackBarRotX_Scroll(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Rotacion = new Vector3D(trackBarRotX.Value, figuraSeleccionada.Rotacion.Y, figuraSeleccionada.Rotacion.Z);
            lblRotX.Text = $"X: {trackBarRotX.Value}°";
            RenderizarEscena();
        }

        private void trackBarRotY_Scroll(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Rotacion = new Vector3D(figuraSeleccionada.Rotacion.X, trackBarRotY.Value, figuraSeleccionada.Rotacion.Z);
            lblRotY.Text = $"Y: {trackBarRotY.Value}°";
            RenderizarEscena();
        }

        private void trackBarRotZ_Scroll(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Rotacion = new Vector3D(figuraSeleccionada.Rotacion.X, figuraSeleccionada.Rotacion.Y, trackBarRotZ.Value);
            lblRotZ.Text = $"Z: {trackBarRotZ.Value}°";
            RenderizarEscena();
        }

        private void numericEscalaX_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Escala = new Vector3D((float)numericEscalaX.Value, figuraSeleccionada.Escala.Y, figuraSeleccionada.Escala.Z);
            RenderizarEscena();
        }

        private void numericEscalaY_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Escala = new Vector3D(figuraSeleccionada.Escala.X, (float)numericEscalaY.Value, figuraSeleccionada.Escala.Z);
            RenderizarEscena();
        }

        private void numericEscalaZ_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Escala = new Vector3D(figuraSeleccionada.Escala.X, figuraSeleccionada.Escala.Y, (float)numericEscalaZ.Value);
            RenderizarEscena();
        }

        // Eventos de mouse
        private void pictureBoxViewport_MouseDown(object sender, MouseEventArgs e)
        {
            ultimoMousePos = e.Location;
            arrastrando = true;
        }

        private void pictureBoxViewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (!arrastrando) return;

            float deltaX = e.X - ultimoMousePos.X;
            float deltaY = e.Y - ultimoMousePos.Y;

            if (camara.Modo == Camara3D.ModoCamara.Orbital)
            {
                camara.RotarOrbital(deltaX * 0.5f, deltaY * 0.5f);
                RenderizarEscena();
            }

            ultimoMousePos = e.Location;
        }

        private void pictureBoxViewport_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
        }

        private void pictureBoxViewport_MouseWheel(object sender, MouseEventArgs e)
        {
            if (camara.Modo == Camara3D.ModoCamara.Orbital)
            {
                camara.Zoom(-e.Delta * 0.001f);
                RenderizarEscena();
            }
        }

        // Menú de cámara
        private void btnCamaraOrbital_Click(object sender, EventArgs e)
        {
            camara.CambiarModo(Camara3D.ModoCamara.Orbital);
            ActualizarInfoCamara();
            RenderizarEscena();
        }

        private void btnCamaraLibre_Click(object sender, EventArgs e)
        {
            camara.CambiarModo(Camara3D.ModoCamara.Libre);
            ActualizarInfoCamara();
            RenderizarEscena();
        }

        private void btnCamaraFija_Click(object sender, EventArgs e)
        {
            camara.CambiarModo(Camara3D.ModoCamara.Fija);
            ActualizarInfoCamara();
            RenderizarEscena();
        }

        private void btnReiniciarCamara_Click(object sender, EventArgs e)
        {
            camara.Reiniciar();
            ActualizarInfoCamara();
            RenderizarEscena();
        }

        // Opciones de visualización
        private void checkBoxIluminacion_CheckedChanged(object sender, EventArgs e)
        {
            iluminacionActiva = checkBoxIluminacion.Checked;
            RenderizarEscena();
        }

        private void checkBoxGrid_CheckedChanged(object sender, EventArgs e)
        {
            mostrarGrid = checkBoxGrid.Checked;
            RenderizarEscena();
        }

        private void checkBoxWireframe_CheckedChanged(object sender, EventArgs e)
        {
            modoWireframe = checkBoxWireframe.Checked;
            RenderizarEscena();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (timerActualizacion != null)
            {
                timerActualizacion.Stop();
                timerActualizacion.Dispose();
            }

            if (graphicsBuffer != null)
            {
                graphicsBuffer.Dispose();
            }

            if (bufferRenderizado != null)
            {
                bufferRenderizado.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}