using ServindAp.Domain.Entities;

namespace ServindAp.Application.Interfaces.Repositories
{
    public interface IHerramientaRepository
    {
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

        /// <summary>
        /// Obtiene herramientas que tienen stock disponible.
        /// </summary>
        Task<IReadOnlyList<Herramienta>> ObtenerConStockAsync();

        /// <summary>
        /// Verifica si una herramienta tiene stock suficiente.
        /// </summary>
        Task<bool> TieneStockDisponibleAsync(int herramientaId, int cantidadRequerida);
    }
}
