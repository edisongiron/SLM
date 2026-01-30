namespace ServindAp.Domain.Entities
{
    /// <summary>
    /// Entidad de solo lectura para el historial de eventos de préstamos y devoluciones.
    /// Esta tabla es append-only y se gestiona automáticamente mediante triggers de SQLite.
    /// NO debe ser modificada desde el código de dominio.
    /// </summary>
    public class HistorialPrestamoHerramienta
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int HerramientaId { get; set; }

/// <summary>
        /// Cantidad involucrada en el evento.
        /// - Para PRESTAMO: cantidad prestada
        /// - Para DEVOLUCION: cantidad devuelta (OLD.cantidad - NEW.cantidad)
        /// - Para DEVOLUCION_CON_DEFECTOS: cantidad devuelta con defectos
        /// - Para CANCELACION: cantidad cancelada
        /// </summary>
        public int Cantidad { get; set; }

/// <summary>
        /// Tipo de evento: PRESTAMO, DEVOLUCION, DEVOLUCION_CON_DEFECTOS, CANCELACION
        /// </summary>
        public string TipoEvento { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora del evento (registrado automáticamente por el trigger)
        /// </summary>
        public DateTime FechaEvento { get; set; }

        /// <summary>
        /// Observaciones opcionales sobre el evento
        /// </summary>
        public string? Observaciones { get; set; }

        // Navegación (opcional)
        public Prestamo? Prestamo { get; set; }
        public Herramienta? Herramienta { get; set; }
    }
}