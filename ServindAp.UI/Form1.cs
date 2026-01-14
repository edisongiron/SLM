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

        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            ConfigurarMaterialSkin();
            CentrarControles();
            this.Resize += (s, e) => CentrarControles();

            //Tabla de Prestamos
            this.Load += Form1_Load;
            TablaPrestamos.CellContentClick += TablaPrestamos_CellContentClick;
            TablaPrestamos.CellPainting += TablaPrestamos_CellPainting;//Botones Editar/Eliminar

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigurarEstiloTabla();
            CrearColumnas();
            CargarDatosPrueba();
            
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
                Accent.Green700,        // Verde
                TextShade.BLACK
            );

            DrawerShowIconsWhenHidden = true;
            DrawerHighlightWithAccent = true; 
            DrawerBackgroundWithAccent = false; 
        }


        private void ConfigurarEstiloTabla()
        {
            // Fondo y bordes
            TablaPrestamos.BackgroundColor = Color.White;
            TablaPrestamos.BorderStyle = BorderStyle.None;
            TablaPrestamos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Colores de las celdas
            TablaPrestamos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(76, 175, 80);
            TablaPrestamos.DefaultCellStyle.SelectionForeColor = Color.White;
            TablaPrestamos.DefaultCellStyle.BackColor = Color.White;
            TablaPrestamos.DefaultCellStyle.ForeColor = Color.Black;
            TablaPrestamos.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            TablaPrestamos.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            TablaPrestamos.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(76, 175, 80);

            // Filas alternadas
            TablaPrestamos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Encabezados verdes
            TablaPrestamos.EnableHeadersVisualStyles = false;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(56, 142, 60);
            TablaPrestamos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            TablaPrestamos.ColumnHeadersHeight = 45;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            TablaPrestamos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            TablaPrestamos.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(56, 142, 60);

            // Configuración general
            TablaPrestamos.RowTemplate.Height = 35;
            TablaPrestamos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TablaPrestamos.MultiSelect = false;
            TablaPrestamos.AllowUserToAddRows = false;
            TablaPrestamos.ReadOnly = true;
            TablaPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            TablaPrestamos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            TablaPrestamos.EditMode = DataGridViewEditMode.EditProgrammatically;
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

            TablaPrestamos.AutoGenerateColumns = true;
            TablaPrestamos.DataSource = tablaDatos;

            // Botón Editar
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "btnEditar";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            TablaPrestamos.Columns.Add(btnEditar);

            // Botón Eliminar
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            TablaPrestamos.Columns.Add(btnEliminar);

      
            TablaPrestamos.Columns["Responsable"].Width = 200;
            TablaPrestamos.Columns["Herramienta"].Width = 220;
            TablaPrestamos.Columns["FechaEntrega"].Width = 140;

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
                    colorBoton = Color.FromArgb(33, 150, 243); // Azul Material
                }
                else
                {
                    colorBoton = Color.FromArgb(211, 47, 47); // Rojo Material
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
            tablaDatos.Rows.Add(1, "Juan Pérez", "Taladro Makita", 1, "10/01/2026", "Activo");
            tablaDatos.Rows.Add(2, "María García", "Martillo", 2, "09/01/2026", "Activo");
            tablaDatos.Rows.Add(3, "Carlos López", "Destornillador Set", 1, "08/01/2026", "Devuelto");
            tablaDatos.Rows.Add(4, "Ana Rodríguez", "Sierra Circular", 1, "11/01/2026", "Activo");
            tablaDatos.Rows.Add(5, "Pedro Martínez", "Llave Inglesa", 3, "07/01/2026", "Activo");
            tablaDatos.Rows.Add(6, "Laura Sánchez", "Taladro DeWalt", 1, "12/01/2026", "Activo");
            tablaDatos.Rows.Add(7, "Edison ", "Martillo", 1, "12/01/2026", "Activo");
        }


        private void BuscadorTxb_TextChanged(object sender, EventArgs e)
        {
            if (TablaPrestamos.DataSource == null)
                return;

            string textoBusqueda = BuscadorTxb.Text.Trim();

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





    }
}
