using OrbitalGuard.Models;
using OrbitalGuard.Repositories.Interfaces;
using OrbitalGuard.Services.Interfaces;

namespace OrbitalGuard.Services
{
    public class SateliteService : ISateliteService
    {
        private readonly ISateliteRepository _repository;

        public SateliteService(ISateliteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Satelite>> ObterTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Satelite?> ObterPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Satelite>> ObterAtivosAsync()
        {
            return await _repository.GetAtivosAsync();
        }

        public async Task CadastrarAsync(Satelite satelite)
        {
            if (string.IsNullOrWhiteSpace(satelite.Nome))
                throw new ArgumentException("Nome do satélite é obrigatório.");

            if (satelite.AltitudeKm <= 0)
                throw new ArgumentException("Altitude deve ser maior que zero.");

            satelite.DataLancamento = DateTime.UtcNow;

            await _repository.AddAsync(satelite);
        }

        public async Task AtualizarAsync(Satelite satelite)
        {
            var existente = await _repository.GetByIdAsync(satelite.Id)
                ?? throw new KeyNotFoundException($"Satélite {satelite.Id} não encontrado.");

            await _repository.UpdateAsync(satelite);
        }

        public async Task RemoverAsync(int id)
        {
            var existente = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Satélite {id} não encontrado.");

            await _repository.DeleteAsync(id);
        }
    }
}
