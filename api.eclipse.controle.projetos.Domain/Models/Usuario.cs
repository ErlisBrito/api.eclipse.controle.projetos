namespace api.eclipse.controle.projetos.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public virtual List<Projetos> Projeto { get; set; }
        public virtual List<Tarefa> Tarefa { get; set; }
    }
}
