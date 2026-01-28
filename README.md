# Instructions.md - Lógica Backend CRUD Herramientas

## Configuración Inicial

### 1. Inyección de Dependencias en el Form

```csharp
public partial class MiForm : MaterialForm
{
    private readonly IApplicationService _appService;

    public MiForm(IApplicationService appService)
    {
        InitializeComponent();
        _appService = appService;
    }
}
```

### 2. Registrar Form en Program.cs

```csharp
services.AddTransient<MiForm>();
```

---

# CRUD DE HERRAMIENTAS - LÓGICA BACKEND

## Variable para guardar la herramienta seleccionada

```csharp
private HerramientaDTO? _herramientaActual;
```

---

## CREATE - Crear Herramienta

```csharp
private async void btnCrear_Click(object sender, EventArgs e)
{
    try
    {
        // Validación básica en UI
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            MessageBox.Show("El nombre es requerido", "Validación");
            return;
        }

        // Crear request
        var request = new CrearHerramientaRequest
        {
            Nombre = txtNombre.Text.Trim(),
            Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) 
                ? null 
                : txtDescripcion.Text.Trim(),
            Stock = (int)numStock.Value
        };

        // Ejecutar Use Case
        var herramienta = await _appService.CrearHerramienta.ExecuteAsync(request);

        MessageBox.Show($"Herramienta creada con ID: {herramienta.Id}", "Éxito");
        LimpiarFormulario();
        await CargarHerramientas();
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
```

---

## READ - Cargar todas las herramientas

```csharp
private async Task CargarHerramientas()
{
    try
    {
        var herramientas = await _appService.ListarHerramientas.ExecuteAsync();
        dgvHerramientas.DataSource = herramientas;
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error al cargar: {ex.Message}", "Error");
    }
}
```

### Llamar al cargar el Form

```csharp
private async void FormHerramientas_Load(object sender, EventArgs e)
{
    await CargarHerramientas();
}
```

---

## READ - Obtener herramienta por ID (para editar)

```csharp
private void DgvHerramientas_CellClick(object? sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex < 0) return;

    var row = dgvHerramientas.Rows[e.RowIndex];
    
    // Guardar la herramienta seleccionada
    _herramientaActual = new HerramientaDTO
    {
        Id = (int)row.Cells["Id"].Value,
        Nombre = (string)row.Cells["Nombre"].Value,
        Descripcion = (string?)row.Cells["Descripcion"].Value,
        Stock = (int)row.Cells["Stock"].Value
    };

    // Cargar datos en los campos
    txtNombre.Text = _herramientaActual.Nombre;
    txtDescripcion.Text = _herramientaActual.Descripcion ?? "";
    numStock.Value = _herramientaActual.Stock;
    lblId.Text = $"ID: {_herramientaActual.Id}";
    
    btnActualizar.Enabled = true;
    btnEliminar.Enabled = true;
}
```

---

## UPDATE - Actualizar Herramienta

```csharp
private async void btnActualizar_Click(object sender, EventArgs e)
{
    try
    {
        if (_herramientaActual == null)
        {
            MessageBox.Show("Seleccione una herramienta para actualizar");
            return;
        }

        // Validación básica en UI
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            MessageBox.Show("El nombre es requerido", "Validación");
            return;
        }

        // Crear request
        var request = new ActualizarHerramientaRequest
        {
            Nombre = txtNombre.Text.Trim(),
            Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) 
                ? null 
                : txtDescripcion.Text.Trim(),
            Stock = (int)numStock.Value
        };

        // Ejecutar Use Case
        await _appService.ActualizarHerramienta.ExecuteAsync(_herramientaActual.Id, request);

        MessageBox.Show("Herramienta actualizada correctamente", "Éxito");
        LimpiarFormulario();
        await CargarHerramientas();
    }
    catch (HerramientaNoEncontradaException ex)
    {
        MessageBox.Show(ex.Message, "Error");
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
```

---

## DELETE - Eliminar Herramienta

```csharp
private async void btnEliminar_Click(object sender, EventArgs e)
{
    try
    {
        if (_herramientaActual == null)
        {
            MessageBox.Show("Seleccione una herramienta para eliminar");
            return;
        }

        // Pedir confirmación
        var confirmar = MessageBox.Show(
            $"¿Desea eliminar la herramienta '{_herramientaActual.Nombre}'?",
            "Confirmar Eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirmar != DialogResult.Yes)
            return;

        // Ejecutar Use Case
        await _appService.EliminarHerramienta.ExecuteAsync(_herramientaActual.Id);

        MessageBox.Show("Herramienta eliminada correctamente", "Éxito");
        LimpiarFormulario();
        await CargarHerramientas();
    }
    catch (HerramientaNoEncontradaException ex)
    {
        MessageBox.Show(ex.Message, "Error");
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error: {ex.Message}", "Error");
    }
}
```

