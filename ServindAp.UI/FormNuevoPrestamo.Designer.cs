namespace ServindAp.UI
{
    partial class FormNuevoPrestamo
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
            panelContenedor = new Panel();
            btnCancelar = new MaterialSkin.Controls.MaterialButton();
            btnAgregar = new MaterialSkin.Controls.MaterialButton();
            FechaEntrega = new DateTimePicker();
            txtObservaciones = new MaterialSkin.Controls.MaterialTextBox();
            txtResponsable = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            label1 = new Label();
            cmbHerramienta = new MaterialSkin.Controls.MaterialComboBox();
            panelContenedor.SuspendLayout();
            SuspendLayout();
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(cmbHerramienta);
            panelContenedor.Controls.Add(btnCancelar);
            panelContenedor.Controls.Add(btnAgregar);
            panelContenedor.Controls.Add(FechaEntrega);
            panelContenedor.Controls.Add(txtObservaciones);
            panelContenedor.Controls.Add(txtResponsable);
            panelContenedor.Controls.Add(materialLabel4);
            panelContenedor.Controls.Add(materialLabel3);
            panelContenedor.Controls.Add(materialLabel2);
            panelContenedor.Controls.Add(materialLabel1);
            panelContenedor.Controls.Add(label1);
            panelContenedor.Location = new Point(100, 35);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(629, 549);
            panelContenedor.TabIndex = 0;
            panelContenedor.Paint += panelContenedor_Paint;
            // 
            // btnCancelar
            // 
            btnCancelar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCancelar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnCancelar.Depth = 0;
            btnCancelar.HighEmphasis = true;
            btnCancelar.Icon = null;
            btnCancelar.Location = new Point(357, 459);
            btnCancelar.Margin = new Padding(4, 6, 4, 6);
            btnCancelar.MouseState = MaterialSkin.MouseState.HOVER;
            btnCancelar.Name = "btnCancelar";
            btnCancelar.NoAccentTextColor = Color.Empty;
            btnCancelar.Size = new Size(88, 36);
            btnCancelar.TabIndex = 13;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnCancelar.UseAccentColor = false;
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            btnAgregar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnAgregar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnAgregar.Depth = 0;
            btnAgregar.HighEmphasis = true;
            btnAgregar.Icon = null;
            btnAgregar.Location = new Point(159, 459);
            btnAgregar.Margin = new Padding(4, 6, 4, 6);
            btnAgregar.MouseState = MaterialSkin.MouseState.HOVER;
            btnAgregar.Name = "btnAgregar";
            btnAgregar.NoAccentTextColor = Color.Empty;
            btnAgregar.Size = new Size(88, 36);
            btnAgregar.TabIndex = 12;
            btnAgregar.Text = "Agregar";
            btnAgregar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnAgregar.UseAccentColor = false;
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // FechaEntrega
            // 
            FechaEntrega.Enabled = false;
            FechaEntrega.Location = new Point(260, 273);
            FechaEntrega.Name = "FechaEntrega";
            FechaEntrega.Size = new Size(199, 27);
            FechaEntrega.TabIndex = 11;
            // 
            // txtObservaciones
            // 
            txtObservaciones.AnimateReadOnly = false;
            txtObservaciones.BorderStyle = BorderStyle.None;
            txtObservaciones.Depth = 0;
            txtObservaciones.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtObservaciones.LeadingIcon = null;
            txtObservaciones.Location = new Point(260, 322);
            txtObservaciones.MaxLength = 50;
            txtObservaciones.MouseState = MaterialSkin.MouseState.OUT;
            txtObservaciones.Multiline = false;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(318, 50);
            txtObservaciones.TabIndex = 10;
            txtObservaciones.Text = "";
            txtObservaciones.TrailingIcon = null;
            // 
            // txtResponsable
            // 
            txtResponsable.AnimateReadOnly = false;
            txtResponsable.BorderStyle = BorderStyle.None;
            txtResponsable.Depth = 0;
            txtResponsable.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtResponsable.LeadingIcon = null;
            txtResponsable.Location = new Point(260, 195);
            txtResponsable.MaxLength = 50;
            txtResponsable.MouseState = MaterialSkin.MouseState.OUT;
            txtResponsable.Multiline = false;
            txtResponsable.Name = "txtResponsable";
            txtResponsable.Size = new Size(318, 50);
            txtResponsable.TabIndex = 8;
            txtResponsable.Text = "";
            txtResponsable.TrailingIcon = null;
            // 
            // materialLabel4
            // 
            materialLabel4.AutoSize = true;
            materialLabel4.Depth = 0;
            materialLabel4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel4.Location = new Point(78, 342);
            materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel4.Name = "materialLabel4";
            materialLabel4.Size = new Size(130, 19);
            materialLabel4.TabIndex = 4;
            materialLabel4.Text = "OBSERVACIONES:";
            // 
            // materialLabel3
            // 
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.Location = new Point(82, 281);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(128, 19);
            materialLabel3.TabIndex = 3;
            materialLabel3.Text = "FECHA ENTREGA:";
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(96, 216);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(114, 19);
            materialLabel2.TabIndex = 2;
            materialLabel2.Text = "RESPONSABLE:";
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(95, 150);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(113, 19);
            materialLabel1.TabIndex = 1;
            materialLabel1.Text = "HERRAMIENTA:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Symbol", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(240, 42);
            label1.Name = "label1";
            label1.Size = new Size(158, 25);
            label1.TabIndex = 0;
            label1.Text = "Nuevo Préstamo";
            // 
            // cmbHerramienta
            // 
            cmbHerramienta.AutoResize = false;
            cmbHerramienta.BackColor = Color.FromArgb(255, 255, 255);
            cmbHerramienta.Depth = 0;
            cmbHerramienta.DrawMode = DrawMode.OwnerDrawVariable;
            cmbHerramienta.DropDownHeight = 174;
            cmbHerramienta.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHerramienta.DropDownWidth = 121;
            cmbHerramienta.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmbHerramienta.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbHerramienta.FormattingEnabled = true;
            cmbHerramienta.IntegralHeight = false;
            cmbHerramienta.ItemHeight = 43;
            cmbHerramienta.Location = new Point(260, 130);
            cmbHerramienta.MaxDropDownItems = 4;
            cmbHerramienta.MouseState = MaterialSkin.MouseState.OUT;
            cmbHerramienta.Name = "cmbHerramienta";
            cmbHerramienta.Size = new Size(318, 49);
            cmbHerramienta.StartIndex = 0;
            cmbHerramienta.TabIndex = 14;
            // 
            // FormNuevoPrestamo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(853, 596);
            Controls.Add(panelContenedor);
            Name = "FormNuevoPrestamo";
            Text = "FormNuevoPrestamo";
            Load += FormNuevoPrestamo_Load;
            panelContenedor.ResumeLayout(false);
            panelContenedor.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelContenedor;
        private Label label1;
        private MaterialSkin.Controls.MaterialTextBox txtObservaciones;
        private MaterialSkin.Controls.MaterialTextBox txtResponsable;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private DateTimePicker FechaEntrega;
        private MaterialSkin.Controls.MaterialButton btnCancelar;
        private MaterialSkin.Controls.MaterialButton btnAgregar;
        private MaterialSkin.Controls.MaterialComboBox cmbHerramienta;
    }
}