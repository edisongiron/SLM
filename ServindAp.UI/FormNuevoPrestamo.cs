using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ServindAp.UI
{
    public partial class FormNuevoPrestamo : Form
    {
        private GradientPanel panelFondo;

        public FormNuevoPrestamo()
        {
            InitializeComponent();

            // ABRIR MAXIMIZADO
            this.WindowState = FormWindowState.Maximized;

            this.StartPosition = FormStartPosition.CenterScreen;

            // FONDO DEGRADADO
            panelFondo = new GradientPanel();
            panelFondo.Dock = DockStyle.Fill;
            panelFondo.Color1 = Color.FromArgb(46, 204, 113);  // Verde esmeralda
            panelFondo.Color2 = Color.FromArgb(52, 152, 219);  // Azul moderno
            this.Controls.Add(panelFondo);

            panelContenedor.Parent = panelFondo;
            panelContenedor.BringToFront();

            this.Shown += FormNuevoPrestamo_Shown;

           
        }


        private void FormNuevoPrestamo_Load(object sender, EventArgs e)
        {
            EstilizarYPosicionarTodo();
            CentrarPanel();
            if (btnAgregar != null)
                btnAgregar.Region = new Region(GetRoundedPath(btnAgregar.ClientRectangle, 12));

            if (btnCancelar != null)
                btnCancelar.Region = new Region(GetRoundedPath(btnCancelar.ClientRectangle, 12));
        }
        private async void FormNuevoPrestamo_Shown(object sender, EventArgs e)
        {
            await Task.Delay(50); 
            this.ActiveControl = null;
            panelFondo.Focus();
        }



        private void EstilizarYPosicionarTodo()
        {

            // FORMULARIO Y PANEL 

            panelContenedor.BackColor = Color.White;
            panelContenedor.Padding = new Padding(50);

            // TÍTULO 
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(33, 37, 41);
            label1.AutoSize = true;
            label1.Location = new Point((panelContenedor.Width - 250) / 2, 40);

            // LABELS 
            materialLabel1.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            materialLabel1.ForeColor = Color.FromArgb(33, 37, 41);

            materialLabel2.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            materialLabel2.ForeColor = Color.FromArgb(33, 37, 41);

            materialLabel3.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            materialLabel3.ForeColor = Color.FromArgb(33, 37, 41);

            materialLabel4.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            materialLabel4.ForeColor = Color.FromArgb(33, 37, 41);

            Cantidad.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            Cantidad.ForeColor = Color.FromArgb(33, 37, 41);

            //  INPUTS 
            txtResponsable.Font = new Font("Segoe UI", 12F);
            txtResponsable.BackColor = Color.FromArgb(248, 249, 250);

            txtObservaciones.Font = new Font("Segoe UI", 12F);
            txtObservaciones.BackColor = Color.FromArgb(248, 249, 250);

            cmbHerramienta.Font = new Font("Segoe UI", 12F);
            cmbHerramienta.BackColor = Color.FromArgb(248, 249, 250);

            materialComboBox1.Font = new Font("Segoe UI", 12F);
            materialComboBox1.BackColor = Color.FromArgb(248, 249, 250);

            FechaEntrega.Font = new Font("Segoe UI", 12F);
            FechaEntrega.Enabled = false;

            // BOTONES 
            // AGREGAR 
            btnAgregar.BackColor = Color.FromArgb(55, 65, 81);
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnAgregar.Size = new Size(150, 45);
            btnAgregar.Text = "AGREGAR";
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.FlatAppearance.BorderSize = 0;

            // CANCELAR 
            btnCancelar.BackColor = Color.FromArgb(0, 123, 255);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancelar.Size = new Size(150, 45);
            btnCancelar.Text = "CANCELAR";
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.UseVisualStyleBackColor = false;

            //  POSICIONES 
            int margenIzq = 80;
            int margenDer = 580;
            int espacio = 80;

            // COLUMNA IZQUIERDA
            materialLabel1.Location = new Point(margenIzq, 140);
            cmbHerramienta.Location = new Point(margenIzq, 170);
            cmbHerramienta.Size = new Size(380, 50);

            materialLabel2.Location = new Point(margenIzq, 140 + espacio + 50);
            txtResponsable.Location = new Point(margenIzq, 170 + espacio + 50);
            txtResponsable.Size = new Size(380, 50);

            // COLUMNA DERECHA
            materialLabel3.Location = new Point(margenDer, 140);
            FechaEntrega.Location = new Point(margenDer, 170);
            FechaEntrega.Size = new Size(350, 30);

            Cantidad.Location = new Point(margenDer, 140 + espacio + 50);
            materialComboBox1.Location = new Point(margenDer, 170 + espacio + 50);
            materialComboBox1.Size = new Size(150, 50);

            // OBSERVACIONES - Ancho completo
            materialLabel4.Location = new Point(margenIzq, 370);
            txtObservaciones.Location = new Point(margenIzq, 400);
            txtObservaciones.Size = new Size(850, 50);

            // BOTONES - Centrados
            int centroPanel = panelContenedor.Width / 2;
            btnAgregar.Location = new Point(centroPanel - 200, 500);
            btnCancelar.Location = new Point(centroPanel + 20, 500);

            // CENTRAR EL PANEL cuando se maximiza o cambia de tamaño
            
        }

        private void CentrarPanel()
        {
            panelContenedor.Left = (this.ClientSize.Width - panelContenedor.Width) / 2;
            panelContenedor.Top = (this.ClientSize.Height - panelContenedor.Height) / 2;
        }

        // Evento para centrar el panel cuando se redimensiona el formulario
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (panelContenedor != null)
            {
                CentrarPanel();
            }

        }



        //Panel degradado

        public class GradientPanel : Panel
        {
            public Color Color1 { get; set; } = Color.FromArgb(76, 175, 80);   // Verde
            public Color Color2 { get; set; } = Color.FromArgb(33, 150, 243);  // Azul

            protected override void OnPaint(PaintEventArgs e)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    this.ClientRectangle,
                    Color1,
                    Color2,
                    LinearGradientMode.ForwardDiagonal))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }

                base.OnPaint(e);
            }
        }


        private void ConfigurarBoton(Button btn, string texto)
        {
            btn.Text = texto;
            btn.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btn.Size = new Size(150, 45);

            btn.BackColor = Color.FromArgb(209, 213, 219);   // Gris claro
            btn.ForeColor = Color.FromArgb(31, 41, 55);      // Gris oscuro texto

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.UseVisualStyleBackColor = false;

            btn.Cursor = Cursors.Hand;

            // Bordes redondeados
            btn.Region = new Region(GetRoundedPath(btn.ClientRectangle, 12));
        }


        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }


        // HOVER BOTÓN AGREGAR
        private void btnAgregar_MouseEnter(object sender, EventArgs e)
        {
            btnAgregar.BackColor = Color.FromArgb(156, 163, 175);
        }

        private void btnAgregar_MouseLeave(object sender, EventArgs e)
        {
            btnAgregar.BackColor = Color.FromArgb(209, 213, 219);
        }

        // HOVER BOTÓN CANCELAR
        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            btnCancelar.BackColor = Color.FromArgb(156, 163, 175);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            btnCancelar.BackColor = Color.FromArgb(209, 213, 219);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}