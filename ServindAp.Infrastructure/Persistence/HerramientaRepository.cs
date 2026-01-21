using Microsoft.EntityFrameworkCore;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Domain.Entities;
using ServindAp.Infrastructure.Data;

namespace ServindAp.Infrastructure.Persistence
{
    public class HerramientaRepository : IHerramientaRepository
    {
        private readonly ServindApDbContext _context;

        public HerramientaRepository(ServindApDbContext context)
        {
            _context = context;
        }

        public async Task<Herramienta?> ObtenerPorIdAsync(int id)
        {
            return await _context.Herramientas.FindAsync(id);
        }

        public async Task<IReadOnlyList<Herramienta>> ObtenerTodasAsync()
        {
            return await _context.Herramientas.ToListAsync();
        }

        public async Task<IReadOnlyList<Herramienta>> ObtenerPorIdsAsync(List<int> ids)
        {
            return await _context.Herramientas
                .Where(h => ids.Contains(h.Id))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Herramienta>> ObtenerConStockAsync()
        {
            return await _context.Herramientas
                .Where(h => h.Stock > 0)
                .ToListAsync();
        }

        public async Task<bool> TieneStockDisponibleAsync(int herramientaId, int cantidadRequerida)
        {
            var herramienta = await _context.Herramientas.FindAsync(herramientaId);
            return herramienta != null && herramienta.Stock >= cantidadRequerida;
        }

        public async Task<int> CrearAsync(Herramienta herramienta)
        {
            _context.Herramientas.Add(herramienta);
            await _context.SaveChangesAsync();
            return herramienta.Id;
        }

        public async Task ActualizarAsync(Herramienta herramienta)
        {
            _context.Herramientas.Update(herramienta);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var herramienta = await _context.Herramientas.FindAsync(id);
            if (herramienta != null)
            {
                _context.Herramientas.Remove(herramienta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
