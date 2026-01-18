using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;

namespace ServindAp.Application.UseCases
{
    public class ListarPrestamosUseCase
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IPrestamoHerramientaRepository _prestamoHerramientaRepository;
        private readonly IHerramientaRepository _herramientaRepository;

        public ListarPrestamosUseCase(
            IPrestamoRepository prestamoRepository,
            IPrestamoHerramientaRepository prestamoHerramientaRepository,
            IHerramientaRepository herramientaRepository)
        {
            _prestamoRepository = prestamoRepository ?? throw new ArgumentNullException(nameof(prestamoRepository));
            _prestamoHerramientaRepository = prestamoHerramientaRepository ?? throw new ArgumentNullException(nameof(prestamoHerramientaRepository));
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        /// <summary>
        /// Ejecuta el caso de uso para cargar todos los préstamos.
        /// No aplica filtros, ordenamiento ni búsqueda; eso se maneja en la UI.
        /// </summary>
        /// <returns>Lista de todos los préstamos con sus herramientas asociadas</returns>
        public async Task<List<PrestamoDTO>> ExecuteAsync()
        {
            // Obtener todos los préstamos
            var prestamos = await _prestamoRepository.ObtenerActivosAsync();

            // Construir los DTOs con sus herramientas asociadas
            var prestamoDTOs = new List<PrestamoDTO>();

            foreach (var prestamo in prestamos)
            {
                var herramientas = await _prestamoHerramientaRepository.ObtenerPorPrestamoIdAsync(prestamo.Id);

                if (herramientas.Count > 0)
                {
                    var herramientaIds = herramientas.Select(h => h.HerramientaId).ToList();
                    var herramientasEntities = await _herramientaRepository.ObtenerPorIdsAsync(herramientaIds);
                    var herramientasMap = herramientasEntities.ToDictionary(h => h.Id, h => h);

                    var prestamoDTO = PrestamoDTO.FromDomain(prestamo, herramientas.ToList(), herramientasMap);
                    prestamoDTOs.Add(prestamoDTO);
                }
                else
                {
                    var prestamoDTO = PrestamoDTO.FromDomain(prestamo, new List<Domain.Entities.PrestamoHerramienta>());
                    prestamoDTOs.Add(prestamoDTO);
                }
            }

            return prestamoDTOs;
        }
    }
}
