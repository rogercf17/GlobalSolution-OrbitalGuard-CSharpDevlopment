namespace OrbitalGuard.Models
{
    public class LeituraClimatica
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public double TemperaturaC { get; set; }
        public double UmidadePercent { get; set; }
        public double PressaoHpa { get; set; }
        public double VelocidadeVentoKmh { get; set; }
        public double IndiceRisco { get; set; }

        public int SateliteId { get; set; }
        public int RegiaoMonitoradaId { get; set; }

        public Satelite Satelite { get; set; } = null!;
        public RegiaoMonitorada RegiaoMonitorada { get; set; } = null!;

        public ICollection<AlertaDesastre> Alertas { get; set; } = new List<AlertaDesastre>();
    }
}