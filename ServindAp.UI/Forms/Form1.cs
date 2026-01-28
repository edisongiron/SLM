using MaterialSkin;
using MaterialSkin.Controls;
using ServindAp.Application.Interfaces;
using ServindAp.UI.UserControls;
using ServindAp.Domain.Exceptions;
using System;
using System.Threading.Tasks;
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;

namespace ServindAp.UI.Forms
{
    public partial class Form1 : MaterialForm
    {

        private DataTable? tablaDatos;
        private MaterialButton? btnNuevoPrestamo;
        private DataTable? tablaHerramientas;
        private MaterialButton? btnNuevaHerramienta;

        private readonly IApplicationService _appService;

        public Form1(IApplicationService appService)
        {
            InitializeComponent();
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));

            WindowState = FormWindowState.Maximized;
            ConfigurarMaterialSkin();
            ConfigurarBuscador();
            ConfigurarBuscadorHerramientas();
            CentrarControles();

            this.Resize += (s, e) => CentrarControles();
            tabPage2.Resize += (s, e) => LayoutTabPrestamos();
            tabPage3.Resize += (s, e) => LayoutTabHerramientas();

            //Tabla de Prestamos
            this.Load += Form1_Load;
            TablaPrestamos.AllowUserToResizeRows = false;
            TablaHerramientas.AllowUserToResizeRows = false;
            TablaPrestamos.CellContentClick += TablaPrestamos_CellContentClick;
            TablaPrestamos.CellPainting += TablaPrestamos_CellPainting;
            panelBuscador.Paint += PanelBuscador_Paint;

            TablaHerramientas.CellContentClick += TablaHerramientas_CellContentClick;
            TablaHerramientas.CellPainting += TablaHerramientas_CellPainting;
            panelBuscadorHerramientas.Paint += PanelBuscadorHerramientas_Paint;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                //Prestamos
                ConfigurarEstiloTabla();
                CrearColumnas();
                await CargarDatosPrueba();
                LayoutTabPrestamos(); // ✅ Ejecutar sincrónicamente
                ConfigurarBotonNuevoPrestamo();

                //Herramientas
                ConfigurarEstiloTablaHerramientas();
                CrearColumnasHerramientas();
                await CargarDatosPruebaHerramientas();
                ConfigurarBotonNuevaHerramienta();
                LayoutTabHerramientas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar la interfaz: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CentrarControles()
        {
            if (!IsHandleCreated || Logo == null || labelSubtitulo == null)
                return;

            int logoX = (this.ClientSize.Width - Logo.Width) / 2;
            int logoY = (this.ClientSize.Height - Logo.Height) / 2 - 80;
            Logo.Location = new Point(logoX, logoY);

            int labelX = (this.ClientSize.Width - labelSubtitulo.Width) / 2;
            int labelY = Logo.Bottom + 20;
            labelSubtitulo.Location = new Point(labelX, labelY);
        }

        private void LayoutTabPrestamos()
        {
            if (!IsHandleCreated || TablaPrestamos == null || tabPage2 == null || panelBuscador == null)
                return;

            int margenSuperior = 90;
            int espacioVertical = 15;

            // Usar 95% del ancho para dejar márgenes laterales
            TablaPrestamos.Width = (int)(tabPage2.ClientSize.Width * 0.95);
            TablaPrestamos.Location = new Point(
                (tabPage2.ClientSize.Width - TablaPrestamos.Width) / 2,
                margenSuperior + panelBuscador.Height + espacioVertical
            );

            TablaPrestamos.Height = tabPage2.ClientSize.Height - TablaPrestamos.Top - 25;

            // BUSCADOR
            panelBuscador.Width = 400;
            panelBuscador.Height = 45;
            panelBuscador.Location = new Point(TablaPrestamos.Left, margenSuperior);

            // BOTÓN NUEVO PRÉSTAMO
            if (btnNuevoPrestamo != null)
            {
                btnNuevoPrestamo.Location = new Point(
                    TablaPrestamos.Right - btnNuevoPrestamo.Width,
                    margenSuperior
                );
            }

            RedondearPanelBuscador();
        }