---

## Limpiar Formulario

```csharp
private void LimpiarFormulario()
{
    txtNombre.Clear();
    txtDescripcion.Clear();
    numStock.Value = 0;
    lblId.Text = "ID: -";
    _herramientaActual = null;
    btnActualizar.Enabled = false;
    btnEliminar.Enabled = false;
    txtNombre.Focus();
}

private void btnLimpiar_Click(object sender, EventArgs e)
{
    LimpiarFormulario();
}
```

---

## Flujo Completo

### CREATE
1. `btnCrear_Click()` → valida → `_appService.CrearHerramienta.ExecuteAsync(request)`
2. Use Case valida en backend → `DatoRequeridoException` si hay error
3. Crea la herramienta en BD
4. Retorna `HerramientaDTO` con ID asignado
5. Recarga el grid con `CargarHerramientas()`

### READ
1. `FormHerramientas_Load()` → llama `CargarHerramientas()`
2. `CargarHerramientas()` → `_appService.ListarHerramientas.ExecuteAsync()`
3. Obtiene lista de todas las herramientas
4. Las muestra en el DataGridView

### UPDATE
1. Usuario selecciona fila en grid → `DgvHerramientas_CellClick()`
2. Guarda en `_herramientaActual`
3. Carga datos en los campos
4. Usuario modifica y click en `btnActualizar_Click()`
5. `_appService.ActualizarHerramienta.ExecuteAsync(id, request)`
6. Recarga grid

### DELETE
1. Usuario selecciona fila → `_herramientaActual` tiene los datos
2. Click en `btnEliminar_Click()`
3. Pide confirmación
4. `_appService.EliminarHerramienta.ExecuteAsync(id)`
5. Recarga grid

---

## Use Cases disponibles

```csharp
// En IApplicationService

// Crear
var herramienta = await _appService.CrearHerramienta.ExecuteAsync(request);

// Listar todas
var herramientas = await _appService.ListarHerramientas.ExecuteAsync();

// Obtener por ID
var herramienta = await _appService.ObtenerHerramienta.ExecuteAsync(id);

// Actualizar
await _appService.ActualizarHerramienta.ExecuteAsync(id, request);

// Eliminar
await _appService.EliminarHerramienta.ExecuteAsync(id);
```

---

## Excepciones a capturar

```csharp
using ServindAp.Domain.Exceptions;

// Para CREATE, UPDATE
catch (DatoRequeridoException ex)
{
    // Falta un dato obligatorio o validación fallida
    MessageBox.Show(ex.Message, "Validación");
}

// Para UPDATE, DELETE, GET
catch (HerramientaNoEncontradaException ex)
{
    // No existe herramienta con ese ID
    MessageBox.Show(ex.Message, "Error");
}

// Cualquier otro error
catch (Exception ex)
{
    MessageBox.Show($"Error: {ex.Message}", "Error");
}
```

---

## Resumen de métodos necesarios

| Método | Propósito |
|--------|-----------|
| `btnCrear_Click()` | CREATE |
| `CargarHerramientas()` | READ lista |
| `DgvHerramientas_CellClick()` | READ seleccionar |
| `btnActualizar_Click()` | UPDATE |
| `btnEliminar_Click()` | DELETE |
| `LimpiarFormulario()` | Limpiar campos |
| `FormHerramientas_Load()` | Cargar al abrir |

---

## Notas Importantes

1. `_herramientaActual` → Guarda la herramienta del grid para UPDATE/DELETE
2. **Validar en UI primero** → luego el Use Case valida en backend
3. **Excepciones específicas primero** → DatoRequeridoException, HerramientaNoEncontradaException
4. **Recarga automática** → Después de CREATE, UPDATE, DELETE
5. **async/await** → Todas las operaciones son asincrónicas
6. Confirmación → En DELETE pedir confirmación al usuario


---

# CRUD DE PRÉSTAMOS - LÓGICA BACKEND

## Variable para guardar el préstamo seleccionado

```csharp
private PrestamoDTO? _prestamoActual;
```

---

## CREATE - Crear Préstamo

