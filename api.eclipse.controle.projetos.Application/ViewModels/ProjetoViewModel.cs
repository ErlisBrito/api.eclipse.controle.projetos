using api.eclipse.controle.projetos.Domain.Enums;

namespace api.eclipse.controle.projetos.Application.ViewModels
{
    public class ProjetoViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataEntrega { get; set; }
        public StatusProjetoEnum StatusProjeto { get; set; }
        public int UsuarioId { get; set; }
    }
}