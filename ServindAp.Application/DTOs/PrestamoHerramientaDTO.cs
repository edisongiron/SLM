namespace ServindAp.Application.DTOs
{
    /// <summary>
    /// DTO para representar la relación entre un Préstamo y una Herramienta.
    /// Contiene la información de qué herramienta se prestó y en qué cantidad.
    /// </summary>
    public class PrestamoHerramientaDTO
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int HerramientaId { get; set; }
        public int Cantidad { get; set; }

        /// <summary>
        /// DTO anidado de la herramienta para facilitar el acceso a sus datos.
        /// </summary>
        public HerramientaDTO? Herramienta { get; set; }

        /// <summary>
        /// Crea una instancia de PrestamoHerramientaDTO a partir de una entidad de dominio.
        /// </summary>
        public static PrestamoHerramientaDTO FromDomain(Domain.Entities.PrestamoHerramienta entity, Domain.Entities.Herramienta? herramienta = null)
        {
            return new PrestamoHerramientaDTO
            {
                Id = entity.Id,
                PrestamoId = entity.PrestamoId,
                HerramientaId = entity.HerramientaId,
                Cantidad = entity.Cantidad,
                Herramienta = herramienta != null ? HerramientaDTO.FromDomain(herramienta) : null
            };
        }
    }
}
