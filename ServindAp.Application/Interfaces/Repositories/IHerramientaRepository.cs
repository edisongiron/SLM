using ServindAp.Domain.Entities;

namespace ServindAp.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interfaz para el repositorio de Herramientas.
    /// Define los contratos para acceder a datos de herramientas.
    /// </summary>
    public interface IHerramientaRepository
    {
        /// <summary>
        /// Obtiene una herramienta por su identificador.
        /// </summary>
        Task<Herramienta?> ObtenerPorIdAsync(int id);

        /// <summary>
        /// Obtiene todas las herramientas disponibles.
        /// </summary>
        Task<IReadOnlyList<Herramienta>> ObtenerTodasAsync();

        /// <summary>
        /// Obtiene múltiples herramientas por sus identificadores.
        /// </summary>
        Task<IReadOnlyList<Herramienta>> ObtenerPorIdsAsync(List<int> ids);

        /// <summary>
        /// Crea una nueva herramienta.
        /// </summary>
        Task<int> CrearAsync(Herramienta herramienta);

        /// <summary>
        /// Actualiza una herramienta existente.
        /// </summary>
        Task ActualizarAsync(Herramienta herramienta);

        /// <summary>
        /// Elimina una herramienta.
        /// </summary>
        Task EliminarAsync(int id);
    }
}
