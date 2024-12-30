using api.eclipse.controle.projetos.Data.Contexts;
using api.eclipse.controle.projetos.Domain.Interfaces;
using api.eclipse.controle.projetos.Domain.Models;

namespace api.eclipse.controle.projetos.Data.Repository
{
    
    public class HistoricoTarefaRepository: IHistoricoTarefaRepository
    {
        private readonly EclipseContext _context;
        public HistoricoTarefaRepository(EclipseContext context)
        {
            _context = context;
        }

        public async Task SalvarHistoricoTarefaAsync(HistoricoTarefa historicoTarefa)
        {
            await _context.AddAsync(historicoTarefa);
            await _context.SaveChangesAsync();
        }

    }
}
