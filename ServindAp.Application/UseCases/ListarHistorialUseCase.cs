using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;

namespace ServindAp.Application.UseCases
{
    public class ListarHistorialUseCase
    {
        private readonly IHistorialRepository _historialRepository;
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IHerramientaRepository _herramientaRepository;

        public ListarHistorialUseCase(
            IHistorialRepository historialRepository,
            IPrestamoRepository prestamoRepository,
            IHerramientaRepository herramientaRepository)
        {
            _historialRepository = historialRepository ?? throw new ArgumentNullException(nameof(historialRepository));
            _prestamoRepository = prestamoRepository ?? throw new ArgumentNullException(nameof(prestamoRepository));
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        /// <summary>
        /// Ejecuta el caso de uso para obtener todo el historial de eventos.
        /// </summary>
        /// <returns>Lista completa del historial con información de responsables y herramientas</returns>
        public async Task<List<HistorialDTO>> ExecuteAsync()
        {
            // Obtener todo el historial
            var historial = await _historialRepository.ObtenerTodoAsync();

            // Obtener IDs únicos para precargar datos relacionados
            var prestamoIds = historial.Select(h => h.PrestamoId).Distinct().ToList();
            var herramientaIds = historial.Select(h => h.HerramientaId).Distinct().ToList();

            // Precargar herramientas
            var herramientas = await _herramientaRepository.ObtenerPorIdsAsync(herramientaIds);
            var herramientasMap = herramientas.ToDictionary(h => h.Id, h => h);

            // Precargar préstamos (obteniendo uno por uno ya que no hay método para obtener por IDs)
            var prestamosMap = new Dictionary<int, Domain.Entities.Prestamo>();
            foreach (var prestamoId in prestamoIds)
            {
                var prestamo = await _prestamoRepository.ObtenerPorIdAsync(prestamoId);
                if (prestamo != null)
                {
                    prestamosMap[prestamoId] = prestamo;
                }
            }

            // Construir DTOs con datos relacionados
            var historialDTOs = historial
                .Select(h => HistorialDTO.FromDomain(
                    h,
                    prestamosMap.TryGetValue(h.PrestamoId, out var prestamo) ? prestamo : null,
                    herramientasMap.TryGetValue(h.HerramientaId, out var herramienta) ? herramienta : null
                ))
                .OrderByDescending(h => h.FechaEvento) // Ordenar por fecha más reciente primero
                .ToList();

            return historialDTOs;
        }
    }
}