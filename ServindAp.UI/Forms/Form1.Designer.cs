using MaterialSkin;
using MaterialSkin.Controls;

namespace ServindAp.UI.Forms
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
            materialTabControl1 = new MaterialTabControl();
            Home = new TabPage();
            labelSubtitulo = new MaterialLabel();
            toolStrip1 = new ToolStrip();
            Logo = new PictureBox();
            tabPage2 = new TabPage();
            panelBuscador = new Panel();
            picLupa = new PictureBox();
            BuscadorTxb = new TextBox();
            TablaPrestamos = new DataGridView();
            tabPage3 = new TabPage();
            TablaHerramientas = new DataGridView();
            panelBuscadorHerramientas = new Panel();
            picLupaHerramientas = new PictureBox();
            txbBuscadorHerramientas = new TextBox();
            imageList1 = new ImageList(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            materialTabControl1.SuspendLayout();
            Home.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            tabPage2.SuspendLayout();
            panelBuscador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLupa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TablaPrestamos).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TablaHerramientas).BeginInit();
            panelBuscadorHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLupaHerramientas).BeginInit();
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
            materialTabControl1.MouseState = MouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 0;
            materialTabControl1.Size = new Size(1696, 909);
            materialTabControl1.TabIndex = 0;
            // 
            // Home
            // 
            Home.Controls.Add(labelSubtitulo);
            Home.Controls.Add(toolStrip1);
            Home.Controls.Add(Logo);
            Home.ImageKey = "home_32dp_000000_FILL0_wght400_GRAD0_opsz40.png";
            Home.Location = new Point(4, 54);
            Home.Name = "Home";
            Home.Padding = new Padding(3);
            Home.Size = new Size(1688, 851);
            Home.TabIndex = 0;
            Home.Text = "Inicio";
            // 
            // labelSubtitulo
            // 
            labelSubtitulo.AutoSize = true;
            labelSubtitulo.Depth = 0;
            labelSubtitulo.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            labelSubtitulo.Location = new Point(3, 351);
            labelSubtitulo.MouseState = MouseState.HOVER;
            labelSubtitulo.Name = "labelSubtitulo";
            labelSubtitulo.Size = new Size(430, 19);
            labelSubtitulo.TabIndex = 3;
            labelSubtitulo.Text = "Sistema de gestión de préstamos de equipos y herramientas.";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Location = new Point(3, 3);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1682, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // Logo
            // 
            Logo.Image = (Image)resources.GetObject("Logo.Image");
            Logo.Location = new Point(3, 4);
            Logo.Margin = new Padding(3, 4, 3, 4);
            Logo.Name = "Logo";
            Logo.Size = new Size(512, 206);
            Logo.SizeMode = PictureBoxSizeMode.AutoSize;
            Logo.TabIndex = 0;
            Logo.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(panelBuscador);
            tabPage2.Controls.Add(TablaPrestamos);
            tabPage2.ImageKey = "swap_horiz_32dp_000000_FILL0_wght400_GRAD0_opsz40.png";
            tabPage2.Location = new Point(4, 54);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1688, 851);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Prestamo";
            // 
            // panelBuscador
            // 
            panelBuscador.BackColor = Color.White;
            panelBuscador.Controls.Add(picLupa);
            panelBuscador.Controls.Add(BuscadorTxb);
            panelBuscador.Location = new Point(118, 51);
            panelBuscador.Name = "panelBuscador";
            panelBuscador.Size = new Size(300, 46);
            panelBuscador.TabIndex = 7;
            // 
            // picLupa
            // 
            picLupa.Image = (Image)resources.GetObject("picLupa.Image");
            picLupa.Location = new Point(58, 6);
            picLupa.Name = "picLupa";
            picLupa.Size = new Size(30, 30);
            picLupa.SizeMode = PictureBoxSizeMode.Zoom;
            picLupa.TabIndex = 7;
            picLupa.TabStop = false;
            // 
            // BuscadorTxb
            // 
            BuscadorTxb.BackColor = Color.White;
            BuscadorTxb.BorderStyle = BorderStyle.None;
            BuscadorTxb.Location = new Point(94, 12);
            BuscadorTxb.Name = "BuscadorTxb";
            BuscadorTxb.Size = new Size(192, 20);
            BuscadorTxb.TabIndex = 6;
            BuscadorTxb.TextChanged += BuscadorTxb_TextChanged;
            BuscadorTxb.Enter += BuscadorTxb_Enter;
            BuscadorTxb.Leave += BuscadorTxb_Leave;
            // 
            // TablaPrestamos
            // 
            TablaPrestamos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TablaPrestamos.Location = new Point(118, 158);
            TablaPrestamos.Name = "TablaPrestamos";
            TablaPrestamos.RowHeadersWidth = 51;
            TablaPrestamos.Size = new Size(1483, 650);
            TablaPrestamos.TabIndex = 4;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(TablaHerramientas);
            tabPage3.Controls.Add(panelBuscadorHerramientas);
            tabPage3.ImageKey = "service_toolbox_32dp_000000_FILL0_wght400_GRAD0_opsz40.png";
            tabPage3.Location = new Point(4, 54);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1688, 851);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Herramientas";
            // 
            // TablaHerramientas
            // 
            TablaHerramientas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TablaHerramientas.Location = new Point(137, 141);
            TablaHerramientas.Name = "TablaHerramientas";
            TablaHerramientas.RowHeadersWidth = 51;
            TablaHerramientas.Size = new Size(1442, 530);
            TablaHerramientas.TabIndex = 9;
            // 
            // panelBuscadorHerramientas
            // 
            panelBuscadorHerramientas.BackColor = Color.White;
            panelBuscadorHerramientas.Controls.Add(picLupaHerramientas);
            panelBuscadorHerramientas.Controls.Add(txbBuscadorHerramientas);
            panelBuscadorHerramientas.Location = new Point(137, 70);
            panelBuscadorHerramientas.Name = "panelBuscadorHerramientas";
            panelBuscadorHerramientas.Size = new Size(300, 46);
            panelBuscadorHerramientas.TabIndex = 8;
            // 
            // picLupaHerramientas
            // 
            picLupaHerramientas.Image = (Image)resources.GetObject("picLupaHerramientas.Image");
            picLupaHerramientas.Location = new Point(58, 6);
            picLupaHerramientas.Name = "picLupaHerramientas";
            picLupaHerramientas.Size = new Size(30, 30);
            picLupaHerramientas.SizeMode = PictureBoxSizeMode.Zoom;
            picLupaHerramientas.TabIndex = 7;
            picLupaHerramientas.TabStop = false;
            // 
            // txbBuscadorHerramientas
            // 
            txbBuscadorHerramientas.BackColor = Color.White;
            txbBuscadorHerramientas.BorderStyle = BorderStyle.None;
            txbBuscadorHerramientas.Location = new Point(94, 12);
            txbBuscadorHerramientas.Name = "txbBuscadorHerramientas";
            txbBuscadorHerramientas.Size = new Size(192, 20);
            txbBuscadorHerramientas.TabIndex = 6;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "home_32dp_000000_FILL0_wght400_GRAD0_opsz40.png");
            imageList1.Images.SetKeyName(1, "swap_horiz_32dp_000000_FILL0_wght400_GRAD0_opsz40.png");
            imageList1.Images.SetKeyName(2, "service_toolbox_32dp_000000_FILL0_wght400_GRAD0_opsz40.png");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1702, 976);
            Controls.Add(materialTabControl1);
            DrawerTabControl = materialTabControl1;
            Name = "Form1";
            materialTabControl1.ResumeLayout(false);
            Home.ResumeLayout(false);
            Home.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            tabPage2.ResumeLayout(false);
            panelBuscador.ResumeLayout(false);
            panelBuscador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLupa).EndInit();
            ((System.ComponentModel.ISupportInitialize)TablaPrestamos).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TablaHerramientas).EndInit();
            panelBuscadorHerramientas.ResumeLayout(false);
            panelBuscadorHerramientas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLupaHerramientas).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabPage Home;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private PictureBox Logo;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private ToolStrip toolStrip1;
        private DataGridView TablaPrestamos;
        private MaterialLabel labelSubtitulo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox BuscadorTxb;
        private ImageList imageList1;
        private PictureBox picLupa;
        private Panel panelBuscador;
        private DataGridView TablaHerramientas;
        private Panel panelBuscadorHerramientas;
        private PictureBox picLupaHerramientas;
        private TextBox txbBuscadorHerramientas;
    }
}