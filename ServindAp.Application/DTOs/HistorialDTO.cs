namespace ServindAp.Application.DTOs
{
    public class HistorialDTO
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public string Responsable { get; set; } = string.Empty;
        public string Herramienta { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public string TipoEvento { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene una descripción amigable del tipo de evento para mostrar en la UI.
        /// </summary>
        public string TipoEventoDescripcion => TipoEvento switch
        {
            "PRESTAMO" => "Préstamo",
            "DEVOLUCION" => "Devolución",
            "DEVOLUCION_CON_DEFECTOS" => "Devolución con Defectos",
            "CANCELACION" => "Cancelación",
            _ => TipoEvento
        };
        public DateTime FechaEvento { get; set; }
        public string? Observaciones { get; set; }

        /// <summary>
        /// Crea una instancia de HistorialDTO a partir de una entidad de dominio.
        /// </summary>
        public static HistorialDTO FromDomain(Domain.Entities.HistorialPrestamoHerramienta historial, Domain.Entities.Prestamo? prestamo = null, Domain.Entities.Herramienta? herramienta = null)
        {
            return new HistorialDTO
            {
                Id = historial.Id,
                PrestamoId = historial.PrestamoId,
                Responsable = prestamo?.Responsable ?? string.Empty,
                Herramienta = herramienta?.Nombre ?? string.Empty,
                Cantidad = historial.Cantidad,
                TipoEvento = historial.TipoEvento,
                FechaEvento = historial.FechaEvento,
                Observaciones = historial.Observaciones
            };
        }
    }
}