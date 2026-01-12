namespace ServindAp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            Home = new TabPage();
            toolStrip1 = new ToolStrip();
            label1 = new Label();
            Logo = new PictureBox();
            tabPage2 = new TabPage();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            materialButton2 = new MaterialSkin.Controls.MaterialButton();
            materialButton1 = new MaterialSkin.Controls.MaterialButton();
            materialTextBox1 = new MaterialSkin.Controls.MaterialTextBox();
            label2 = new Label();
            tabPage3 = new TabPage();
            imageList1 = new ImageList(components);
            materialTabControl1.SuspendLayout();
            Home.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // materialTabControl1
            // 
            materialTabControl1.Controls.Add(Home);
            materialTabControl1.Controls.Add(tabPage2);
            materialTabControl1.Controls.Add(tabPage3);
            materialTabControl1.Depth = 0;
            materialTabControl1.Dock = DockStyle.Fill;
            materialTabControl1.ImageList = imageList1;
            materialTabControl1.ItemSize = new Size(120, 50);
            materialTabControl1.Location = new Point(3, 64);
            materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 0;
            materialTabControl1.Size = new Size(1329, 664);
            materialTabControl1.TabIndex = 0;
            // 
            // Home
            // 
            Home.Controls.Add(toolStrip1);
            Home.Controls.Add(label1);
            Home.Controls.Add(Logo);
            Home.ImageKey = "Icono house.png";
            Home.Location = new Point(4, 54);
            Home.Name = "Home";
            Home.Padding = new Padding(3);
            Home.Size = new Size(1321, 606);
            Home.TabIndex = 0;
            Home.Text = "Inicio";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Location = new Point(3, 3);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1315, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Rockwell", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(331, 354);
            label1.Name = "label1";
            label1.Size = new Size(662, 28);
            label1.TabIndex = 1;
            label1.Text = "Sistema de gestion de entregas de equipos y herramientas";
            // 
            // Logo
            // 
            Logo.Image = (Image)resources.GetObject("Logo.Image");
            Logo.Location = new Point(399, 23);
            Logo.Name = "Logo";
            Logo.Size = new Size(512, 206);
            Logo.SizeMode = PictureBoxSizeMode.AutoSize;
            Logo.TabIndex = 0;
            Logo.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Controls.Add(materialButton2);
            tabPage2.Controls.Add(materialButton1);
            tabPage2.Controls.Add(materialTextBox1);
            tabPage2.Controls.Add(label2);
            tabPage2.ImageKey = "Icono Cambio.png";
            tabPage2.Location = new Point(4, 54);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1321, 606);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Prestamo";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Rockwell", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(109, 450);
            label3.Name = "label3";
            label3.Size = new Size(110, 24);
            label3.TabIndex = 5;
            label3.Text = "Buscador:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(109, 164);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(840, 188);
            dataGridView1.TabIndex = 4;
            // 
            // materialButton2
            // 
            materialButton2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton2.Depth = 0;
            materialButton2.HighEmphasis = true;
            materialButton2.Icon = null;
            materialButton2.Location = new Point(975, 195);
            materialButton2.Margin = new Padding(4, 6, 4, 6);
            materialButton2.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton2.Name = "materialButton2";
            materialButton2.NoAccentTextColor = Color.Empty;
            materialButton2.Size = new Size(158, 36);
            materialButton2.TabIndex = 3;
            materialButton2.Text = "materialButton2";
            materialButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton2.UseAccentColor = false;
            materialButton2.UseVisualStyleBackColor = true;
            // 
            // materialButton1
            // 
            materialButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton1.Depth = 0;
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = null;
            materialButton1.Location = new Point(975, 255);
            materialButton1.Margin = new Padding(4, 6, 4, 6);
            materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(158, 36);
            materialButton1.TabIndex = 2;
            materialButton1.Text = "materialButton1";
            materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            // 
            // materialTextBox1
            // 
            materialTextBox1.AnimateReadOnly = false;
            materialTextBox1.BorderStyle = BorderStyle.None;
            materialTextBox1.Depth = 0;
            materialTextBox1.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBox1.LeadingIcon = null;
            materialTextBox1.Location = new Point(109, 486);
            materialTextBox1.MaxLength = 50;
            materialTextBox1.MouseState = MaterialSkin.MouseState.OUT;
            materialTextBox1.Multiline = false;
            materialTextBox1.Name = "materialTextBox1";
            materialTextBox1.Size = new Size(280, 50);
            materialTextBox1.TabIndex = 1;
            materialTextBox1.Text = "";
            materialTextBox1.TrailingIcon = null;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Rockwell", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(109, 89);
            label2.Name = "label2";
            label2.Size = new Size(220, 28);
            label2.TabIndex = 0;
            label2.Text = "Préstamos Activos";
            // 
            // tabPage3
            // 
            tabPage3.ImageKey = "Icono Herramienta.png";
            tabPage3.Location = new Point(4, 54);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1321, 606);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Herramientas";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "Icono Cambio.png");
            imageList1.Images.SetKeyName(1, "Icono Herramienta.png");
            imageList1.Images.SetKeyName(2, "Icono house.png");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1335, 731);
            Controls.Add(materialTabControl1);
            DrawerTabControl = materialTabControl1;
            Name = "Form1";
            Load += Form1_Load;
            materialTabControl1.ResumeLayout(false);
            Home.ResumeLayout(false);
            Home.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabPage Home;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private PictureBox Logo;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private Label label1;
        private ToolStrip toolStrip1;
        private DataGridView dataGridView1;
        private MaterialSkin.Controls.MaterialButton materialButton2;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private MaterialSkin.Controls.MaterialTextBox materialTextBox1;
        private Label label2;
        private Label label3;
        private ImageList imageList1;
    }
}
