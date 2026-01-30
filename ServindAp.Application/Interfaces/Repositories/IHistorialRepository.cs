using ServindAp.Domain.Entities;

namespace ServindAp.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repository de solo lectura y escritura limitada para el historial.
    /// El historial es gestionado principalmente por triggers de SQLite,
    /// este repository solo permite consultas y actualización de observaciones.
    /// </summary>
    public interface IHistorialRepository
    {
        /// <summary>
        /// Obtiene todo el historial de eventos de préstamos y devoluciones.
        /// </summary>
        /// <returns>Lista completa del historial con sus relaciones</returns>
        Task<List<HistorialPrestamoHerramienta>> ObtenerTodoAsync();

        /// <summary>
        /// Actualiza las observaciones en los registros de DEVOLUCION más recientes
        /// de un préstamo específico que aún no tienen observaciones.
        /// </summary>
        /// <param name="prestamoId">ID del préstamo</param>
        /// <param name="observaciones">Observaciones a agregar</param>
        Task ActualizarObservacionesDevolucionRecienteAsync(int prestamoId, string observaciones);
    }
}