```csharp
private async void btnCrear_Click(object sender, EventArgs e)
{
    try
    {
        // Validación básica en UI
        if (string.IsNullOrWhiteSpace(txtResponsable.Text))
        {
            MessageBox.Show("El responsable es requerido", "Validación");
            return;
        }

        if (cmbHerramienta.SelectedValue == null)
        {
            MessageBox.Show("Debe seleccionar una herramienta", "Validación");
            return;
        }

        // Crear lista de herramientas
        var herramientas = new List<HerramientaPrestamoRequest>
        {
            new HerramientaPrestamoRequest
            {
                HerramientaId = (int)cmbHerramienta.SelectedValue,
                Cantidad = (int)numCantidad.Value
            }
        };

        // Crear request
        var request = new CrearPrestamoRequest
        {
            Responsable = txtResponsable.Text.Trim(),
            FechaEntrega = dtpFechaEntrega.Value,
            Observaciones = string.IsNullOrWhiteSpace(txtObservaciones.Text) 
                ? null 
                : txtObservaciones.Text.Trim(),
            Herramientas = herramientas
        };

        // Ejecutar Use Case
        var prestamo = await _appService.CrearPrestamo.ExecuteAsync(request);

        MessageBox.Show($"Préstamo creado con ID: {prestamo.Id}", "Éxito");
        LimpiarFormulario();
        await CargarPrestamos();
    }
    catch (StockInsuficienteException ex)
    {
        MessageBox.Show(ex.Message, "Stock Insuficiente");
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
```

---

## READ - Cargar todos los préstamos

```csharp
private async Task CargarPrestamos()
{
    try
    {
        var prestamos = await _appService.ListarPrestamos.ExecuteAsync();
        dgvPrestamos.DataSource = prestamos;
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error al cargar: {ex.Message}", "Error");
    }
}
```

### Llamar al cargar el Form

```csharp
private async void FormPrestamos_Load(object sender, EventArgs e)
{
    await CargarPrestamos();
    await CargarHerramientasEnComboBox();
}
```

---

## READ - Obtener préstamo por ID (para editar)

```csharp
private async void DgvPrestamos_CellClick(object? sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex < 0) return;

    // Obtener el ID del préstamo seleccionado
    int prestamoId = Convert.ToInt32(dgvPrestamos.Rows[e.RowIndex].Cells["Id"].Value);

    try
    {
        // Obtener el préstamo completo con sus herramientas
        _prestamoActual = await _appService.ObtenerPrestamo.ExecuteAsync(prestamoId);

        // Cargar datos en los campos
        txtResponsable.Text = _prestamoActual.Responsable;
        dtpFechaEntrega.Value = _prestamoActual.FechaEntrega;
        txtObservaciones.Text = _prestamoActual.Observaciones ?? "";
        lblId.Text = $"ID: {_prestamoActual.Id}";
        lblEstado.Text = $"Estado: {_prestamoActual.Estado}";

        // Si tiene herramientas, cargar la primera en los controles
        if (_prestamoActual.Herramientas?.Any() == true)
        {
            var primeraHerramienta = _prestamoActual.Herramientas.First();
            cmbHerramienta.SelectedValue = primeraHerramienta.HerramientaId;
            numCantidad.Value = primeraHerramienta.Cantidad;
        }

        // Habilitar botones
        btnActualizar.Enabled = true;
        btnEliminar.Enabled = !_prestamoActual.FechaDevolucion.HasValue; // Solo si no está devuelto
    }
    catch (PrestamoNoEncontradoException ex)
    {
        MessageBox.Show(ex.Message, "Error");
    }
}
```

---

## UPDATE - Actualizar Préstamo

