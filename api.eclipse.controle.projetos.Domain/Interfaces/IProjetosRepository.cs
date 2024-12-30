using api.eclipse.controle.projetos.Domain.Models;

namespace api.eclipse.controle.projetos.Domain.Interfaces
{
    public interface IProjetosRepository
    {
        Task<List<Projetos>> ListarProjetosAsync();
        Task SalvarProjetoAsync(Projetos projetos);
        Task<Projetos> ObterProjetoAsync(int id);
        Task AtualizarProjetoAsync(Projetos projetos);
        Task DeletarProjetoAsync(Projetos projetos);
        List<Projetos> ListarProjetosPorUsuario(int usuarioId);
    }
}
