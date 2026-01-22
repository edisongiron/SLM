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
