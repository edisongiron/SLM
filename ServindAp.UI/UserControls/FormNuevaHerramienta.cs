using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace ServindAp.UI.UserControls
{
    public partial class FormNuevaHerramienta : Form
    {
        private GradientPanel panelFondo;

       

        public FormNuevaHerramienta()
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

            panelContenedor2.Parent = panelFondo;
            panelContenedor2.BringToFront();

            this.Shown += FormNuevaHerramienta_Shown;
        }

        private async void FormNuevaHerramienta_Shown(object? sender, EventArgs e)
        {
            await Task.Delay(50);  // Este sí usa await, se queda con async
            this.ActiveControl = null;
            panelFondo.Focus();
        }

        private void FormNuevaHerramienta_Load(object sender, EventArgs e)
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



        private void EstilizarYPosicionarTodo()
        {
            // PANEL CONTENEDOR
            panelContenedor2.Width = 850;
            panelContenedor2.Height = 500;  // Más pequeño porque solo tiene 3 campos
            panelContenedor2.BackColor = Color.White;
            panelContenedor2.Padding = new Padding(50);

            // TÍTULO 
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(33, 37, 41);
            label1.AutoSize = true;
            label1.Text = "Nueva Herramienta";
            label1.Location = new Point((panelContenedor2.Width - label1.Width) / 2, 30);

            // LABELS 
            materialLabel2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            materialLabel2.ForeColor = Color.FromArgb(33, 37, 41);
            

            NameCantidad.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            NameCantidad.ForeColor = Color.FromArgb(33, 37, 41);
            

            materialLabel1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            materialLabel1.ForeColor = Color.FromArgb(33, 37, 41);
            

            // INPUTS 
            txtNombre.Font = new Font("Segoe UI", 11F);
            txtNombre.BackColor = Color.FromArgb(248, 249, 250);

            cmbCant.Font = new Font("Segoe UI", 11F);
            cmbCant.BackColor = Color.FromArgb(248, 249, 250);
            // Agregar opciones al ComboBox de cantidad
            cmbCant.Items.Clear();
            for (int i = 1; i <= 100; i++)
            {
                cmbCant.Items.Add(i);
            }

            txtDescripcion.Font = new Font("Segoe UI", 11F);
            txtDescripcion.BackColor = Color.FromArgb(248, 249, 250);
            txtDescripcion.Multiline = true;  // Para que sea más grande

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

            // POSICIONES 
            int margenIzq = 55;
            int margenDer = 450;
            int espacio = 65;

            // COLUMNA IZQUIERDA
            materialLabel2.Location = new Point(margenIzq, 100);
            txtNombre.Location = new Point(margenIzq, 125);
            txtNombre.Size = new Size(330, 40);

            // COLUMNA DERECHA
            NameCantidad.Location = new Point(margenDer + 100, 100);
            cmbCant.Location = new Point(margenDer + 100, 125);
            cmbCant.Size = new Size(200, 40);

            // DESCRIPCIÓN - Ancho completo
            materialLabel1.Location = new Point(margenIzq, 100 + espacio + 40);
            txtDescripcion.Location = new Point(margenIzq, 125 + espacio + 40);
            txtDescripcion.Size = new Size(720, 80);  // Más alto para descripción

            // BOTONES - Centrados
            int centroPanel = panelContenedor2.Width / 2;
            btnAgregar.Location = new Point(centroPanel - 155, 360);
            btnCancelar.Location = new Point(centroPanel + 15, 360);
        }

        private void CentrarPanel()
        {
            panelContenedor2.Left = (this.ClientSize.Width - panelContenedor2.Width) / 2;
            panelContenedor2.Top = (this.ClientSize.Height - panelContenedor2.Height) / 2;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (panelContenedor2 != null)
            {
                CentrarPanel();
            }
        }

        private void AgregarSombraPanel()
        {
            // Capa 1 - Más alejada (difuminado externo)
            Panel sombra1 = new Panel();
            sombra1.BackColor = Color.FromArgb(20, 0, 0, 0);
            sombra1.Size = new Size(
                panelContenedor2.Width + 20,
                panelContenedor2.Height + 20
            );
            sombra1.Location = new Point(
                panelContenedor2.Left + 10,
                panelContenedor2.Top + 10
            );
            panelFondo.Controls.Add(sombra1);

            // Capa 2 - Intermedia
            Panel sombra2 = new Panel();
            sombra2.BackColor = Color.FromArgb(35, 0, 0, 0);
            sombra2.Size = new Size(
                panelContenedor2.Width + 14,
                panelContenedor2.Height + 14
            );
            sombra2.Location = new Point(
                panelContenedor2.Left + 7,
                panelContenedor2.Top + 7
            );
            panelFondo.Controls.Add(sombra2);

            // Capa 3 - Más cercana (núcleo de la sombra)
            Panel sombra3 = new Panel();
            sombra3.BackColor = Color.FromArgb(50, 0, 0, 0);
            sombra3.Size = new Size(
                panelContenedor2.Width + 8,
                panelContenedor2.Height + 8
            );
            sombra3.Location = new Point(
                panelContenedor2.Left + 4,
                panelContenedor2.Top + 4
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
            pathPanel.AddArc(panelContenedor2.Width - radio, 0, radio, radio, 270, 90);
            pathPanel.AddArc(panelContenedor2.Width - radio, panelContenedor2.Height - radio, radio, radio, 0, 90);
            pathPanel.AddArc(0, panelContenedor2.Height - radio, radio, radio, 90, 90);
            pathPanel.CloseFigure();
            panelContenedor2.Region = new Region(pathPanel);
        }

        // PANEL DEGRADADO
        public class GradientPanel : Panel
        {
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Color Color1 { get; set; } = Color.FromArgb(76, 175, 80);   // Verde
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
            btnAgregar.BackColor = Color.FromArgb(39, 174, 96);
        }

        private void btnAgregar_MouseLeave(object sender, EventArgs e)
        {
            btnAgregar.BackColor = Color.FromArgb(46, 204, 113);
        }

        // HOVER BOTÓN CANCELAR
        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            btnCancelar.BackColor = Color.FromArgb(127, 140, 141);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            btnCancelar.BackColor = Color.FromArgb(149, 165, 166);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Por favor ingresa el nombre de la herramienta",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNombre.Focus();
                return false;
            }

            if (cmbCant.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Por favor selecciona la cantidad",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                cmbCant.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show(
                    "Por favor ingresa una descripción",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtDescripcion.Focus();
                return false;
            }

            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario()) return;

                // AQUÍ VA LA LÓGICA PARA GUARDAR EN LA BASE DE DATOS
                // Por ahora solo mostramos el mensaje de éxito

                MessageBox.Show(
                    "Herramienta agregada exitosamente",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

