namespace OrbitalGuard.DTOs
{
    public class SateliteDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Fabricante { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public double AltitudeKm { get; set; }
        public string TipoOrbita { get; set; } = string.Empty;
        public double CoberturaDegraus { get; set; }
    }
}
