using OrbitalGuard.Models;

namespace OrbitalGuard.Repositories.Interfaces
{
    public interface ILeituraClimaticaRepository : IRepository<LeituraClimatica>
    {
        Task<IEnumerable<LeituraClimatica>> GetBySateliteIdAsync(int sateliteId);
        Task<IEnumerable<LeituraClimatica>> GetByRegiaoMonitoradaIdAsync(int regiaoId);
    }
}
