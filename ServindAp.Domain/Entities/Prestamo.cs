using ServindAp.Domain.Exceptions;
using ServindAp.Domain.Enums;

namespace ServindAp.Domain.Entities
{
    public class Prestamo
    {
        public int Id { get; set; }
        public string Responsable { get; set; } = null!;
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
            EstadoPrestamoId = (int)TipoEstadoPrestamo.Activo; // Activo por defecto
            Herramientas = new List<PrestamoHerramienta>();
        }

        // ===== LÓGICA DE NEGOCIO =====

        public bool EstaActivo() => EstadoPrestamoId == (int)TipoEstadoPrestamo.Activo;

        public bool EstaDevuelto() =>
            EstadoPrestamoId == (int)TipoEstadoPrestamo.Devuelto ||
            EstadoPrestamoId == (int)TipoEstadoPrestamo.DevueltoConDefectos;

        public void RegistrarDevolucion(DateTime fechaDevolucion, bool tieneDefectos = false)
        {
            if (EstaDevuelto())
                throw new PrestamoYaDevueltoException();

            if (fechaDevolucion < FechaEntrega)
                throw new FechaInvalidaException("La fecha de devolución no puede ser anterior a la fecha de entrega");

            FechaDevolucion = fechaDevolucion;
            EstadoPrestamoId = tieneDefectos
                ? (int)TipoEstadoPrestamo.DevueltoConDefectos
                : (int)TipoEstadoPrestamo.Devuelto;
        }

        public void AgregarHerramienta(int herramientaId, int cantidad)
        {
            if (cantidad <= 0)
                throw new CantidadInvalidaException("La cantidad debe ser mayor a cero");

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