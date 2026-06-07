using OrbitalGuard.Domain.Enums;
using OrbitalGuard.Models;
using OrbitalGuard.Repositories.Interfaces;
using OrbitalGuard.Services.Interfaces;

namespace OrbitalGuard.Services
{
    public class AlertaDesastreService : IAlertaDesastreService
    {
        private readonly IAlertaDesastreRepository _repository;

        public AlertaDesastreService(IAlertaDesastreRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AlertaDesastre>> ObterTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AlertaDesastre?> ObterPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<AlertaDesastre>> ObterPorNivelAsync(NivelAlerta nivel)
        {
            return await _repository.GetByNivelAsync(nivel);
        }

        public async Task<IEnumerable<AlertaDesastre>> ObterPorTipoAsync(TipoDesastre tipo)
        {
            return await _repository.GetByTipoAsync(tipo);
        }

        public async Task CadastrarAsync(AlertaDesastre alerta)
        {
            if (string.IsNullOrWhiteSpace(alerta.Descricao))
                throw new ArgumentException("Descrição do alerta é obrigatória.");

            alerta.DataHoraAlerta = DateTime.UtcNow;
            alerta.Resolvido = false;

            await _repository.AddAsync(alerta);
        }

        public async Task ResolverAlertaAsync(int id)
        {
            var alerta = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Alerta {id} não encontrado.");

            if (alerta.Resolvido)
                throw new InvalidOperationException("Este alerta já foi resolvido.");

            alerta.Resolvido = true;
            alerta.DataHoraResolucao = DateTime.UtcNow;

            await _repository.UpdateAsync(alerta);
        }

        public async Task RemoverAsync(int id)
        {
            var alerta = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Alerta {id} não encontrado.");

            await _repository.DeleteAsync(id);
        }
    }
}
