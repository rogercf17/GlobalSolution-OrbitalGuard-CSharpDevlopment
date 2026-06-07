using OrbitalGuard.Models;

namespace OrbitalGuard.Repositories.Interfaces
{
    public interface ISateliteRepository : IRepository<Satelite>
    {
        Task<IEnumerable<Satelite>> GetAtivosAsync();
    }
}
