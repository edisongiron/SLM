using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    public class EliminarPrestamoUseCase
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IPrestamoHerramientaRepository _prestamoHerramientaRepository;
        private readonly IHerramientaRepository _herramientaRepository;

        public EliminarPrestamoUseCase(
            IPrestamoRepository prestamoRepository,
            IPrestamoHerramientaRepository prestamoHerramientaRepository,
            IHerramientaRepository herramientaRepository)
        {
            _prestamoRepository = prestamoRepository;
            _prestamoHerramientaRepository = prestamoHerramientaRepository;
            _herramientaRepository = herramientaRepository;
        }

        public async Task ExecuteAsync(int prestamoId)
        {
            var prestamo = await _prestamoRepository.ObtenerPorIdAsync(prestamoId);
            if (prestamo == null)
                throw new PrestamoNoEncontradoException(prestamoId);

            if (prestamo.EstaDevuelto())
            {
                throw new InvalidOperationException(
                    "No se puede eliminar un préstamo ya devuelto. Los préstamos devueltos forman parte del historial."
                );
            }

            var herramientasPrestamo = await _prestamoHerramientaRepository.ObtenerPorPrestamoIdAsync(prestamoId);

            foreach (var ph in herramientasPrestamo)
            {
                var herramienta = await _herramientaRepository.ObtenerPorIdAsync(ph.HerramientaId);
                if (herramienta != null)
                {
                    herramienta.AumentarStock(ph.Cantidad);
                    await _herramientaRepository.ActualizarAsync(herramienta);
                }
            }

            await _prestamoHerramientaRepository.EliminarPorPrestamoIdAsync(prestamoId);

            await _prestamoRepository.EliminarAsync(prestamoId);
        }
    }
}
