namespace OrbitalGuard.Models
{
    public class Satelite : EquipamentoEspacial
    {
        public double AltitudeKm { get; set; }
        public string TipoOrbita { get; set; } = string.Empty;
        public double CoberturaDegraus { get; set; }

        public ICollection<LeituraClimatica> leituras { get; set; } = new List<LeituraClimatica>();

        public override string ObterDescricao()
        {
            return $"Satélite {Nome} em órbita {TipoOrbita} a {AltitudeKm}km de altitude.";
        }
    }
}
