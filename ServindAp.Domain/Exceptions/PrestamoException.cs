using ServindAp.Domain.Entities;
using ServindAp.Domain.Enums;
using ServindAp.Domain.Exceptions;
using System;

namespace ServindAp.Domain.Exceptions
{
    public class PrestamoYaDevueltoException : DomainException
    {
        public PrestamoYaDevueltoException()
            : base("El préstamo ya ha sido devuelto") { }
    }

    public class FechaInvalidaException : DomainException
    {
        public FechaInvalidaException(string mensaje) : base(mensaje) { }
    }
}
