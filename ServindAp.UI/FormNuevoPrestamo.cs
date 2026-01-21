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

            RedondearPanel();
            AgregarSombraPanel();

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

            panelContenedor.Width = 850;
            panelContenedor.Height = 550;
            panelContenedor.BackColor = Color.White;
            panelContenedor.Padding = new Padding(35);

            // FORMULARIO Y PANEL 

            panelContenedor.BackColor = Color.White;
            panelContenedor.Padding = new Padding(50);

            // TÍTULO 
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(33, 37, 41);
            label1.AutoSize = true;
            label1.Location = new Point((panelContenedor.Width - label1.Width) / 2, 30);

            // LABELS 
            materialLabel1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            materialLabel1.ForeColor = Color.FromArgb(33, 37, 41);

            materialLabel2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            materialLabel2.ForeColor = Color.FromArgb(33, 37, 41);

            materialLabel3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            materialLabel3.ForeColor = Color.FromArgb(33, 37, 41);

            materialLabel4.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            materialLabel4.ForeColor = Color.FromArgb(33, 37, 41);

            Cantidad.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            Cantidad.ForeColor = Color.FromArgb(33, 37, 41);

            //  INPUTS 
            txtResponsable.Font = new Font("Segoe UI", 11F);
            txtResponsable.BackColor = Color.FromArgb(248, 249, 250);

            txtObservaciones.Font = new Font("Segoe UI", 11F);
            txtObservaciones.BackColor = Color.FromArgb(248, 249, 250);

            cmbHerramienta.Font = new Font("Segoe UI", 11F);
            cmbHerramienta.BackColor = Color.FromArgb(248, 249, 250);

            materialComboBox1.Font = new Font("Segoe UI", 11F);
            materialComboBox1.BackColor = Color.FromArgb(248, 249, 250);

            FechaEntrega.Font = new Font("Segoe UI", 10F);
            FechaEntrega.Enabled = false;

            // BOTONES 
            // AGREGAR 
            btnAgregar.BackColor = Color.FromArgb(46, 204, 113); // Verde esmeralda
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAgregar.Size = new Size(140, 42);
            btnAgregar.Text = "AGREGAR";
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.FlatAppearance.BorderSize = 0;

            // CANCELAR 
            btnCancelar.BackColor = Color.FromArgb(149, 165, 166); // Gris neutro
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancelar.Size = new Size(140, 42);
            btnCancelar.Text = "CANCELAR";
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.UseVisualStyleBackColor = false;

            //  POSICIONES 
            int margenIzq = 55;
            int margenDer = 450;
            int espacio = 65;

            // COLUMNA IZQUIERDA
            materialLabel1.Location = new Point(margenIzq, 100);
            cmbHerramienta.Location = new Point(margenIzq, 125);
            cmbHerramienta.Size = new Size(330, 40);

            materialLabel2.Location = new Point(margenIzq, 100 + espacio + 40);
            txtResponsable.Location = new Point(margenIzq, 125 + espacio + 40);
            txtResponsable.Size = new Size(330, 40);

            // COLUMNA DERECHA
            materialLabel3.Location = new Point(margenDer, 100);
            FechaEntrega.Location = new Point(margenDer, 125);
            FechaEntrega.Size = new Size(320, 28);

            Cantidad.Location = new Point(margenDer, 100 + espacio + 40);
            materialComboBox1.Location = new Point(margenDer, 125 + espacio + 40);
            materialComboBox1.Size = new Size(120, 40);

            // OBSERVACIONES - Ancho completo
            materialLabel4.Location = new Point(margenIzq, 315);
            txtObservaciones.Location = new Point(margenIzq, 340);
            txtObservaciones.Size = new Size(720, 40);

            // BOTONES - Centrados
            int centroPanel = panelContenedor.Width / 2;
            btnAgregar.Location = new Point(centroPanel - 155, 420);
            btnCancelar.Location = new Point(centroPanel + 15, 420);

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


        private void AgregarSombraPanel()
        {
            // Capa 1 - Más alejada (difuminado externo)
            Panel sombra1 = new Panel();
            sombra1.BackColor = Color.FromArgb(20, 0, 0, 0); // Aumentado de 10 a 20
            sombra1.Size = new Size(
                panelContenedor.Width + 20,
                panelContenedor.Height + 20
            );
            sombra1.Location = new Point(
                panelContenedor.Left + 10,
                panelContenedor.Top + 10
            );
            panelFondo.Controls.Add(sombra1);

            // Capa 2 - Intermedia
            Panel sombra2 = new Panel();
            sombra2.BackColor = Color.FromArgb(35, 0, 0, 0); // Aumentado de 15 a 35
            sombra2.Size = new Size(
                panelContenedor.Width + 14,
                panelContenedor.Height + 14
            );
            sombra2.Location = new Point(
                panelContenedor.Left + 7,
                panelContenedor.Top + 7
            );
            panelFondo.Controls.Add(sombra2);

            // Capa 3 - Más cercana (núcleo de la sombra)
            Panel sombra3 = new Panel();
            sombra3.BackColor = Color.FromArgb(50, 0, 0, 0); // Aumentado de 25 a 50
            sombra3.Size = new Size(
                panelContenedor.Width + 8,
                panelContenedor.Height + 8
            );
            sombra3.Location = new Point(
                panelContenedor.Left + 4,
                panelContenedor.Top + 4
            );
            panelFondo.Controls.Add(sombra3);

            // Enviar todas las sombras atrás
            sombra1.SendToBack();
            sombra2.SendToBack();
            sombra3.SendToBack();
        }


        private void RedondearPanel()
        {
            int radio = 8; 
            GraphicsPath pathPanel = new GraphicsPath();
            pathPanel.AddArc(0, 0, radio, radio, 180, 90);
            pathPanel.AddArc(panelContenedor.Width - radio, 0, radio, radio, 270, 90);
            pathPanel.AddArc(panelContenedor.Width - radio, panelContenedor.Height - radio, radio, radio, 0, 90);
            pathPanel.AddArc(0, panelContenedor.Height - radio, radio, radio, 90, 90);
            pathPanel.CloseFigure();
            panelContenedor.Region = new Region(pathPanel);
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
            btnAgregar.BackColor = Color.FromArgb(39, 174, 96); // Verde más oscuro
        }

        private void btnAgregar_MouseLeave(object sender, EventArgs e)
        {
            btnAgregar.BackColor = Color.FromArgb(46, 204, 113); // Verde original
        }


        // HOVER BOTÓN CANCELAR
        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            btnCancelar.BackColor = Color.FromArgb(127, 140, 141); // Gris más oscuro
        }


        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            btnCancelar.BackColor = Color.FromArgb(149, 165, 166); // Gris original
        }


        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {
        }



        private bool ValidarFormulario()
        {
            if (cmbHerramienta.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Por favor selecciona una herramienta",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                cmbHerramienta.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtResponsable.Text))
            {
                MessageBox.Show(
                    "Por favor ingresa el nombre del responsable",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtResponsable.Focus();
                return false;
            }

            if (materialComboBox1.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Por favor selecciona la cantidad",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                materialComboBox1.Focus();
                return false;
            }

            if (FechaEntrega.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show(
                    "La fecha de entrega debe ser hoy o posterior",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            return true;
        }


       
    }
}