using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    public class RegistrarDevolucionUseCase
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IPrestamoHerramientaRepository _prestamoHerramientaRepository;
        private readonly IHerramientaRepository _herramientaRepository;

        public RegistrarDevolucionUseCase(
            IPrestamoRepository prestamoRepository,
            IPrestamoHerramientaRepository prestamoHerramientaRepository,
            IHerramientaRepository herramientaRepository)
        {
            _prestamoRepository = prestamoRepository ?? throw new ArgumentNullException(nameof(prestamoRepository));
            _prestamoHerramientaRepository = prestamoHerramientaRepository ?? throw new ArgumentNullException(nameof(prestamoHerramientaRepository));
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        /// <summary>
        /// Ejecuta el caso de uso para registrar una devolución.
        /// </summary>
        /// <param name="request">Datos de la devolución</param>
        /// <returns>DTO del préstamo actualizado</returns>
        public async Task<PrestamoDTO> ExecuteAsync(RegistrarDevolucionRequest request)
        {
            ValidarRequest(request);

            // Obtener el préstamo
            var prestamo = await _prestamoRepository.ObtenerPorIdAsync(request.PrestamoId);
            
            if (prestamo == null)
                throw new PrestamoNoEncontradoException(request.PrestamoId);

            // Obtener las herramientas del préstamo para devolver el stock
            var herramientasPrestamo = await _prestamoHerramientaRepository.ObtenerPorPrestamoIdAsync(prestamo.Id);

            // Registrar la devolución en la entidad de dominio
            // Esto lanzará excepciones del dominio si hay problemas
            prestamo.RegistrarDevolucion(request.FechaDevolucion, request.TieneDefectos);

            // Devolver el stock de las herramientas retornables
            foreach (var ph in herramientasPrestamo)
            {
                var herramienta = await _herramientaRepository.ObtenerPorIdAsync(ph.HerramientaId);
                
                if (herramienta != null && herramienta.EsRetornable)
                {
                    herramienta.AumentarStock(ph.Cantidad);
                    await _herramientaRepository.ActualizarAsync(herramienta);
                }
            }

            // Persistir los cambios del préstamo
            await _prestamoRepository.ActualizarAsync(prestamo);

            // Construir y retornar el DTO con los datos actualizados
            var herramientas = await _prestamoHerramientaRepository.ObtenerPorPrestamoIdAsync(prestamo.Id);
            
            Dictionary<int, Domain.Entities.Herramienta>? herramientasMap = null;
            if (herramientas.Count > 0)
            {
                var herramientaIds = herramientas.Select(h => h.HerramientaId).ToList();
                var herramientasEntities = await _herramientaRepository.ObtenerPorIdsAsync(herramientaIds);
                herramientasMap = herramientasEntities.ToDictionary(h => h.Id, h => h);
            }

            return PrestamoDTO.FromDomain(prestamo, herramientas.ToList(), herramientasMap);
        }

        private void ValidarRequest(RegistrarDevolucionRequest request)
        {
            if (request == null)
                throw new DatoRequeridoException("request");

            if (request.PrestamoId <= 0)
                throw new DatoRequeridoException("ID del préstamo");

            if (request.FechaDevolucion > DateTime.Now)
                throw new FechaInvalidaException("La fecha de devolución no puede ser una fecha futura");
        }
    }

    /// <summary>
    /// Request para registrar la devolución de un préstamo.
    /// </summary>
    public class RegistrarDevolucionRequest
    {
        /// <summary>
        /// ID del préstamo a devolver.
        /// </summary>
        public int PrestamoId { get; set; }

        /// <summary>
        /// Fecha en la que se realiza la devolución.
        /// </summary>
        public DateTime FechaDevolucion { get; set; }

        /// <summary>
        /// Indica si las herramientas fueron devueltas con defectos.
        /// </summary>
        public bool TieneDefectos { get; set; }

        /// <summary>
        /// Observaciones adicionales sobre la devolución.
        /// </summary>
        public string? Observaciones { get; set; }
    }
}
