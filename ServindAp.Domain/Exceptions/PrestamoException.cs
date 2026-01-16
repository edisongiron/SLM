
namespace ServindAp.Domain.Exceptions
{
    public class PrestamoYaDevueltoException : DomainException
    {
        public PrestamoYaDevueltoException()
            : base("El préstamo ya ha sido devuelto") { }
    }

    public class PrestamoNoEncontradoException : DomainException
    {
        public PrestamoNoEncontradoException(int prestamoId)
            : base($"No se encontró el préstamo con ID {prestamoId}") { }
    }

    public class PrestamoSinHerramientasException : DomainException
    {
        public PrestamoSinHerramientasException()
            : base("El préstamo debe tener al menos una herramienta") { }
    }

    public class CantidadInvalidaException : DomainException
    {
        public CantidadInvalidaException()
            : base("La cantidad debe ser mayor a cero") { }

        public CantidadInvalidaException(string mensaje) : base(mensaje) { }
    }
}
