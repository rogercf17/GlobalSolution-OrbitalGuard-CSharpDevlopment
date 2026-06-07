using Microsoft.EntityFrameworkCore;
using OrbitalGuard.Data;
using OrbitalGuard.Models;
using OrbitalGuard.Repositories.Interfaces;

namespace OrbitalGuard.Repositories
{
    public class SateliteRepository : ISateliteRepository
    {
        private readonly AppDbContext _context;

        public SateliteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Satelite>> GetAllAsync()
        {
            return await _context.satelites
                .Include(S => S.leituras)
                .ToListAsync();
        }

        public async Task<Satelite?> GetByIdAsync(int id)
        {
            return await _context.satelites
                .Include(s => s.leituras)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Satelite>> GetAtivosAsync()
        {
            return await _context.satelites
                .Where(s => s.Ativo)
                .Include(s => s.leituras)
                .ToListAsync();
        }

        public async Task AddAsync(Satelite entity)
        {
            await _context.satelites.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Satelite entity)
        {
            _context.satelites.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var satelite = await GetByIdAsync(id);
            if (satelite != null)
            {
                _context.satelites.Remove(satelite);
                await _context.SaveChangesAsync();
            }
        }
    }
}
