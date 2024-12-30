using api.eclipse.controle.projetos.Data.Contexts;
using api.eclipse.controle.projetos.Data.Repository;
using api.eclipse.controle.projetos.Domain.Enums;
using api.eclipse.controle.projetos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace api.eclipse.controle.projetos.Tests.Repository
{
    [TestFixture]
    public class TarefaRepository_Test
    {
        private EclipseContext _context;
        private TarefaRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EclipseContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new EclipseContext(options);
            _repository = new TarefaRepository(_context);

            // Limpa o banco de dados antes de cada teste
            _context.Tarefa.RemoveRange(_context.Tarefa);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
  
            _context?.Dispose();
        }

        [Test]
        public async Task SalvarTarefaAsync_DeveAdicionarTarefa()
        {
            // Arrange
            var tarefa = new Tarefa
            {
                Id = 1,
                ProjetoId = 1,
                UsuarioId = 1,
                Titulo = "Tarefa 1",
                StatusId = StatusProjetoEnum.Criado,
                PrioridadeId = PrioridadeEnum.Media,
                Descricao = "Tarefa 1",
                DataCadastro = DateTime.Now
            };

            // Act
            await _repository.SalvarTarefaAsync(tarefa);

            // Assert
            var tarefaNoDb = await _context.Tarefa.FindAsync(1);
            Assert.IsNotNull(tarefaNoDb);
            Assert.AreEqual(tarefa.Descricao, tarefaNoDb.Descricao);
        }

        [Test]
        public async Task AtualizarTarefaAsync_DeveAtualizarTarefa()
        {
            // Arrange
            var tarefa = new Tarefa
            {
                Id = 1,
                ProjetoId = 1,
                UsuarioId = 1,
                Titulo = "Tarefa 1",
                StatusId = StatusProjetoEnum.Criado,
                PrioridadeId = PrioridadeEnum.Media,
                Descricao = "Tarefa 1",
                DataCadastro = DateTime.Now
            };
            await _repository.SalvarTarefaAsync(tarefa);

            // Act
            tarefa.Descricao = "Tarefa Atualizada";
            await _repository.AtualizarTarefaAsync(tarefa);

            // Assert
            var tarefaAtualizada = await _context.Tarefa.FindAsync(1);
            Assert.AreEqual("Tarefa Atualizada", tarefaAtualizada.Descricao);
        }

        [Test]
        public void ObterTarefaPeloProjetoId_DeveRetornarTarefasDoProjeto()
        {
            // Arrange
            var tarefa1 = new Tarefa { Id = 1, ProjetoId = 1, StatusId = StatusProjetoEnum.Criado, Descricao = "Tarefa 1", DataCadastro = DateTime.Now, Titulo="Tarefa 1", PrioridadeId = PrioridadeEnum.Media };
            var tarefa2 = new Tarefa { Id = 2, ProjetoId = 1, StatusId = StatusProjetoEnum.Criado, Descricao = "Tarefa 2", DataCadastro = DateTime.Now, Titulo = "Tarefa ", PrioridadeId = PrioridadeEnum.Media };
            var tarefa3 = new Tarefa { Id = 3, ProjetoId = 2, StatusId = StatusProjetoEnum.EmDesenvolvimento, Descricao = "Tarefa 3", DataCadastro = DateTime.Now, Titulo = "Tarefa ", PrioridadeId = PrioridadeEnum.Media };
            _context.Tarefa.AddRange(tarefa1, tarefa2, tarefa3);
            _context.SaveChanges();

            // Act
            var tarefasDoProjeto1 = _repository.ObterTarefaPeloProjetoId(1);

            // Assert
            Assert.AreEqual(2, tarefasDoProjeto1.Count); 
        }


        [Test]
        public async Task ListaTarefaPeloProjetoId_DeveRetornarTodasTarefasDoProjeto()
        {
            // Arrange
            var tarefa1 = new Tarefa
            {
                Id = 1,
                ProjetoId = 1,
                UsuarioId = 1,
                StatusId = StatusProjetoEnum.EmDesenvolvimento,
                Titulo = "Tarefa 1", // Adicionando a propriedade obrigatória
                Descricao = "Descrição da Tarefa 1", // A propriedade Descricao também deve ser preenchida
                DataCadastro = DateTime.Now,
                DataEntrega = DateTime.Now.AddDays(2)
            };

            var tarefa2 = new Tarefa
            {
                Id = 2,
                ProjetoId = 1,
                UsuarioId = 2,
                StatusId = StatusProjetoEnum.EmDesenvolvimento,
                Titulo = "Tarefa 2", // Adicionando a propriedade obrigatória
                Descricao = "Descrição da Tarefa 2", // A propriedade Descricao também deve ser preenchida
                DataCadastro = DateTime.Now,
                DataEntrega = DateTime.Now.AddDays(4)
            };

            await _repository.SalvarTarefaAsync(tarefa1);
            await _repository.SalvarTarefaAsync(tarefa2);

            // Act
            var tarefas = _repository.ListaTarefaPeloProjetoId(1);

            // Assert
            Assert.IsNotNull(tarefas);
            Assert.AreEqual(2, tarefas.Count); // Espera-se duas tarefas para o projeto com id 1
        }
        [Test]
        public async Task ObterTarefaAsync_DeveRetornarTarefa()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, ProjetoId = 1, StatusId = StatusProjetoEnum.Criado, Descricao = "Tarefa 1", DataCadastro = DateTime.Now , Titulo = "Tarefa ", PrioridadeId = PrioridadeEnum.Media };
            await _repository.SalvarTarefaAsync(tarefa);

            // Act
            var tarefaObtida = await _repository.ObterTarefaAsync(1);

            // Assert
            Assert.IsNotNull(tarefaObtida);
            Assert.AreEqual(tarefa.Id, tarefaObtida.Id);
        }

        [Test]
        public async Task GerarRelatorioDesempenhoAsync_DeveRetornarRelatorio()
        {
            // Arrange
            var tarefa1 = new Tarefa
            {
                Id = 1,
                ProjetoId = 1,
                UsuarioId = 1,
                StatusId = StatusProjetoEnum.Finalizado,
                Titulo = "Tarefa 1",
                Descricao = "Descrição da Tarefa 1", // A propriedade Descricao agora está preenchida
                DataCadastro = DateTime.Now,
                DataEntrega = DateTime.Now.AddDays(-5)
            };

            var tarefa2 = new Tarefa
            {
                Id = 2,
                ProjetoId = 1,
                UsuarioId = 1,
                StatusId = StatusProjetoEnum.Finalizado,
                Titulo = "Tarefa 2",
                Descricao = "Descrição da Tarefa 2", // A propriedade Descricao agora está preenchida
                DataCadastro = DateTime.Now,
                DataEntrega = DateTime.Now.AddDays(-3)
            };

            await _repository.SalvarTarefaAsync(tarefa1);
            await _repository.SalvarTarefaAsync(tarefa2);

            // Act
            var relatorio = await _repository.GerarRelatorioDesempenhoAsync();

            // Assert
            Assert.IsNotNull(relatorio);
            Assert.AreEqual(1, relatorio.Count); // Espera-se um único usuário com tarefas finalizadas no intervalo de 30 dias
            Assert.AreEqual(2, relatorio[0].MediaTarefasConcluidas); // Espera-se 2 tarefas finalizadas para o usuário
        }

        [Test]
        public void DeletarTarefas_DeveRemoverTarefas()
        {
            // Arrange
            var tarefa1 = new Tarefa { Id = 1, ProjetoId = 1, StatusId = StatusProjetoEnum.Criado, Descricao = "Tarefa 1", DataCadastro = DateTime.Now , Titulo = "Tarefa ", PrioridadeId = PrioridadeEnum.Media };
            var tarefa2 = new Tarefa { Id = 2, ProjetoId = 1, StatusId = StatusProjetoEnum.Criado, Descricao = "Tarefa 2", DataCadastro = DateTime.Now , Titulo = "Tarefa ", PrioridadeId = PrioridadeEnum.Media };
            _context.Tarefa.AddRange(tarefa1, tarefa2);
            _context.SaveChanges();

            // Act
            _repository.DeletarTarefas(new List<Tarefa> { tarefa1, tarefa2 });

            // Assert
            Assert.AreEqual(0, _context.Tarefa.Count());
        }

        [Test]
        public void DeletarTarefaAsync_DeveRemoverTarefa()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, ProjetoId = 1, StatusId = StatusProjetoEnum.Criado, Descricao = "Tarefa 1", DataCadastro = DateTime.Now , Titulo = "Tarefa ", PrioridadeId = PrioridadeEnum.Media};
            _context.Tarefa.Add(tarefa);
            _context.SaveChanges();

            // Act
            _repository.DeletarTarefaAsync(tarefa);

            // Assert
            Assert.AreEqual(0, _context.Tarefa.Count());
        }
    }

}
