using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServindAp.Domain.Enums
{
    public enum TipoEstadoPrestamo
    {
        Pendiente = 1,
        Activo = 2,
        Devuelto = 3,
        DevueltoParcial = 4,
        Vencido = 5,
        Cancelado = 6
    }
}
