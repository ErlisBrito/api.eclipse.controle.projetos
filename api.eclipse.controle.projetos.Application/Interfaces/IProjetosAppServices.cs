using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Common;

namespace api.eclipse.controle.projetos.Application.Interfaces
{
    public interface IProjetosAppServices
    {
        Task<Resultado<List<ProjetoViewModel>>> ListarProjetosAsync();
        Task<Resultado<ProjetoViewModel>> SalvarProjetoAsync(ProjetoViewModel model);
        Task<Resultado<ProjetoViewModel>> ObterProjetoAsync(int id);
        Task<Resultado<ProjetoViewModel>> AtualizarProjetoAsync(ProjetoViewModel model);
        Task<Resultado<ProjetoViewModel>> DeletarProjetoAsync(int id);
        Task<Resultado<List<ProjetoViewModel>>> ListarProjetosPorUsuarioAsync(int usuarioId);
    }
}
