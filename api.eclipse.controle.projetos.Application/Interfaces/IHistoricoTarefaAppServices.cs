using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Common;
using api.eclipse.controle.projetos.Domain.Models;

namespace api.eclipse.controle.projetos.Application.Interfaces
{
    public interface IHistoricoTarefaAppServices
    {
        Task<Resultado<HistoricoTarefaViewModel>> SalvarHistoricoTarefaAsync(Tarefa model, string acao);
        Task<Resultado<HistoricoTarefaViewModel>> SalvarComentariosTarefaAsync(HistoricoTarefaViewModel model, string acao);
    }
}
