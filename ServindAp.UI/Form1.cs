using MaterialSkin;
using MaterialSkin.Controls;
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System;

namespace ServindAp.UI
{
    public partial class Form1 : MaterialForm
    {

        private DataTable tablaDatos;
        private MaterialButton btnNuevoPrestamo;

        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            ConfigurarMaterialSkin();
            ConfigurarBuscador();
            CentrarControles();


            this.Resize += (s, e) => CentrarControles();
            tabPage2.Resize += (s, e) => LayoutTabPrestamos();


            //Tabla de Prestamos
            this.Load += (s, e) => Form1_Load(s, e);
            TablaPrestamos.CellContentClick += (s, e) => TablaPrestamos_CellContentClick(s, e);
            TablaPrestamos.CellPainting += (s, e) => TablaPrestamos_CellPainting(s, e);//Botones Editar/Eliminar
            panelBuscador.Paint += (s, e) => PanelBuscador_Paint(s, e);

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigurarEstiloTabla();
            CrearColumnas();
            CargarDatosPrueba();
            LayoutTabPrestamos();
            ConfigurarBotonNuevoPrestamo();
        }


        private void Home_Resize(object sender, EventArgs e)
        {
            CentrarControles();
        }


        private void CentrarControles()
        {
            int logoX = (this.ClientSize.Width - Logo.Width) / 2;
            int logoY = (this.ClientSize.Height - Logo.Height) / 2 - 80;
            Logo.Location = new Point(logoX, logoY);

            int labelX = (this.ClientSize.Width - labelSubtitulo.Width) / 2;
            int labelY = Logo.Bottom + 20;
            labelSubtitulo.Location = new Point(labelX, labelY);
        }


        private void LayoutTabPrestamos()
        {
            panelBuscador.Width = 420;
            panelBuscador.Height = 45;

            int buscadorX = 200;
            int espacio = 15;

            panelBuscador.Width = 400;

            // BUSCADOR (panel contenedor)
            panelBuscador.Location = new Point(TablaPrestamos.Location.X, 90);

            // TABLA
            int anchoTabla = (int)(tabPage2.ClientSize.Width * 0.8);
            TablaPrestamos.Width = anchoTabla;

            TablaPrestamos.Location = new Point(
            (tabPage2.ClientSize.Width - TablaPrestamos.Width) / 2 ,
            panelBuscador.Bottom + espacio
            );

            // ALTURA DE LA TABLA (ocupa el resto)
            TablaPrestamos.Height =
            tabPage2.ClientSize.Height - TablaPrestamos.Top - 25;
            RedondearPanelBuscador();

            if (btnNuevoPrestamo != null)
            {
                btnNuevoPrestamo.Location = new Point(
                    TablaPrestamos.Right - btnNuevoPrestamo.Width,
                    panelBuscador.Top
                );
            }

        }


        private void ConfigurarMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            // Crea/obtiene la instancia única del gestor de MaterialSkin

