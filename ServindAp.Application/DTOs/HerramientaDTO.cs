namespace ServindAp.Application.DTOs
{
    public class HerramientaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int Stock { get; set; }

        public string EstadoStock
        {
            get
            {
                if (Stock == 0) return "Sin stock";
                if (Stock <= 5) return "Stock bajo";
                return "Disponible";
            }
        }

        public static HerramientaDTO FromDomain(Domain.Entities.Herramienta herramienta)
        {
            return new HerramientaDTO
            {
                Id = herramienta.Id,
                Nombre = herramienta.Nombre,
                Descripcion = herramienta.Descripcion,
                Stock = herramienta.Stock
            };
        }

        public Domain.Entities.Herramienta ToDomain()
        {
            return new Domain.Entities.Herramienta(
                Nombre,
                Descripcion,
                Stock
            );
        }
    }
}
