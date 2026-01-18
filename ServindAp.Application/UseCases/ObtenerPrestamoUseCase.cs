using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    public class ObtenerPrestamoUseCase
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IPrestamoHerramientaRepository _prestamoHerramientaRepository;
        private readonly IHerramientaRepository _herramientaRepository;

        public ObtenerPrestamoUseCase(
            IPrestamoRepository prestamoRepository,
            IPrestamoHerramientaRepository prestamoHerramientaRepository,
            IHerramientaRepository herramientaRepository)
        {
            _prestamoRepository = prestamoRepository ?? throw new ArgumentNullException(nameof(prestamoRepository));
            _prestamoHerramientaRepository = prestamoHerramientaRepository ?? throw new ArgumentNullException(nameof(prestamoHerramientaRepository));
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        /// <summary>
        /// Ejecuta el caso de uso para obtener un préstamo.
        /// </summary>
        /// <param name="prestamoId">ID del préstamo a obtener</param>
        /// <returns>DTO del préstamo</returns>
        public async Task<PrestamoDTO> ExecuteAsync(int prestamoId)
        {
            if (prestamoId <= 0)
                throw new DatoRequeridoException("ID del préstamo");

            var prestamo = await _prestamoRepository.ObtenerPorIdAsync(prestamoId);
            
            if (prestamo == null)
                throw new PrestamoNoEncontradoException(prestamoId);

            // Obtener las herramientas asociadas
            var herramientas = await _prestamoHerramientaRepository.ObtenerPorPrestamoIdAsync(prestamoId);

            Dictionary<int, Domain.Entities.Herramienta>? herramientasMap = null;
            if (herramientas.Count > 0)
            {
                var herramientaIds = herramientas.Select(h => h.HerramientaId).ToList();
                var herramientasEntities = await _herramientaRepository.ObtenerPorIdsAsync(herramientaIds);
                herramientasMap = herramientasEntities.ToDictionary(h => h.Id, h => h);
            }

            return PrestamoDTO.FromDomain(prestamo, herramientas.ToList(), herramientasMap);
        }
    }
}
