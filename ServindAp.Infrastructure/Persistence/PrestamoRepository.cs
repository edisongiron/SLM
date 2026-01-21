using Microsoft.EntityFrameworkCore;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Entities;
using ServindAp.Domain.Enums;
using ServindAp.Infrastructure.Data;

namespace ServindAp.Infrastructure.Persistence
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly ServindApDbContext _context;

        public PrestamoRepository(ServindApDbContext context)
        {
            _context = context;
        }

        public async Task<Prestamo?> ObtenerPorIdAsync(int id)
        {
            return await _context.Prestamos.FindAsync(id);
        }

        public async Task<IReadOnlyList<Prestamo>> ObtenerActivosAsync()
        {
            return await _context.Prestamos
                .Where(p => p.Estado == TipoEstadoPrestamo.Activo)
                .ToListAsync();
        }

        public async Task<int> CrearAsync(Prestamo prestamo)
        {
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            return prestamo.Id;
        }

        public async Task ActualizarAsync(Prestamo prestamo)
        {
            _context.Prestamos.Update(prestamo);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
