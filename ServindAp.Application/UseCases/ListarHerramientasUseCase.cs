using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;

namespace ServindAp.Application.UseCases
{
    /// <summary>
    /// UseCase para listar todas las herramientas disponibles en el sistema.
    /// Útil para mostrarlas en los formularios de creación de préstamos.
    /// </summary>
    public class ListarHerramientasUseCase
    {
        private readonly IHerramientaRepository _herramientaRepository;

        public ListarHerramientasUseCase(IHerramientaRepository herramientaRepository)
        {
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        /// <summary>
        /// Ejecuta el caso de uso para listar herramientas.
        /// </summary>
        /// <param name="filtro">Parámetros opcionales de filtrado</param>
        /// <returns>Lista de DTOs de herramientas</returns>
        public async Task<List<HerramientaDTO>> ExecuteAsync(ListarHerramientasFilter? filtro = null)
        {
            filtro ??= new ListarHerramientasFilter();

            var herramientas = await _herramientaRepository.ObtenerTodasAsync();

            // Aplicar filtros
            var herramientasFiltradas = herramientas.AsEnumerable();

            if (filtro.SoloRetornables)
            {
                herramientasFiltradas = herramientasFiltradas.Where(h => h.EsRetornable);
            }

            // Convertir a DTOs
            var herramientasDTOs = herramientasFiltradas
                .Select(HerramientaDTO.FromDomain)
                .ToList();

            // Aplicar ordenamiento
            return OrdenarHerramientas(herramientasDTOs, filtro.OrdenarPor);
        }

        private List<HerramientaDTO> OrdenarHerramientas(List<HerramientaDTO> herramientas, string? ordenarPor)
        {
            return (ordenarPor?.ToLower()) switch
            {
                "nombre_desc" => herramientas.OrderByDescending(h => h.Nombre).ToList(),
                _ => herramientas.OrderBy(h => h.Nombre).ToList()
            };
        }
    }

    /// <summary>
    /// Filtros para listar herramientas.
    /// </summary>
    public class ListarHerramientasFilter
    {
        /// <summary>
        /// Si es true, solo obtiene las herramientas que son retornables.
        /// </summary>
        public bool SoloRetornables { get; set; } = false;

        /// <summary>
        /// Campo por el cual ordenar (nombre, nombre_desc).
        /// </summary>
        public string? OrdenarPor { get; set; } = "nombre";
    }
}
