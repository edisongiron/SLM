using MaterialSkin;
using MaterialSkin.Controls;
using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces;
using ServindAp.Domain.Exceptions;
using System.Data;

namespace ServindAp.UI.UserControls
{
    public partial class PrestamoDetalleForm : MaterialForm
    {
        private readonly IApplicationService _appService;
        private PrestamoDTO? _prestamoActual;
        private bool _modoEdicion = false;

        // Controles del Form
        private Panel panelContenido; // Panel principal con scroll
        private MaterialLabel lblTitulo;
        private MaterialLabel lblIdValue;
        private MaterialLabel lblResponsable;
        private MaterialTextBox2 txtResponsable;
        private MaterialLabel lblFechaEntrega;
        private DateTimePicker dtpFechaEntrega;
        private MaterialLabel lblFechaDevolucion;
        private MaterialLabel lblFechaDevolucionValue;
        private MaterialLabel lblObservaciones;
        private TextBox txtObservaciones;
        private MaterialLabel lblEstado;
        private MaterialLabel lblEstadoValue;
        private MaterialLabel lblHerramientas;
        private DataGridView dgvHerramientas;
        private MaterialButton btnEditar;
        private MaterialButton btnGuardar;
        private MaterialButton btnCancelar;
        private MaterialButton btnCerrar;
        private MaterialButton btnRegistrarDevolucion;
        private Panel panelBotones;