```csharp
private async void btnActualizar_Click(object sender, EventArgs e)
{
    try
    {
        if (_prestamoActual == null)
        {
            MessageBox.Show("Seleccione un préstamo para actualizar");
            return;
        }

        // Validación básica en UI
        if (string.IsNullOrWhiteSpace(txtResponsable.Text))
        {
            MessageBox.Show("El responsable es requerido", "Validación");
            return;
        }

        if (cmbHerramienta.SelectedValue == null)
        {
            MessageBox.Show("Debe seleccionar una herramienta", "Validación");
            return;
        }

        // Crear lista de herramientas
        var herramientas = new List<HerramientaPrestamoRequest>
        {
            new HerramientaPrestamoRequest
            {
                HerramientaId = (int)cmbHerramienta.SelectedValue,
                Cantidad = (int)numCantidad.Value
            }
        };

        // Crear request
        var request = new ActualizarPrestamoRequest
        {
            Responsable = txtResponsable.Text.Trim(),
            FechaEntrega = dtpFechaEntrega.Value,
            Observaciones = string.IsNullOrWhiteSpace(txtObservaciones.Text) 
                ? null 
                : txtObservaciones.Text.Trim(),
            Herramientas = herramientas
        };

        // Ejecutar Use Case - NOTA: Pasamos el ID como primer parámetro
        await _appService.ActualizarPrestamo.ExecuteAsync(_prestamoActual.Id, request);

        MessageBox.Show("Préstamo actualizado correctamente", "Éxito");
        LimpiarFormulario();
        await CargarPrestamos();
    }
    catch (PrestamoNoEncontradoException ex)
    {
        MessageBox.Show(ex.Message, "Error");
    }
    catch (StockInsuficienteException ex)
    {
        MessageBox.Show(ex.Message, "Stock Insuficiente");
    }
    catch (InvalidOperationException ex)
    {
        MessageBox.Show(ex.Message, "Operación no permitida");
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
```

---

## DELETE - Eliminar Préstamo

```csharp
private async void btnEliminar_Click(object sender, EventArgs e)
{
    try
    {
        if (_prestamoActual == null)
        {
            MessageBox.Show("Seleccione un préstamo para eliminar");
            return;
        }

        // No permitir eliminar si ya fue devuelto
        if (_prestamoActual.FechaDevolucion.HasValue)
        {
            MessageBox.Show("No se puede eliminar un préstamo ya devuelto", "Error");
            return;
        }

        // Pedir confirmación
        var confirmar = MessageBox.Show(
            $"¿Desea eliminar el préstamo del responsable '{_prestamoActual.Responsable}'?\n" +
            $"Esto devolverá el stock de las herramientas.",
            "Confirmar Eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirmar != DialogResult.Yes)
            return;

        // Ejecutar Use Case
        await _appService.EliminarPrestamo.ExecuteAsync(_prestamoActual.Id);

        MessageBox.Show("Préstamo eliminado correctamente", "Éxito");
        LimpiarFormulario();
        await CargarPrestamos();
    }
    catch (PrestamoNoEncontradoException ex)
    {
        MessageBox.Show(ex.Message, "Error");
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error: {ex.Message}", "Error");
    }
}
```

---

## DEVOLUCIÓN - Registrar Devolución de Préstamo

```csharp
private async void btnDevolver_Click(object sender, EventArgs e)
{
    try
    {
        if (_prestamoActual == null)
        {
            MessageBox.Show("Seleccione un préstamo para devolver");
            return;
        }

        // Verificar si ya fue devuelto
        if (_prestamoActual.FechaDevolucion.HasValue)
        {
            MessageBox.Show("Este préstamo ya fue devuelto", "Información");
            return;
        }

        // Preguntar si hay defectos
        var tieneDefectos = MessageBox.Show(
            "¿Las herramientas tienen algún defecto o daño?",
            "Registrar Devolución",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        ) == DialogResult.Yes;

        // Crear request
        var request = new RegistrarDevolucionRequest
        {
            FechaDevolucion = DateTime.Now,
            TieneDefectos = tieneDefectos
        };

        // Ejecutar Use Case
        await _appService.RegistrarDevolucion.ExecuteAsync(_prestamoActual.Id, request);

        string mensaje = tieneDefectos 
            ? "Devolución registrada con defectos" 
            : "Devolución registrada correctamente";

        MessageBox.Show(mensaje, "Éxito");
        LimpiarFormulario();
        await CargarPrestamos();
    }
    catch (PrestamoYaDevueltoException ex)
    {
        MessageBox.Show(ex.Message, "Error");
    }
    catch (PrestamoNoEncontradoException ex)
    {
        MessageBox.Show(ex.Message, "Error");
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error: {ex.Message}", "Error");
    }
}
```

---

## Limpiar Formulario

```csharp
private void LimpiarFormulario()
{
    txtResponsable.Clear();
    txtObservaciones.Clear();
    dtpFechaEntrega.Value = DateTime.Now;
    cmbHerramienta.SelectedIndex = -1;
    numCantidad.Value = 1;
    lblId.Text = "ID: -";
    lblEstado.Text = "Estado: -";
    _prestamoActual = null;
    btnActualizar.Enabled = false;
    btnEliminar.Enabled = false;
    txtResponsable.Focus();
}

private void btnLimpiar_Click(object sender, EventArgs e)
{
    LimpiarFormulario();
}
```

