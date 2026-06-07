namespace OrbitalGuard.DTOs
{
    public class RegiaoMonitoradaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double AreaKm2 { get; set; }
    }
}
