using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    public class ActualizarHerramientaUseCase
    {
        private readonly IHerramientaRepository _herramientaRepository;

        public ActualizarHerramientaUseCase(IHerramientaRepository herramientaRepository)
        {
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        public async Task<HerramientaDTO> ExecuteAsync(int id, ActualizarHerramientaRequest request)
        {
            var herramienta = await _herramientaRepository.ObtenerPorIdAsync(id);
            if (herramienta == null)
                throw new HerramientaNoEncontradaException(id);

            ValidarRequest(request);

            herramienta.Nombre = request.Nombre;
            herramienta.Descripcion = request.Descripcion;
            herramienta.Stock = request.Stock;

            await _herramientaRepository.ActualizarAsync(herramienta);

            return HerramientaDTO.FromDomain(herramienta);
        }

        private void ValidarRequest(ActualizarHerramientaRequest request)
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

    public class ActualizarHerramientaRequest
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Stock { get; set; }
    }
}