---

## Cargar Herramientas en ComboBox

```csharp
private async Task CargarHerramientasEnComboBox()
{
    try
    {
        var herramientas = await _appService.ListarHerramientas.ExecuteAsync();
        
        cmbHerramienta.DataSource = herramientas;
        cmbHerramienta.DisplayMember = "Nombre";
        cmbHerramienta.ValueMember = "Id";
        cmbHerramienta.SelectedIndex = -1; // Ninguno seleccionado por defecto
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error al cargar herramientas: {ex.Message}", "Error");
    }
}
```

---

## Flujo Completo de Préstamos

### CREATE
1. `btnCrear_Click()` → valida → crea lista de herramientas
2. `_appService.CrearPrestamo.ExecuteAsync(request)`
3. Use Case valida stock y datos
4. Crea préstamo y reduce stock de herramientas
5. Retorna `PrestamoDTO`
6. Recarga el grid

### READ
1. `FormPrestamos_Load()` → llama `CargarPrestamos()`
2. `CargarPrestamos()` → `_appService.ListarPrestamos.ExecuteAsync()`
3. Muestra en DataGridView

### UPDATE
1. Usuario selecciona fila → `DgvPrestamos_CellClick()`
2. Obtiene préstamo completo con `ObtenerPrestamo.ExecuteAsync(id)`
3. Guarda en `_prestamoActual`
4. Carga datos en campos
5. Usuario modifica y click en `btnActualizar_Click()`
6. **IMPORTANTE:** `_appService.ActualizarPrestamo.ExecuteAsync(id, request)`
7. Use Case:
   - Devuelve stock de herramientas anteriores
   - Valida nuevas herramientas
   - Actualiza préstamo
   - Reduce stock de nuevas herramientas
8. Recarga grid

### DELETE
1. Usuario selecciona fila
2. Verifica que no esté devuelto
3. Pide confirmación
4. `_appService.EliminarPrestamo.ExecuteAsync(id)`
5. Devuelve stock de herramientas
6. Recarga grid

### DEVOLUCIÓN
1. Usuario selecciona préstamo activo
2. Click en `btnDevolver_Click()`
3. Pregunta si tiene defectos
4. `_appService.RegistrarDevolucion.ExecuteAsync(id, request)`
5. Actualiza estado y devuelve stock
6. Recarga grid

---

## Use Cases de Préstamos Disponibles

```csharp
// En IApplicationService

// Crear préstamo
var prestamo = await _appService.CrearPrestamo.ExecuteAsync(request);

// Listar todos
var prestamos = await _appService.ListarPrestamos.ExecuteAsync();

// Obtener por ID
var prestamo = await _appService.ObtenerPrestamo.ExecuteAsync(id);

// Actualizar préstamo - NOTA: Requiere ID como primer parámetro
await _appService.ActualizarPrestamo.ExecuteAsync(id, request);

// Eliminar préstamo
await _appService.EliminarPrestamo.ExecuteAsync(id);

// Registrar devolución
await _appService.RegistrarDevolucion.ExecuteAsync(id, request);
```

---

## Request para Actualizar Préstamo

```csharp
var request = new ActualizarPrestamoRequest
{
    Responsable = "Juan Pérez",
    FechaEntrega = DateTime.Now,
    Observaciones = "Observaciones actualizadas",
    Herramientas = new List<HerramientaPrestamoRequest>
    {
        new HerramientaPrestamoRequest 
        { 
            HerramientaId = 1, 
            Cantidad = 2 
        },
        new HerramientaPrestamoRequest 
        { 
            HerramientaId = 3, 
            Cantidad = 1 
        }
    }
};

// IMPORTANTE: Pasar el ID como primer parámetro
await _appService.ActualizarPrestamo.ExecuteAsync(prestamoId, request);
```

---

## Excepciones de Préstamos a Capturar

