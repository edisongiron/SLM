using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Entities;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    /// <summary>
    /// UseCase para crear un nuevo préstamo.
    /// Valida los datos de entrada y persiste el préstamo con sus herramientas asociadas.
    /// </summary>
    public class CrearPrestamoUseCase
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IHerramientaRepository _herramientaRepository;
        private readonly IPrestamoHerramientaRepository _prestamoHerramientaRepository;

        public CrearPrestamoUseCase(
            IPrestamoRepository prestamoRepository,
            IHerramientaRepository herramientaRepository,
            IPrestamoHerramientaRepository prestamoHerramientaRepository)
        {
            _prestamoRepository = prestamoRepository ?? throw new ArgumentNullException(nameof(prestamoRepository));
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
            _prestamoHerramientaRepository = prestamoHerramientaRepository ?? throw new ArgumentNullException(nameof(prestamoHerramientaRepository));
        }

        /// <summary>
        /// Ejecuta el caso de uso para crear un préstamo.
        /// </summary>
        /// <param name="request">Datos para crear el préstamo</param>
        /// <returns>DTO del préstamo creado</returns>
        public async Task<PrestamoDTO> ExecuteAsync(CrearPrestamoRequest request)
        {
            ValidarRequest(request);

            // Crear la entidad Préstamo
            var prestamo = new Prestamo(
                request.Responsable,
                request.FechaEntrega,
                request.Observaciones
            );

            // Validar que las herramientas existan
            var herramientaIds = request.Herramientas.Select(h => h.HerramientaId).ToList();
            var herramientas = await _herramientaRepository.ObtenerPorIdsAsync(herramientaIds);

            if (herramientas.Count != herramientaIds.Count)
            {
                var idsEncontrados = herramientas.Select(h => h.Id).ToList();
                var idFaltante = herramientaIds.First(id => !idsEncontrados.Contains(id));
                throw new HerramientaNoEncontradaException(idFaltante);
            }

            // Crear el préstamo
            var prestamoId = await _prestamoRepository.CrearAsync(prestamo);
            prestamo.Id = prestamoId;

            // Agregar las herramientas al préstamo
            foreach (var herramientaPrestada in request.Herramientas)
            {
                var prestamoHerramienta = new PrestamoHerramienta(
                    prestamoId,
                    herramientaPrestada.HerramientaId,
                    herramientaPrestada.Cantidad
                );

                await _prestamoHerramientaRepository.CrearAsync(prestamoHerramienta);
            }

            // Construir el DTO de respuesta con las herramientas
            var herramientasMap = herramientas.ToDictionary(h => h.Id, h => h);
            var prestamoHerramientasDTO = request.Herramientas
                .Select(h => new PrestamoHerramientaDTO
                {
                    PrestamoId = prestamoId,
                    HerramientaId = h.HerramientaId,
                    Cantidad = h.Cantidad,
                    Herramienta = herramientasMap.ContainsKey(h.HerramientaId) 
                        ? HerramientaDTO.FromDomain(herramientasMap[h.HerramientaId])
                        : null
                })
                .ToList();

            return new PrestamoDTO
            {
                Id = prestamoId,
                Responsable = prestamo.Responsable,
                FechaEntrega = prestamo.FechaEntrega,
                FechaDevolucion = prestamo.FechaDevolucion,
                Observaciones = prestamo.Observaciones,
                Estado = prestamo.Estado.ToString(),
                Herramientas = prestamoHerramientasDTO
            };
        }

        private void ValidarRequest(CrearPrestamoRequest request)
        {
            if (request == null)
                throw new DatoRequeridoException("request");

            if (string.IsNullOrWhiteSpace(request.Responsable))
                throw new ResponsableRequeridoException();

            if (request.FechaEntrega > DateTime.Now)
                throw new FechaInvalidaException("La fecha de entrega no puede ser una fecha futura");

            if (request.Herramientas == null || request.Herramientas.Count == 0)
                throw new PrestamoSinHerramientasException();

            foreach (var herramienta in request.Herramientas)
            {
                if (herramienta.HerramientaId <= 0)
                    throw new DatoRequeridoException("ID de herramienta");

                if (herramienta.Cantidad <= 0)
                    throw new CantidadInvalidaException("La cantidad debe ser mayor a cero");
            }
        }
    }

    /// <summary>
    /// Request para crear un préstamo.
    /// </summary>
    public class CrearPrestamoRequest
    {
        public string Responsable { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; }
        public string? Observaciones { get; set; }
        public List<HerramientaPrestamoRequest> Herramientas { get; set; } = new();
    }

    /// <summary>
    /// Información de una herramienta dentro de un préstamo.
    /// </summary>
    public class HerramientaPrestamoRequest
    {
        public int HerramientaId { get; set; }
        public int Cantidad { get; set; }
    }
}
