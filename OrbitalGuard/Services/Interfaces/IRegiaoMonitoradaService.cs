using OrbitalGuard.Domain.Enums;
using OrbitalGuard.Models;

namespace OrbitalGuard.Services.Interfaces
{
    public interface IRegiaoMonitoradaService
    {
        Task<IEnumerable<RegiaoMonitorada>> ObterTodosAsync();
        Task<RegiaoMonitorada?> ObterPorIdAsync(int id);
        Task<IEnumerable<RegiaoMonitorada>> ObterPorPaisAsync(string pais);
        Task CadastrarAsync(RegiaoMonitorada regiao);
        Task AtualizarAsync(RegiaoMonitorada regiao);
        Task RemoverAsync(int id);
    }
}