```csharp
using ServindAp.Domain.Exceptions;

// Para CREATE, UPDATE
catch (StockInsuficienteException ex)
{
    // No hay suficiente stock de una herramienta
    MessageBox.Show(ex.Message, "Stock Insuficiente");
}

catch (DatoRequeridoException ex)
{
    // Falta un dato obligatorio
    MessageBox.Show(ex.Message, "Validación");
}

catch (PrestamoSinHerramientasException ex)
{
    // El préstamo debe tener al menos una herramienta
    MessageBox.Show(ex.Message, "Validación");
}

// Para UPDATE, DELETE, GET, DEVOLUCIÓN
catch (PrestamoNoEncontradoException ex)
{
    // No existe préstamo con ese ID
    MessageBox.Show(ex.Message, "Error");
}

catch (PrestamoYaDevueltoException ex)
{
    // Intentando devolver un préstamo ya devuelto
    MessageBox.Show(ex.Message, "Error");
}

catch (InvalidOperationException ex)
{
    // Operación no permitida (ej: editar préstamo devuelto)
    MessageBox.Show(ex.Message, "Operación no permitida");
}

// Cualquier otro error
catch (Exception ex)
{
    MessageBox.Show($"Error: {ex.Message}", "Error");
}
```

---

## ⚠️ IMPORTANTE: Diferencias en ActualizarPrestamo

### ❌ INCORRECTO:
```csharp
// NO hagas esto:
var request = new ActualizarPrestamoRequest
{
    Id = prestamoId,  // ❌ NO tiene propiedad Id
    Responsable = "...",
    // ...
};

await _appService.ActualizarPrestamo.ExecuteAsync(request);
```

### ✅ CORRECTO:
```csharp
// Haz esto:
var request = new ActualizarPrestamoRequest
{
    Responsable = "...",
    FechaEntrega = DateTime.Now,
    Observaciones = "...",
    Herramientas = herramientas
};

// ID va como primer parámetro separado
await _appService.ActualizarPrestamo.ExecuteAsync(prestamoId, request);
```

---

## Proceso de Actualización de Préstamo (Detallado)

Cuando ejecutas `ActualizarPrestamo.ExecuteAsync(id, request)`, el UseCase hace lo siguiente:

1. **Valida el request**
   - Responsable no vacío y entre 3-100 caracteres
   - Fecha de entrega no futura
   - Al menos una herramienta
   - Cantidades mayores a 0
   - Sin herramientas duplicadas

2. **Verifica que el préstamo existe**
   - Busca el préstamo por ID
   - Si no existe → `PrestamoNoEncontradoException`

3. **Valida que el préstamo no esté devuelto**
   - Si está devuelto → `InvalidOperationException`

4. **Devuelve el stock de las herramientas anteriores**
   - Obtiene las herramientas que estaban en el préstamo
   - Para cada una, aumenta su stock
   - Actualiza en BD

5. **Valida las nuevas herramientas**
   - Verifica que todas existan
   - Verifica que tengan stock suficiente
   - Si no → `HerramientaNoEncontradaException` o `StockInsuficienteException`

6. **Actualiza los datos del préstamo**
   - Responsable
   - Fecha de entrega
   - Observaciones

7. **Elimina las herramientas antiguas**
   - Borra las relaciones anteriores de la tabla intermedia

8. **Agrega las nuevas herramientas**
   - Crea nuevas relaciones en tabla intermedia
   - Reduce el stock de cada herramienta
   - Actualiza en BD

9. **Retorna el PrestamoDTO actualizado**
   - Con todas las herramientas nuevas

---

## Resumen de Métodos para Préstamos

| Método | Propósito |
|--------|-----------|
| `btnCrear_Click()` | CREATE |
| `CargarPrestamos()` | READ lista |
| `DgvPrestamos_CellClick()` | READ seleccionar |
| `btnActualizar_Click()` | UPDATE |
| `btnEliminar_Click()` | DELETE |
| `btnDevolver_Click()` | DEVOLUCIÓN |
| `LimpiarFormulario()` | Limpiar campos |
| `FormPrestamos_Load()` | Cargar al abrir |
| `CargarHerramientasEnComboBox()` | Cargar herramientas disponibles |

---

## Notas Importantes de Préstamos

1. `_prestamoActual` → Guarda el préstamo del grid para UPDATE/DELETE/DEVOLUCIÓN
2. **ID separado** → En `ActualizarPrestamo.ExecuteAsync(id, request)` el ID va como primer parámetro
3. **Stock automático** → Al actualizar, devuelve stock antiguo y reduce nuevo automáticamente
4. **No editar devueltos** → Los préstamos devueltos no se pueden editar ni eliminar
5. **Validaciones en ambos lados** → UI valida primero, luego el UseCase valida en backend
6. **Herramientas múltiples** → Puedes agregar varias herramientas a un préstamo
7. **async/await** → Todas las operaciones son asincrónicas
8. **Confirmación** → En DELETE pedir confirmación al usuario
