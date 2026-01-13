namespace ServindAp.Domain.Entities
{
    public class Prestamo
    {
        public int Id { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public string? Observaciones { get; set; }
        public int EstadoPrestamoId { get; set; }

        // Navegación
        public EstadoPrestamo? EstadoPrestamo { get; set; }
        public List<PrestamoHerramienta> Herramientas { get; set; }

        // Constructor vacío
        public Prestamo()
        {
            Herramientas = new List<PrestamoHerramienta>();
        }

        // Constructor con datos
        public Prestamo(string responsable, DateTime fechaEntrega, string? observaciones)
        {
            Responsable = responsable;
            FechaEntrega = fechaEntrega;
            Observaciones = observaciones;
            EstadoPrestamoId = 2; // Activo por defecto
            Herramientas = new List<PrestamoHerramienta>();
        }

        // ===== LÓGICA DE NEGOCIO =====

        public bool EstaActivo() => FechaDevolucion == null;

        public bool EstaDevuelto() => FechaDevolucion != null;

        public void RegistrarDevolucion(DateTime fechaDevolucion)
        {
            if (EstaDevuelto())
                throw new InvalidOperationException("El préstamo ya fue devuelto");

            if (fechaDevolucion < FechaEntrega)
                throw new ArgumentException("La fecha de devolución no puede ser anterior a la fecha de entrega");

            FechaDevolucion = fechaDevolucion;
            EstadoPrestamoId = 3; // Devuelto
        }

        public void AgregarHerramienta(int herramientaId, int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero");

            Herramientas.Add(new PrestamoHerramienta
            {
                PrestamoId = this.Id,
                HerramientaId = herramientaId,
                Cantidad = cantidad
            });
        }

        public int TotalHerramientas() => Herramientas.Count;
    }
}