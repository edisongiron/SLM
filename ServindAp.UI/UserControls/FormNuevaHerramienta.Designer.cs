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
            lblStock = new MaterialSkin.Controls.MaterialLabel();
            txtNombre = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            label1 = new Label();
            panelContenedor2 = new Panel();
            txtStock = new MaterialSkin.Controls.MaterialTextBox();
            btnCancelar = new Button();
            btnAgregar = new Button();
            txtDescripcion = new MaterialSkin.Controls.MaterialTextBox();
            panelContenedor2.SuspendLayout();
            SuspendLayout();
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Depth = 0;
            lblStock.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblStock.Location = new Point(559, 124);
            lblStock.MouseState = MaterialSkin.MouseState.HOVER;
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(82, 19);
            lblStock.TabIndex = 24;
            lblStock.Text = "CANTIDAD:";
            // 
            // txtNombre
            // 
            txtNombre.AnimateReadOnly = false;
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Depth = 0;
            txtNombre.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtNombre.LeadingIcon = null;
            txtNombre.Location = new Point(67, 161);
            txtNombre.MaxLength = 50;
            txtNombre.MouseState = MaterialSkin.MouseState.OUT;
            txtNombre.Multiline = false;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(318, 50);
            txtNombre.TabIndex = 21;
            txtNombre.Text = "";
            txtNombre.TrailingIcon = null;
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(67, 124);
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
            materialLabel1.Location = new Point(67, 261);
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
            label1.Location = new Point(326, 39);
            label1.Name = "label1";
            label1.Size = new Size(181, 25);
            label1.TabIndex = 17;
            label1.Text = "Nueva Herramienta";
            // 
            // panelContenedor2
            // 
            panelContenedor2.Controls.Add(txtStock);
            panelContenedor2.Controls.Add(btnCancelar);
            panelContenedor2.Controls.Add(btnAgregar);
            panelContenedor2.Controls.Add(txtDescripcion);
            panelContenedor2.Controls.Add(label1);
            panelContenedor2.Controls.Add(materialLabel1);
            panelContenedor2.Controls.Add(lblStock);
            panelContenedor2.Controls.Add(materialLabel2);
            panelContenedor2.Controls.Add(txtNombre);
            panelContenedor2.Location = new Point(59, 125);
            panelContenedor2.Name = "panelContenedor2";
            panelContenedor2.Size = new Size(825, 501);
            panelContenedor2.TabIndex = 26;
            // 
            // txtStock
            // 
            txtStock.AnimateReadOnly = false;
            txtStock.BorderStyle = BorderStyle.None;
            txtStock.Depth = 0;
            txtStock.Font = new Font("Segoe UI Emoji", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStock.LeadingIcon = null;
            txtStock.Location = new Point(442, 160);
            txtStock.MaxLength = 50;
            txtStock.MouseState = MaterialSkin.MouseState.OUT;
            txtStock.Multiline = false;
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(318, 50);
            txtStock.TabIndex = 29;
            txtStock.Text = "";
            txtStock.TrailingIcon = null;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(442, 411);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 28;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            btnCancelar.MouseEnter += btnCancelar_MouseEnter;
            btnCancelar.MouseLeave += btnCancelar_MouseLeave;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(277, 411);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
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
            txtDescripcion.Location = new Point(67, 305);
            txtDescripcion.MaxLength = 50;
            txtDescripcion.MouseState = MaterialSkin.MouseState.OUT;
            txtDescripcion.Multiline = false;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(325, 50);
            txtDescripcion.TabIndex = 26;
            txtDescripcion.Text = "";
            txtDescripcion.TrailingIcon = null;
            // 
            // FormNuevaHerramienta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1213, 717);
            Controls.Add(panelContenedor2);
            Name = "FormNuevaHerramienta";
            Text = "FormNuevaHerramienta";
            Load += FormNuevaHerramienta_Load;
            panelContenedor2.ResumeLayout(false);
            panelContenedor2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private MaterialSkin.Controls.MaterialLabel lblStock;
        private MaterialSkin.Controls.MaterialTextBox txtNombre;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private Label label1;
        private Panel panelContenedor2;
        private MaterialSkin.Controls.MaterialTextBox txtDescripcion;
        private Button btnCancelar;
        private Button btnAgregar;
        private MaterialSkin.Controls.MaterialTextBox txtStock;
    }
}