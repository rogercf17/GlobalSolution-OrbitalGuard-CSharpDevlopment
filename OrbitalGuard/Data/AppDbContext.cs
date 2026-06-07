using Microsoft.EntityFrameworkCore;
using OrbitalGuard.Models;

namespace OrbitalGuard.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Satelite> satelites { get; set; }
        public DbSet<RegiaoMonitorada> regiaoMonitoradas { get; set; }
        public DbSet<LeituraClimatica> leituraClimaticas { get; set; }
        public DbSet<AlertaDesastre> alertaDesastres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Satelite>().ToTable("TB_SATELITES");
            modelBuilder.Entity<Satelite>()
                .Property(s => s.Ativo)
                .HasColumnType("NUMBER(1)");

            modelBuilder.Entity<RegiaoMonitorada>().ToTable("TB_REGIOES");

            modelBuilder.Entity<LeituraClimatica>().ToTable("TB_LEITURAS")
                .HasOne(l => l.Satelite)
                .WithMany(s => s.leituras)
                .HasForeignKey(l => l.SateliteId);
            modelBuilder.Entity<LeituraClimatica>()
                .HasOne(l => l.RegiaoMonitorada)
                .WithMany(r => r.Leituras)
                .HasForeignKey(l => l.RegiaoMonitoradaId);

            modelBuilder.Entity<AlertaDesastre>().ToTable("TB_ALERTAS")
                .HasOne(a => a.LeituraClimatica)
                .WithMany(l => l.Alertas)
                .HasForeignKey(a => a.LeituraClimaticaId);
            modelBuilder.Entity<AlertaDesastre>()
                .Property(a => a.Resolvido)
                .HasColumnType("NUMBER(1)");
        }
    }
}
