namespace ServindAp.UI.UserControls
{
    partial class FormNuevaHerramienta
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
            NameCantidad = new MaterialSkin.Controls.MaterialLabel();
            txtNombre = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            label1 = new Label();
            panelContenedor2 = new Panel();
            btnCancelar = new Button();
            btnAgregar = new Button();
            txtDescripcion = new MaterialSkin.Controls.MaterialTextBox();
            labelStock = new MaterialSkin.Controls.MaterialTextBox();
            panelContenedor2.SuspendLayout();
            SuspendLayout();
            // 
            // NameCantidad
            // 
            NameCantidad.AutoSize = true;
            NameCantidad.Depth = 0;
            NameCantidad.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            NameCantidad.Location = new Point(489, 93);
            NameCantidad.MouseState = MaterialSkin.MouseState.HOVER;
            NameCantidad.Name = "NameCantidad";
            NameCantidad.Size = new Size(82, 19);
            NameCantidad.TabIndex = 24;
            NameCantidad.Text = "CANTIDAD:";
            // 
            // txtNombre
            // 
            txtNombre.AnimateReadOnly = false;
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Depth = 0;
            txtNombre.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtNombre.LeadingIcon = null;
            txtNombre.Location = new Point(59, 121);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.MaxLength = 50;
            txtNombre.MouseState = MaterialSkin.MouseState.OUT;
            txtNombre.Multiline = false;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(278, 50);
            txtNombre.TabIndex = 21;
            txtNombre.Text = "";
            txtNombre.TrailingIcon = null;
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(59, 93);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(70, 19);
            materialLabel2.TabIndex = 19;
            materialLabel2.Text = "NOMBRE:";
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(59, 196);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(105, 19);
            materialLabel1.TabIndex = 18;
            materialLabel1.Text = "DESCRIPCION:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Symbol", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(285, 29);
            label1.Name = "label1";
            label1.Size = new Size(155, 20);
            label1.TabIndex = 17;
            label1.Text = "Nueva Herramienta";
            // 
            // panelContenedor2
            // 
            panelContenedor2.Controls.Add(labelStock);
            panelContenedor2.Controls.Add(btnCancelar);
            panelContenedor2.Controls.Add(btnAgregar);
            panelContenedor2.Controls.Add(txtDescripcion);
            panelContenedor2.Controls.Add(label1);
            panelContenedor2.Controls.Add(materialLabel1);
            panelContenedor2.Controls.Add(NameCantidad);
            panelContenedor2.Controls.Add(materialLabel2);
            panelContenedor2.Controls.Add(txtNombre);
            panelContenedor2.Location = new Point(52, 94);
            panelContenedor2.Margin = new Padding(3, 2, 3, 2);
            panelContenedor2.Name = "panelContenedor2";
            panelContenedor2.Size = new Size(722, 376);
            panelContenedor2.TabIndex = 26;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(387, 308);
            btnCancelar.Margin = new Padding(3, 2, 3, 2);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(82, 22);
            btnCancelar.TabIndex = 28;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            btnCancelar.MouseEnter += btnCancelar_MouseEnter;
            btnCancelar.MouseLeave += btnCancelar_MouseLeave;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(242, 308);
            btnAgregar.Margin = new Padding(3, 2, 3, 2);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(82, 22);
            btnAgregar.TabIndex = 27;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            btnAgregar.MouseEnter += btnAgregar_MouseEnter;
            btnAgregar.MouseLeave += btnAgregar_MouseLeave;
            // 
            // txtDescripcion
            // 
            txtDescripcion.AnimateReadOnly = false;
            txtDescripcion.BorderStyle = BorderStyle.None;
            txtDescripcion.Depth = 0;
            txtDescripcion.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtDescripcion.LeadingIcon = null;
            txtDescripcion.Location = new Point(59, 229);
            txtDescripcion.Margin = new Padding(3, 2, 3, 2);
            txtDescripcion.MaxLength = 50;
            txtDescripcion.MouseState = MaterialSkin.MouseState.OUT;
            txtDescripcion.Multiline = false;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(284, 50);
            txtDescripcion.TabIndex = 26;
            txtDescripcion.Text = "";
            txtDescripcion.TrailingIcon = null;
            // 
            // labelStock
            // 
            labelStock.AnimateReadOnly = false;
            labelStock.BorderStyle = BorderStyle.None;
            labelStock.Depth = 0;
            labelStock.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            labelStock.LeadingIcon = null;
            labelStock.Location = new Point(387, 120);
            labelStock.Margin = new Padding(3, 2, 3, 2);
            labelStock.MaxLength = 50;
            labelStock.MouseState = MaterialSkin.MouseState.OUT;
            labelStock.Multiline = false;
            labelStock.Name = "labelStock";
            labelStock.Size = new Size(278, 50);
            labelStock.TabIndex = 29;
            labelStock.Text = "";
            labelStock.TrailingIcon = null;
            // 
            // FormNuevaHerramienta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1061, 538);
            Controls.Add(panelContenedor2);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormNuevaHerramienta";
            Text = "FormNuevaHerramienta";
            Load += FormNuevaHerramienta_Load;
            panelContenedor2.ResumeLayout(false);
            panelContenedor2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private MaterialSkin.Controls.MaterialLabel NameCantidad;
        private MaterialSkin.Controls.MaterialTextBox txtNombre;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private Label label1;
        private Panel panelContenedor2;
        private MaterialSkin.Controls.MaterialTextBox txtDescripcion;
        private Button btnCancelar;
        private Button btnAgregar;
        private MaterialSkin.Controls.MaterialTextBox labelStock;
    }
}