        public PrestamoDetalleForm(IApplicationService appService, int prestamoId)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));

            InitializeComponent();
            ConfigurarMaterialSkin();
            InicializarControles();

            _ = CargarPrestamo(prestamoId);
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
            this.Text = "Detalle del Préstamo";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new Size(700, 500); // Tamaño mínimo

            // Panel principal con scroll
            panelContenido = new Panel
            {
                Location = new Point(0, 64), // Debajo de la barra de título de MaterialForm
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 64),
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(panelContenido);

            int margenIzq = 40;
            int margenTop = 20; // Margen reducido ya que está dentro del panel
            int espacioVertical = 15;
            int altoControl = 50;
            int yActual = margenTop;

            // Título
            lblTitulo = new MaterialLabel
            {
                Text = "Información del Préstamo",
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

            // Responsable
            lblResponsable = new MaterialLabel
            {
                Text = "Responsable:",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblResponsable);
            yActual += 30;

            txtResponsable = new MaterialTextBox2
            {
                Location = new Point(margenIzq, yActual),
                Width = 400,
                ReadOnly = true,
                Enabled = false
            };
            panelContenido.Controls.Add(txtResponsable);
            yActual += altoControl + espacioVertical;

            // Fecha de Entrega
            lblFechaEntrega = new MaterialLabel
            {
                Text = "Fecha de Entrega:",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblFechaEntrega);
            yActual += 30;

            dtpFechaEntrega = new DateTimePicker
            {
                Location = new Point(margenIzq, yActual),
                Width = 250,
                Format = DateTimePickerFormat.Short,
                Enabled = false
            };
            panelContenido.Controls.Add(dtpFechaEntrega);
            yActual += 30 + espacioVertical;

            // Fecha de Devolución
            lblFechaDevolucion = new MaterialLabel
            {
                Text = "Fecha de Devolución:",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblFechaDevolucion);
            yActual += 30;

            lblFechaDevolucionValue = new MaterialLabel
            {
                Text = "Pendiente",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Body1
            };
            panelContenido.Controls.Add(lblFechaDevolucionValue);
            yActual += 40;

            // Estado
            lblEstado = new MaterialLabel
            {
                Text = "Estado:",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblEstado);
            yActual += 30;

            lblEstadoValue = new MaterialLabel
            {
                Text = "-",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Body1
            };
            panelContenido.Controls.Add(lblEstadoValue);
            yActual += 40;

            // Observaciones
            lblObservaciones = new MaterialLabel
            {
                Text = "Observaciones:",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblObservaciones);
            yActual += 30;

            txtObservaciones = new TextBox
            {
                Location = new Point(margenIzq, yActual),
                Width = 800,
                Height = 80,
                Multiline = true,
                ReadOnly = true,
                Enabled = false
            };
            panelContenido.Controls.Add(txtObservaciones);
            yActual += 90 + espacioVertical;

            // Herramientas
            lblHerramientas = new MaterialLabel
            {
                Text = "Herramientas Prestadas:",
                Location = new Point(margenIzq, yActual),
                AutoSize = true,
                FontType = MaterialSkinManager.fontType.Subtitle2,
                HighEmphasis = true
            };
            panelContenido.Controls.Add(lblHerramientas);
            yActual += 40;

            dgvHerramientas = new DataGridView
            {
                Location = new Point(margenIzq, yActual),
                Width = 800,
                Height = 200, // Altura aumentada para mejor visualización
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D
            };
            ConfigurarEstiloGridHerramientas();
            panelContenido.Controls.Add(dgvHerramientas);
            yActual += 210 + espacioVertical;

            // Panel de Botones (FIJO en la parte inferior del Form, fuera del panel scrolleable)
            panelBotones = new Panel
            {
                Location = new Point(0, this.ClientSize.Height - 70),
                Width = this.ClientSize.Width,
                Height = 70,
                BackColor = Color.FromArgb(250, 250, 250),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Ajustar posición de los botones dentro del panel
            int margenBoton = 60;

            btnEditar = new MaterialButton
            {
                Text = "Editar",
                Type = MaterialButton.MaterialButtonType.Contained,
                HighEmphasis = false,
                UseAccentColor = false,
                Size = new Size(120, 40),
                Location = new Point(40, 15),
                AutoSize = false
            };

            btnEditar.BackColor = Color.FromArgb(76, 175, 80);
            btnEditar.ForeColor = Color.White;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.FlatAppearance.BorderSize = 0;

            btnEditar.Click += BtnEditar_Click;
            panelBotones.Controls.Add(btnEditar);

            btnGuardar = new MaterialButton
            {
                Text = "Guardar",
                Type = MaterialButton.MaterialButtonType.Contained,
                UseAccentColor = false,
                Size = new Size(120, 40),
                Location = new Point(margenBoton, 15),
                Visible = false
            };
            btnGuardar.Click += BtnGuardar_Click;
            panelBotones.Controls.Add(btnGuardar);

            btnCancelar = new MaterialButton
            {
                Text = "Cancelar",
                Type = MaterialButton.MaterialButtonType.Text,
                Size = new Size(120, 40),
                Location = new Point(margenBoton + 130, 15),
                Visible = false
            };
            btnCancelar.Click += BtnCancelar_Click;
            panelBotones.Controls.Add(btnCancelar);

            btnRegistrarDevolucion = new MaterialButton
            {
                Text = "Registrar Devolución",
                Type = MaterialButton.MaterialButtonType.Contained,
                UseAccentColor = false,
                Size = new Size(180, 40),
                Location = new Point(this.ClientSize.Width - margenBoton - 310, 15),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                BackColor = Color.FromArgb(255, 152, 0),
                Visible = false
            };
            btnRegistrarDevolucion.Click += BtnRegistrarDevolucion_Click;
            panelBotones.Controls.Add(btnRegistrarDevolucion);

            btnCerrar = new MaterialButton
            {
                Text = "Cerrar",
                Type = MaterialButton.MaterialButtonType.Outlined,
                Size = new Size(120, 40),
                Location = new Point(this.ClientSize.Width - margenBoton - 120, 15),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnCerrar.Click += (s, e) => this.Close();
            panelBotones.Controls.Add(btnCerrar);

            this.Controls.Add(panelBotones);

            // Ajustar el tamaño del panel de contenido para que termine antes de los botones
            panelContenido.Height = panelBotones.Top - panelContenido.Top;
        }

        private void ConfigurarEstiloGridHerramientas()
        {
            dgvHerramientas.EnableHeadersVisualStyles = false;

            // HEADER
            dgvHerramientas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 175, 80); // VERDE
            dgvHerramientas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHerramientas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHerramientas.ColumnHeadersHeight = 42;
            dgvHerramientas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // CELDAS
            dgvHerramientas.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvHerramientas.DefaultCellStyle.BackColor = Color.White;
            dgvHerramientas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            dgvHerramientas.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvHerramientas.GridColor = Color.FromArgb(224, 224, 224);
        }


        private async Task CargarPrestamo(int prestamoId)
        {
            try
            {
                _prestamoActual = await _appService.ObtenerPrestamo.ExecuteAsync(prestamoId);
                MostrarDatos();
            }
            catch (PrestamoNoEncontradoException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el préstamo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void MostrarDatos()
        {
            if (_prestamoActual == null) return;

            lblIdValue.Text = $"ID: {_prestamoActual.Id}";
            txtResponsable.Text = _prestamoActual.Responsable;
            dtpFechaEntrega.Value = _prestamoActual.FechaEntrega;

            if (_prestamoActual.FechaDevolucion.HasValue)
            {
                lblFechaDevolucionValue.Text = _prestamoActual.FechaDevolucion.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                lblFechaDevolucionValue.Text = "Pendiente";
            }

            lblEstadoValue.Text = _prestamoActual.Estado;
            txtObservaciones.Text = _prestamoActual.Observaciones ?? "Sin observaciones";

            CargarHerramientasEnGrid();

            // Ocultar botón Editar si el préstamo ya fue devuelto
            btnEditar.Visible = !_prestamoActual.FechaDevolucion.HasValue;

            // Mostrar botón de devolución solo si NO ha sido devuelto
            btnRegistrarDevolucion.Visible = !_prestamoActual.FechaDevolucion.HasValue;
        }


        private void CargarHerramientasEnGrid()
        {
            if (_prestamoActual?.Herramientas == null) return;

            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("HerramientaId", typeof(int));
            table.Columns.Add("Herramienta", typeof(string));
            table.Columns.Add("Descripción", typeof(string));
            table.Columns.Add("Cantidad Pendiente", typeof(int));
            table.Columns.Add("Stock Disponible", typeof(int));

            foreach (var ph in _prestamoActual.Herramientas)
            {
                table.Rows.Add(
                    ph.Id,
                    ph.HerramientaId,
                    ph.Herramienta?.Nombre ?? "N/A",
                    ph.Herramienta?.Descripcion ?? "Sin descripción",
                    ph.Cantidad, // Ahora Cantidad representa lo que aún está prestado
                    ph.Herramienta?.Stock ?? 0
                );
            }

            dgvHerramientas.DataSource = table;

            // Ocultar columnas ID y HerramientaId
            if (dgvHerramientas.Columns.Contains("ID"))
            {
                dgvHerramientas.Columns["ID"]?.Visible = false;
            }
            if (dgvHerramientas.Columns.Contains("HerramientaId"))
            {
                dgvHerramientas.Columns["HerramientaId"]?.Visible = false;
            }

            // Resaltar filas con cantidades pendientes
            foreach (DataGridViewRow row in dgvHerramientas.Rows)
            {
                int pendiente = Convert.ToInt32(row.Cells["Cantidad Pendiente"].Value);
                if (pendiente > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 224); // Naranja claro - Aún tiene herramientas pendientes
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233); // Verde claro - Completamente devuelto
                }
            }
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (_prestamoActual?.FechaDevolucion.HasValue == true)
            {
                MessageBox.Show("No se puede editar un préstamo ya devuelto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _modoEdicion = true;
            CambiarModoEdicion(true);
        }

        private void CambiarModoEdicion(bool editar)
        {
            // Habilitar/Deshabilitar campos editables
            txtResponsable.Enabled = editar;
            txtResponsable.ReadOnly = !editar;

            dtpFechaEntrega.Enabled = editar;

            txtObservaciones.Enabled = editar;
            txtObservaciones.ReadOnly = !editar;

            // Cambiar color de fondo para indicar que es editable
            if (editar)
            {
                txtObservaciones.BackColor = Color.White;
            }
            else
            {
                txtObservaciones.BackColor = Color.FromArgb(245, 245, 245);
            }

            // Mostrar/Ocultar botones
            btnEditar.Visible = !editar;
            btnGuardar.Visible = editar;
            btnCancelar.Visible = editar;

            // Ocultar botón de devolución en modo edición
            btnRegistrarDevolucion.Visible = !editar && !(_prestamoActual?.FechaDevolucion.HasValue ?? true);
        }

        private async void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (_prestamoActual == null) return;

            try
            {
                // Validación básica
                if (string.IsNullOrWhiteSpace(txtResponsable.Text))
                {
                    MessageBox.Show("El responsable es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear lista de herramientas para la actualización
                var herramientas = _prestamoActual.Herramientas.Select(ph =>
                    new ServindAp.Application.UseCases.HerramientaPrestamoRequest
                    {
                        HerramientaId = ph.HerramientaId,
                        Cantidad = ph.Cantidad
                    }).ToList();

                // Crear request
                var request = new ServindAp.Application.UseCases.ActualizarPrestamoRequest
                {
                    Responsable = txtResponsable.Text.Trim(),
                    FechaEntrega = dtpFechaEntrega.Value,
                    Observaciones = string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim(),
                    Herramientas = herramientas
                };

                // Ejecutar Use Case
                await _appService.ActualizarPrestamo.ExecuteAsync(_prestamoActual.Id, request);

                MessageBox.Show("Préstamo actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar datos
                await CargarPrestamo(_prestamoActual.Id);
                _modoEdicion = false;
                CambiarModoEdicion(false);
            }
            catch (PrestamoNoEncontradoException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (StockInsuficienteException ex)
            {
                MessageBox.Show(ex.Message, "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            _modoEdicion = false;
            CambiarModoEdicion(false);
            MostrarDatos(); // Restaurar datos originales
        }

        private async void BtnRegistrarDevolucion_Click(object? sender, EventArgs e)
        {
            if (_prestamoActual == null) return;

            try
            {
                // Verificar si ya fue completamente devuelto
                if (_prestamoActual.FechaDevolucion.HasValue)
                {
                    MessageBox.Show("Este préstamo ya fue completamente devuelto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Verificar si hay herramientas pendientes (Cantidad > 0)
                var herramientasPendientes = _prestamoActual.Herramientas.Where(h => h.Cantidad > 0).ToList();
                if (!herramientasPendientes.Any())
                {
                    MessageBox.Show("No hay herramientas pendientes por devolver", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Abrir diálogo de devolución parcial
                using (var dialogoDevolucion = new FormDevolucionParcial(herramientasPendientes))
                {
                    if (dialogoDevolucion.ShowDialog() == DialogResult.OK)
                    {
                        var herramientasADevolver = dialogoDevolucion.ObtenerHerramientasSeleccionadas();

                        if (!herramientasADevolver.Any())
                        {
                            MessageBox.Show("Debe seleccionar al menos una herramienta para devolver", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Crear request para devolución parcial
                        var request = new ServindAp.Application.UseCases.RegistrarDevolucionParcialRequest
                        {
                            HerramientasADevolver = herramientasADevolver,
                            TieneDefectos = dialogoDevolucion.TieneDefectos,
                            Observaciones = dialogoDevolucion.Observaciones
                        };

                        // Ejecutar Use Case de devolución parcial
                        await _appService.RegistrarDevolucionParcial.ExecuteAsync(_prestamoActual.Id, request);

                        string mensaje = request.TieneDefectos
                            ? "Devolución registrada con defectos"
                            : "Devolución registrada correctamente";

                        MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar datos para mostrar el nuevo estado
                        await CargarPrestamo(_prestamoActual.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar devolución: {ex.Message}\n\n{ex.GetType().Name}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrestamoDetalleForm_Load(object sender, EventArgs e)
        {

        }
    }
}