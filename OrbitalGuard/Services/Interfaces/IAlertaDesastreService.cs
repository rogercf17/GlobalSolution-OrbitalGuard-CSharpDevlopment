using OrbitalGuard.Domain.Enums;
using OrbitalGuard.Models;

namespace OrbitalGuard.Services.Interfaces
{
    public interface IAlertaDesastreService
    {
        Task<IEnumerable<AlertaDesastre>> ObterTodosAsync();
        Task<AlertaDesastre?> ObterPorIdAsync(int id);
        Task<IEnumerable<AlertaDesastre>> ObterPorNivelAsync(NivelAlerta nivel);
        Task<IEnumerable<AlertaDesastre>> ObterPorTipoAsync(TipoDesastre tipo);
        Task CadastrarAsync(AlertaDesastre alerta);
        Task ResolverAlertaAsync(int id);
        Task RemoverAsync(int id);
    }
}
