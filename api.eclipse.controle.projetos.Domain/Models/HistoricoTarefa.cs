namespace api.eclipse.controle.projetos.Domain.Models
{
    public class HistoricoTarefa
    {
        public int Id { get; set; }
        public string Descricao {get; set;}
        public string ObjetoDeEnvio {get; set;}
        public string Acao { get; set;}
        public DateTime  DataCadastro { get; set;}
        public int TarefaId {get; set;}
        public int UsuarioId {get; set;}
    }
}
