using ServindAp.Domain.Entities;

namespace ServindAp.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interfaz para el repositorio de PrestamoHerramienta.
    /// Define los contratos para acceder a las relaciones entre préstamos y herramientas.
    /// </summary>
    public interface IPrestamoHerramientaRepository
    {
        /// <summary>
        /// Obtiene todas las herramientas asociadas a un préstamo específico.
        /// </summary>
        Task<IReadOnlyList<PrestamoHerramienta>> ObtenerPorPrestamoIdAsync(int prestamoId);

        /// <summary>
        /// Obtiene una asociación específica de préstamo-herramienta.
        /// </summary>
        Task<PrestamoHerramienta?> ObtenerPorIdAsync(int id);

        /// <summary>
        /// Crea una nueva asociación entre un préstamo y una herramienta.
        /// </summary>
        Task<int> CrearAsync(PrestamoHerramienta prestamoHerramienta);

        /// <summary>
        /// Actualiza una asociación existente.
        /// </summary>
        Task ActualizarAsync(PrestamoHerramienta prestamoHerramienta);

        /// <summary>
        /// Elimina una asociación.
        /// </summary>
        Task EliminarAsync(int id);

        /// <summary>
        /// Elimina todas las herramientas asociadas a un préstamo.
        /// </summary>
        Task EliminarPorPrestamoIdAsync(int prestamoId);
    }
}
