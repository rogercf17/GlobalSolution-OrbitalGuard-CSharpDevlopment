namespace OrbitalGuard.Models
{
    public abstract class EquipamentoEspacial
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Fabricante { get; set; } = string.Empty;
        public DateTime DataLancamento { get; set; }
        public bool Ativo { get; set; } = true;
        public abstract string ObterDescricao();
    }
}
