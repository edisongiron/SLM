using ServindAp.Application.Interfaces;
using ServindAp.Application.UseCases;
using ServindAp.Domain.Exceptions;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ServindAp.UI.UserControls
{
    public partial class FormNuevoPrestamo : Form
    {
        private GradientPanel panelFondo;
        private readonly IApplicationService _appService;

        private List<HerramientaControl> herramientasAdicionales = new List<HerramientaControl>();
        private int contadorHerramientas = 0;
        private const int POSICION_BASE_HERRAMIENTAS = 280; // Posición Y donde empiezan las herramientas adicionales

        public FormNuevoPrestamo(IApplicationService appService)
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

            panelContenedor.Parent = panelFondo;
            panelContenedor.BringToFront();

            this.Shown += FormNuevoPrestamo_Shown;


            panelContenedor.Click += (s, e) => panelFondo.Focus();
            panelFondo.Click += (s, e) => panelFondo.Focus();
        }


        private async void FormNuevoPrestamo_Load(object sender, EventArgs e)
        {
            await CargarHerramientas();
            EstilizarYPosicionarTodo();
            CentrarPanel();

            if (btnAgregar != null)
                btnAgregar.Region = new Region(GetRoundedPath(btnAgregar.ClientRectangle, 12));

            if (btnCancelar != null)
                btnCancelar.Region = new Region(GetRoundedPath(btnCancelar.ClientRectangle, 12));

            btnAgregarOtraHerramienta.Click += BtnAgregarOtraHerramienta_Click;

            this.ActiveControl = null;
            panelFondo.Focus();

        }


        private async void FormNuevoPrestamo_Shown(object? sender, EventArgs e)
        {
            await Task.Delay(50);
            this.ActiveControl = null;
            panelFondo.Focus();

            txtResponsable.Parent?.Focus();

        }


        private async Task CargarHerramientas()
        {
            try
            {
                var herramientas = await _appService.ListarHerramientas.ExecuteAsync();

                cmbHerramienta.DataSource = herramientas;
                cmbHerramienta.DisplayMember = "Nombre";
                cmbHerramienta.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
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
   

            //  INPUTS 
            txtResponsable.BackColor = Color.FromArgb(248, 249, 250);

            txtObservaciones.BackColor = Color.FromArgb(248, 249, 250);

            cmbHerramienta.Font = new Font("Segoe UI", 11F);
            cmbHerramienta.BackColor = Color.FromArgb(248, 249, 250);

            FechaEntrega.Font = new Font("Segoe UI", 10F);
            FechaEntrega.Enabled = false;

            txtCantidad.BackColor = Color.FromArgb(248, 249, 250);

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

            
            int margenIzq = 55;
            int margenDer = 450;
            int espacio = 65;

            // COLUMNA IZQUIERDA
            materialLabel2.Location = new Point(margenIzq, 100);  // "RESPONSABLE:"
            txtResponsable.Location = new Point(margenIzq, 125);
            txtResponsable.Size = new Size(380, 50);


            materialLabel1.Location = new Point(margenIzq, 100 + espacio + 40);  // "HERRAMIENTA:" 
            cmbHerramienta.Location = new Point(margenIzq, 125 + espacio + 40);
            cmbHerramienta.Size = new Size(330, 40);

            // COLUMNA DERECHA
            materialLabel3.Location = new Point(margenDer, 100);  // "FECHA ENTREGA:"
            FechaEntrega.Location = new Point(margenDer, 125);
            FechaEntrega.Size = new Size(320, 28);

            Cantidad.Location = new Point(margenDer, 100 + espacio + 40);  // "CANTIDAD:"
            txtCantidad.Location = new Point(margenDer, 125 + espacio + 40);
            txtCantidad.Size = new Size(180, 40);


            // OBSERVACIONES - Ancho completo
            materialLabel4.Location = new Point(margenIzq, 315);
            txtObservaciones.Location = new Point(margenIzq, 340);
            txtObservaciones.Size = new Size(720, 40);

            // BOTONES - Centrados
            int centroPanel = panelContenedor.Width / 2;
            btnAgregar.Location = new Point(centroPanel - 155, 420);
            btnCancelar.Location = new Point(centroPanel + 15, 420);

            // CENTRAR EL PANEL cuando se maximiza o cambia de tamaño


            // BOTÓN "+ AGREGAR OTRA HERRAMIENTA"
            btnAgregarOtraHerramienta.Text = "+ Agregar ";
            btnAgregarOtraHerramienta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAgregarOtraHerramienta.ForeColor = Color.FromArgb(46, 204, 113); // Verde
            btnAgregarOtraHerramienta.BackColor = Color.Transparent;
            btnAgregarOtraHerramienta.FlatStyle = FlatStyle.Flat;
            btnAgregarOtraHerramienta.FlatAppearance.BorderSize = 0;
            btnAgregarOtraHerramienta.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 255, 245);
            btnAgregarOtraHerramienta.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 250, 230);
            btnAgregarOtraHerramienta.Size = new Size(200, 40);
            btnAgregarOtraHerramienta.Cursor = Cursors.Hand;
            btnAgregarOtraHerramienta.Location = new Point(660, 205); 
        }

        private void CentrarPanel()
        {
            panelContenedor.Left = (this.ClientSize.Width - panelContenedor.Width) / 2;
            panelContenedor.Top = (this.ClientSize.Height - panelContenedor.Height) / 2;
        }

        // centra el panel cuando se redimensiona el formulario
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (panelContenedor != null)
            {
                CentrarPanel();
            }

        }


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

            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show(
                    "Por favor ingresa la cantidad",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            if (!int.TryParse(txtCantidad.Text.Trim(), out _))
            {
                MessageBox.Show(
                    "La cantidad debe ser un número",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }
       
            return true;
        }


        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario()) return;

                var listaHerramientas = new List<HerramientaPrestamoRequest>();

                // 1. Agregar la herramienta PRINCIPAL
                listaHerramientas.Add(new HerramientaPrestamoRequest
                {
                    HerramientaId = int.Parse(cmbHerramienta.SelectedValue?.ToString() ?? "0"),
                    Cantidad = int.Parse(txtCantidad.Text.Trim())
                });

                // 2. Agregar herramientas ADICIONALES
                foreach (var herr in herramientasAdicionales)
                {
                    // Validar que tenga herramienta seleccionada y cantidad
                    if (herr.CmbHerramienta.SelectedValue != null &&
                        !string.IsNullOrWhiteSpace(herr.TxtCantidad.Text))
                    {
                        int cantidad;
                        if (int.TryParse(herr.TxtCantidad.Text.Trim(), out cantidad) && cantidad > 0)
                        {
                            listaHerramientas.Add(new HerramientaPrestamoRequest
                            {
                                HerramientaId = int.Parse(herr.CmbHerramienta.SelectedValue.ToString()),
                                Cantidad = cantidad
                            });
                        }
                    }
                }

                // Crear el préstamo con TODAS las herramientas
                var request = new CrearPrestamoRequest
                {
                    Responsable = txtResponsable.Text.Trim(),
                    FechaEntrega = FechaEntrega.Value,
                    Observaciones = string.IsNullOrWhiteSpace(txtObservaciones.Text)
                        ? null
                        : txtObservaciones.Text.Trim(),
                    Herramientas = listaHerramientas
                };

                var prestamo = await _appService.CrearPrestamo.ExecuteAsync(request);

                MessageBox.Show(
                    $"Préstamo agregado exitosamente con {listaHerramientas.Count} herramienta(s)",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

            }
            catch (DatoRequeridoException ex)
            {
                MessageBox.Show(ex.Message, "Validación");
            }
            catch (StockInsuficienteException ex)
            {
                MessageBox.Show(ex.Message, "Stock Insuficiente");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        public class HerramientaControl
        {
            public MaterialSkin.Controls.MaterialComboBox CmbHerramienta { get; set; } = null!;
            public MaterialSkin.Controls.MaterialLabel LblHerramienta { get; set; } = null!;
            public MaterialSkin.Controls.MaterialLabel LblCantidad { get; set; } = null!;
            public MaterialSkin.Controls.MaterialTextBox TxtCantidad { get; set; } = null!;
            public Button BtnEliminar { get; set; } = null!;
            public int PosicionY { get; set; }
        }


        // "+ Agregar otra herramienta"
        private async void BtnAgregarOtraHerramienta_Click(object sender, EventArgs e)
        {
            panelContenedor.SuspendLayout();

            try
            {
                contadorHerramientas++;
                int posicionY = POSICION_BASE_HERRAMIENTAS + (contadorHerramientas * 80);

                var nuevoControl = new HerramientaControl
                {
                    PosicionY = posicionY,

                    LblHerramienta = new MaterialSkin.Controls.MaterialLabel
                    {
                        Text = "HERRAMIENTA:",
                        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(33, 37, 41),
                        Location = new Point(55, posicionY),
                        AutoSize = true,
                        Depth = 0
                    },

                    CmbHerramienta = new MaterialSkin.Controls.MaterialComboBox
                    {
                        Font = new Font("Segoe UI", 11F),
                        BackColor = Color.FromArgb(248, 249, 250),
                        Location = new Point(55, posicionY + 25),
                        Size = new Size(280, 49),
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Depth = 0
                    },

                    LblCantidad = new MaterialSkin.Controls.MaterialLabel
                    {
                        Text = "CANTIDAD:",
                        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(33, 37, 41),
                        Location = new Point(360, posicionY),
                        AutoSize = true,
                        Depth = 0
                    },

                    TxtCantidad = new MaterialSkin.Controls.MaterialTextBox
                    {
                        Font = new Font("Segoe UI", 11F),
                        BackColor = Color.FromArgb(248, 249, 250),
                        Location = new Point(360, posicionY + 25),
                        Size = new Size(180, 40),
                        BorderStyle = BorderStyle.None,
                        Depth = 0
                    },

                    BtnEliminar = new Button
                    {
                        Text = "", 
                        BackColor = Color.FromArgb(239, 83, 80),
                        FlatStyle = FlatStyle.Flat,
                        Size = new Size(35, 35),
                        Location = new Point(560, posicionY + 25),
                        Cursor = Cursors.Hand,
                        TabStop = false,

                        BackgroundImage = Properties.Resources.Icono,
                        BackgroundImageLayout = ImageLayout.Zoom
                    }
                };

                nuevoControl.BtnEliminar.FlatAppearance.BorderSize = 0;


                nuevoControl.BtnEliminar.MouseEnter += (s, ev) =>
                {
                    nuevoControl.BtnEliminar.BackColor = Color.FromArgb(229, 57, 53);
                };

                nuevoControl.BtnEliminar.MouseLeave += (s, ev) =>
                {
                    nuevoControl.BtnEliminar.BackColor = Color.FromArgb(239, 83, 80);
                };


                var herramientas = await _appService.ListarHerramientas.ExecuteAsync();
                nuevoControl.CmbHerramienta.DataSource = herramientas;
                nuevoControl.CmbHerramienta.DisplayMember = "Nombre";
                nuevoControl.CmbHerramienta.ValueMember = "Id";

                nuevoControl.BtnEliminar.Click += (s, ev) => EliminarHerramienta(nuevoControl);

                panelContenedor.Controls.Add(nuevoControl.LblHerramienta);
                panelContenedor.Controls.Add(nuevoControl.CmbHerramienta);
                panelContenedor.Controls.Add(nuevoControl.LblCantidad);
                panelContenedor.Controls.Add(nuevoControl.TxtCantidad);
                panelContenedor.Controls.Add(nuevoControl.BtnEliminar);

                herramientasAdicionales.Add(nuevoControl);

           
                AjustarPosicionesInferiores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar herramientas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                panelContenedor.ResumeLayout(true);
            }
        }



        private void EliminarHerramienta(HerramientaControl control)
        {
            panelContenedor.SuspendLayout();

            try
            {
                panelContenedor.Controls.Remove(control.LblHerramienta);
                panelContenedor.Controls.Remove(control.CmbHerramienta);
                panelContenedor.Controls.Remove(control.LblCantidad);
                panelContenedor.Controls.Remove(control.TxtCantidad);
                panelContenedor.Controls.Remove(control.BtnEliminar);

                int indiceEliminado = herramientasAdicionales.IndexOf(control);
                herramientasAdicionales.Remove(control);
                contadorHerramientas--;

                // 3. SOLO reorganizar las que están DESPUÉS de la eliminada
                for (int i = indiceEliminado; i < herramientasAdicionales.Count; i++)
                {
                    int nuevaPosY = POSICION_BASE_HERRAMIENTAS + ((i + 1) * 80);
                    var ctrl = herramientasAdicionales[i];

                    ctrl.LblHerramienta.Location = new Point(55, nuevaPosY);
                    ctrl.CmbHerramienta.Location = new Point(55, nuevaPosY + 25);
                    ctrl.LblCantidad.Location = new Point(360, nuevaPosY);
                    ctrl.TxtCantidad.Location = new Point(360, nuevaPosY + 25);
                    ctrl.BtnEliminar.Location = new Point(560, nuevaPosY + 25);
                    ctrl.PosicionY = nuevaPosY;
                }

                AjustarPosicionesInferiores();
            }
            finally
            {
                panelContenedor.ResumeLayout(true);
            }
        }


        // MÉTODO: Reorganizar herramientas después de eliminar una
        private void ReorganizarHerramientas()
        {
            for (int i = 0; i < herramientasAdicionales.Count; i++)
            {
                int nuevaPosY = POSICION_BASE_HERRAMIENTAS + ((i + 1) * 80);
                var control = herramientasAdicionales[i];

                // Actualizar posiciones
                control.LblHerramienta.Location = new Point(55, nuevaPosY);
                control.CmbHerramienta.Location = new Point(55, nuevaPosY + 25);
                control.LblCantidad.Location = new Point(360, nuevaPosY);
                control.TxtCantidad.Location = new Point(360, nuevaPosY + 25);
                control.BtnEliminar.Location = new Point(500, nuevaPosY + 30);
                control.PosicionY = nuevaPosY;
            }

        }


        //Ajustar posiciones de Observaciones y Botones
        private void AjustarPosicionesInferiores()
        {
            int baseY = POSICION_BASE_HERRAMIENTAS + (contadorHerramientas * 80) + 100;

            materialLabel4.Location = new Point(55, baseY);
            txtObservaciones.Location = new Point(55, baseY + 30);

            int centroPanel = panelContenedor.Width / 2;
            btnAgregar.Location = new Point(centroPanel - 155, baseY + 100);
            btnCancelar.Location = new Point(centroPanel + 15, baseY + 100);

            int nuevaAltura = baseY + 200;

            if (nuevaAltura > panelContenedor.Height)
            {
                panelContenedor.Height = nuevaAltura;
                CentrarPanel();
            }
        }



    }
}