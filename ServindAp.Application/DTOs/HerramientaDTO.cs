namespace ServindAp.Application.DTOs
{
    /// <summary>
    /// DTO para representar una Herramienta en la capa de aplicación.
    /// Utilizado para transferir datos de herramientas entre capas.
    /// </summary>
    public class HerramientaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsRetornable { get; set; }

        /// <summary>
        /// Crea una instancia de HerramientaDTO a partir de una entidad de dominio.
        /// </summary>
        public static HerramientaDTO FromDomain(Domain.Entities.Herramienta herramienta)
        {
            return new HerramientaDTO
            {
                Id = herramienta.Id,
                Nombre = herramienta.Nombre,
                Descripcion = herramienta.Descripcion,
                EsRetornable = herramienta.EsRetornable
            };
        }

        /// <summary>
        /// Convierte el DTO a una entidad de dominio.
        /// </summary>
        public Domain.Entities.Herramienta ToDomain()
        {
            return new Domain.Entities.Herramienta(
                Nombre,
                Descripcion,
                EsRetornable
            );
        }
    }
}
