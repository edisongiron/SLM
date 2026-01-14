
namespace ServindAp.Domain.Exceptions
{
    public class HerramientaNoEncontradaException : DomainException
    {
        public HerramientaNoEncontradaException(int herramientaId)
            : base($"La herramienta con ID {herramientaId} no existe") { }
    }

    public class StockInsuficienteException : DomainException
    {
        public StockInsuficienteException(string nombreHerramienta)
            : base($"Stock insuficiente para la herramienta: {nombreHerramienta}") { }

        public StockInsuficienteException(int herramientaId, int cantidadRequerida, int cantidadDisponible)
            : base($"Stock insuficiente. Herramienta ID: {herramientaId}. Requerido: {cantidadRequerida}, Disponible: {cantidadDisponible}") { }
    }

    public class HerramientaNoRetornableException : DomainException
    {
        public HerramientaNoRetornableException(string nombreHerramienta)
            : base($"La herramienta '{nombreHerramienta}' no es retornable") { }
    }
}