            materialSkinManager.AddFormToManage(this);
            // Registra este formulario (Form1) para que MaterialSkin lo controle

            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Green600,       // Barra superior - Verde medio (67, 160, 71)
                Primary.Teal700,        // Hover/Selección - Verde esmeralda oscuro (0, 121, 107)
                Primary.LightGreen500,  // Sombras/Detalles - Verde limón (139, 195, 74)
                Accent.Green400,        // Verde
                TextShade.WHITE
            );

            DrawerShowIconsWhenHidden = true;
            DrawerHighlightWithAccent = true;
            DrawerBackgroundWithAccent = false;
        }


        private void ConfigurarEstiloTabla()
        {
            // Fondo blanco limpio
            TablaPrestamos.BackgroundColor = Color.White;
            TablaPrestamos.BorderStyle = BorderStyle.None;
            TablaPrestamos.CellBorderStyle = DataGridViewCellBorderStyle.None; // Sin bordes entre celdas
            TablaPrestamos.GridColor = Color.FromArgb(240, 240, 240); // Líneas muy sutiles

            // Colores de las celdas (verde selección)
            TablaPrestamos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201); // Verde pastel
            TablaPrestamos.DefaultCellStyle.SelectionForeColor = Color.FromArgb(60, 60, 60); // Texto gris oscuro
            TablaPrestamos.DefaultCellStyle.BackColor = Color.White;
            TablaPrestamos.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60); // Gris oscuro para texto
            TablaPrestamos.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            TablaPrestamos.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8); // Espaciado interno
            TablaPrestamos.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            TablaPrestamos.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(76, 175, 80);

            // Filas alternadas muy sutiles
            TablaPrestamos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // ENCABEZADO GRIS CLARO
            TablaPrestamos.EnableHeadersVisualStyles = false;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Gris muy claro
            TablaPrestamos.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 80); // Gris oscuro texto
            TablaPrestamos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            TablaPrestamos.ColumnHeadersHeight = 45;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            TablaPrestamos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            TablaPrestamos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 245, 245);

            // Configuración general - FILAS MÁS ALTAS
            TablaPrestamos.RowTemplate.Height = 50; // Aumentado a 50
            TablaPrestamos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TablaPrestamos.MultiSelect = false;
            TablaPrestamos.AllowUserToAddRows = false;
            TablaPrestamos.ReadOnly = true;
            TablaPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            TablaPrestamos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            TablaPrestamos.EditMode = DataGridViewEditMode.EditProgrammatically;
            TablaPrestamos.RowHeadersVisible = false; // Ocultar columna de encabezados de fila
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

            // Botón Editar
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "btnEditar";
            btnEditar.HeaderText = "";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            TablaPrestamos.Columns.Add(btnEditar);

            // Botón Eliminar
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            TablaPrestamos.Columns.Add(btnEliminar);


            TablaPrestamos.Columns["Responsable"].Width = 200;
            TablaPrestamos.Columns["Herramienta"].Width = 220;
            TablaPrestamos.Columns["Cantidad"].Width = 110;
            TablaPrestamos.Columns["FechaEntrega"].Width = 150;
            TablaPrestamos.Columns["Observaciones"].Width = 300;
        }


        private void TablaPrestamos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Color de botones

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var columna = TablaPrestamos.Columns[e.ColumnIndex];

            if (columna.Name == "btnEditar" || columna.Name == "btnEliminar")
            {
                e.PaintBackground(e.CellBounds, true);

                Color colorBoton;
                string texto = e.FormattedValue?.ToString(); 

                if (columna.Name == "btnEditar")
                {
                    colorBoton = Color.FromArgb(66, 165, 245); // Azul
                }
                else
                {
                    colorBoton = Color.FromArgb(239, 83, 80); // Rojo 
                }

                Rectangle rect = new Rectangle(
                    e.CellBounds.X + 6,
                    e.CellBounds.Y + 6,
                    e.CellBounds.Width - 12,
                    e.CellBounds.Height - 12
                );

                using (SolidBrush brush = new SolidBrush(colorBoton))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillRectangle(brush, rect);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    texto,
                    new Font("Segoe UI", 9, FontStyle.Bold),
                    rect,
                    Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );

                e.Handled = true;
            }


        }


        private void CargarDatosPrueba()
        {
            tablaDatos.Rows.Add(1, "Juan Pérez", "Taladro Makita", 1, "10/01/2026", "Activo", "Incluye maletín y 5 brocas");
            tablaDatos.Rows.Add(2, "María García", "Martillo", 2, "09/01/2026", "Activo", "Mango de fibra de vidrio");
            tablaDatos.Rows.Add(3, "Carlos López", "Destornillador Set", 1, "08/01/2026", "Devuelto", "Set completo 12 piezas");
            tablaDatos.Rows.Add(4, "Ana Rodríguez", "Sierra Circular", 1, "11/01/2026", "Activo", "Incluye disco de corte");
            tablaDatos.Rows.Add(5, "Pedro Martínez", "Llave Inglesa", 3, "07/01/2026", "Activo", "Tamaños: 8\", 10\", 12\"");
            tablaDatos.Rows.Add(6, "Laura Sánchez", "Taladro DeWalt", 1, "12/01/2026", "Activo", "Batería recargable");
            tablaDatos.Rows.Add(7, "Edison", "Martillo", 1, "12/01/2026", "Activo", "Peso 500g");
        }


        private void BuscadorTxb_TextChanged(object sender, EventArgs e)
        {
            if (TablaPrestamos.DataSource == null)
                return;


            string textoBusqueda = BuscadorTxb.Text.Trim();

            if (textoBusqueda == "Buscar...")
            {
                textoBusqueda = "";
            }


            var vista = (TablaPrestamos.DataSource as DataTable).DefaultView;

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                vista.RowFilter = string.Empty;
            }
            else
            {
                vista.RowFilter =
                    $"Responsable LIKE '%{textoBusqueda}%' OR " +
                    $"Herramienta LIKE '%{textoBusqueda}%' OR " +
                    $"Estado LIKE '%{textoBusqueda}%'";
            }

        }


        //Buscador
        private const string PLACEHOLDER_BUSCADOR = "Buscar...";
        private void ConfigurarBuscador()
        {
            // Estilo del TextBox
            BuscadorTxb.Text = PLACEHOLDER_BUSCADOR;
            BuscadorTxb.ForeColor = Color.Gray;
            BuscadorTxb.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            BuscadorTxb.BackColor = Color.FromArgb(245, 245, 245);
            BuscadorTxb.Height = 35;
            BuscadorTxb.Padding = new Padding(5);
            BuscadorTxb.Location = new Point(picLupa.Right + 10, (panelBuscador.Height - BuscadorTxb.Height) / 2);
            BuscadorTxb.Width = panelBuscador.Width - 70;
            picLupa.Location = new Point(15, (panelBuscador.Height - picLupa.Height) / 2);
            BuscadorTxb.TabStop = false;
            BuscadorTxb.HideSelection = true;
            BuscadorTxb.BorderStyle = BorderStyle.None;


            // Eventos obligatorios
            BuscadorTxb.Enter += BuscadorTxb_Enter;
            BuscadorTxb.Leave += BuscadorTxb_Leave;

        }

        private void BuscadorTxb_Enter(object sender, EventArgs e)
        {
            if (BuscadorTxb.Text == PLACEHOLDER_BUSCADOR)
            {
                BuscadorTxb.Text = "";
                BuscadorTxb.ForeColor = Color.Black;
            }
        }

        private void BuscadorTxb_Leave(object sender, EventArgs e)
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

        private void PanelBuscador_Paint(object sender, PaintEventArgs e)
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


        //Acciones de botones Editar/Eliminar
        private void TablaPrestamos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (TablaPrestamos.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                int id = Convert.ToInt32(TablaPrestamos.Rows[e.RowIndex].Cells["ID"].Value);

                MessageBox.Show(
                    $"Editar préstamo ID: {id}",
                    "Editar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            if (TablaPrestamos.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                var confirmacion = MessageBox.Show(
                    "¿Seguro que deseas eliminar este préstamo?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmacion == DialogResult.Yes)
                {
                    TablaPrestamos.Rows.RemoveAt(e.RowIndex);
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


        private void btnNuevoPrestamo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Abrir formulario Nuevo Préstamo");
        }


    }
}
