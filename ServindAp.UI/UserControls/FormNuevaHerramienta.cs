using ServindAp.Application.Interfaces;
using ServindAp.Application.UseCases;
using ServindAp.Domain.Entities;
using ServindAp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ServindAp.UI.UserControls
{
    public partial class FormNuevaHerramienta : Form
    {
        private GradientPanel panelFondo;
        private readonly IApplicationService _appService;


        public FormNuevaHerramienta(IApplicationService appService)
        {
            InitializeComponent();
            _appService = appService;

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
            await Task.Delay(50);
            this.ActiveControl = null;
            panelFondo.Focus();
        }

        private void FormNuevaHerramienta_Load(object sender, EventArgs e)
        {
            EstilizarYPosicionarTodo();
            CentrarPanel();
            RedondearPanel();

            if (btnAgregar != null)
                btnAgregar.Region = new Region(GetRoundedPath(btnAgregar.ClientRectangle, 12));

            if (btnCancelar != null)
                btnCancelar.Region = new Region(GetRoundedPath(btnCancelar.ClientRectangle, 12));
        }



        private void EstilizarYPosicionarTodo()
        {
            // PANEL CONTENEDOR
            panelContenedor2.Width = 850;
            panelContenedor2.Height = 500;
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


            lblStock.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStock.ForeColor = Color.FromArgb(33, 37, 41);


            materialLabel1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            materialLabel1.ForeColor = Color.FromArgb(33, 37, 41);


            // INPUTS 
            txtNombre.BackColor = Color.FromArgb(248, 249, 250);

            txtDescripcion.Font = new Font("Segoe UI", 11F);
            txtDescripcion.BackColor = Color.FromArgb(248, 249, 250);
            txtDescripcion.Multiline = true;
            txtDescripcion.MaxLength = 0;

            // BOTONES 
            // AGREGAR 
            btnAgregar.BackColor = Color.FromArgb(46, 204, 113);
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAgregar.Size = new Size(140, 42);
            btnAgregar.Text = "AGREGAR";
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.FlatAppearance.BorderSize = 0;

            // CANCELAR 
            btnCancelar.BackColor = Color.FromArgb(149, 165, 166);
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
            lblStock.Location = new Point(margenDer, 100);
            txtStock.Location = new Point(margenDer, 125);
            txtStock.Size = new Size(200, 40);
            txtStock.Multiline = false;

            // DESCRIPCIÓN - Ancho completo
            materialLabel1.Location = new Point(margenIzq, 100 + espacio + 40);
            txtDescripcion.Location = new Point(margenIzq, 125 + espacio + 40);
            txtDescripcion.Size = new Size(720, 80);

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
            public Color Color1 { get; set; } = Color.FromArgb(76, 175, 80);
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Color Color2 { get; set; } = Color.FromArgb(33, 150, 243);

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

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es requerido", "Validación");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtStock.Text))
                {
                    MessageBox.Show("El stock es requerido", "Validación");
                    return;
                }

                if (!int.TryParse(txtStock.Text.Trim(), out int stock))
                {
                    MessageBox.Show("El stock debe ser un número", "Validación");
                    return;
                }

                var request = new CrearHerramientaRequest
                {
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text)
                        ? null
                        : txtDescripcion.Text.Trim(),
                    Stock = stock
                };

                var herramienta = await _appService.CrearHerramienta.ExecuteAsync(request);

                MessageBox.Show($"Herramienta creada con ID: {herramienta.Id}");

                this.Close();
            }
            catch (DatoRequeridoException ex)
            {
                MessageBox.Show(ex.Message, "Validación");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }
    }
}