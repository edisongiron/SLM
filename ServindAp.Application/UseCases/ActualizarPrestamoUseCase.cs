using ServindAp.Application.DTOs;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Entities;
using ServindAp.Domain.Exceptions;

namespace ServindAp.Application.UseCases
{
    /// <summary>
    /// Caso de uso para actualizar un préstamo existente.
    /// Permite modificar el responsable, fecha, observaciones y las herramientas prestadas.
    /// </summary>
    public class ActualizarPrestamoUseCase
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IHerramientaRepository _herramientaRepository;
        private readonly IPrestamoHerramientaRepository _prestamoHerramientaRepository;

        public ActualizarPrestamoUseCase(
            IPrestamoRepository prestamoRepository,
            IHerramientaRepository herramientaRepository,
            IPrestamoHerramientaRepository prestamoHerramientaRepository)
        {
            _prestamoRepository = prestamoRepository ?? throw new ArgumentNullException(nameof(prestamoRepository));
            _herramientaRepository = herramientaRepository ?? throw new ArgumentNullException(nameof(herramientaRepository));
            _prestamoHerramientaRepository = prestamoHerramientaRepository ?? throw new ArgumentNullException(nameof(prestamoHerramientaRepository));
        }

        /// <summary>
        /// Ejecuta la actualización del préstamo.
        /// Proceso:
        /// 1. Valida el request y verifica que el préstamo existe
        /// 2. Devuelve el stock de las herramientas anteriores
        /// 3. Valida que las nuevas herramientas existan y tengan stock
        /// 4. Actualiza los datos del préstamo
        /// 5. Elimina las herramientas antiguas
        /// 6. Agrega las nuevas herramientas y reduce el stock
        /// </summary>
        public async Task<PrestamoDTO> ExecuteAsync(int prestamoId, ActualizarPrestamoRequest request)
        {
            // 1. VALIDACIONES INICIALES
            ValidarRequest(request);

            // Verificar que el préstamo existe
            var prestamo = await _prestamoRepository.ObtenerPorIdAsync(prestamoId);
            if (prestamo == null)
                throw new PrestamoNoEncontradoException(prestamoId);

            // No permitir editar préstamos devueltos
            if (prestamo.EstaDevuelto())
                throw new InvalidOperationException("No se puede actualizar un préstamo ya devuelto");

            // 2. DEVOLVER STOCK DE HERRAMIENTAS ANTERIORES
            var herramientasAnteriores = await _prestamoHerramientaRepository.ObtenerPorPrestamoIdAsync(prestamoId);
            
            foreach (var herramientaAnterior in herramientasAnteriores)
            {
                var herramienta = await _herramientaRepository.ObtenerPorIdAsync(herramientaAnterior.HerramientaId);
                if (herramienta != null)
                {
                    // Devolver el stock que estaba prestado
                    herramienta.AumentarStock(herramientaAnterior.Cantidad);
                    await _herramientaRepository.ActualizarAsync(herramienta);
                }
            }

            // 3. VALIDAR NUEVAS HERRAMIENTAS
            var herramientaIds = request.Herramientas.Select(h => h.HerramientaId).ToList();
            var herramientas = await _herramientaRepository.ObtenerPorIdsAsync(herramientaIds);

            // Verificar que todas las herramientas existen
            if (herramientas.Count != herramientaIds.Count)
            {
                var idsEncontrados = herramientas.Select(h => h.Id).ToList();
                var idFaltante = herramientaIds.First(id => !idsEncontrados.Contains(id));
                throw new HerramientaNoEncontradaException(idFaltante);
            }

            // Validar stock disponible para las nuevas herramientas
            foreach (var item in request.Herramientas)
            {
                var herramienta = herramientas.First(h => h.Id == item.HerramientaId);

                if (!herramienta.TieneStockDisponible(item.Cantidad))
                {
                    throw new StockInsuficienteException(
                        herramienta.Nombre,
                        item.Cantidad,
                        herramienta.Stock
                    );
                }
            }

            // 4. ACTUALIZAR DATOS DEL PRÉSTAMO
            prestamo.Responsable = request.Responsable;
            prestamo.FechaEntrega = request.FechaEntrega;
            prestamo.Observaciones = request.Observaciones;

            await _prestamoRepository.ActualizarAsync(prestamo);

            // 5. ELIMINAR HERRAMIENTAS ANTIGUAS
            await _prestamoHerramientaRepository.EliminarPorPrestamoIdAsync(prestamo.Id);

            // 6. AGREGAR NUEVAS HERRAMIENTAS Y REDUCIR STOCK
            foreach (var item in request.Herramientas)
            {
                // Crear la relación préstamo-herramienta
                var prestamoHerramienta = new PrestamoHerramienta(
                    prestamo.Id,
                    item.HerramientaId,
                    item.Cantidad
                );

                await _prestamoHerramientaRepository.CrearAsync(prestamoHerramienta);

                // Reducir el stock
                var herramienta = herramientas.First(h => h.Id == item.HerramientaId);
                herramienta.ReducirStock(item.Cantidad);
                await _herramientaRepository.ActualizarAsync(herramienta);
            }

            // 7. CONSTRUIR DTO DE RESPUESTA
            var herramientasActualizadas = await _prestamoHerramientaRepository.ObtenerPorPrestamoIdAsync(prestamo.Id);
            var herramientasMap = herramientas.ToDictionary(h => h.Id, h => h);

            return PrestamoDTO.FromDomain(prestamo, herramientasActualizadas.ToList(), herramientasMap);
        }

        private void ValidarRequest(ActualizarPrestamoRequest request)
        {
            if (request == null)
                throw new DatoRequeridoException("request");

            if (string.IsNullOrWhiteSpace(request.Responsable))
                throw new ResponsableRequeridoException();

            if (request.Responsable.Length < 3)
                throw new DatoRequeridoException("El nombre del responsable debe tener al menos 3 caracteres");

            if (request.Responsable.Length > 100)
                throw new DatoRequeridoException("El nombre del responsable no puede exceder 100 caracteres");

            if (request.FechaEntrega > DateTime.Now)
                throw new FechaInvalidaException("La fecha de entrega no puede ser una fecha futura");

            if (request.Herramientas == null || !request.Herramientas.Any())
                throw new PrestamoSinHerramientasException();

            // Validar cada herramienta
            foreach (var herramienta in request.Herramientas)
            {
                if (herramienta.HerramientaId <= 0)
                    throw new DatoRequeridoException("ID de herramienta");

                if (herramienta.Cantidad <= 0)
                    throw new CantidadInvalidaException("La cantidad debe ser mayor a cero");
            }

            // Validar que no haya herramientas duplicadas
            var herramientasDuplicadas = request.Herramientas
                .GroupBy(h => h.HerramientaId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (herramientasDuplicadas.Any())
            {
                throw new InvalidOperationException(
                    $"No se pueden agregar herramientas duplicadas. IDs duplicados: {string.Join(", ", herramientasDuplicadas)}"
                );
            }
        }
    }

    /// <summary>
    /// Request para actualizar un préstamo.
    /// </summary>
    public class ActualizarPrestamoRequest
    {
        public string Responsable { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; }
        public string? Observaciones { get; set; }
        public List<HerramientaPrestamoRequest> Herramientas { get; set; } = new();
    }
}
