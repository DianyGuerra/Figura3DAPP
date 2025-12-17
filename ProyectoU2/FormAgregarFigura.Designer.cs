namespace Motor3D
{
    partial class FormAgregarFigura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxTipo = new System.Windows.Forms.ComboBox();
            this.numericTamaño = new System.Windows.Forms.NumericUpDown();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblTamaño = new System.Windows.Forms.Label();
            this.panelPreview = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericTamaño)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxTipo
            // 
            this.comboBoxTipo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboBoxTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipo.ForeColor = System.Drawing.Color.White;
            this.comboBoxTipo.FormattingEnabled = true;
            this.comboBoxTipo.Items.AddRange(new object[] {
            "Cubo",
            "Esfera",
            "Pirámide",
            "Cilindro",
            "Cono",
            "Toroide",
            "Plano"});
            this.comboBoxTipo.Location = new System.Drawing.Point(20, 45);
            this.comboBoxTipo.Name = "comboBoxTipo";
            this.comboBoxTipo.Size = new System.Drawing.Size(200, 23);
            this.comboBoxTipo.TabIndex = 0;
            this.comboBoxTipo.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipo_SelectedIndexChanged);
            // 
            // numericTamaño
            // 
            this.numericTamaño.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericTamaño.DecimalPlaces = 1;
            this.numericTamaño.ForeColor = System.Drawing.Color.White;
            this.numericTamaño.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericTamaño.Location = new System.Drawing.Point(20, 110);
            this.numericTamaño.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericTamaño.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericTamaño.Name = "numericTamaño";
            this.numericTamaño.Size = new System.Drawing.Size(200, 23);
            this.numericTamaño.TabIndex = 1;
            this.numericTamaño.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(20, 240);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 35);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(130, 240);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.ForeColor = System.Drawing.Color.White;
            this.lblTipo.Location = new System.Drawing.Point(20, 20);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(96, 15);
            this.lblTipo.TabIndex = 4;
            this.lblTipo.Text = "Tipo de Figura:";
            // 
            // lblTamaño
            // 
            this.lblTamaño.AutoSize = true;
            this.lblTamaño.ForeColor = System.Drawing.Color.White;
            this.lblTamaño.Location = new System.Drawing.Point(20, 85);
            this.lblTamaño.Name = "lblTamaño";
            this.lblTamaño.Size = new System.Drawing.Size(55, 15);
            this.lblTamaño.TabIndex = 5;
            this.lblTamaño.Text = "Tamaño:";
            // 
            // panelPreview
            // 
            this.panelPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(30)))));
            this.panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreview.Location = new System.Drawing.Point(240, 20);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(140, 140);
            this.panelPreview.TabIndex = 6;
            this.panelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelPreview_Paint);
            // 
            // FormAgregarFigura
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.lblTamaño);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.numericTamaño);
            this.Controls.Add(this.comboBoxTipo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAgregarFigura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar Figura 3D";
            ((System.ComponentModel.ISupportInitialize)(this.numericTamaño)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTipo;
        private System.Windows.Forms.NumericUpDown numericTamaño;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblTamaño;
        private System.Windows.Forms.Panel panelPreview;
    }
}