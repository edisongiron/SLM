
namespace ServindAp.Domain.Exceptions
{
    public class FechaInvalidaException : DomainException
    {
        public FechaInvalidaException(string mensaje) : base(mensaje) { }
    }

    public class ResponsableRequeridoException : DomainException
    {
        public ResponsableRequeridoException()
            : base("El responsable es obligatorio") { }
    }

    public class DatoRequeridoException : DomainException
    {
        public DatoRequeridoException(string nombreCampo)
            : base($"El campo '{nombreCampo}' es obligatorio") { }
    }
}
