namespace ServindAp.Application.DTOs
{
    public class PrestamoDTO
    {
        public int Id { get; set; }
        public string Responsable { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public string? Observaciones { get; set; }
        public string Estado { get; set; } = string.Empty;

        /// <summary>
        /// Lista de herramientas prestadas.
        /// </summary>
        public List<PrestamoHerramientaDTO> Herramientas { get; set; } = new();

        /// <summary>
        /// Crea una instancia de PrestamoDTO a partir de una entidad de dominio.
        /// </summary>
        public static PrestamoDTO FromDomain(Domain.Entities.Prestamo prestamo, List<Domain.Entities.PrestamoHerramienta>? herramientas = null, Dictionary<int, Domain.Entities.Herramienta>? herramientasMap = null)
        {
            var dto = new PrestamoDTO
            {
                Id = prestamo.Id,
                Responsable = prestamo.Responsable,
                FechaEntrega = prestamo.FechaEntrega,
                FechaDevolucion = prestamo.FechaDevolucion,
                Observaciones = prestamo.Observaciones,
                Estado = prestamo.Estado.ToString()
            };

            if (herramientas != null)
            {
                dto.Herramientas = herramientas
                    .Select(ph => PrestamoHerramientaDTO.FromDomain(
                        ph,
                        herramientasMap?.ContainsKey(ph.HerramientaId) == true ? herramientasMap[ph.HerramientaId] : null
                    ))
                    .ToList();
            }

            return dto;
        }

        /// <summary>
        /// Convierte el DTO a una entidad de dominio.
        /// </summary>
        public Domain.Entities.Prestamo ToDomain()
        {
            return new Domain.Entities.Prestamo(
                Responsable,
                FechaEntrega,
                Observaciones
            );
        }
    }
}
