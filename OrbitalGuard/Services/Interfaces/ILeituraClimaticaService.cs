using OrbitalGuard.Models;

namespace OrbitalGuard.Services.Interfaces
{
    public interface ILeituraClimaticaService
    {
        Task<IEnumerable<LeituraClimatica>> ObterTodasAsync();
        Task<LeituraClimatica?> ObterPorIdAsync(int id);
        Task<IEnumerable<LeituraClimatica>> ObterPorSateliteAsync(int sateliteId);
        Task<IEnumerable<LeituraClimatica>> ObterPorRegiaoAsync(int regiaoId);
        Task CadastrarAsync(LeituraClimatica leitura);
        Task AtualizarAsync(LeituraClimatica leitura);
        Task RemoverAsync(int id);
    }
}
