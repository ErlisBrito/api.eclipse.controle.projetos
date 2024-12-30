using api.eclipse.controle.projetos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.eclipse.controle.projetos.Data.Mappings
{
    public class HistoricoTarefaMap : IEntityTypeConfiguration<HistoricoTarefa>
    {
        public void Configure(EntityTypeBuilder<HistoricoTarefa> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
