using ServindAp.Domain.Entities;

namespace ServindAp.Application.Interfaces.Repositories
{
    public interface IPrestamoRepository
    {
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
