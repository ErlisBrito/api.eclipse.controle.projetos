using api.eclipse.controle.projetos.Data.Contexts;
using api.eclipse.controle.projetos.Data.Repository;
using api.eclipse.controle.projetos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace api.eclipse.controle.projetos.Tests.Repository
{
    [TestFixture]
    public class ProjetosRepository_Test
    {
        private ProjetosRepository _repository;
        private EclipseContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EclipseContext>()
                   .UseInMemoryDatabase("TestDatabase")
                   .Options;

            _context = new EclipseContext(options);
            _repository = new ProjetosRepository(_context);

            _context.Projetos.RemoveRange(_context.Projetos);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        [Test]
        public async Task DeletarProjetoAsync_DeveRemoverProjeto()
        {
            // Arrange
            var projeto = new Projetos
            {
                Id = 1,
                Titulo = "Projeto para Deletar",
                UsuarioId = 1,
                Descricao = "Descrição do Projeto para Deletar",
                DataCadastro = DateTime.Now
            };

            // Adiciona o projeto
            await _repository.SalvarProjetoAsync(projeto);

            // Act: Deletar o projeto
            await _repository.DeletarProjetoAsync(projeto);

            // Assert: Verifica se o projeto foi removido
            var projetoDeletado = await _repository.ObterProjetoAsync(1);
            Assert.IsNull(projetoDeletado);  // Verifica se o projeto foi removido

            // Limpar o banco de dados após o teste
            _context.Projetos.RemoveRange(_context.Projetos);
            await _context.SaveChangesAsync();
        }


        [Test]
        public async Task ListarProjetosAsync_DeveRetornarListaDeProjetos()
        {
            // Arrange
            _context.Projetos.Add(new Projetos
            {
                Id = 1,
                Titulo = "Projeto 1",
                UsuarioId = 1,
                Descricao = "Descrição do Projeto 1",
                DataCadastro = DateTime.Now
            });
            _context.Projetos.Add(new Projetos
            {
                Id = 2,
                Titulo = "Projeto 2",
                UsuarioId = 2,
                Descricao = "Descrição do Projeto 2",
                DataCadastro = DateTime.Now
            });
            await _context.SaveChangesAsync();

            // Act
            var projetos = await _repository.ListarProjetosAsync();

            // Assert
            Assert.AreEqual(2, projetos.Count);
        }


        [Test]
        public async Task ListarProjetosPorUsuario_DeveRetornarProjetosDoUsuarioAsync()
        {
            // Arrange
            _context.Projetos.Add(new Projetos
            {
                Id = 1,
                Titulo = "Projeto 1",
                UsuarioId = 1,
                Descricao = "Descrição do Projeto 1",
                DataCadastro = DateTime.Now
            });
            _context.Projetos.Add(new Projetos
            {
                Id = 2,
                Titulo = "Projeto 2",
                UsuarioId = 1,
                Descricao = "Descrição do Projeto 2",
                DataCadastro = DateTime.Now
            });
            _context.Projetos.Add(new Projetos
            {
                Id = 3,
                Titulo = "Projeto 3",
                UsuarioId = 2,
                Descricao = "Descrição do Projeto 3",
                DataCadastro = DateTime.Now
            });
            await _context.SaveChangesAsync();

            // Act
            var projetosUsuario1 = _repository.ListarProjetosPorUsuario(1);
            var projetosUsuario2 = _repository.ListarProjetosPorUsuario(2);

            // Assert
            Assert.AreEqual(2, projetosUsuario1.Count);  // Deve retornar 2 projetos para o usuário 1
            Assert.AreEqual(1, projetosUsuario2.Count);  // Deve retornar 1 projeto para o usuário 2
        }

        [Test]
        public async Task ObterProjetoAsync_DeveRetornarProjeto()
        {
            // Arrange
            _context.Projetos.Add(new Projetos
            {
                Id = 1,
                Titulo = "Projeto 1",
                UsuarioId = 1,
                Descricao = "Descrição do Projeto 1",
                DataCadastro = DateTime.Now
            });
            await _context.SaveChangesAsync();

            // Act
            var projeto = await _repository.ObterProjetoAsync(1);

            // Assert
            Assert.IsNotNull(projeto);
            Assert.AreEqual(1, projeto.Id);
            Assert.AreEqual("Projeto 1", projeto.Titulo);
        }


        [Test]
        public async Task SalvarProjetoAsync_DeveAdicionarProjeto()
        {
            // Arrange
            var projeto = new Projetos
            {
                Id = 1,
                Titulo = "Novo Projeto",
                UsuarioId = 1,
                Descricao = "Descrição do Novo Projeto",
                DataCadastro = DateTime.Now
            };

            // Act
            await _repository.SalvarProjetoAsync(projeto);

            // Assert
            var projetoSalvo = await _repository.ObterProjetoAsync(1);
            Assert.IsNotNull(projetoSalvo);
            Assert.AreEqual("Novo Projeto", projetoSalvo.Titulo);
        }


        [Test]
        public async Task AtualizarProjetoAsync_DeveAtualizarProjeto()
        {
            // Arrange
            var projeto = new Projetos
            {
                Id = 1,
                Titulo = "Projeto Original",
                UsuarioId = 1,
                Descricao = "Descrição do Projeto Original",
                DataCadastro = DateTime.Now
            };
            await _repository.SalvarProjetoAsync(projeto);

            projeto.Titulo = "Projeto Atualizado";
            projeto.Descricao = "Descrição Atualizada";

            // Act
            await _repository.AtualizarProjetoAsync(projeto);

            // Assert
            var projetoAtualizado = await _repository.ObterProjetoAsync(1);
            Assert.AreEqual("Projeto Atualizado", projetoAtualizado.Titulo);
            Assert.AreEqual("Descrição Atualizada", projetoAtualizado.Descricao);
        }




    }
}

