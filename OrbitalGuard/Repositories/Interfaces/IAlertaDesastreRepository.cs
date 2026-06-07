using OrbitalGuard.Domain.Enums;
using OrbitalGuard.Models;

namespace OrbitalGuard.Repositories.Interfaces
{
    public interface IAlertaDesastreRepository : IRepository<AlertaDesastre>
    {
        Task<IEnumerable<AlertaDesastre>> GetByNivelAsync(NivelAlerta nivel);
        Task<IEnumerable<AlertaDesastre>> GetByTipoAsync(TipoDesastre tipo);
    }
}