        private void LayoutTabHerramientas()
        {
            if (!IsHandleCreated || TablaHerramientas == null || tabPage3 == null || panelBuscadorHerramientas == null)
                return;

            int margenSuperior = 90;
            int espacioVertical = 15;

            // Usar 95% del ancho para dejar márgenes laterales
            TablaHerramientas.Width = (int)(tabPage3.ClientSize.Width * 0.95);
            TablaHerramientas.Location = new Point(
                (tabPage3.ClientSize.Width - TablaHerramientas.Width) / 2,
                margenSuperior + panelBuscadorHerramientas.Height + espacioVertical
            );

            TablaHerramientas.Height = tabPage3.ClientSize.Height - TablaHerramientas.Top - 25;

            // BUSCADOR
            panelBuscadorHerramientas.Width = 400;
            panelBuscadorHerramientas.Height = 45;
            panelBuscadorHerramientas.Location = new Point(TablaHerramientas.Left, margenSuperior);

            // ELEMENTOS DENTRO DEL PANEL
            if (picLupaHerramientas != null)
            {
                picLupaHerramientas.Width = 24;
                picLupaHerramientas.Height = 24;
                picLupaHerramientas.Location = new Point(15, (panelBuscadorHerramientas.Height - 24) / 2);
                picLupaHerramientas.SizeMode = PictureBoxSizeMode.Zoom;
            }

            if (txbBuscadorHerramientas != null)
            {
                txbBuscadorHerramientas.Location = new Point(
                    (picLupaHerramientas?.Right ?? 15) + 10,
                    (panelBuscadorHerramientas.Height - txbBuscadorHerramientas.Height) / 2
                );
                txbBuscadorHerramientas.Width = panelBuscadorHerramientas.Width - (picLupaHerramientas?.Right ?? 15) - 25;
            }

            // BOTÓN NUEVA HERRAMIENTA
            if (btnNuevaHerramienta != null)
            {
                btnNuevaHerramienta.Location = new Point(
                    TablaHerramientas.Right - btnNuevaHerramienta.Width,
                    margenSuperior
                );
            }

            RedondearPanelBuscadorHerramientas();
        }

