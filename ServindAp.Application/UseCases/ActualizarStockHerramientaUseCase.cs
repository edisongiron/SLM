using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    public class ActualizarStockHerramientaUseCase
    {
        private readonly IHerramientaRepository _herramientaRepository;

        public ActualizarStockHerramientaUseCase(IHerramientaRepository herramientaRepository)
        {
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        /// <summary>
        /// Reduce el stock de una herramienta.
        /// </summary>
        public async Task ReducirStockAsync(int herramientaId, int cantidad)
        {
            var herramienta = await _herramientaRepository.ObtenerPorIdAsync(herramientaId);

            if (herramienta == null)
                throw new HerramientaNoEncontradaException(herramientaId);

            // Usar lógica de dominio
            herramienta.ReducirStock(cantidad);

            await _herramientaRepository.ActualizarAsync(herramienta);
        }

        /// <summary>
        /// Aumenta el stock de una herramienta.
        /// </summary>
        public async Task AumentarStockAsync(int herramientaId, int cantidad)
        {
            var herramienta = await _herramientaRepository.ObtenerPorIdAsync(herramientaId);

            if (herramienta == null)
                throw new HerramientaNoEncontradaException(herramientaId);

            // Usar lógica de dominio
            herramienta.AumentarStock(cantidad);

            await _herramientaRepository.ActualizarAsync(herramienta);
        }

        /// <summary>
        /// Establece el stock de una herramienta directamente.
        /// </summary>
        public async Task EstablecerStockAsync(int herramientaId, int nuevoStock)
        {
            if (nuevoStock < 0)
                throw new ArgumentException("El stock no puede ser negativo");

            var herramienta = await _herramientaRepository.ObtenerPorIdAsync(herramientaId);

            if (herramienta == null)
                throw new HerramientaNoEncontradaException(herramientaId);

            herramienta.Stock = nuevoStock;

            await _herramientaRepository.ActualizarAsync(herramienta);
        }
    }
}
