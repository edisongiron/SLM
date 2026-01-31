using Microsoft.EntityFrameworkCore;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Entities;
using ServindAp.Infrastructure.Data;

namespace ServindAp.Infrastructure.Persistence
{
    public class PrestamoHerramientaRepository : IPrestamoHerramientaRepository
    {
        private readonly ServindApDbContext _context;

        public PrestamoHerramientaRepository(ServindApDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<PrestamoHerramienta>> ObtenerPorPrestamoIdAsync(int prestamoId)
        {
            return await _context.PrestamosHerramientas
                .Include(ph => ph.Herramienta)
                .Where(ph => ph.PrestamoId == prestamoId)
                .ToListAsync();
        }

        public async Task<PrestamoHerramienta?> ObtenerPorIdAsync(int id)
        {
            return await _context.PrestamosHerramientas
                .Include(ph => ph.Herramienta)
                .FirstOrDefaultAsync(ph => ph.Id == id);
        }

        public async Task<int> CrearAsync(PrestamoHerramienta prestamoHerramienta)
        {
            _context.PrestamosHerramientas.Add(prestamoHerramienta);
            await _context.SaveChangesAsync();
            return prestamoHerramienta.Id;
        }

        public async Task ActualizarAsync(PrestamoHerramienta prestamoHerramienta)
        {
            // Marcar explícitamente la propiedad TieneDefectosTemp como modificada
            // para garantizar que Entity Framework la incluya en el UPDATE
            // Esto es crucial para que el trigger pueda detectar el cambio
            _context.Entry(prestamoHerramienta).Property(e => e.TieneDefectosTemp).IsModified = true;
            
            _context.PrestamosHerramientas.Update(prestamoHerramienta);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var item = await _context.PrestamosHerramientas.FindAsync(id);
            if (item != null)
            {
                _context.PrestamosHerramientas.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarPorPrestamoIdAsync(int prestamoId)
        {
            var items = await _context.PrestamosHerramientas
                .Where(ph => ph.PrestamoId == prestamoId)
                .ToListAsync();

            _context.PrestamosHerramientas.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
