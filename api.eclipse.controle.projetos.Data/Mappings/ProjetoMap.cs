using api.eclipse.controle.projetos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.eclipse.controle.projetos.Data.Mappings
{
    public class ProjetoMap : IEntityTypeConfiguration<Projetos>
    {
        public void Configure(EntityTypeBuilder<Projetos> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Usuario)
                   .WithMany(p => p.Projeto)
                   .HasForeignKey(p => p.UsuarioId);

            builder.HasMany(p => p.Tarefas)
                   .WithOne(t => t.Projeto) 
                   .HasForeignKey(t => t.ProjetoId)  
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
