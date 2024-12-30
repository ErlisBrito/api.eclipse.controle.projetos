using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Common;

namespace api.eclipse.controle.projetos.Application.Interfaces
{
    public interface ITarefasAppServices
    {
        Task<Resultado<TarefaViewModel>> SalvarTarefaAsync(TarefaViewModel model);
        Task<Resultado<TarefaViewModel>> ObterTarefaAsync(int id);
        Task<Resultado<TarefaViewModel>> AtualizarTarefaAsync(TarefaViewModel model);
        Task<Resultado<List<TarefaViewModel>>> ListarTarefasAsync();
        Task<Resultado<TarefaViewModel>> DeletarTarefaAsync(int id);
        Task<Resultado<List<TarefaViewModel>>> ListarTarefaPorProjetoAsync(int projetoId);
    }
}
