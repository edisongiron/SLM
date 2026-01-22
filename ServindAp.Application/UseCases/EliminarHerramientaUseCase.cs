using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    public class EliminarHerramientaUseCase
    {
        private readonly IHerramientaRepository _herramientaRepository;

        public EliminarHerramientaUseCase(IHerramientaRepository herramientaRepository)
        {
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        public async Task ExecuteAsync(int id)
        {
            var herramienta = await _herramientaRepository.ObtenerPorIdAsync(id);
            if (herramienta == null)
                throw new HerramientaNoEncontradaException(id);

            await _herramientaRepository.EliminarAsync(id);
        }
    }
}
