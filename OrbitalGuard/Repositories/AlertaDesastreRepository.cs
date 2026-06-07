using Microsoft.EntityFrameworkCore;
using OrbitalGuard.Data;
using OrbitalGuard.Domain.Enums;
using OrbitalGuard.Models;
using OrbitalGuard.Repositories.Interfaces;

namespace OrbitalGuard.Repositories
{
    public class AlertaDesastreRepository : IAlertaDesastreRepository
    {
        private readonly AppDbContext _context;

        public AlertaDesastreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlertaDesastre>> GetAllAsync()
        {
            return await _context.alertaDesastres
                .Include(a => a.LeituraClimatica)
                .ToListAsync();
        }

        public async Task<AlertaDesastre?> GetByIdAsync(int id)
        {
            return await _context.alertaDesastres
                .Include(a => a.LeituraClimatica)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<AlertaDesastre>> GetByNivelAsync(NivelAlerta nivelAlerta)
        {
            return await _context.alertaDesastres
                .Where(a => a.NivelAlerta == nivelAlerta)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlertaDesastre>> GetByTipoAsync(TipoDesastre tipoDesastre)
        {
            return await _context.alertaDesastres
                .Where(a => a.TipoDesastre == tipoDesastre)
                .ToListAsync();
        }

        public async Task AddAsync(AlertaDesastre entity)
        {
            await _context.alertaDesastres.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AlertaDesastre entity)
        {
            _context.alertaDesastres.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var alerta = await GetByIdAsync(id);
            if (alerta != null)
            {
                _context.alertaDesastres.Remove(alerta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
