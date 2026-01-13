namespace ServindAp.Domain.Entities
{
    public class PrestamoHerramienta
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int HerramientaId { get; set; }
        public int Cantidad { get; set; }

        // Navegación
        public Prestamo? Prestamo { get; set; }
        public Herramienta? Herramienta { get; set; }

        // Constructor vacío
        public PrestamoHerramienta() { }

        public PrestamoHerramienta(int prestamoId, int herramientaId, int cantidad)
        {
            PrestamoId = prestamoId;
            HerramientaId = herramientaId;
            Cantidad = cantidad;
        }
    }
}
