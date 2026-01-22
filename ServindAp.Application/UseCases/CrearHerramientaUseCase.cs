using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Entities;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    public class CrearHerramientaUseCase
    {
        private readonly IHerramientaRepository _herramientaRepository;

        public CrearHerramientaUseCase(IHerramientaRepository herramientaRepository)
        {
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        public async Task<HerramientaDTO> ExecuteAsync(CrearHerramientaRequest request)
        {
            ValidarRequest(request);

            var herramienta = new Herramienta(
                request.Nombre,
                request.Descripcion,
                request.Stock
            );

            int id = await _herramientaRepository.CrearAsync(herramienta);
            herramienta.Id = id;

            return HerramientaDTO.FromDomain(herramienta);
        }

        private void ValidarRequest(CrearHerramientaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new DatoRequeridoException("Nombre");

            if (request.Nombre.Length < 3)
                throw new DatoRequeridoException("El nombre debe tener al menos 3 caracteres");

            if (request.Nombre.Length > 100)
                throw new DatoRequeridoException("El nombre no puede exceder 100 caracteres");

            if (request.Stock < 0)
                throw new DatoRequeridoException("El stock no puede ser negativo");
        }
    }

    public class CrearHerramientaRequest
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Stock { get; set; } = 0;
    }
}
