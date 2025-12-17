using System;
using System.Drawing;
using System.Windows.Forms;
using static Motor3D.Motor3DCore;

namespace Motor3D
{
    /// <summary>
    /// Formulario para agregar nuevas figuras 3D a la escena.
    /// </summary>
    public partial class FormAgregarFigura : Form
    {
        public Figura3D FiguraCreada { get; private set; }

        public FormAgregarFigura()
        {
            InitializeComponent();
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            // Seleccionar el primer elemento por defecto
            if (comboBoxTipo.Items.Count > 0)
            {
                comboBoxTipo.SelectedIndex = 0;
            }
        }

        private void PanelPreview_Paint(object sender, PaintEventArgs e)
        {
            if (comboBoxTipo.SelectedIndex < 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.FromArgb(20, 20, 30));

            // Dibujar una representación simple de la figura seleccionada
            string tipoFigura = comboBoxTipo.SelectedItem.ToString();
            int centerX = panelPreview.Width / 2;
            int centerY = panelPreview.Height / 2;
            int size = 50;

            using (Pen pen = new Pen(Color.DeepSkyBlue, 2))
            using (Brush brush = new SolidBrush(Color.FromArgb(100, Color.DeepSkyBlue)))
            {
                switch (tipoFigura)
                {
                    case "Cubo":
                        // Dibujar un cubo en perspectiva isométrica
                        Point[] front = {
                            new Point(centerX - size/2, centerY),
                            new Point(centerX + size/2, centerY),
                            new Point(centerX + size/2, centerY + size),
                            new Point(centerX - size/2, centerY + size)
                        };
                        Point[] back = {
                            new Point(centerX - size/2 + 20, centerY - 20),
                            new Point(centerX + size/2 + 20, centerY - 20),
                            new Point(centerX + size/2 + 20, centerY + size - 20),
                            new Point(centerX - size/2 + 20, centerY + size - 20)
                        };

                        g.FillPolygon(brush, front);
                        g.DrawPolygon(pen, front);
                        g.DrawPolygon(pen, back);
                        g.DrawLine(pen, front[0], back[0]);
                        g.DrawLine(pen, front[1], back[1]);
                        g.DrawLine(pen, front[2], back[2]);
                        g.DrawLine(pen, front[3], back[3]);
                        break;

                    case "Esfera":
                        g.FillEllipse(brush, centerX - size / 2, centerY - size / 2, size, size);
                        g.DrawEllipse(pen, centerX - size / 2, centerY - size / 2, size, size);
                        g.DrawEllipse(pen, centerX - size / 2 + 10, centerY - size / 2, size - 20, size);
                        g.DrawEllipse(pen, centerX - size / 2, centerY - size / 2 + 10, size, size - 20);
                        break;

                    case "Pirámide":
                        Point[] pyramid = {
                            new Point(centerX, centerY - size/2),
                            new Point(centerX - size/2, centerY + size/2),
                            new Point(centerX + size/2, centerY + size/2)
                        };
                        Point apex = new Point(centerX + 15, centerY - size / 3);

                        g.FillPolygon(brush, pyramid);
                        g.DrawPolygon(pen, pyramid);
                        g.DrawLine(pen, pyramid[0], apex);
                        g.DrawLine(pen, pyramid[1], apex);
                        g.DrawLine(pen, pyramid[2], apex);
                        break;

                    case "Cilindro":
                        g.FillEllipse(brush, centerX - size / 2, centerY - size / 2, size, size / 3);
                        g.FillRectangle(brush, centerX - size / 2, centerY - size / 2 + size / 6, size, size);
                        g.DrawEllipse(pen, centerX - size / 2, centerY - size / 2, size, size / 3);
                        g.DrawEllipse(pen, centerX - size / 2, centerY + size / 2, size, size / 3);
                        g.DrawLine(pen, centerX - size / 2, centerY - size / 2 + size / 6, centerX - size / 2, centerY + size / 2 + size / 6);
                        g.DrawLine(pen, centerX + size / 2, centerY - size / 2 + size / 6, centerX + size / 2, centerY + size / 2 + size / 6);
                        break;

                    case "Cono":
                        Point[] cone = {
                            new Point(centerX, centerY - size/2),
                            new Point(centerX - size/2, centerY + size/2),
                            new Point(centerX + size/2, centerY + size/2)
                        };
                        g.FillPolygon(brush, cone);
                        g.DrawPolygon(pen, cone);
                        g.DrawEllipse(pen, centerX - size / 2, centerY + size / 2 - 10, size, 20);
                        break;

                    case "Toroide":
                        using (Pen thickPen = new Pen(Color.DeepSkyBlue, 8))
                        {
                            g.DrawEllipse(thickPen, centerX - size / 2, centerY - size / 2, size, size);
                        }
                        g.DrawEllipse(pen, centerX - size / 2 + 10, centerY - size / 2, size - 20, size);
                        g.DrawEllipse(pen, centerX - size / 2, centerY - size / 2 + 10, size, size - 20);
                        break;

                    case "Plano":
                        Point[] plane = {
                            new Point(centerX - size/2, centerY - 10),
                            new Point(centerX + size/2, centerY - 10),
                            new Point(centerX + size/2 + 15, centerY + 10),
                            new Point(centerX - size/2 + 15, centerY + 10)
                        };
                        g.FillPolygon(brush, plane);
                        g.DrawPolygon(pen, plane);

                        // Dibujar grid en el plano
                        for (int i = 1; i < 4; i++)
                        {
                            float t = i / 4f;
                            int x1 = (int)(plane[0].X + (plane[1].X - plane[0].X) * t);
                            int y1 = (int)(plane[0].Y + (plane[1].Y - plane[0].Y) * t);
                            int x2 = (int)(plane[3].X + (plane[2].X - plane[3].X) * t);
                            int y2 = (int)(plane[3].Y + (plane[2].Y - plane[3].Y) * t);
                            g.DrawLine(pen, x1, y1, x2, y2);
                        }
                        break;
                }
            }

            // Dibujar nombre de la figura
            using (Font font = new Font("Arial", 9, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.White))
            {
                string text = tipoFigura;
                SizeF textSize = g.MeasureString(text, font);
                g.DrawString(text, font, textBrush,
                    (panelPreview.Width - textSize.Width) / 2,
                    panelPreview.Height - textSize.Height - 5);
            }
        }

