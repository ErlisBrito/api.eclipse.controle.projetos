using api.eclipse.controle.projetos.Data.Contexts;
using api.eclipse.controle.projetos.Domain.Enums;
using api.eclipse.controle.projetos.Domain.Interfaces;
using api.eclipse.controle.projetos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace api.eclipse.controle.projetos.Data.Repository
{
    public class TarefaRepository : ITarefaRepository
    {

        private readonly EclipseContext _context;
        public TarefaRepository(EclipseContext context)
        {
            _context = context;
        }

        public async Task SalvarTarefaAsync(Tarefa tarefa)
        {
            await _context.AddAsync(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarTarefaAsync(Tarefa tarefa)
        {
            _context.Update(tarefa);
            _context.SaveChanges();
        }

        public List<Tarefa> ObterTarefaPeloProjetoId(int id) =>
                         [.. _context.Tarefa.Where(p => p.ProjetoId.Equals(id) && p.StatusId != StatusProjetoEnum.Finalizado)];

        public List<Tarefa> ListaTarefaPeloProjetoId(int id) =>
                        [.. _context.Tarefa.Where(p => p.ProjetoId.Equals(id))];

        public async Task<Tarefa> ObterTarefaAsync(int id) 
                        => await _context.Tarefa.FirstOrDefaultAsync(t => t.Id.Equals(id));

        public async Task<List<Tarefa>> ListarTarefasAsync() =>  await _context.Tarefa.ToListAsync();


        public async Task<List<RelatorioDesempenho>> GerarRelatorioDesempenhoAsync()
        {
            var dataLimite = DateTime.Now.AddDays(-30);

            var resultado = await _context.Tarefa
                .Where(t => t.StatusId.Equals(StatusProjetoEnum.Finalizado) 
                 && t.DataEntrega != null 
                 && t.DataEntrega >= dataLimite)
                .GroupBy(t => t.UsuarioId)
                .Select(g => new RelatorioDesempenho
                {
                    UsuarioId = g.Key,
                    MediaTarefasConcluidas = g.Count()
                })
                .ToListAsync();

            return resultado;
        }

        public void DeletarTarefas(List<Tarefa> Tarefas)
        {
            _context.RemoveRange(Tarefas);
            _context.SaveChanges();
        }
         
        public void DeletarTarefaAsync(Tarefa Tarefa)
        {
            _context.Remove(Tarefa);
            _context.SaveChanges();
        }

    }
}
