using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    /// <summary>
    /// Caso de uso para registrar la devolución parcial de herramientas de un préstamo.
    /// Permite devolver herramientas de forma individual o en cantidades parciales.
    /// Simplemente reduce la cantidad prestada de cada herramienta.
    /// </summary>
    public class RegistrarDevolucionParcialUseCase
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IPrestamoHerramientaRepository _prestamoHerramientaRepository;
        private readonly IHerramientaRepository _herramientaRepository;

        public RegistrarDevolucionParcialUseCase(
            IPrestamoRepository prestamoRepository,
            IPrestamoHerramientaRepository prestamoHerramientaRepository,
            IHerramientaRepository herramientaRepository)
        {
            _prestamoRepository = prestamoRepository ?? throw new ArgumentNullException(nameof(prestamoRepository));
            _prestamoHerramientaRepository = prestamoHerramientaRepository ?? throw new ArgumentNullException(nameof(prestamoHerramientaRepository));
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
        }

        /// <summary>
        /// Ejecuta el registro de devolución parcial de herramientas.
        /// </summary>
        public async Task<PrestamoDTO> ExecuteAsync(int prestamoId, RegistrarDevolucionParcialRequest request)
        {
            ValidarRequest(request);

            // Obtener el préstamo
            var prestamo = await _prestamoRepository.ObtenerPorIdAsync(prestamoId);
            if (prestamo == null)
                throw new PrestamoNoEncontradoException(prestamoId);

            // No permitir devoluciones si el préstamo ya está completamente devuelto
            if (prestamo.EstaDevuelto())
                throw new PrestamoYaDevueltoException();

            // Obtener todas las herramientas del préstamo
            var todasLasHerramientas = await _prestamoHerramientaRepository.ObtenerPorPrestamoIdAsync(prestamoId);

            // Procesar cada herramienta a devolver
            foreach (var itemDevolucion in request.HerramientasADevolver)
            {
                // Buscar la herramienta en el préstamo
                var prestamoHerramienta = todasLasHerramientas.FirstOrDefault(h => h.HerramientaId == itemDevolucion.HerramientaId);
                
                if (prestamoHerramienta == null)
                    throw new InvalidOperationException($"La herramienta con ID {itemDevolucion.HerramientaId} no pertenece a este préstamo");

                // Validar que no se devuelva más de lo prestado
                if (itemDevolucion.CantidadADevolver > prestamoHerramienta.Cantidad)
                {
                    throw new InvalidOperationException(
                        $"No se puede devolver más de lo prestado. " +
                        $"Cantidad actual prestada: {prestamoHerramienta.Cantidad}, " +
                        $"Intentando devolver: {itemDevolucion.CantidadADevolver}");
                }

                // Registrar la devolución parcial en la entidad (reduce la cantidad)
                prestamoHerramienta.RegistrarDevolucion(itemDevolucion.CantidadADevolver);

                // Actualizar en BD
                await _prestamoHerramientaRepository.ActualizarAsync(prestamoHerramienta);

                // Devolver el stock a la herramienta
                var herramienta = await _herramientaRepository.ObtenerPorIdAsync(itemDevolucion.HerramientaId);
                if (herramienta != null)
                {
                    herramienta.AumentarStock(itemDevolucion.CantidadADevolver);
                    await _herramientaRepository.ActualizarAsync(herramienta);
                }
            }

            // Verificar si TODAS las herramientas han sido devueltas completamente (Cantidad = 0)
            var herramientasActualizadas = await _prestamoHerramientaRepository.ObtenerPorPrestamoIdAsync(prestamoId);
            bool todasDevueltas = herramientasActualizadas.All(h => h.EstaCompletamenteDevuelta());

            // Si todas las herramientas fueron devueltas, marcar el préstamo como devuelto
            if (todasDevueltas && !prestamo.EstaDevuelto())
            {
                prestamo.RegistrarDevolucion(DateTime.Now, request.TieneDefectos);
                await _prestamoRepository.ActualizarAsync(prestamo);
            }

            // Construir y retornar el DTO actualizado
            var herramientaIds = herramientasActualizadas.Select(h => h.HerramientaId).ToList();
            var herramientasEntities = await _herramientaRepository.ObtenerPorIdsAsync(herramientaIds);
            var herramientasMap = herramientasEntities.ToDictionary(h => h.Id, h => h);

            return PrestamoDTO.FromDomain(prestamo, herramientasActualizadas.ToList(), herramientasMap);
        }

        private void ValidarRequest(RegistrarDevolucionParcialRequest request)
        {
            if (request == null)
                throw new DatoRequeridoException("request");

            if (request.HerramientasADevolver == null || !request.HerramientasADevolver.Any())
                throw new DatoRequeridoException("Debe especificar al menos una herramienta a devolver");

            foreach (var item in request.HerramientasADevolver)
            {
                if (item.HerramientaId <= 0)
                    throw new DatoRequeridoException("ID de herramienta");

                if (item.CantidadADevolver <= 0)
                    throw new CantidadInvalidaException("La cantidad a devolver debe ser mayor a cero");
            }
        }
    }

    /// <summary>
    /// Request para registrar una devolución parcial de herramientas.
    /// </summary>
    public class RegistrarDevolucionParcialRequest
    {
        /// <summary>
        /// Lista de herramientas a devolver con sus cantidades.
        /// </summary>
        public List<HerramientaDevolucionItem> HerramientasADevolver { get; set; } = new();

        /// <summary>
        /// Indica si alguna de las herramientas devueltas tiene defectos.
        /// </summary>
        public bool TieneDefectos { get; set; }

        /// <summary>
        /// Observaciones sobre la devolución.
        /// </summary>
        public string? Observaciones { get; set; }
    }

    /// <summary>
    /// Representa una herramienta a devolver con su cantidad.
    /// </summary>
    public class HerramientaDevolucionItem
    {
        public int HerramientaId { get; set; }
        public int CantidadADevolver { get; set; }
    }
}
