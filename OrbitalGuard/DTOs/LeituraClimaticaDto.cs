namespace OrbitalGuard.DTOs
{
    public class LeituraClimaticaDto
    {
        public double TemperaturaC { get; set; }
        public double UmidadePercent { get; set; }
        public double PressaoHpa { get; set; }
        public double VelocidadeVentoKmh { get; set; }
        public double IndiceRisco { get; set; }
        public int SateliteId { get; set; }
        public int RegiaoMonitoradaId { get; set; }
    }
}