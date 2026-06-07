using OrbitalGuard.Models;

namespace OrbitalGuard.Services.Interfaces
{
    public interface ISateliteService
    {
        Task<IEnumerable<Satelite>> ObterTodosAsync();
        Task<Satelite?> ObterPorIdAsync(int id);
        Task<IEnumerable<Satelite>> ObterAtivosAsync();
        Task CadastrarAsync(Satelite satelite);
        Task AtualizarAsync(Satelite satelite);
        Task RemoverAsync(int id);
    }
}
