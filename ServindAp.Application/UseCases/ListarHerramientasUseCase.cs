using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;

namespace ServindAp.Application.UseCases
{
    public class ListarHerramientasUseCase
    {
        private readonly IHerramientaRepository _herramientaRepository;

        public ListarHerramientasUseCase(IHerramientaRepository herramientaRepository)
        {
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        public async Task<List<HerramientaDTO>> ExecuteAsync(ListarHerramientasFilter? filtro = null)
        {
            filtro ??= new ListarHerramientasFilter();

            var herramientas = await _herramientaRepository.ObtenerTodasAsync();

            var herramientasFiltradas = herramientas.AsEnumerable();

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
        public string? OrdenarPor { get; set; } = "nombre";
    }
}