        private void ConfigurarMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Green600,
                Primary.Teal700,
                Primary.LightGreen500,
                Accent.Green400,
                TextShade.WHITE
            );

            DrawerShowIconsWhenHidden = true;
            DrawerHighlightWithAccent = true;
            DrawerBackgroundWithAccent = false;
        }

        private void ConfigurarEstiloTabla()
        {
            TablaPrestamos.BackgroundColor = Color.White;
            TablaPrestamos.BorderStyle = BorderStyle.None;
            TablaPrestamos.CellBorderStyle = DataGridViewCellBorderStyle.None;
            TablaPrestamos.GridColor = Color.FromArgb(240, 240, 240);

            TablaPrestamos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            TablaPrestamos.DefaultCellStyle.SelectionForeColor = Color.FromArgb(60, 60, 60);
            TablaPrestamos.DefaultCellStyle.BackColor = Color.White;
            TablaPrestamos.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            TablaPrestamos.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            TablaPrestamos.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            TablaPrestamos.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            TablaPrestamos.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(76, 175, 80);

            TablaPrestamos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            TablaPrestamos.EnableHeadersVisualStyles = false;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            TablaPrestamos.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 80);
            TablaPrestamos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            TablaPrestamos.ColumnHeadersHeight = 45;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            TablaPrestamos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            TablaPrestamos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 245, 245);

            TablaPrestamos.RowTemplate.Height = 50;
            TablaPrestamos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TablaPrestamos.MultiSelect = false;
            TablaPrestamos.AllowUserToAddRows = false;
            TablaPrestamos.ReadOnly = true;
            TablaPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            TablaPrestamos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            TablaPrestamos.EditMode = DataGridViewEditMode.EditProgrammatically;
            TablaPrestamos.RowHeadersVisible = false;
        }

        private void CrearColumnas()
        {
            tablaDatos = new DataTable();

            tablaDatos.Columns.Add("ID", typeof(int));
            tablaDatos.Columns.Add("Responsable", typeof(string));
            tablaDatos.Columns.Add("Herramienta", typeof(string));
            tablaDatos.Columns.Add("Cantidad", typeof(int));
            tablaDatos.Columns.Add("FechaEntrega", typeof(string));
            tablaDatos.Columns.Add("Estado", typeof(string));
            tablaDatos.Columns.Add("Observaciones", typeof(string));

            TablaPrestamos.AutoGenerateColumns = true;
            TablaPrestamos.DataSource = tablaDatos;

            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "btnEditar";
            btnEditar.HeaderText = "";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            btnEditar.Width = 80;
            TablaPrestamos.Columns.Add(btnEditar);

            DataGridViewButtonColumn btnVer = new DataGridViewButtonColumn();
            btnVer.Name = "btnVer";
            btnVer.HeaderText = "";
            btnVer.Text = "Ver";
            btnVer.UseColumnTextForButtonValue = true;
            btnVer.Width = 80;
            TablaPrestamos.Columns.Add(btnVer);

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.Width = 80;
            TablaPrestamos.Columns.Add(btnEliminar);

            // ✅ Anchos optimizados para mejor visualización
            TablaPrestamos.Columns["ID"].Width = 60;
            TablaPrestamos.Columns["Responsable"].Width = 180;
            TablaPrestamos.Columns["Herramienta"].Width = 200;
            TablaPrestamos.Columns["Cantidad"].Width = 100;
            TablaPrestamos.Columns["FechaEntrega"].Width = 130;
            TablaPrestamos.Columns["Estado"].Width = 120;
            TablaPrestamos.Columns["Observaciones"].Width = 250;
        }

        private async Task CargarDatosPrueba()
        {
            try
            {
                tablaDatos?.Rows.Clear();
                var prestamos = await _appService.ListarPrestamos.ExecuteAsync();

                foreach (var prestamo in prestamos)
                {
                    var herramienta = prestamo.Herramientas?.FirstOrDefault()?.Herramienta;
                    tablaDatos?.Rows.Add(
                        prestamo.Id,
                        prestamo.Responsable,
                        herramienta?.Nombre ?? "N/A",
                        prestamo.Herramientas?.FirstOrDefault()?.Cantidad ?? 0,
                        prestamo.FechaEntrega.ToString("dd/MM/yyyy"),
                        prestamo.Estado ?? "Desconocido",
                        prestamo.Observaciones ?? ""
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar préstamos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarEstiloTablaHerramientas()
        {
            TablaHerramientas.BackgroundColor = Color.White;
            TablaHerramientas.BorderStyle = BorderStyle.None;
            TablaHerramientas.CellBorderStyle = DataGridViewCellBorderStyle.None;
            TablaHerramientas.GridColor = Color.FromArgb(240, 240, 240);

            TablaHerramientas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            TablaHerramientas.DefaultCellStyle.SelectionForeColor = Color.FromArgb(60, 60, 60);
            TablaHerramientas.DefaultCellStyle.BackColor = Color.White;
            TablaHerramientas.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            TablaHerramientas.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            TablaHerramientas.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            TablaHerramientas.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            TablaHerramientas.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(76, 175, 80);

            TablaHerramientas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            TablaHerramientas.EnableHeadersVisualStyles = false;
            TablaHerramientas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            TablaHerramientas.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 80);
            TablaHerramientas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            TablaHerramientas.ColumnHeadersHeight = 45;
            TablaHerramientas.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            TablaHerramientas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TablaHerramientas.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            TablaHerramientas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            TablaHerramientas.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 245, 245);

            TablaHerramientas.RowTemplate.Height = 50;
            TablaHerramientas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TablaHerramientas.MultiSelect = false;
            TablaHerramientas.AllowUserToAddRows = false;
            TablaHerramientas.ReadOnly = true;
            TablaHerramientas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            TablaHerramientas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            TablaHerramientas.EditMode = DataGridViewEditMode.EditProgrammatically;
            TablaHerramientas.RowHeadersVisible = false;
        }

        private void CrearColumnasHerramientas()
        {
            tablaHerramientas = new DataTable();

            tablaHerramientas.Columns.Add("ID", typeof(int));
            tablaHerramientas.Columns.Add("Nombre", typeof(string));
            tablaHerramientas.Columns.Add("Descripcion", typeof(string));
            tablaHerramientas.Columns.Add("Stock", typeof(int));

            TablaHerramientas.AutoGenerateColumns = true;
            TablaHerramientas.DataSource = tablaHerramientas;

            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "btnEditarHerr";
            btnEditar.HeaderText = "";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            btnEditar.Width = 90;
            TablaHerramientas.Columns.Add(btnEditar);

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminarHerr";
            btnEliminar.HeaderText = "";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.Width = 90;
            TablaHerramientas.Columns.Add(btnEliminar);

            // ✅ Anchos optimizados para mejor visualización
            TablaHerramientas.Columns["ID"].Width = 80;
            TablaHerramientas.Columns["Nombre"].Width = 250;
            TablaHerramientas.Columns["Descripcion"].Width = 450;
            TablaHerramientas.Columns["Stock"].Width = 120;
        }

        private async Task CargarDatosPruebaHerramientas()
        {
            try
            {
                tablaHerramientas?.Rows.Clear();
                var herramientas = await _appService.ListarHerramientas.ExecuteAsync();

                foreach (var herramienta in herramientas)
                {
                    tablaHerramientas?.Rows.Add(
                        herramienta.Id,
                        herramienta.Nombre,
                        herramienta.Descripcion ?? "Sin descripción",
                        herramienta.Stock
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar herramientas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TablaPrestamos_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var columna = TablaPrestamos.Columns[e.ColumnIndex];
            if (columna == null)
                return;

            if (columna.Name == "btnEditar" || columna.Name == "btnVer" || columna.Name == "btnEliminar")
            {
                if (e.Graphics == null)
                    return;

                // ✅ Pintar fondo primero
                e.PaintBackground(e.CellBounds, true);

                string texto = columna.Name == "btnEditar" ? "Editar" : columna.Name == "btnVer" ? "Ver" : "Eliminar";
                Color fill = columna.Name == "btnEditar" ? Color.FromArgb(66, 165, 245) : columna.Name == "btnVer" ? Color.FromArgb(200, 200, 200) : Color.FromArgb(239, 83, 80);

                // ✅ Margen fijo para mejor control
                int margen = 6;
                Rectangle inner = new Rectangle(
                    e.CellBounds.X + margen,
                    e.CellBounds.Y + 6,
                    e.CellBounds.Width - (margen * 2),
                    e.CellBounds.Height - 12
                );

                if (inner.Width > 0 && inner.Height > 0)
                {
                    using (SolidBrush brush = new SolidBrush(fill))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.FillRectangle(brush, inner);
                    }
                    TextRenderer.DrawText(e.Graphics, texto, new Font("Segoe UI", 9, FontStyle.Bold), inner, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                e.Handled = true;
            }
        }

        private void BuscadorTxb_TextChanged(object sender, EventArgs e)
        {
            if (TablaPrestamos.DataSource == null)
                return;

            string textoBusqueda = BuscadorTxb.Text.Trim();

            if (textoBusqueda == "Buscar...")
                textoBusqueda = "";

            var vista = (TablaPrestamos?.DataSource as DataTable)?.DefaultView;

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                vista?.RowFilter = string.Empty;
            }
            else
            {
                vista?.RowFilter =
                    $"Responsable LIKE '%{textoBusqueda}%' OR " +
                    $"Herramienta LIKE '%{textoBusqueda}%' OR " +
                    $"Estado LIKE '%{textoBusqueda}%'";
            }
        }

        private const string PLACEHOLDER_BUSCADOR = "Buscar...";

        private void ConfigurarBuscador()
        {
            picLupa.Width = 24;
            picLupa.Height = 24;
            picLupa.Location = new Point(15, (panelBuscador.Height - picLupa.Height) / 2);
            picLupa.SizeMode = PictureBoxSizeMode.Zoom;

            BuscadorTxb.Text = PLACEHOLDER_BUSCADOR;
            BuscadorTxb.ForeColor = Color.Gray;
            BuscadorTxb.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            BuscadorTxb.BackColor = Color.FromArgb(245, 245, 245);
            BuscadorTxb.Height = 35;
            BuscadorTxb.Padding = new Padding(5);
            BuscadorTxb.Location = new Point(picLupa.Right + 10, (panelBuscador.Height - BuscadorTxb.Height) / 2);
            BuscadorTxb.Width = panelBuscador.Width - 70;
            BuscadorTxb.TabStop = false;
            BuscadorTxb.HideSelection = true;
            BuscadorTxb.BorderStyle = BorderStyle.None;

            BuscadorTxb.Enter += BuscadorTxb_Enter;
            BuscadorTxb.Leave += BuscadorTxb_Leave;
            BuscadorTxb.TextChanged += BuscadorTxb_TextChanged;
        }

        private void BuscadorTxb_Enter(object? sender, EventArgs e)
        {
            if (BuscadorTxb.Text == PLACEHOLDER_BUSCADOR)
            {
                BuscadorTxb.Text = "";
                BuscadorTxb.ForeColor = Color.Black;
            }
        }

        private void BuscadorTxb_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BuscadorTxb.Text))
            {
                BuscadorTxb.Text = PLACEHOLDER_BUSCADOR;
                BuscadorTxb.ForeColor = Color.Gray;
            }
        }

        private void RedondearPanelBuscador()
        {
            int radio = 15;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(panelBuscador.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(panelBuscador.Width - radio, panelBuscador.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, panelBuscador.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();

            panelBuscador.Region = new Region(path);
        }

        private void PanelBuscador_Paint(object? sender, PaintEventArgs e)
        {
            int radio = 18;
            int grosor = 2;

            Rectangle rect = panelBuscador.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(Color.FromArgb(180, 180, 180), grosor))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                path.AddArc(rect.X, rect.Y, radio, radio, 180, 90);
                path.AddArc(rect.Right - radio, rect.Y, radio, radio, 270, 90);
                path.AddArc(rect.Right - radio, rect.Bottom - radio, radio, radio, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radio, radio, radio, 90, 90);
                path.CloseFigure();

                panelBuscador.Region = new Region(path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private const string PLACEHOLDER_BUSCADOR_HERR = "Buscar...";

        private void ConfigurarBuscadorHerramientas()
        {
            picLupaHerramientas.Width = 24;
            picLupaHerramientas.Height = 24;
            picLupaHerramientas.Location = new Point(15, (panelBuscadorHerramientas.Height - picLupaHerramientas.Height) / 2);
            picLupaHerramientas.SizeMode = PictureBoxSizeMode.Zoom;

            txbBuscadorHerramientas.Text = PLACEHOLDER_BUSCADOR_HERR;
            txbBuscadorHerramientas.ForeColor = Color.Gray;
            txbBuscadorHerramientas.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txbBuscadorHerramientas.BackColor = Color.FromArgb(245, 245, 245);
            txbBuscadorHerramientas.Height = 35;
            txbBuscadorHerramientas.Padding = new Padding(5);
            txbBuscadorHerramientas.Location = new Point(picLupaHerramientas.Right + 10, (panelBuscadorHerramientas.Height - txbBuscadorHerramientas.Height) / 2);
            txbBuscadorHerramientas.Width = panelBuscadorHerramientas.Width - 70;
            txbBuscadorHerramientas.TabStop = false;
            txbBuscadorHerramientas.HideSelection = true;
            txbBuscadorHerramientas.BorderStyle = BorderStyle.None;

            txbBuscadorHerramientas.Enter += txbBuscadorHerramientas_Enter;
            txbBuscadorHerramientas.Leave += txbBuscadorHerramientas_Leave;
            txbBuscadorHerramientas.TextChanged += txbBuscadorHerramientas_TextChanged;
        }

        private void txbBuscadorHerramientas_Enter(object? sender, EventArgs e)
        {
            if (txbBuscadorHerramientas.Text == PLACEHOLDER_BUSCADOR_HERR)
            {
                txbBuscadorHerramientas.Text = "";
                txbBuscadorHerramientas.ForeColor = Color.Black;
            }
        }

        private void txbBuscadorHerramientas_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbBuscadorHerramientas.Text))
            {
                txbBuscadorHerramientas.Text = PLACEHOLDER_BUSCADOR_HERR;
                txbBuscadorHerramientas.ForeColor = Color.Gray;
            }
        }

        private void txbBuscadorHerramientas_TextChanged(object? sender, EventArgs e)
        {
            if (TablaHerramientas.DataSource == null)
                return;

            string textoBusqueda = txbBuscadorHerramientas.Text.Trim();

            if (textoBusqueda == PLACEHOLDER_BUSCADOR_HERR)
                textoBusqueda = "";

            var vista = (TablaHerramientas?.DataSource as DataTable)?.DefaultView;

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                vista?.RowFilter = string.Empty;
            }
            else
            {
                vista?.RowFilter =
                    $"Nombre LIKE '%{textoBusqueda}%' OR " +
                    $"Descripcion LIKE '%{textoBusqueda}%'";
            }
        }

        private void RedondearPanelBuscadorHerramientas()
        {
            int radio = 15;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(panelBuscadorHerramientas.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(panelBuscadorHerramientas.Width - radio, panelBuscadorHerramientas.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, panelBuscadorHerramientas.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();

            panelBuscadorHerramientas.Region = new Region(path);
        }

        private void PanelBuscadorHerramientas_Paint(object? sender, PaintEventArgs e)
        {
            int radio = 18;
            int grosor = 2;

            Rectangle rect = panelBuscadorHerramientas.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(Color.FromArgb(180, 180, 180), grosor))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                path.AddArc(rect.X, rect.Y, radio, radio, 180, 90);
                path.AddArc(rect.Right - radio, rect.Y, radio, radio, 270, 90);
                path.AddArc(rect.Right - radio, rect.Bottom - radio, radio, radio, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radio, radio, radio, 90, 90);
                path.CloseFigure();

                panelBuscadorHerramientas.Region = new Region(path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void TablaHerramientas_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var columna = TablaHerramientas.Columns[e.ColumnIndex];

            if (columna.Name == "btnEditarHerr" || columna.Name == "btnEliminarHerr")
            {
                var graphics = e.Graphics;
                if (graphics == null)
                    return;

                // ✅ Pintar fondo primero
                e.PaintBackground(e.CellBounds, true);

                Color colorBoton = columna.Name == "btnEditarHerr" ? Color.FromArgb(66, 165, 245) : Color.FromArgb(239, 83, 80);
                string texto = e.FormattedValue?.ToString() ?? "";

                // ✅ Margen fijo para mejor control
                int margen = 6;
                Rectangle rect = new Rectangle(
                    e.CellBounds.X + margen,
                    e.CellBounds.Y + 6,
                    e.CellBounds.Width - (margen * 2),
                    e.CellBounds.Height - 12
                );

                if (rect.Width > 0 && rect.Height > 0)
                {
                    using (SolidBrush brush = new SolidBrush(colorBoton))
                    {
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.FillRectangle(brush, rect);
                    }

                    TextRenderer.DrawText(
                        graphics,
                        texto,
                        new Font("Segoe UI", 9, FontStyle.Bold),
                        rect,
                        Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }
                e.Handled = true;
            }
        }

        private void TablaHerramientas_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (TablaHerramientas.Columns[e.ColumnIndex].Name == "btnEditarHerr")
            {
                int id = Convert.ToInt32(TablaHerramientas.Rows[e.RowIndex].Cells["ID"].Value);
                MessageBox.Show($"Editar herramienta ID: {id}", "Editar");
            }

            if (TablaHerramientas.Columns[e.ColumnIndex].Name == "btnEliminarHerr")
            {
                int id = Convert.ToInt32(TablaHerramientas.Rows[e.RowIndex].Cells["ID"].Value);

                var confirmacion = MessageBox.Show(
                    "¿Seguro que deseas eliminar esta herramienta?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmacion == DialogResult.Yes)
                {
                    EliminarHerramienta(id);
                }
            }
        }

        private async void EliminarHerramienta(int id)
        {
            try
            {
                await _appService.EliminarHerramienta.ExecuteAsync(id);
                MessageBox.Show("Herramienta eliminada correctamente", "Éxito");
                await RecargarHerramientas();
            }
            catch (HerramientaNoEncontradaException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarBotonNuevaHerramienta()
        {
            btnNuevaHerramienta = new MaterialButton();

            btnNuevaHerramienta.Text = "+   Nueva herramienta";
            btnNuevaHerramienta.Type = MaterialButton.MaterialButtonType.Contained;
            btnNuevaHerramienta.UseAccentColor = true;
            btnNuevaHerramienta.AutoSize = false;
            btnNuevaHerramienta.Size = new Size(220, 45);
            btnNuevaHerramienta.HighEmphasis = true;
            btnNuevaHerramienta.Density = MaterialButton.MaterialButtonDensity.Default;
            btnNuevaHerramienta.Click += btnNuevaHerramienta_Click;

            tabPage3.Controls.Add(btnNuevaHerramienta);
        }

        private void btnNuevaHerramienta_Click(object? sender, EventArgs e)
        {
            FormNuevaHerramienta herramienta = new FormNuevaHerramienta(_appService);
            herramienta.ShowDialog();
            _ = RecargarHerramientas();
        }

        private async Task RecargarHerramientas()
        {
            await CargarDatosPruebaHerramientas();
        }

        private async void TablaPrestamos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var col = TablaPrestamos.Columns[e.ColumnIndex]?.Name;
            if (col == null)
                return;

            int id = Convert.ToInt32(TablaPrestamos.Rows[e.RowIndex].Cells["ID"].Value);

            if (col == "btnEditar")
            {
                MessageBox.Show($"Editar préstamo ID: {id}", "Editar");
            }
            else if (col == "btnVer")
            {
                // Abrir modal de detalle
                PrestamoDetalleForm detalleForm = new PrestamoDetalleForm(_appService, id);
                detalleForm.ShowDialog();
                
                // Recargar datos por si hubo cambios
                await RecargarPrestamos();
                await RecargarHerramientas();
            }
            else if (col == "btnEliminar")
            {
                var confirmacion = MessageBox.Show(
                    "¿Seguro que deseas eliminar este préstamo?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        await _appService.EliminarPrestamo.ExecuteAsync(id);
                        MessageBox.Show("Préstamo eliminado correctamente", "Éxito");
                        await RecargarPrestamos();
                        await RecargarHerramientas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ConfigurarBotonNuevoPrestamo()
        {
            btnNuevoPrestamo = new MaterialButton();

            btnNuevoPrestamo.Text = "+   Nuevo préstamo";
            btnNuevoPrestamo.Type = MaterialButton.MaterialButtonType.Contained;
            btnNuevoPrestamo.UseAccentColor = true;
            btnNuevoPrestamo.AutoSize = false;
            btnNuevoPrestamo.Size = new Size(210, 45);
            btnNuevoPrestamo.HighEmphasis = true;
            btnNuevoPrestamo.Density = MaterialButton.MaterialButtonDensity.Default;
            btnNuevoPrestamo.Click += btnNuevoPrestamo_Click;

            tabPage2.Controls.Add(btnNuevoPrestamo);
        }

        private void btnNuevoPrestamo_Click(object? sender, EventArgs e)
        {
            FormNuevoPrestamo prestamo = new FormNuevoPrestamo(_appService);
            prestamo.ShowDialog();
            _ = Task.WhenAll(RecargarPrestamos(), RecargarHerramientas());
        }

        private async Task RecargarPrestamos()
        {
            await CargarDatosPrueba();
        }
    }
}