using Microsoft.EntityFrameworkCore;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Infrastructure.Data;

namespace ServindAp.Infrastructure.Persistence
{
    /// <summary>
    /// Implementación del repository para la tabla Historial.
    /// Nota: La tabla Historial es principalmente gestionada por triggers de SQLite.
    /// Este repository solo proporciona operaciones específicas que no pueden
    /// ser manejadas automáticamente por los triggers.
    /// </summary>
    public class HistorialRepository : IHistorialRepository
    {
        private readonly ServindApDbContext _context;

        public HistorialRepository(ServindApDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Obtiene todo el historial de eventos con sus relaciones.
        /// </summary>
        public async Task<List<Domain.Entities.HistorialPrestamoHerramienta>> ObtenerTodoAsync()
        {
            return await _context.HistorialPrestamosHerramientas
                .Include(h => h.Prestamo)
                .Include(h => h.Herramienta)
                .OrderByDescending(h => h.FechaEvento)
                .ToListAsync();
        }

        /// <summary>
        /// Actualiza las observaciones en los registros de DEVOLUCION más recientes
        /// de un préstamo específico que aún no tienen observaciones.
        /// 
        /// Este método es necesario porque los triggers de SQLite no tienen acceso
        /// a las observaciones del formulario, solo ven los datos de Prestamo_Herramienta.
        /// </summary>
        public async Task ActualizarObservacionesDevolucionRecienteAsync(int prestamoId, string observaciones)
        {
            if (string.IsNullOrWhiteSpace(observaciones))
                return;

            // Actualizar solo los registros de DEVOLUCION recientes (último minuto)
            // que no tienen observaciones aún
            var sql = @"
                UPDATE Historial 
                SET observaciones = {0}
                WHERE prestamo_id = {1} 
                  AND tipo_evento = 'DEVOLUCION'
                  AND (observaciones IS NULL OR observaciones = '')
                  AND fecha_evento >= datetime('now', '-1 minute', 'localtime')
            ";

            await _context.Database.ExecuteSqlRawAsync(sql, observaciones, prestamoId);
        }
    }
}