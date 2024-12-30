using api.eclipse.controle.projetos.Data.Mappings;
using api.eclipse.controle.projetos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace api.eclipse.controle.projetos.Data.Contexts
{
    public class EclipseContext : DbContext
    {
        public EclipseContext(DbContextOptions<EclipseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Projetos> Projetos { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<HistoricoTarefa> HistoricoTarefa { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjetoMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
        }
    }
}
