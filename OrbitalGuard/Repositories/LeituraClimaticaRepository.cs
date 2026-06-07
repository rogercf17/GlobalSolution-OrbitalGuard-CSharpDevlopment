using Microsoft.EntityFrameworkCore;
using OrbitalGuard.Data;
using OrbitalGuard.Models;
using OrbitalGuard.Repositories.Interfaces;

namespace OrbitalGuard.Repositories
{
    public class LeituraClimaticaRepository : ILeituraClimaticaRepository
    {
        private readonly AppDbContext _context;

        public LeituraClimaticaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeituraClimatica>> GetAllAsync()
        {
            return await _context.leituraClimaticas
                .Include(l => l.Satelite)
                .Include(l => l.RegiaoMonitorada)
                .ToListAsync();
        }

        public async Task<LeituraClimatica?> GetByIdAsync(int id)
        {
            return await _context.leituraClimaticas
                .Include(l => l.Satelite)
                .Include(l => l.RegiaoMonitorada)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<LeituraClimatica>> GetBySateliteIdAsync(int sateliteId)
        {
            return await _context.leituraClimaticas
                .Where(l => l.SateliteId == sateliteId)
                .Include(l => l.RegiaoMonitorada)
                .ToListAsync();
        }

        public async Task<IEnumerable<LeituraClimatica>> GetByRegiaoMonitoradaIdAsync(int regiaoId)
        {
            return await _context.leituraClimaticas
                .Where(l => l.RegiaoMonitoradaId == regiaoId)
                .Include(l => l.Satelite)
                .ToListAsync();
        }

        public async Task AddAsync(LeituraClimatica entity)
        {
            await _context.leituraClimaticas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LeituraClimatica entity)
        {
            _context.leituraClimaticas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var leitura = await GetByIdAsync(id);
            if (leitura != null)
            {
                _context.leituraClimaticas.Remove(leitura);
                await _context.SaveChangesAsync();
            }
        }
    }
}
