using OrbitalGuard.Models;
using OrbitalGuard.Repositories.Interfaces;
using OrbitalGuard.Services.Interfaces;

namespace OrbitalGuard.Services
{
    public class LeituraClimaticaService : ILeituraClimaticaService
    {
        private readonly ILeituraClimaticaRepository _repository;

        public LeituraClimaticaService(ILeituraClimaticaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LeituraClimatica>> ObterTodasAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<LeituraClimatica?> ObterPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<LeituraClimatica>> ObterPorSateliteAsync(int sateliteId)
        {
            if (sateliteId <= 0)
                throw new ArgumentException("ID do satélite inválido.");

            return await _repository.GetBySateliteIdAsync(sateliteId);
        }

        public async Task<IEnumerable<LeituraClimatica>> ObterPorRegiaoAsync(int regiaoId)
        {
            if (regiaoId <= 0)
                throw new ArgumentException("ID da região inválido.");

            return await _repository.GetByRegiaoMonitoradaIdAsync(regiaoId);
        }

        public async Task CadastrarAsync(LeituraClimatica leitura)
        {
            // Validações básicas
            if (leitura.SateliteId <= 0)
                throw new ArgumentException("Satélite é obrigatório.");

            if (leitura.RegiaoMonitoradaId <= 0)
                throw new ArgumentException("Região monitorada é obrigatória.");

            if (leitura.TemperaturaC < -100 || leitura.TemperaturaC > 70)
                throw new ArgumentException("Temperatura fora do intervalo permitido.");

            if (leitura.UmidadePercent < 0 || leitura.UmidadePercent > 100)
                throw new ArgumentException("Umidade deve estar entre 0 e 100.");

            if (leitura.PressaoHpa <= 0)
                throw new ArgumentException("Pressão deve ser maior que zero.");

            // Forçar timestamp UTC (caso venha local)
            leitura.Timestamp = DateTime.SpecifyKind(leitura.Timestamp, DateTimeKind.Utc);

            leitura.IndiceRisco = CalcularIndiceRisco(leitura);

            await _repository.AddAsync(leitura);
        }

        public async Task AtualizarAsync(LeituraClimatica leitura)
        {
            var existente = await _repository.GetByIdAsync(leitura.Id)
                ?? throw new KeyNotFoundException($"Leitura {leitura.Id} não encontrada.");

            // Não permitir alteração do Timestamp original (se for uma leitura histórica)
            leitura.Timestamp = existente.Timestamp;

            // Mesmas validações da criação
            if (leitura.SateliteId <= 0)
                throw new ArgumentException("Satélite é obrigatório.");

            if (leitura.RegiaoMonitoradaId <= 0)
                throw new ArgumentException("Região monitorada é obrigatória.");

            await _repository.UpdateAsync(leitura);
        }

        public async Task RemoverAsync(int id)
        {
            var existente = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Leitura {id} não encontrada.");

            await _repository.DeleteAsync(id);
        }

        // Exemplo de cálculo do Índice de Risco (customize conforme sua lógica)
        private double CalcularIndiceRisco(LeituraClimatica leitura)
        {
            // Fórmula fictícia: combina temperatura, umidade, pressão e vento
            double risco = 0;
            if (leitura.TemperaturaC > 35) risco += 0.3;
            if (leitura.UmidadePercent > 80) risco += 0.2;
            if (leitura.PressaoHpa < 1000) risco += 0.2;
            if (leitura.VelocidadeVentoKmh > 50) risco += 0.3;
            return Math.Min(risco, 1.0);
        }
    }
}
