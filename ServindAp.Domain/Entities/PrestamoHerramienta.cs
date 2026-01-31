namespace ServindAp.Domain.Entities
{
    public class PrestamoHerramienta
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int HerramientaId { get; set; }
        public int Cantidad { get; set; }
        
        // Columna temporal para detectar defectos en devoluciones parciales
        // No se mapea a la base de datos permanentemente, solo se usa temporalmente
        public bool TieneDefectosTemp { get; set; }

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
        
        /// <summary>
        /// Verifica si la herramienta ha sido completamente devuelta
        /// </summary>
        public bool EstaCompletamenteDevuelta() => Cantidad == 0;
        
        /// <summary>
        /// Registra una devolución parcial reduciendo la cantidad prestada
        /// </summary>
        public void RegistrarDevolucion(int cantidadADevolver)
        {
            if (cantidadADevolver <= 0)
                throw new ArgumentException("La cantidad a devolver debe ser mayor a cero");
                
            if (cantidadADevolver > Cantidad)
                throw new InvalidOperationException($"No se puede devolver más de lo prestado. Cantidad prestada: {Cantidad}, Intentando devolver: {cantidadADevolver}");
                
            Cantidad -= cantidadADevolver;
        }
    }
}
