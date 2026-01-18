namespace ServindAp.Application.DTOs
{
    public class HerramientaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsRetornable { get; set; }
        public int Stock { get; set; }

        // Propiedades calculadas para la UI
        public string EstadoStock
        {
            get
            {
                if (Stock == 0) return "Sin stock";
                if (Stock <= 5) return "Stock bajo";
                return "Disponible";
            }
        }

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
                EsRetornable = herramienta.EsRetornable,
                Stock = herramienta.Stock
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
                EsRetornable,
                Stock
            );
        }
    }
}
