using OrbitalGuard.Domain.Enums;

namespace OrbitalGuard.DTOs
{
    public class AlertaDesastreDto
    {
        public TipoDesastre TipoDesastre { get; set; }
        public NivelAlerta NivelAlerta { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int LeituraClimaticaId { get; set; }
    }
}
