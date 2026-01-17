using ServindAp.Domain.Entities;

namespace ServindAp.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interfaz para el repositorio de Préstamos.
    /// Define los contratos para acceder a datos de préstamos.
    /// </summary>
    public interface IPrestamoRepository
    {
        /// <summary>
        /// Obtiene un préstamo por su identificador.
        /// </summary>
        Task<Prestamo?> ObtenerPorIdAsync(int id);

        /// <summary>
        /// Obtiene todos los préstamos con estado "Activo".
        /// </summary>
        Task<IReadOnlyList<Prestamo>> ObtenerActivosAsync();

        /// <summary>
        /// Crea un nuevo préstamo.
        /// </summary>
        Task<int> CrearAsync(Prestamo prestamo);

        /// <summary>
        /// Actualiza un préstamo existente.
        /// </summary>
        Task ActualizarAsync(Prestamo prestamo);

        /// <summary>
        /// Elimina un préstamo.
        /// </summary>
        Task EliminarAsync(int id);
    }
}
