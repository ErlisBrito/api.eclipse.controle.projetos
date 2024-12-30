using api.eclipse.controle.projetos.Data.Contexts;
using api.eclipse.controle.projetos.Domain.Interfaces;
using api.eclipse.controle.projetos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace api.eclipse.controle.projetos.Data.Repository
{
    public class ProjetosRepository : IProjetosRepository
    {
        private readonly EclipseContext _context;
        public ProjetosRepository(EclipseContext context)
        {
            _context = context;
        }

        public async Task<List<Projetos>> ListarProjetosAsync() => await _context.Projetos.ToListAsync();

        public List<Projetos> ListarProjetosPorUsuario(int usuarioId) => 
                   [.. _context.Projetos.Where(p => p.UsuarioId.Equals(usuarioId))];

        public async Task<Projetos> ObterProjetoAsync(int id) =>
               await _context.Projetos.FirstOrDefaultAsync(p => p.Id.Equals(id));

        public async Task SalvarProjetoAsync(Projetos projetos)
        {
            await _context.AddAsync(projetos);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarProjetoAsync(Projetos projetos)
        {
            _context.Update(projetos);
            _context.SaveChanges();
        }

        public async Task DeletarProjetoAsync(Projetos projetos)
        {
            _context.Remove(projetos);
            _context.SaveChanges();
        }
    }
}
