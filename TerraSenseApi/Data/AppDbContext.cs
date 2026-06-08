using Microsoft.EntityFrameworkCore;
using TerraSenseApi.Models;

namespace TerraSenseApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<RelatorioPlantacao> RelatoriosPlantacoes { get; set; }
    public DbSet<ObservacaoRelatorio> ObservacoesRelatorio { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RelatorioPlantacao>()
            .HasMany(r => r.Observacoes)
            .WithOne(o => o.RelatorioPlantacao)
            .HasForeignKey(o => o.RelatorioPlantacaoId);

        modelBuilder.Entity<RelatorioPlantacao>()
            .Property(r => r.Temperatura)
            .HasPrecision(5, 2);

        modelBuilder.Entity<RelatorioPlantacao>()
            .Property(r => r.Umidade)
            .HasPrecision(5, 2);

        modelBuilder.Entity<RelatorioPlantacao>()
            .Property(r => r.Chuva)
            .HasPrecision(6, 2);

        modelBuilder.Entity<RelatorioPlantacao>()
            .Property(r => r.Ndvi)
            .HasPrecision(4, 2);

        modelBuilder.Entity<RelatorioPlantacao>()
            .Property(r => r.RadiacaoSolar)
            .HasPrecision(8, 2);
    }
}