using Microsoft.EntityFrameworkCore;
using OrbitalGuard.Data;
using OrbitalGuard.Models;
using OrbitalGuard.Repositories.Interfaces;

namespace OrbitalGuard.Repositories
{
    public class RegiaoMonitoradaRepository : IRegiaoMonitoradaRepository
    {
        private readonly AppDbContext _context;

        public RegiaoMonitoradaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RegiaoMonitorada>> GetAllAsync()
        {
            return await _context.regiaoMonitoradas
                .Include(r => r.Leituras)
                .Include(r => r.Alertas)
                .ToListAsync();
        }

        public async Task<RegiaoMonitorada?> GetByIdAsync(int id)
        {
            return await _context.regiaoMonitoradas
                .Include(r => r.Leituras)
                .Include(r => r.Alertas)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<RegiaoMonitorada>> GetByPaisAsync(string pais)
        {
            return await _context.regiaoMonitoradas
                .Where(r => r.Pais == pais)
                .Include(r => r.Leituras)
                .Include(r => r.Alertas)
                .ToListAsync();
        }

        public async Task AddAsync(RegiaoMonitorada entity)
        {
            await _context.regiaoMonitoradas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RegiaoMonitorada entity)
        {
            _context.regiaoMonitoradas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var regiao = await GetByIdAsync(id);
            if (regiao != null)
            {
                _context.regiaoMonitoradas.Remove(regiao);
                await _context.SaveChangesAsync();
            }
        }
    }
}
