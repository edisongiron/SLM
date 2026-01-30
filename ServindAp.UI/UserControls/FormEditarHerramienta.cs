
using MaterialSkin;
using MaterialSkin.Controls;
using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces;
using ServindAp.Application.UseCases;
using ServindAp.Domain.Exceptions;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServindAp.UI.UserControls
{
    public partial class FormEditarHerramienta : MaterialForm
    {
        private readonly IApplicationService _appService;
        private HerramientaDTO _herramientaActual;
        private int _herramientaId;

        // Controles del Form
        private Panel panelContenido;
        private MaterialLabel lblTitulo;
        private MaterialLabel lblIdValue;
        private MaterialLabel lblNombre;
        private MaterialTextBox2 txtNombre;
        private MaterialLabel lblDescripcion;
        private TextBox txtDescripcion;
        private MaterialLabel lblStock;
        private MaterialTextBox2 txtStock;
        private Panel panelBotones;
        private MaterialButton btnGuardar;
        private MaterialButton btnCancelar;

        public FormEditarHerramienta(IApplicationService appService, int herramientaId)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
            _herramientaId = herramientaId;

            InitializeComponent();
            ConfigurarMaterialSkin();
            InicializarControles();

            _ = CargarHerramienta();
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
            // Configuración del Form
            this.Text = "Editar Herramienta";
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new Size(700, 600);

            // Panel principal
            panelContenido = new Panel
            {
                Location = new Point(0, 64),
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 134),
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(panelContenido);

            int margenIzq = 40;
            int margenTop = 20;
            int espacioVertical = 15;
            int yActual = margenTop;

            // Título
            lblTitulo = new MaterialLabel
            {
                Text = "Editar Información de Herramienta",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.H5,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblTitulo);
            yActual += 50;

            // ID (solo visualización)
            lblIdValue = new MaterialLabel
            {
                Text = "ID: -",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Body1
            };
            panelContenido.Controls.Add(lblIdValue);
            yActual += 40;

            // Nombre
            lblNombre = new MaterialLabel
            {
                Text = "Nombre:",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblNombre);
            yActual += 30;

            txtNombre = new MaterialTextBox2
            {
                Location = new Point(margenIzq, yActual),
                Width = 500
            };
            panelContenido.Controls.Add(txtNombre);
            yActual += 60 + espacioVertical;

            // Descripción
            lblDescripcion = new MaterialLabel
            {
                Text = "Descripción:",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblDescripcion);
            yActual += 30;

            txtDescripcion = new TextBox
            {
                Location = new Point(margenIzq, yActual),
                Width = 580,
                Height = 100,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            panelContenido.Controls.Add(txtDescripcion);
            yActual += 110 + espacioVertical;

            // Stock
            lblStock = new MaterialLabel
            {
                Text = "Stock:",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblStock);
            yActual += 30;

            txtStock = new MaterialTextBox2
            {
                Location = new Point(margenIzq, yActual),
                Width = 150
            };
            panelContenido.Controls.Add(txtStock);

            // Panel de Botones (FIJO en la parte inferior)
            panelBotones = new Panel
            {
                Location = new Point(0, this.ClientSize.Height - 70),
                Width = this.ClientSize.Width,
                Height = 70,
                BackColor = Color.FromArgb(250, 250, 250),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            btnGuardar = new MaterialButton
            {
                Text = "Guardar",
                Type = MaterialButton.MaterialButtonType.Contained,
                UseAccentColor = false,
                Size = new Size(120, 40),
                Location = new Point(40, 15),
                BackColor = Color.FromArgb(76, 175, 80)
            };
            btnGuardar.Click += BtnGuardar_Click;
            panelBotones.Controls.Add(btnGuardar);

            btnCancelar = new MaterialButton
            {
                Text = "Cancelar",
                Type = MaterialButton.MaterialButtonType.Outlined,
                Size = new Size(120, 40),
                Location = new Point(170, 15)
            };
            btnCancelar.Click += (s, e) => this.Close();
            panelBotones.Controls.Add(btnCancelar);

            this.Controls.Add(panelBotones);

            // Ajustar el tamaño del panel de contenido
            panelContenido.Height = panelBotones.Top - panelContenido.Top;
        }

        private async Task CargarHerramienta()
        {
            try
            {
                var herramienta = await _appService.ObtenerHerramienta.ExecuteAsync(_herramientaId);
                _herramientaActual = herramienta;
                MostrarDatos();
            }
            catch (HerramientaNoEncontradaException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la herramienta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void MostrarDatos()
        {
            if (_herramientaActual == null) return;

            lblIdValue.Text = $"ID: {_herramientaActual.Id}";
            txtNombre.Text = _herramientaActual.Nombre;
            txtDescripcion.Text = _herramientaActual.Descripcion ?? "";
            txtStock.Text = _herramientaActual.Stock.ToString();
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
                {
                    MessageBox.Show("El stock debe ser un número válido mayor o igual a 0", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStock.Focus();
                    return;
                }

                // Crear request
                var request = new ActualizarHerramientaRequest
                {
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim(),
                    Stock = stock
                };

                // Ejecutar Use Case
                await _appService.ActualizarHerramienta.ExecuteAsync(_herramientaId, request);

                MessageBox.Show("Herramienta actualizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (HerramientaNoEncontradaException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DatoRequeridoException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormEditarHerramienta_Load(object sender, EventArgs e)
        {
        }

    }
}