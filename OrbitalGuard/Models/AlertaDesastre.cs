using OrbitalGuard.Domain.Enums;

namespace OrbitalGuard.Models
{
    public class AlertaDesastre
    {
        public int Id { get; set; }
        public TipoDesastre TipoDesastre { get; set; }
        public NivelAlerta NivelAlerta { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataHoraAlerta { get; set; } = DateTime.UtcNow;
        public bool Resolvido { get; set; } = false;
        public DateTime? DataHoraResolucao { get; set; }

        // Chave estrangeira
        public int LeituraClimaticaId { get; set; }

        // Navegação
        public LeituraClimatica LeituraClimatica { get; set; } = null!;
    }
}