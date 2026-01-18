using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServindAp.Domain.Entities
{
    public class Herramienta
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool EsRetornable { get; set; }
        public int Stock { get; set; } = 0;

        // Constructor vacío
        public Herramienta() { }

        public Herramienta(string nombre, string? descripcion, bool esRetornable, int stock = 0)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            EsRetornable = esRetornable;
            Stock = stock;
        }

        // Lógica de negocio
        public bool RequiereDevolucion() => EsRetornable;

        public bool TieneStockDisponible(int cantidadRequerida) => Stock >= cantidadRequerida;

        public void ReducirStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero");

            if (Stock < cantidad)
                throw new InvalidOperationException($"Stock insuficiente. Disponible: {Stock}, Requerido: {cantidad}");

            Stock -= cantidad;
        }

        public void AumentarStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero");

            Stock += cantidad;
        }
    }
}
