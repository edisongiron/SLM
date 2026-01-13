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

        // Constructor vacío
        public Herramienta() { }

        public Herramienta(string nombre, string? descripcion, bool esRetornable)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            EsRetornable = esRetornable;
        }

        // Lógica de negocio
        public bool RequiereDevolucion() => EsRetornable;
    }
}
