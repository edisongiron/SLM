namespace ServindAp.UI.UserControls
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
            btnCancelar = new Button();
            btnAgregar = new Button();
            materialComboBox1 = new MaterialSkin.Controls.MaterialComboBox();
            Cantidad = new MaterialSkin.Controls.MaterialLabel();
            cmbHerramienta = new MaterialSkin.Controls.MaterialComboBox();
            FechaEntrega = new DateTimePicker();
            txtObservaciones = new MaterialSkin.Controls.MaterialTextBox();
            txtResponsable = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            label1 = new Label();
            panelContenedor.SuspendLayout();
            SuspendLayout();
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(btnCancelar);
            panelContenedor.Controls.Add(btnAgregar);
            panelContenedor.Controls.Add(materialComboBox1);
            panelContenedor.Controls.Add(Cantidad);
            panelContenedor.Controls.Add(cmbHerramienta);
            panelContenedor.Controls.Add(FechaEntrega);
            panelContenedor.Controls.Add(txtObservaciones);
            panelContenedor.Controls.Add(txtResponsable);
            panelContenedor.Controls.Add(materialLabel4);
            panelContenedor.Controls.Add(materialLabel3);
            panelContenedor.Controls.Add(materialLabel2);
            panelContenedor.Controls.Add(materialLabel1);
            panelContenedor.Controls.Add(label1);
            panelContenedor.Location = new Point(101, 35);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1094, 639);
            panelContenedor.TabIndex = 0;
            panelContenedor.Paint += panelContenedor_Paint;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(477, 421);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 18;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click_1;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(311, 421);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 17;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // materialComboBox1
            // 
            materialComboBox1.AutoResize = false;
            materialComboBox1.BackColor = Color.FromArgb(255, 255, 255);
            materialComboBox1.Depth = 0;
            materialComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            materialComboBox1.DropDownHeight = 174;
            materialComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBox1.DropDownWidth = 121;
            materialComboBox1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialComboBox1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialComboBox1.FormattingEnabled = true;
            materialComboBox1.IntegralHeight = false;
            materialComboBox1.ItemHeight = 43;
            materialComboBox1.Location = new Point(560, 260);
            materialComboBox1.MaxDropDownItems = 4;
            materialComboBox1.MouseState = MaterialSkin.MouseState.OUT;
            materialComboBox1.Name = "materialComboBox1";
            materialComboBox1.Size = new Size(151, 49);
            materialComboBox1.StartIndex = 0;
            materialComboBox1.TabIndex = 16;
            // 
            // Cantidad
            // 
            Cantidad.AutoSize = true;
            Cantidad.Depth = 0;
            Cantidad.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            Cantidad.Location = new Point(560, 237);
            Cantidad.MouseState = MaterialSkin.MouseState.HOVER;
            Cantidad.Name = "Cantidad";
            Cantidad.Size = new Size(82, 19);
            Cantidad.TabIndex = 15;
            Cantidad.Text = "CANTIDAD:";
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
            cmbHerramienta.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmbHerramienta.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbHerramienta.FormattingEnabled = true;
            cmbHerramienta.IntegralHeight = false;
            cmbHerramienta.ItemHeight = 43;
            cmbHerramienta.Location = new Point(15, 141);
            cmbHerramienta.MaxDropDownItems = 4;
            cmbHerramienta.MouseState = MaterialSkin.MouseState.OUT;
            cmbHerramienta.Name = "cmbHerramienta";
            cmbHerramienta.Size = new Size(318, 49);
            cmbHerramienta.StartIndex = 0;
            cmbHerramienta.TabIndex = 14;
            // 
            // FechaEntrega
            // 
            FechaEntrega.Enabled = false;
            FechaEntrega.Location = new Point(551, 151);
            FechaEntrega.Name = "FechaEntrega";
            FechaEntrega.Size = new Size(212, 27);
            FechaEntrega.TabIndex = 11;
            // 
            // txtObservaciones
            // 
            txtObservaciones.AnimateReadOnly = false;
            txtObservaciones.BorderStyle = BorderStyle.None;
            txtObservaciones.Depth = 0;
            txtObservaciones.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtObservaciones.LeadingIcon = null;
            txtObservaciones.Location = new Point(311, 328);
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
            txtResponsable.Location = new Point(15, 261);
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
            materialLabel4.Location = new Point(166, 341);
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
            materialLabel3.Location = new Point(542, 107);
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
            materialLabel2.Location = new Point(14, 237);
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
            materialLabel1.Location = new Point(15, 107);
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
            label1.Location = new Point(368, 39);
            label1.Name = "label1";
            label1.Size = new Size(158, 25);
            label1.TabIndex = 0;
            label1.Text = "Nuevo Préstamo";
            // 
            // FormNuevoPrestamo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1263, 749);
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
        private MaterialSkin.Controls.MaterialComboBox cmbHerramienta;
        private MaterialSkin.Controls.MaterialLabel Cantidad;
        private MaterialSkin.Controls.MaterialComboBox materialComboBox1;
        private Button btnCancelar;
        private Button btnAgregar;
    }
}