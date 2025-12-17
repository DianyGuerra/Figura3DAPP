namespace Motor3D
{
    partial class Form3DViewer
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxViewport;
        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.GroupBox groupBoxFiguras;
        private System.Windows.Forms.ListBox listBoxFiguras;
        private System.Windows.Forms.Button btnAgregarFigura;
        private System.Windows.Forms.Button btnEliminarFigura;
        private System.Windows.Forms.GroupBox groupBoxCamara;
        private System.Windows.Forms.Button btnCamaraOrbital;
        private System.Windows.Forms.Button btnCamaraLibre;
        private System.Windows.Forms.Button btnCamaraFija;
        private System.Windows.Forms.Button btnReiniciarCamara;
        private System.Windows.Forms.Label lblInfoCamara;
        private System.Windows.Forms.GroupBox groupBoxOpciones;
        private System.Windows.Forms.CheckBox checkBoxIluminacion;
        private System.Windows.Forms.CheckBox checkBoxGrid;
        private System.Windows.Forms.CheckBox checkBoxWireframe;
        private System.Windows.Forms.Panel panelDerecho;
        private System.Windows.Forms.GroupBox groupBoxPropiedades;
        private System.Windows.Forms.TabControl tabControlPropiedades;
        private System.Windows.Forms.TabPage tabPagePosicion;
        private System.Windows.Forms.Label lblPosicion;
        private System.Windows.Forms.Label lblPosX;
        private System.Windows.Forms.NumericUpDown numericPosX;
        private System.Windows.Forms.Label lblPosY;
        private System.Windows.Forms.NumericUpDown numericPosY;
        private System.Windows.Forms.Label lblPosZ;
        private System.Windows.Forms.NumericUpDown numericPosZ;
        private System.Windows.Forms.TabPage tabPageRotacion;
        private System.Windows.Forms.Label lblRotacion;
        private System.Windows.Forms.Label lblRotX;
        private System.Windows.Forms.TrackBar trackBarRotX;
        private System.Windows.Forms.Label lblRotY;
        private System.Windows.Forms.TrackBar trackBarRotY;
        private System.Windows.Forms.Label lblRotZ;
        private System.Windows.Forms.TrackBar trackBarRotZ;
        private System.Windows.Forms.TabPage tabPageEscala;
        private System.Windows.Forms.Label lblEscala;
        private System.Windows.Forms.Label lblEscalaX;
        private System.Windows.Forms.NumericUpDown numericEscalaX;
        private System.Windows.Forms.Label lblEscalaY;
        private System.Windows.Forms.NumericUpDown numericEscalaY;
        private System.Windows.Forms.Label lblEscalaZ;
        private System.Windows.Forms.NumericUpDown numericEscalaZ;
        private System.Windows.Forms.TabPage tabPageColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnColor;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBoxViewport = new System.Windows.Forms.PictureBox();
            this.panelIzquierdo = new System.Windows.Forms.Panel();
            this.groupBoxOpciones = new System.Windows.Forms.GroupBox();
            this.checkBoxWireframe = new System.Windows.Forms.CheckBox();
            this.checkBoxGrid = new System.Windows.Forms.CheckBox();
            this.checkBoxIluminacion = new System.Windows.Forms.CheckBox();
            this.groupBoxCamara = new System.Windows.Forms.GroupBox();
            this.lblInfoCamara = new System.Windows.Forms.Label();
            this.btnReiniciarCamara = new System.Windows.Forms.Button();
            this.btnCamaraFija = new System.Windows.Forms.Button();
            this.btnCamaraLibre = new System.Windows.Forms.Button();
            this.btnCamaraOrbital = new System.Windows.Forms.Button();
            this.groupBoxFiguras = new System.Windows.Forms.GroupBox();
            this.btnEliminarFigura = new System.Windows.Forms.Button();
            this.btnAgregarFigura = new System.Windows.Forms.Button();
            this.listBoxFiguras = new System.Windows.Forms.ListBox();
            this.panelDerecho = new System.Windows.Forms.Panel();
            this.groupBoxPropiedades = new System.Windows.Forms.GroupBox();
            this.tabControlPropiedades = new System.Windows.Forms.TabControl();
            this.tabPagePosicion = new System.Windows.Forms.TabPage();
            this.numericPosZ = new System.Windows.Forms.NumericUpDown();
            this.lblPosZ = new System.Windows.Forms.Label();
            this.numericPosY = new System.Windows.Forms.NumericUpDown();
            this.lblPosY = new System.Windows.Forms.Label();
            this.numericPosX = new System.Windows.Forms.NumericUpDown();
            this.lblPosX = new System.Windows.Forms.Label();
            this.lblPosicion = new System.Windows.Forms.Label();
            this.tabPageRotacion = new System.Windows.Forms.TabPage();
            this.trackBarRotZ = new System.Windows.Forms.TrackBar();
            this.lblRotZ = new System.Windows.Forms.Label();
            this.trackBarRotY = new System.Windows.Forms.TrackBar();
            this.lblRotY = new System.Windows.Forms.Label();
            this.trackBarRotX = new System.Windows.Forms.TrackBar();
            this.lblRotX = new System.Windows.Forms.Label();
            this.lblRotacion = new System.Windows.Forms.Label();
            this.tabPageEscala = new System.Windows.Forms.TabPage();
            this.numericEscalaZ = new System.Windows.Forms.NumericUpDown();
            this.lblEscalaZ = new System.Windows.Forms.Label();
            this.numericEscalaY = new System.Windows.Forms.NumericUpDown();
            this.lblEscalaY = new System.Windows.Forms.Label();
            this.numericEscalaX = new System.Windows.Forms.NumericUpDown();
            this.lblEscalaX = new System.Windows.Forms.Label();
            this.lblEscala = new System.Windows.Forms.Label();
            this.tabPageColor = new System.Windows.Forms.TabPage();
            this.btnColor = new System.Windows.Forms.Button();
            this.lblColor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxViewport)).BeginInit();
            this.panelIzquierdo.SuspendLayout();
            this.groupBoxOpciones.SuspendLayout();
            this.groupBoxCamara.SuspendLayout();
            this.groupBoxFiguras.SuspendLayout();
            this.panelDerecho.SuspendLayout();
            this.groupBoxPropiedades.SuspendLayout();
            this.tabControlPropiedades.SuspendLayout();
            this.tabPagePosicion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPosX)).BeginInit();
            this.tabPageRotacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotX)).BeginInit();
            this.tabPageEscala.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericEscalaZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEscalaY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEscalaX)).BeginInit();
            this.tabPageColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxViewport
            // 
            this.pictureBoxViewport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(30)))));
            this.pictureBoxViewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxViewport.Location = new System.Drawing.Point(250, 0);
            this.pictureBoxViewport.Name = "pictureBoxViewport";
            this.pictureBoxViewport.Size = new System.Drawing.Size(800, 700);
            this.pictureBoxViewport.TabIndex = 0;
            this.pictureBoxViewport.TabStop = false;
            this.pictureBoxViewport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxViewport_MouseDown);
            this.pictureBoxViewport.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxViewport_MouseMove);
            this.pictureBoxViewport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxViewport_MouseUp);
            this.pictureBoxViewport.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBoxViewport_MouseWheel);
            // 
            // panelIzquierdo
            // 
            this.panelIzquierdo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelIzquierdo.Controls.Add(this.groupBoxOpciones);
            this.panelIzquierdo.Controls.Add(this.groupBoxCamara);
            this.panelIzquierdo.Controls.Add(this.groupBoxFiguras);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(250, 700);
            this.panelIzquierdo.TabIndex = 1;
            // 
            // groupBoxOpciones
            // 
            this.groupBoxOpciones.Controls.Add(this.checkBoxWireframe);
            this.groupBoxOpciones.Controls.Add(this.checkBoxGrid);
            this.groupBoxOpciones.Controls.Add(this.checkBoxIluminacion);
            this.groupBoxOpciones.ForeColor = System.Drawing.Color.White;
            this.groupBoxOpciones.Location = new System.Drawing.Point(12, 524);
            this.groupBoxOpciones.Name = "groupBoxOpciones";
            this.groupBoxOpciones.Size = new System.Drawing.Size(226, 120);
            this.groupBoxOpciones.TabIndex = 2;
            this.groupBoxOpciones.TabStop = false;
            this.groupBoxOpciones.Text = "Opciones de Visualización";
            // 
            // checkBoxWireframe
            // 
            this.checkBoxWireframe.AutoSize = true;
            this.checkBoxWireframe.Location = new System.Drawing.Point(15, 80);
            this.checkBoxWireframe.Name = "checkBoxWireframe";
            this.checkBoxWireframe.Size = new System.Drawing.Size(125, 19);
            this.checkBoxWireframe.TabIndex = 2;
            this.checkBoxWireframe.Text = "Modo Wireframe";
            this.checkBoxWireframe.UseVisualStyleBackColor = true;
            this.checkBoxWireframe.CheckedChanged += new System.EventHandler(this.checkBoxWireframe_CheckedChanged);
            // 
            // checkBoxGrid
            // 
            this.checkBoxGrid.AutoSize = true;
            this.checkBoxGrid.Checked = true;
            this.checkBoxGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGrid.Location = new System.Drawing.Point(15, 55);
            this.checkBoxGrid.Name = "checkBoxGrid";
            this.checkBoxGrid.Size = new System.Drawing.Size(94, 19);
            this.checkBoxGrid.TabIndex = 1;
            this.checkBoxGrid.Text = "Mostrar Grid";
            this.checkBoxGrid.UseVisualStyleBackColor = true;
            this.checkBoxGrid.CheckedChanged += new System.EventHandler(this.checkBoxGrid_CheckedChanged);
            // 
            // checkBoxIluminacion
            // 
            this.checkBoxIluminacion.AutoSize = true;
            this.checkBoxIluminacion.Checked = true;
            this.checkBoxIluminacion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIluminacion.Location = new System.Drawing.Point(15, 30);
            this.checkBoxIluminacion.Name = "checkBoxIluminacion";
            this.checkBoxIluminacion.Size = new System.Drawing.Size(90, 19);
            this.checkBoxIluminacion.TabIndex = 0;
            this.checkBoxIluminacion.Text = "Iluminación";
            this.checkBoxIluminacion.UseVisualStyleBackColor = true;
            this.checkBoxIluminacion.CheckedChanged += new System.EventHandler(this.checkBoxIluminacion_CheckedChanged);
            // 
            // groupBoxCamara
            // 
            this.groupBoxCamara.Controls.Add(this.lblInfoCamara);
            this.groupBoxCamara.Controls.Add(this.btnReiniciarCamara);
            this.groupBoxCamara.Controls.Add(this.btnCamaraFija);
            this.groupBoxCamara.Controls.Add(this.btnCamaraLibre);
            this.groupBoxCamara.Controls.Add(this.btnCamaraOrbital);
            this.groupBoxCamara.ForeColor = System.Drawing.Color.White;
            this.groupBoxCamara.Location = new System.Drawing.Point(12, 268);
            this.groupBoxCamara.Name = "groupBoxCamara";
            this.groupBoxCamara.Size = new System.Drawing.Size(226, 250);
            this.groupBoxCamara.TabIndex = 1;
            this.groupBoxCamara.TabStop = false;
            this.groupBoxCamara.Text = "Cámara 3D";
            // 
            // lblInfoCamara
            // 
            this.lblInfoCamara.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblInfoCamara.Location = new System.Drawing.Point(10, 175);
            this.lblInfoCamara.Name = "lblInfoCamara";
            this.lblInfoCamara.Size = new System.Drawing.Size(206, 65);
            this.lblInfoCamara.TabIndex = 4;
            this.lblInfoCamara.Text = "Info Cámara";
            // 
            // btnReiniciarCamara
            // 
            this.btnReiniciarCamara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnReiniciarCamara.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReiniciarCamara.Location = new System.Drawing.Point(10, 133);
            this.btnReiniciarCamara.Name = "btnReiniciarCamara";
            this.btnReiniciarCamara.Size = new System.Drawing.Size(206, 30);
            this.btnReiniciarCamara.TabIndex = 3;
            this.btnReiniciarCamara.Text = "↻ Reiniciar Cámara";
            this.btnReiniciarCamara.UseVisualStyleBackColor = false;
            this.btnReiniciarCamara.Click += new System.EventHandler(this.btnReiniciarCamara_Click);
            // 
            // btnCamaraFija
            // 
            this.btnCamaraFija.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCamaraFija.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCamaraFija.Location = new System.Drawing.Point(10, 97);
            this.btnCamaraFija.Name = "btnCamaraFija";
            this.btnCamaraFija.Size = new System.Drawing.Size(206, 30);
            this.btnCamaraFija.TabIndex = 2;
            this.btnCamaraFija.Text = "Modo Fijo";
            this.btnCamaraFija.UseVisualStyleBackColor = false;
            this.btnCamaraFija.Click += new System.EventHandler(this.btnCamaraFija_Click);
            // 
            // btnCamaraLibre
            // 
            this.btnCamaraLibre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCamaraLibre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCamaraLibre.Location = new System.Drawing.Point(10, 61);
            this.btnCamaraLibre.Name = "btnCamaraLibre";
            this.btnCamaraLibre.Size = new System.Drawing.Size(206, 30);
            this.btnCamaraLibre.TabIndex = 1;
            this.btnCamaraLibre.Text = "Modo Libre";
            this.btnCamaraLibre.UseVisualStyleBackColor = false;
            this.btnCamaraLibre.Click += new System.EventHandler(this.btnCamaraLibre_Click);
            // 
            // btnCamaraOrbital
            // 
            this.btnCamaraOrbital.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCamaraOrbital.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCamaraOrbital.Location = new System.Drawing.Point(10, 25);
            this.btnCamaraOrbital.Name = "btnCamaraOrbital";
            this.btnCamaraOrbital.Size = new System.Drawing.Size(206, 30);
            this.btnCamaraOrbital.TabIndex = 0;
            this.btnCamaraOrbital.Text = "Modo Orbital";
            this.btnCamaraOrbital.UseVisualStyleBackColor = false;
            this.btnCamaraOrbital.Click += new System.EventHandler(this.btnCamaraOrbital_Click);
            // 
            // groupBoxFiguras
            // 
            this.groupBoxFiguras.Controls.Add(this.btnEliminarFigura);
            this.groupBoxFiguras.Controls.Add(this.btnAgregarFigura);
            this.groupBoxFiguras.Controls.Add(this.listBoxFiguras);
            this.groupBoxFiguras.ForeColor = System.Drawing.Color.White;
            this.groupBoxFiguras.Location = new System.Drawing.Point(12, 12);
            this.groupBoxFiguras.Name = "groupBoxFiguras";
            this.groupBoxFiguras.Size = new System.Drawing.Size(226, 250);
            this.groupBoxFiguras.TabIndex = 0;
            this.groupBoxFiguras.TabStop = false;
            this.groupBoxFiguras.Text = "Figuras en Escena";
            // 
            // btnEliminarFigura
            // 
            this.btnEliminarFigura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnEliminarFigura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarFigura.ForeColor = System.Drawing.Color.White;
            this.btnEliminarFigura.Location = new System.Drawing.Point(116, 205);
            this.btnEliminarFigura.Name = "btnEliminarFigura";
            this.btnEliminarFigura.Size = new System.Drawing.Size(100, 30);
            this.btnEliminarFigura.TabIndex = 2;
            this.btnEliminarFigura.Text = "- Eliminar";
            this.btnEliminarFigura.UseVisualStyleBackColor = false;
            this.btnEliminarFigura.Click += new System.EventHandler(this.btnEliminarFigura_Click);
            // 
            // btnAgregarFigura
            // 
            this.btnAgregarFigura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAgregarFigura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarFigura.ForeColor = System.Drawing.Color.White;
            this.btnAgregarFigura.Location = new System.Drawing.Point(10, 205);
            this.btnAgregarFigura.Name = "btnAgregarFigura";
            this.btnAgregarFigura.Size = new System.Drawing.Size(100, 30);
            this.btnAgregarFigura.TabIndex = 1;
            this.btnAgregarFigura.Text = "+ Agregar";
            this.btnAgregarFigura.UseVisualStyleBackColor = false;
            this.btnAgregarFigura.Click += new System.EventHandler(this.btnAgregarFigura_Click);
            // 
            // listBoxFiguras
            // 
            this.listBoxFiguras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listBoxFiguras.ForeColor = System.Drawing.Color.White;
            this.listBoxFiguras.FormattingEnabled = true;
            this.listBoxFiguras.ItemHeight = 15;
            this.listBoxFiguras.Location = new System.Drawing.Point(10, 25);
            this.listBoxFiguras.Name = "listBoxFiguras";
            this.listBoxFiguras.Size = new System.Drawing.Size(206, 169);
            this.listBoxFiguras.TabIndex = 0;
            this.listBoxFiguras.SelectedIndexChanged += new System.EventHandler(this.listBoxFiguras_SelectedIndexChanged);
            // 
            // panelDerecho
            // 
            this.panelDerecho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelDerecho.Controls.Add(this.groupBoxPropiedades);
            this.panelDerecho.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDerecho.Location = new System.Drawing.Point(1050, 0);
            this.panelDerecho.Name = "panelDerecho";
            this.panelDerecho.Size = new System.Drawing.Size(250, 700);
            this.panelDerecho.TabIndex = 2;
            // 
            // groupBoxPropiedades
            // 
            this.groupBoxPropiedades.Controls.Add(this.tabControlPropiedades);
            this.groupBoxPropiedades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPropiedades.ForeColor = System.Drawing.Color.White;
            this.groupBoxPropiedades.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPropiedades.Name = "groupBoxPropiedades";
            this.groupBoxPropiedades.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxPropiedades.Size = new System.Drawing.Size(250, 700);
            this.groupBoxPropiedades.TabIndex = 0;
            this.groupBoxPropiedades.TabStop = false;
            this.groupBoxPropiedades.Text = "Propiedades de Figura";
            // 
            // tabControlPropiedades
            // 
            this.tabControlPropiedades.Controls.Add(this.tabPagePosicion);
            this.tabControlPropiedades.Controls.Add(this.tabPageRotacion);
            this.tabControlPropiedades.Controls.Add(this.tabPageEscala);
            this.tabControlPropiedades.Controls.Add(this.tabPageColor);
            this.tabControlPropiedades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPropiedades.Location = new System.Drawing.Point(10, 26);
            this.tabControlPropiedades.Name = "tabControlPropiedades";
            this.tabControlPropiedades.SelectedIndex = 0;
            this.tabControlPropiedades.Size = new System.Drawing.Size(230, 664);
            this.tabControlPropiedades.TabIndex = 0;
            // 
            // tabPagePosicion
            // 
            this.tabPagePosicion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tabPagePosicion.Controls.Add(this.numericPosZ);
            this.tabPagePosicion.Controls.Add(this.lblPosZ);
            this.tabPagePosicion.Controls.Add(this.numericPosY);
            this.tabPagePosicion.Controls.Add(this.lblPosY);
            this.tabPagePosicion.Controls.Add(this.numericPosX);
            this.tabPagePosicion.Controls.Add(this.lblPosX);
            this.tabPagePosicion.Controls.Add(this.lblPosicion);
            this.tabPagePosicion.Location = new System.Drawing.Point(4, 24);
            this.tabPagePosicion.Name = "tabPagePosicion";
            this.tabPagePosicion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePosicion.Size = new System.Drawing.Size(222, 636);
            this.tabPagePosicion.TabIndex = 0;
            this.tabPagePosicion.Text = "Posición";
            // 
            // numericPosZ
            // 
            this.numericPosZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericPosZ.DecimalPlaces = 2;
            this.numericPosZ.ForeColor = System.Drawing.Color.White;
            this.numericPosZ.Location = new System.Drawing.Point(45, 109);
            this.numericPosZ.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericPosZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericPosZ.Name = "numericPosZ";
            this.numericPosZ.Size = new System.Drawing.Size(165, 23);
            this.numericPosZ.TabIndex = 6;
            this.numericPosZ.ValueChanged += new System.EventHandler(this.numericPosZ_ValueChanged);
            // 
            // lblPosZ
            // 
            this.lblPosZ.ForeColor = System.Drawing.Color.White;
            this.lblPosZ.Location = new System.Drawing.Point(10, 111);
            this.lblPosZ.Name = "lblPosZ";
            this.lblPosZ.Size = new System.Drawing.Size(30, 20);
            this.lblPosZ.TabIndex = 5;
            this.lblPosZ.Text = "Z:";
            // 
            // numericPosY
            // 
            this.numericPosY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericPosY.DecimalPlaces = 2;
            this.numericPosY.ForeColor = System.Drawing.Color.White;
            this.numericPosY.Location = new System.Drawing.Point(45, 76);
            this.numericPosY.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericPosY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericPosY.Name = "numericPosY";
            this.numericPosY.Size = new System.Drawing.Size(165, 23);
            this.numericPosY.TabIndex = 4;
            this.numericPosY.ValueChanged += new System.EventHandler(this.numericPosY_ValueChanged);
            // 
            // lblPosY
            // 
            this.lblPosY.ForeColor = System.Drawing.Color.White;
            this.lblPosY.Location = new System.Drawing.Point(10, 78);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(30, 20);
            this.tabPageRotacion.Location = new System.Drawing.Point(4, 24);
            this.tabPageRotacion.Name = "tabPageRotacion";
            this.tabPageRotacion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRotacion.Size = new System.Drawing.Size(222, 636);
            this.tabPageRotacion.TabIndex = 1;
            this.tabPageRotacion.Text = "Rotación";
            // 
            // lblRotacion
            // 
            this.lblRotacion.ForeColor = System.Drawing.Color.White;
            this.lblRotacion.Location = new System.Drawing.Point(10, 15);
            this.lblRotacion.Name = "lblRotacion";
            this.lblRotacion.Size = new System.Drawing.Size(200, 20);
            this.lblRotacion.TabIndex = 0;
            this.lblRotacion.Text = "Rotación (X, Y, Z):";
            // 
            // lblRotX
            // 
            this.lblRotX.ForeColor = System.Drawing.Color.White;
            this.lblRotX.Location = new System.Drawing.Point(10, 45);
            this.lblRotX.Name = "lblRotX";
            this.lblRotX.Size = new System.Drawing.Size(200, 20);
            this.lblRotX.TabIndex = 1;
            this.lblRotX.Text = "X: 0°";
            // 
            // trackBarRotX
            // 
            this.trackBarRotX.Location = new System.Drawing.Point(10, 68);
            this.trackBarRotX.Maximum = 360;
            this.trackBarRotX.Minimum = -360;
            this.trackBarRotX.Name = "trackBarRotX";
            this.trackBarRotX.Size = new System.Drawing.Size(200, 45);
            this.trackBarRotX.TabIndex = 2;
            this.trackBarRotX.TickFrequency = 45;
            this.trackBarRotX.Scroll += new System.EventHandler(this.trackBarRotX_Scroll);
            // 
            // lblRotY
            // 
            this.lblRotY.ForeColor = System.Drawing.Color.White;
            this.lblRotY.Location = new System.Drawing.Point(10, 118);
            this.lblRotY.Name = "lblRotY";
            this.lblRotY.Size = new System.Drawing.Size(200, 20);
            this.lblRotY.TabIndex = 3;
            this.lblRotY.Text = "Y: 0°";
            // 
            // trackBarRotY
            // 
            this.trackBarRotY.Location = new System.Drawing.Point(10, 141);
            this.trackBarRotY.Maximum = 360;
            this.trackBarRotY.Minimum = -360;
            this.trackBarRotY.Name = "trackBarRotY";
            this.trackBarRotY.Size = new System.Drawing.Size(200, 45);
            this.trackBarRotY.TabIndex = 4;
            this.trackBarRotY.TickFrequency = 45;
            this.trackBarRotY.Scroll += new System.EventHandler(this.trackBarRotY_Scroll);
            // 
            // lblRotZ
            // 
            this.lblRotZ.ForeColor = System.Drawing.Color.White;
            this.lblRotZ.Location = new System.Drawing.Point(10, 191);
            this.lblRotZ.Name = "lblRotZ";
            this.lblRotZ.Size = new System.Drawing.Size(200, 20);
            this.lblRotZ.TabIndex = 5;
            this.lblRotZ.Text = "Z: 0°";
            // 
            // trackBarRotZ
            // 
            this.trackBarRotZ.Location = new System.Drawing.Point(10, 214);
            this.trackBarRotZ.Maximum = 360;
            this.trackBarRotZ.Minimum = -360;
            this.trackBarRotZ.Name = "trackBarRotZ";
            this.trackBarRotZ.Size = new System.Drawing.Size(200, 45);
            this.trackBarRotZ.TabIndex = 6;
            this.trackBarRotZ.TickFrequency = 45;
            this.trackBarRotZ.Scroll += new System.EventHandler(this.trackBarRotZ_Scroll);
            // 
            // tabPageEscala
            // 
            this.tabPageEscala.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tabPageEscala.Controls.Add(this.lblEscala);
            this.tabPageEscala.Controls.Add(this.lblEscalaX);
            this.tabPageEscala.Controls.Add(this.numericEscalaX);
            this.tabPageEscala.Controls.Add(this.lblEscalaY);
            this.tabPageEscala.Controls.Add(this.numericEscalaY);
            this.tabPageEscala.Controls.Add(this.lblEscalaZ);
            this.tabPageEscala.Controls.Add(this.numericEscalaZ);
            this.tabPageEscala.Location = new System.Drawing.Point(4, 24);
            this.tabPageEscala.Name = "tabPageEscala";
            this.tabPageEscala.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEscala.Size = new System.Drawing.Size(222, 636);
            this.tabPageEscala.TabIndex = 2;
            this.tabPageEscala.Text = "Escala";
            // 
            // lblEscala
            // 
            this.lblEscala.ForeColor = System.Drawing.Color.White;
            this.lblEscala.Location = new System.Drawing.Point(10, 15);
            this.lblEscala.Name = "lblEscala";
            this.lblEscala.Size = new System.Drawing.Size(200, 20);
            this.lblEscala.TabIndex = 0;
            this.lblEscala.Text = "Escala (X, Y, Z):";
            // 
            // lblEscalaX
            // 
            this.lblEscalaX.ForeColor = System.Drawing.Color.White;
            this.lblEscalaX.Location = new System.Drawing.Point(10, 45);
            this.lblEscalaX.Name = "lblEscalaX";
            this.lblEscalaX.Size = new System.Drawing.Size(30, 20);
            this.lblEscalaX.TabIndex = 1;
            this.lblEscalaX.Text = "X:";
            // 
            // numericEscalaX
            // 
            this.numericEscalaX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericEscalaX.DecimalPlaces = 2;
            this.numericEscalaX.ForeColor = System.Drawing.Color.White;
            this.numericEscalaX.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.numericEscalaX.Location = new System.Drawing.Point(45, 43);
            this.numericEscalaX.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numericEscalaX.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            this.numericEscalaX.Name = "numericEscalaX";
            this.numericEscalaX.Size = new System.Drawing.Size(165, 23);
            this.numericEscalaX.TabIndex = 2;
            this.numericEscalaX.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericEscalaX.ValueChanged += new System.EventHandler(this.numericEscalaX_ValueChanged);
            // 
            // lblEscalaY
            // 
            this.lblEscalaY.ForeColor = System.Drawing.Color.White;
            this.lblEscalaY.Location = new System.Drawing.Point(10, 78);
            this.lblEscalaY.Name = "lblEscalaY";
            this.lblEscalaY.Size = new System.Drawing.Size(30, 20);
            this.lblEscalaY.TabIndex = 3;
            this.lblEscalaY.Text = "Y:";
            // 
            // numericEscalaY
            // 
            this.numericEscalaY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericEscalaY.DecimalPlaces = 2;
            this.numericEscalaY.ForeColor = System.Drawing.Color.White;
            this.numericEscalaY.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.numericEscalaY.Location = new System.Drawing.Point(45, 76);
            this.numericEscalaY.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numericEscalaY.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            this.numericEscalaY.Name = "numericEscalaY";
            this.numericEscalaY.Size = new System.Drawing.Size(165, 23);
            this.numericEscalaY.TabIndex = 4;
            this.numericEscalaY.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericEscalaY.ValueChanged += new System.EventHandler(this.numericEscalaY_ValueChanged);
            // 
            // lblEscalaZ
            // 
            this.lblEscalaZ.ForeColor = System.Drawing.Color.White;
            this.lblEscalaZ.Location = new System.Drawing.Point(10, 111);
            this.lblEscalaZ.Name = "lblEscalaZ";
            this.lblEscalaZ.Size = new System.Drawing.Size(30, 20);
            this.lblEscalaZ.TabIndex = 5;
            this.lblEscalaZ.Text = "Z:";
            // 
            // numericEscalaZ
            // 
            this.numericEscalaZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericEscalaZ.DecimalPlaces = 2;
            this.numericEscalaZ.ForeColor = System.Drawing.Color.White;
            this.numericEscalaZ.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.numericEscalaZ.Location = new System.Drawing.Point(45, 109);
            this.numericEscalaZ.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numericEscalaZ.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            this.numericEscalaZ.Name = "numericEscalaZ";
            this.numericEscalaZ.Size = new System.Drawing.Size(165, 23);
            this.numericEscalaZ.TabIndex = 6;
            this.numericEscalaZ.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericEscalaZ.ValueChanged += new System.EventHandler(this.numericEscalaZ_ValueChanged);
            // 
            // tabPageColor
            // 
            this.tabPageColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tabPageColor.Controls.Add(this.lblColor);
            this.tabPageColor.Controls.Add(this.btnColor);
            this.tabPageColor.Location = new System.Drawing.Point(4, 24);
            this.tabPageColor.Name = "tabPageColor";
            this.tabPageColor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageColor.Size = new System.Drawing.Size(222, 636);
            this.tabPageColor.TabIndex = 3;
            this.tabPageColor.Text = "Color";
            // 
            // lblColor
            // 
            this.lblColor.ForeColor = System.Drawing.Color.White;
            this.lblColor.Location = new System.Drawing.Point(10, 15);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(200, 20);
            this.lblColor.TabIndex = 0;
            this.lblColor.Text = "Color de la Figura:";
            // 
            // btnColor
            // 
            this.btnColor.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor.Location = new System.Drawing.Point(10, 45);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(200, 40);
            this.btnColor.TabIndex = 1;
            this.btnColor.Text = "Seleccionar Color";
            this.btnColor.UseVisualStyleBackColor = false;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // Form3DViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.pictureBoxViewport);
            this.Controls.Add(this.panelDerecho);
            this.Controls.Add(this.panelIzquierdo);
            this.Name = "Form3DViewer";
            this.Text = "Motor 3D - Visor Interactivo";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxViewport)).EndInit();
            this.panelIzquierdo.ResumeLayout(false);
            this.groupBoxFiguras.ResumeLayout(false);
            this.groupBoxCamara.ResumeLayout(false);
            this.groupBoxOpciones.ResumeLayout(false);
            this.groupBoxOpciones.PerformLayout();
            this.panelDerecho.ResumeLayout(false);
            this.groupBoxPropiedades.ResumeLayout(false);
            this.tabControlPropiedades.ResumeLayout(false);
            this.tabPagePosicion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPosZ)).EndInit();
            this.tabPageRotacion.ResumeLayout(false);
            this.tabPageRotacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotZ)).EndInit();
            this.tabPageEscala.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericEscalaX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEscalaY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEscalaZ)).EndInit();
            this.tabPageColor.ResumeLayout(false);
            this.ResumeLayout(false);
        }


    }
}