        private void comboBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelPreview.Invalidate();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (comboBoxTipo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor selecciona un tipo de figura.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            float tamaño = (float)numericTamaño.Value;
            string tipo = comboBoxTipo.SelectedItem.ToString();

            // Crear la figura según el tipo seleccionado
            switch (tipo)
            {
                case "Cubo":
                    FiguraCreada = Figuras3DFactory.CrearCubo(tamaño);
                    break;
                case "Esfera":
                    FiguraCreada = Figuras3DFactory.CrearEsfera(tamaño,16);
                    break;
                case "Pirámide":
                    FiguraCreada = Figuras3DFactory.CrearPiramide(tamaño);
                    break;
                case "Cilindro":
                    FiguraCreada = Figuras3DFactory.CrearCilindro(tamaño / 2, tamaño, 16);
                    break;
                case "Cono":
                    FiguraCreada = Figuras3DFactory.CrearCono(tamaño / 2, tamaño, 16);
                    break;
                case "Toroide":
                    FiguraCreada = Figuras3DFactory.CrearToro(tamaño, tamaño / 3f, 16, 12);
                    break;
                /*case "Plano":
                    FiguraCreada = Figuras3DFactory.CrearPlano(tamaño, tamaño, 4, 4);
                    break;*/
                default:
                    FiguraCreada = Figuras3DFactory.CrearCubo(tamaño);
                    break;
            }

            // Asignar color aleatorio
            Random rnd = new Random();
            Color[] colores = {
                Color.DeepSkyBlue,
                Color.LimeGreen,
                Color.Orange,
                Color.HotPink,
                Color.Purple,
                Color.Yellow,
                Color.Cyan,
                Color.Red
            };
            FiguraCreada.Color = colores[rnd.Next(colores.Length)];

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}