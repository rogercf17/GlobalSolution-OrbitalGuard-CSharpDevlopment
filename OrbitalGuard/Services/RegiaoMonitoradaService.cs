using OrbitalGuard.Domain.Enums;
using OrbitalGuard.DTOs;
using OrbitalGuard.Models;
using OrbitalGuard.Repositories.Interfaces;
using OrbitalGuard.Services.Interfaces;

namespace OrbitalGuard.Services
{
    public class RegiaoMonitoradaService : IRegiaoMonitoradaService
    {
        private readonly IRegiaoMonitoradaRepository _repository;

        public RegiaoMonitoradaService(IRegiaoMonitoradaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RegiaoMonitorada>> ObterTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<RegiaoMonitorada?> ObterPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<RegiaoMonitorada>> ObterPorPaisAsync(string pais)
        {
            return await _repository.GetByPaisAsync(pais);
        }

        public async Task CadastrarAsync(RegiaoMonitorada regiao)
        {
            if (string.IsNullOrEmpty(regiao.Nome))
                throw new ArgumentException("Nome da região é obrigatório.");
            if (string.IsNullOrEmpty(regiao.Pais))
                throw new ArgumentException("País da região é obrigatório.");
            if (regiao.AreaKm2 <= 0)
                throw new ArgumentException("Área deve ser maior que zero.");

            await _repository.AddAsync(regiao);
        }

        public async Task AtualizarAsync(RegiaoMonitorada regiao)
        {
            var existente = await _repository.GetByIdAsync(regiao.Id)
                ?? throw new KeyNotFoundException($"Satélite {regiao.Id} não encontrado.");

            await _repository.UpdateAsync(regiao);
        }

        public async Task RemoverAsync(int id)
        {
            var existente = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Região {id} não encontrada.");

            await _repository.DeleteAsync(id);
        }
    }
}
