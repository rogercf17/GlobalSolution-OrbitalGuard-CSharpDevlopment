using OrbitalGuard.Models;

namespace OrbitalGuard.Repositories.Interfaces
{
    public interface IRegiaoMonitoradaRepository : IRepository<RegiaoMonitorada>
    {
        Task<IEnumerable<RegiaoMonitorada>> GetByPaisAsync(string pais);
    }
}
