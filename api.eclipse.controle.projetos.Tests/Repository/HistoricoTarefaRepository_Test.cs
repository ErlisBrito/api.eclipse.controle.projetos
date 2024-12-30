using api.eclipse.controle.projetos.Data.Contexts;
using api.eclipse.controle.projetos.Data.Repository;
using api.eclipse.controle.projetos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace api.eclipse.controle.projetos.Tests.Repository
{
    [TestFixture]
    public class HistoricoTarefaRepository_Test
    {
        private HistoricoTarefaRepository _repository;
        private  EclipseContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EclipseContext>()
                .UseInMemoryDatabase("TestDatabase") 
                .Options;

            _context = new EclipseContext(options);
            _repository = new HistoricoTarefaRepository(_context);
        }
        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        [Test]
        public async Task SalvarHistoricoTarefaAsync_DeveAdicionarHistoricoEChamarSaveChanges()
        {
            // Arrange
            var historicoTarefa = new HistoricoTarefa
            {
                Id = 1,
                TarefaId = 1,
                UsuarioId = 1,
                Acao = "Criar Tarefa",
                DataCadastro = System.DateTime.Now,
                Descricao = "Descrição da tarefa",  // Adicionando a propriedade Descricao
                ObjetoDeEnvio = "{ \"key\": \"value\" }"  // Adicionando a propriedade ObjetoDeEnvio
            };

            // Act
            await _repository.SalvarHistoricoTarefaAsync(historicoTarefa);

            // Assert
            var savedHistoricoTarefa = await _context.Set<HistoricoTarefa>().FindAsync(historicoTarefa.Id);
            Assert.NotNull(savedHistoricoTarefa);  
            Assert.AreEqual(historicoTarefa.TarefaId, savedHistoricoTarefa.TarefaId);
            Assert.AreEqual("Descrição da tarefa", savedHistoricoTarefa.Descricao); 
            Assert.AreEqual("{ \"key\": \"value\" }", savedHistoricoTarefa.ObjetoDeEnvio); 
        }
    }
}
