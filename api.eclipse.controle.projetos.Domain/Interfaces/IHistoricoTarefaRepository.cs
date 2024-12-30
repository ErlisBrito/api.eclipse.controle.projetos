using api.eclipse.controle.projetos.Domain.Models;

namespace api.eclipse.controle.projetos.Domain.Interfaces
{
    public interface IHistoricoTarefaRepository
    {
        Task SalvarHistoricoTarefaAsync(HistoricoTarefa historicoTarefa);
    }
}
