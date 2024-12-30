using api.eclipse.controle.projetos.Domain.Models;

namespace api.eclipse.controle.projetos.Domain.Interfaces
{
    public interface ITarefaRepository
    {
        List<Tarefa> ObterTarefaPeloProjetoId(int id);
        List<Tarefa> ListaTarefaPeloProjetoId(int id);
        Task<List<Tarefa>> ListarTarefasAsync();
        void DeletarTarefas(List<Tarefa> Tarefas);
        void DeletarTarefaAsync(Tarefa Tarefa);
        Task SalvarTarefaAsync(Tarefa tarefa);
        Task AtualizarTarefaAsync(Tarefa tarefa);
        Task<Tarefa> ObterTarefaAsync(int id);
    }
}
