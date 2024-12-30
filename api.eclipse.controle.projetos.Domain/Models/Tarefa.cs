using api.eclipse.controle.projetos.Domain.Enums;

namespace api.eclipse.controle.projetos.Domain.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; } 
        public DateTime? DataInicio { get; set; }
        public DateTime? DataEntrega { get; set; }
        public StatusProjetoEnum StatusId { get; set; }
        public PrioridadeEnum PrioridadeId {  get; set; }   
        public int ProjetoId { get; set; }
        public int UsuarioId { get; set; }
        public virtual Projetos Projeto { get; set; }

    }
}
