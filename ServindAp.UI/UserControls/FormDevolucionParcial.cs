using MaterialSkin;
using MaterialSkin.Controls;
using ServindAp.Application.DTOs;
using ServindAp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ServindAp.UI.UserControls
{
    public partial class FormDevolucionParcial : MaterialForm
    {
        private DataGridView dgvHerramientas;
        private MaterialCheckbox chkTieneDefectos;
        private MaterialTextBox2 txtObservaciones;
        private MaterialButton btnAceptar;
        private MaterialButton btnCancelar;
        private MaterialLabel lblTitulo;
        private MaterialLabel lblInstrucciones;

        private List<PrestamoHerramientaDTO> _herramientasPendientes;

        public bool TieneDefectos => chkTieneDefectos.Checked;
        public string Observaciones => txtObservaciones.Text ?? string.Empty;

        public FormDevolucionParcial(List<PrestamoHerramientaDTO> herramientasPendientes)
        {
            _herramientasPendientes = herramientasPendientes ?? throw new ArgumentNullException(nameof(herramientasPendientes));

            InitializeComponent();
            ConfigurarMaterialSkin();
            InicializarControles();
            CargarHerramientas();
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
        }

        private void InicializarControles()
        {
            this.Text = "Registrar Devolución Parcial";
            this.Size = new Size(700, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            int margen = 30;
            int yActual = 80;

            // Título
            lblTitulo = new MaterialLabel
            {
                Text = "Seleccione las herramientas a devolver",
                Location = new Point(margen, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.H6,
                HighEmphasis = true
            };
            this.Controls.Add(lblTitulo);
            yActual += 40;

            // Instrucciones
            lblInstrucciones = new MaterialLabel
            {
                Text = "Ingrese la cantidad a devolver de cada herramienta (0 = no devolver)",
                Location = new Point(margen, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Body2
            };
            this.Controls.Add(lblInstrucciones);
            yActual += 35;

            // DataGridView
            dgvHerramientas = new DataGridView
            {
                Location = new Point(margen, yActual),
                Width = 640,
                Height = 220,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                ReadOnly = false,
                EditMode = DataGridViewEditMode.EditOnEnter
            };
            ConfigurarEstiloGrid();
            this.Controls.Add(dgvHerramientas);
            yActual += 230;

            // Checkbox defectos
            chkTieneDefectos = new MaterialCheckbox
            {
                Text = "Las herramientas tienen defectos o daños",
                Location = new Point(margen, yActual),
                AutoSize = true
            };
            this.Controls.Add(chkTieneDefectos);
            yActual += 40;

            // Observaciones
            var lblObs = new MaterialLabel
            {
                Text = "Observaciones (opcional):",
                Location = new Point(margen, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2
            };
            this.Controls.Add(lblObs);
            yActual += 30;

            txtObservaciones = new MaterialTextBox2
            {
                Location = new Point(margen, yActual),
                Width = 640,
                Hint = "Ingrese observaciones sobre la devolución..."
            };
            this.Controls.Add(txtObservaciones);
            yActual += 60;

            // Botones
            btnCancelar = new MaterialButton
            {
                Text = "Cancelar",
                Type = MaterialButton.MaterialButtonType.Text,
                Size = new Size(120, 40),
                Location = new Point(this.Width - 280, yActual)
            };
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancelar);

            btnAceptar = new MaterialButton
            {
                Text = "Aceptar",
                Type = MaterialButton.MaterialButtonType.Contained,
                UseAccentColor = false,
                Size = new Size(120, 40),
                Location = new Point(this.Width - 150, yActual)
            };
            btnAceptar.Click += BtnAceptar_Click;
            this.Controls.Add(btnAceptar);

            btnAceptar.BackColor = Color.FromArgb(76, 175, 80); 
            btnAceptar.ForeColor = Color.White;
        }

        private void ConfigurarEstiloGrid()
        {
            dgvHerramientas.EnableHeadersVisualStyles = false;
            dgvHerramientas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 175, 80);
            dgvHerramientas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHerramientas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHerramientas.ColumnHeadersHeight = 40;
            dgvHerramientas.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvHerramientas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            dgvHerramientas.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvHerramientas.RowTemplate.Height = 35;
        }

        private void CargarHerramientas()
        {
            var table = new DataTable();
            table.Columns.Add("HerramientaId", typeof(int));
            table.Columns.Add("Herramienta", typeof(string));
            table.Columns.Add("Cantidad Pendiente", typeof(int));
            table.Columns.Add("Cantidad a Devolver", typeof(int));

            foreach (var herramienta in _herramientasPendientes)
            {
                table.Rows.Add(
                    herramienta.HerramientaId,
                    herramienta.Herramienta?.Nombre ?? "N/A",
                    herramienta.Cantidad,
                    0 // Por defecto, no devolver nada
                );
            }

            dgvHerramientas.DataSource = table;

            // Ocultar columna ID
            dgvHerramientas.Columns["HerramientaId"]?.Visible = false;

            dgvHerramientas.Columns["Herramienta"]?.FillWeight = 35;
            dgvHerramientas.Columns["Cantidad Pendiente"]?.FillWeight = 40;
            dgvHerramientas.Columns["Cantidad a Devolver"]?.FillWeight = 35;

            // Validación en tiempo real
            dgvHerramientas.CellValidating += DgvHerramientas_CellValidating;
            dgvHerramientas.CellEndEdit += DgvHerramientas_CellEndEdit;
        }

        private void DgvHerramientas_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvHerramientas.Columns[e.ColumnIndex].Name == "Cantidad a Devolver")
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int cantidadDevolver))
                {
                    e.Cancel = true;
                    MessageBox.Show("Debe ingresar un número válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cantidadDevolver < 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("La cantidad no puede ser negativa", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int cantidadPendiente = Convert.ToInt32(dgvHerramientas.Rows[e.RowIndex].Cells["Cantidad Pendiente"].Value);
                if (cantidadDevolver > cantidadPendiente)
                {
                    e.Cancel = true;
                    MessageBox.Show($"No puede devolver más de lo pendiente ({cantidadPendiente})", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DgvHerramientas_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            // Resaltar filas con cantidades a devolver
            foreach (DataGridViewRow row in dgvHerramientas.Rows)
            {
                int cantidadADevolver = Convert.ToInt32(row.Cells["Cantidad a Devolver"].Value);
                if (cantidadADevolver > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233); // Verde claro
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void BtnAceptar_Click(object? sender, EventArgs e)
        {
            // Validar que al menos una herramienta tenga cantidad > 0
            var herramientasSeleccionadas = ObtenerHerramientasSeleccionadas();

            if (!herramientasSeleccionadas.Any())
            {
                MessageBox.Show("Debe seleccionar al menos una herramienta para devolver", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public List<HerramientaDevolucionItem> ObtenerHerramientasSeleccionadas()
        {
            var lista = new List<HerramientaDevolucionItem>();

            foreach (DataGridViewRow row in dgvHerramientas.Rows)
            {
                int cantidadADevolver = Convert.ToInt32(row.Cells["Cantidad a Devolver"].Value);

                if (cantidadADevolver > 0)
                {
                    lista.Add(new HerramientaDevolucionItem
                    {
                        HerramientaId = Convert.ToInt32(row.Cells["HerramientaId"].Value),
                        CantidadADevolver = cantidadADevolver
                    });
                }
            }

            return lista;
        }

        private void FormDevolucionParcial_Load(object sender, EventArgs e)
        {

        }
    }
}