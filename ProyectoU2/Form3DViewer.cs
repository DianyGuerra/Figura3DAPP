using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
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
        private Timer timerRender;
        private bool necesitaRender = true;

        private readonly Dictionary<int, SolidBrush> _brushCache = new Dictionary<int, SolidBrush>();
        private readonly Dictionary<int, Pen> _penCache = new Dictionary<int, Pen>();

        private readonly PointF[] _tmp3 = new PointF[3];
        private readonly PointF[] _tmp4 = new PointF[4];

        private readonly HashSet<Keys> _keys = new HashSet<Keys>();
        private bool _look = false;

        private struct FaceKey
        {
            public int Index;
            public float Depth;
        }


        public Form3DViewer()
        {
            InitializeComponent();
            InicializarSistema3D();

            typeof(PictureBox).InvokeMember(
            "DoubleBuffered",
            System.Reflection.BindingFlags.SetProperty
            | System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.NonPublic,
            null,
            pictureBoxViewport,
            new object[] { true }
            );

            this.KeyPreview = true;
            this.KeyDown += Form3DViewer_KeyDown;
            this.KeyUp += Form3DViewer_KeyUp;

        }

        /// <summary>
        /// Inicializa el sistema 3D completo.
        /// </summary>
        private void InicializarSistema3D()
        {
            try
            {
                // Configurar viewport
                pictureBoxViewport.Paint -= pictureBoxViewport_Paint;
                pictureBoxViewport.Paint += pictureBoxViewport_Paint;
                pictureBoxViewport.Resize += (s, e) => RecrearBuffer();

                pictureBoxViewport.BackColor = Color.FromArgb(20, 20, 30);

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
                timerRender = new Timer();
                timerRender.Interval = 16; // 60fps máx
                timerRender.Tick += (s, e) =>
                {
                    bool huboMovimiento = ProcesarMovimientoLibre();

                    if (huboMovimiento) necesitaRender = true;

                    if (!necesitaRender) return;
                    necesitaRender = false;
                    RenderizarEscena();
                };
                timerRender.Start();

                // Actualizar UI
                ActualizarListaFiguras();
                ActualizarInfoCamara();

                // Renderizado inicial
                SolicitarRender();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SolicitarRender() => necesitaRender = true;


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

            SolicitarRender();
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

            SolicitarRender();
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
            SolicitarRender();
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
                pictureBoxViewport.Invalidate();

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
            var matrizModelView = matrizVista * matrizModelo;

            int n = figura.Vertices.Count;
            if (n <= 0) return;

            // Arrays reutilizables por llamada
            var vView = new Vector3D[n];
            var vClip = new Vector3D[n];

            // 1) Transformar a VIEW
            for (int i = 0; i < n; i++)
                vView[i] = matrizModelView.TransformarPunto(figura.Vertices[i]);

            // 2) Proyectar a CLIP
            for (int i = 0; i < n; i++)
                vClip[i] = matrizProyeccion.TransformarPunto(vView[i]);

            // 3) Ordenar caras por profundidad (Painter)
            int faceCount = figura.Caras.Count;
            var keys = new FaceKey[faceCount];

            for (int fi = 0; fi < faceCount; fi++)
            {
                var cara = figura.Caras[fi];
                float d = vView[cara[0]].Z + vView[cara[1]].Z + vView[cara[2]].Z;
                keys[fi] = new FaceKey { Index = fi, Depth = d / 3f };
            }

            Array.Sort(keys, (a, b) => a.Depth.CompareTo(b.Depth));

            // 4) Dibujar caras
            for (int k = 0; k < faceCount; k++)
            {
                var cara = figura.Caras[keys[k].Index];
                int m = cara.Length;
                if (m < 3) continue;

                // Normal en VIEW (culling)
                var a = vView[cara[0]];
                var b = vView[cara[1]];
                var c = vView[cara[2]];
                var normalView = Vector3D.ProductoCruz(b - a, c - a).Normalizar();
                if (normalView.Z <= 0) continue;

                Color colorFinal = figura.Color;

                if (iluminacionActiva)
                {
                    // Normal
                    var aw = matrizModelo.TransformarPunto(figura.Vertices[cara[0]]);
                    var bw = matrizModelo.TransformarPunto(figura.Vertices[cara[1]]);
                    var cw = matrizModelo.TransformarPunto(figura.Vertices[cara[2]]);
                    var normalWorld = Vector3D.ProductoCruz(bw - aw, cw - aw).Normalizar();

                    colorFinal = CalcularIluminacion(normalWorld, direccionLuz, figura.Color, 0.8f);
                }

                if (m == 3)
                {
                    _tmp3[0] = A2D(vClip[cara[0]], pictureBoxViewport.Width, pictureBoxViewport.Height);
                    _tmp3[1] = A2D(vClip[cara[1]], pictureBoxViewport.Width, pictureBoxViewport.Height);
                    _tmp3[2] = A2D(vClip[cara[2]], pictureBoxViewport.Width, pictureBoxViewport.Height);

                    if (modoWireframe)
                    {
                        var pen = GetPen(colorFinal, 1f);
                        graphicsBuffer.DrawPolygon(pen, _tmp3);
                    }
                    else
                    {
                        var brush = GetBrush(colorFinal);
                        var pen = GetPen(Color.FromArgb(100, colorFinal), 1f);
                        graphicsBuffer.FillPolygon(brush, _tmp3);
                        graphicsBuffer.DrawPolygon(pen, _tmp3);
                    }
                }
                else if (m == 4)
                {
                    _tmp4[0] = A2D(vClip[cara[0]], pictureBoxViewport.Width, pictureBoxViewport.Height);
                    _tmp4[1] = A2D(vClip[cara[1]], pictureBoxViewport.Width, pictureBoxViewport.Height);
                    _tmp4[2] = A2D(vClip[cara[2]], pictureBoxViewport.Width, pictureBoxViewport.Height);
                    _tmp4[3] = A2D(vClip[cara[3]], pictureBoxViewport.Width, pictureBoxViewport.Height);

                    if (modoWireframe)
                    {
                        var pen = GetPen(colorFinal, 1f);
                        graphicsBuffer.DrawPolygon(pen, _tmp4);
                    }
                    else
                    {
                        var brush = GetBrush(colorFinal);
                        var pen = GetPen(Color.FromArgb(100, colorFinal), 1f);
                        graphicsBuffer.FillPolygon(brush, _tmp4);
                        graphicsBuffer.DrawPolygon(pen, _tmp4);
                    }
                }
                else
                {
                    continue;
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

        /// <summary>
        /// Método para recrear el buffer de renderizado al cambiar el tamaño del viewport.
        /// </summary>
        private void RecrearBuffer()
        {
            graphicsBuffer?.Dispose();
            bufferRenderizado?.Dispose();

            if (pictureBoxViewport.Width <= 0 || pictureBoxViewport.Height <= 0) return;

            bufferRenderizado = new Bitmap(pictureBoxViewport.Width, pictureBoxViewport.Height);
            graphicsBuffer = Graphics.FromImage(bufferRenderizado);
            graphicsBuffer.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SolicitarRender();
        }


        private SolidBrush GetBrush(Color c)
        {
            int key = c.ToArgb();
            SolidBrush b;
            if (!_brushCache.TryGetValue(key, out b))
            {
                b = new SolidBrush(c);
                _brushCache[key] = b;
            }
            return b;
        }

        private Pen GetPen(Color c, float w)
        {
            int key = (c.ToArgb() * 397) ^ w.GetHashCode();
            Pen p;
            if (!_penCache.TryGetValue(key, out p))
            {
                p = new Pen(c, w);
                _penCache[key] = p;
            }
            return p;
        }

        // Método para procesar el movimiento de la cámara en modo libre
        private bool ProcesarMovimientoLibre()
        {
            if (camara == null || camara.Modo != Camara3D.ModoCamara.Libre) return false;

            float speed = 0.08f;
            if (_keys.Contains(Keys.ShiftKey)) speed *= 2.0f;

            float adelante = 0, derecha = 0, arriba = 0;

            if (_keys.Contains(Keys.W)) adelante += speed;
            if (_keys.Contains(Keys.S)) adelante -= speed;

            if (_keys.Contains(Keys.D)) derecha += speed;
            if (_keys.Contains(Keys.A)) derecha -= speed;

            if (_keys.Contains(Keys.E)) arriba += speed;
            if (_keys.Contains(Keys.Q)) arriba -= speed;

            if (adelante != 0 || derecha != 0 || arriba != 0)
            {
                camara.MoverLibre(adelante, derecha, arriba);
                return true;
            }

            return false;
        }

        private void ActualizarAyudaCamara()
        {
            if (lblAyudaCamara == null || camara == null) return;

            switch (camara.Modo)
            {
                case Camara3D.ModoCamara.Orbital:
                    lblAyudaCamara.Text =
                        "CÁMARA ORBITAL\n" +
                        "- Arrastrar (Click IZQ + mover): rotar alrededor del objetivo\n" +
                        "- Rueda del mouse: zoom (acercar/alejar)\n" +
                        "- Reiniciar: vuelve a la vista por defecto";
                    break;

                case Camara3D.ModoCamara.Libre:
                    lblAyudaCamara.Text =
                        "CÁMARA LIBRE (FPS)\n" +
                        "- Click DER + arrastrar: mirar\n" +
                        "- W/S: adelante/atrás | A/D: izquierda/derecha\n" +
                        "- Q/E: bajar/subir | Shift: más rápido\n" +
                        "- Reiniciar: vuelve a la vista por defecto";
                    break;

                case Camara3D.ModoCamara.Fija:
                default:
                    lblAyudaCamara.Text =
                        "CÁMARA FIJA\n" +
                        "- No se mueve con mouse/teclas\n" +
                        "- Manipula las figuras con traslación/rotación/escala\n" +
                        "- Reiniciar: vuelve a la vista por defecto";
                    break;
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
                    SolicitarRender();
                }
            }
        }

        // Eventos de transformación
        private void numericPosX_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Posicion = new Vector3D((float)numericPosX.Value, figuraSeleccionada.Posicion.Y, figuraSeleccionada.Posicion.Z);
            SolicitarRender();
        }

        private void numericPosY_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Posicion = new Vector3D(figuraSeleccionada.Posicion.X, (float)numericPosY.Value, figuraSeleccionada.Posicion.Z);
            SolicitarRender();
        }

        private void numericPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Posicion = new Vector3D(figuraSeleccionada.Posicion.X, figuraSeleccionada.Posicion.Y, (float)numericPosZ.Value);
            SolicitarRender();
        }

        // Rotación

        private static float Normalize360(float deg)
        {
            deg %= 360f;
            if (deg < 0) deg += 360f;
            return deg;
        }

        private void trackBarRotX_Scroll(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;

            float x = Normalize360(trackBarRotX.Value);

            figuraSeleccionada.Rotacion = new Vector3D(x,
                figuraSeleccionada.Rotacion.Y,
                figuraSeleccionada.Rotacion.Z);

            lblRotX.Text = $"X: {(int)x}°";
            SolicitarRender();
        }


        private void trackBarRotY_Scroll(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;

            float y = Normalize360(trackBarRotY.Value);

            figuraSeleccionada.Rotacion = new Vector3D(figuraSeleccionada.Rotacion.X,y,
                figuraSeleccionada.Rotacion.Z);

            lblRotY.Text = $"Y: {(int)y}°";
            SolicitarRender();
        }

        private void trackBarRotZ_Scroll(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;

            float z = Normalize360(trackBarRotZ.Value);

            figuraSeleccionada.Rotacion = new Vector3D(figuraSeleccionada.Rotacion.X,
                figuraSeleccionada.Rotacion.Y,
                z);

            lblRotZ.Text = $"Z: {(int)z}°";
            SolicitarRender();  
        }

        private void numericEscalaX_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Escala = new Vector3D((float)numericEscalaX.Value, figuraSeleccionada.Escala.Y, figuraSeleccionada.Escala.Z);
            SolicitarRender();
        }

        private void numericEscalaY_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Escala = new Vector3D(figuraSeleccionada.Escala.X, (float)numericEscalaY.Value, figuraSeleccionada.Escala.Z);
            SolicitarRender();
        }

        private void numericEscalaZ_ValueChanged(object sender, EventArgs e)
        {
            if (figuraSeleccionada == null) return;
            figuraSeleccionada.Escala = new Vector3D(figuraSeleccionada.Escala.X, figuraSeleccionada.Escala.Y, (float)numericEscalaZ.Value);
            SolicitarRender();
        }

        // Eventos de mouse
        private void pictureBoxViewport_MouseDown(object sender, MouseEventArgs e)
        {
            ultimoMousePos = e.Location;

            if (camara == null) return;

            if (camara.Modo == Camara3D.ModoCamara.Orbital && e.Button == MouseButtons.Left)
            {
                arrastrando = true;
                pictureBoxViewport.Focus();
                SolicitarRender();
                return;
            }

            if (camara.Modo == Camara3D.ModoCamara.Libre && e.Button == MouseButtons.Right)
            {
                _look = true;
                pictureBoxViewport.Focus();
                SolicitarRender();
                return;
            }
        }



        private void pictureBoxViewport_MouseMove(object sender, MouseEventArgs e)
        {
            float dx = e.X - ultimoMousePos.X;
            float dy = e.Y - ultimoMousePos.Y;
            ultimoMousePos = e.Location;

            if (camara == null) return;

            if (camara.Modo == Camara3D.ModoCamara.Orbital && arrastrando)
            {
                camara.RotarOrbital(dx * 0.5f, dy * 0.5f);
                SolicitarRender();
                return;
            }

            if (camara.Modo == Camara3D.ModoCamara.Libre && _look)
            {
                // dy negativo para que “arriba” mire arriba
                camara.RotarLibre(dx * camara.SensMouse, -dy * camara.SensMouse);
                SolicitarRender();
                return;
            }
        }



        private void pictureBoxViewport_MouseUp(object sender, MouseEventArgs e)
        {
            if (camara == null) return;

            if (camara.Modo == Camara3D.ModoCamara.Orbital && e.Button == MouseButtons.Left)
                arrastrando = false;

            if (camara.Modo == Camara3D.ModoCamara.Libre && e.Button == MouseButtons.Right)
                _look = false;

            SolicitarRender();
        }



        private void pictureBoxViewport_MouseWheel(object sender, MouseEventArgs e)
        {
            if (camara.Modo == Camara3D.ModoCamara.Orbital)
            {
                camara.Zoom(-e.Delta * 0.001f);
                SolicitarRender();
            }
        }

        // Menú de cámara
        private void btnCamaraOrbital_Click(object sender, EventArgs e)
        {
            camara.CambiarModo(Camara3D.ModoCamara.Orbital);
            ActualizarInfoCamara();
            ActualizarAyudaCamara();
            SolicitarRender();
        }

        private void btnCamaraLibre_Click(object sender, EventArgs e)
        {
            camara.CambiarModo(Camara3D.ModoCamara.Libre);
            ActualizarInfoCamara();
            ActualizarAyudaCamara();
            SolicitarRender();
        }

        private void btnCamaraFija_Click(object sender, EventArgs e)
        {
            camara.CambiarModo(Camara3D.ModoCamara.Fija);
            ActualizarInfoCamara();
            ActualizarAyudaCamara();
            SolicitarRender();
        }

        private void btnReiniciarCamara_Click(object sender, EventArgs e)
        {
            camara.Reiniciar();
            ActualizarInfoCamara();
            SolicitarRender();
        }

        // Opciones de visualización
        private void checkBoxIluminacion_CheckedChanged(object sender, EventArgs e)
        {
            iluminacionActiva = checkBoxIluminacion.Checked;
            SolicitarRender();
        }

        private void checkBoxGrid_CheckedChanged(object sender, EventArgs e)
        {
            mostrarGrid = checkBoxGrid.Checked;
            SolicitarRender();
        }

        private void checkBoxWireframe_CheckedChanged(object sender, EventArgs e)
        {
            modoWireframe = checkBoxWireframe.Checked;
            SolicitarRender();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (graphicsBuffer != null)
            {
                graphicsBuffer.Dispose();
            }

            if (bufferRenderizado != null)
            {
                bufferRenderizado.Dispose();
            }
            timerRender?.Stop();
            timerRender?.Dispose();
            foreach (var b in _brushCache.Values) b.Dispose();
            foreach (var p in _penCache.Values) p.Dispose();
            _brushCache.Clear();
            _penCache.Clear();


            base.OnFormClosing(e);
        }

        private void pictureBoxViewport_Paint(object sender, PaintEventArgs e)
        {
            if (bufferRenderizado != null)
                e.Graphics.DrawImageUnscaled(bufferRenderizado, 0, 0);
        }

        private void Form3DViewer_KeyDown(object sender, KeyEventArgs e)
        {
            _keys.Add(e.KeyCode);
            SolicitarRender();
        }

        private void Form3DViewer_KeyUp(object sender, KeyEventArgs e)
        {
            _keys.Remove(e.KeyCode);
            SolicitarRender();
        }

    }
}
