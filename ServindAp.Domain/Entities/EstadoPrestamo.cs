namespace ServindAp.Domain.Entities
{
    public class EstadoPrestamo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public EstadoPrestamo() { }

        public EstadoPrestamo(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
