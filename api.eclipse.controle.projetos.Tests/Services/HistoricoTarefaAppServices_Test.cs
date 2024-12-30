using api.eclipse.controle.projetos.Application.Services;
using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Domain.Interfaces;
using api.eclipse.controle.projetos.Domain.Models;
using Moq;
using System.Net;

namespace api.eclipse.controle.projetos.Tests.Services
{

    [TestFixture] 
    public class HistoricoTarefaAppServices_Test
    {
        private Mock<IHistoricoTarefaRepository> _historicoTarefaRepositoryMock;
        private Mock<ITarefaRepository> _tarefaRepositoryMock;
        private HistoricoTarefaAppServices _historicoTarefaAppServices;

        [SetUp]
        public void Setup()
        {
            _historicoTarefaRepositoryMock = new Mock<IHistoricoTarefaRepository>();
            _tarefaRepositoryMock = new Mock<ITarefaRepository>();

            _historicoTarefaAppServices = new HistoricoTarefaAppServices(
                    null, 
                _historicoTarefaRepositoryMock.Object,
                _tarefaRepositoryMock.Object
            );
        }

        [Test]
        public async Task SalvarHistoricoTarefaAsync_DeveSalvarHistoricoComSucesso()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, UsuarioId = 123 };
            string acao = "Criar Tarefa";

            _historicoTarefaRepositoryMock.Setup(x => x.SalvarHistoricoTarefaAsync(It.IsAny<HistoricoTarefa>()))
                                          .Returns(Task.CompletedTask); 
            var resultado = await _historicoTarefaAppServices.SalvarHistoricoTarefaAsync(tarefa, acao);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual("Tarefa cadastrada com sucesso!", resultado.Mensagem);
            _historicoTarefaRepositoryMock.Verify(x => x.SalvarHistoricoTarefaAsync(It.IsAny<HistoricoTarefa>()), Times.Once); // Verifica se o método foi chamado uma vez
        }

        [Test]
        public async Task SalvarHistoricoTarefaAsync_DeveRetornarErroQuandoFalhar()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, UsuarioId = 123 };
            string acao = "Criar Tarefa";

            // Simula erro no repositório (por exemplo, exceção ao salvar)
            _historicoTarefaRepositoryMock.Setup(x => x.SalvarHistoricoTarefaAsync(It.IsAny<HistoricoTarefa>()))
                                          .ThrowsAsync(new Exception("Erro interno"));

            // Act
            var resultado = await _historicoTarefaAppServices.SalvarHistoricoTarefaAsync(tarefa, acao);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, resultado.StatusCode);
            Assert.IsTrue(resultado.Mensagem.Contains("Erro ao salvar o Tarefa: Erro interno"));
        }

        [Test]
        public async Task SalvarComentariosTarefaAsync_DeveSalvarComentarioComSucesso()
        {
            // Arrange
            var comentario = new HistoricoTarefaViewModel
            {
                TarefaId = 1,
                UsuarioId = 123,
                Descricao = "Comentário de teste"
            };
            string acao = "Adicionar Comentário";

            _historicoTarefaRepositoryMock.Setup(x => x.SalvarHistoricoTarefaAsync(It.IsAny<HistoricoTarefa>()))
                                          .Returns(Task.CompletedTask); // Simula sucesso no salvamento

            // Act
            var resultado = await _historicoTarefaAppServices.SalvarComentariosTarefaAsync(comentario, acao);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual("Tarefa cadastrada com sucesso!", resultado.Mensagem);
            _historicoTarefaRepositoryMock.Verify(x => x.SalvarHistoricoTarefaAsync(It.IsAny<HistoricoTarefa>()), Times.Once); // Verifica se o método foi chamado uma vez
        }

        [Test]
        public async Task SalvarComentariosTarefaAsync_DeveRetornarErroQuandoFalhar()
        {
            // Arrange
            var comentario = new HistoricoTarefaViewModel
            {
                TarefaId = 1,
                UsuarioId = 123,
                Descricao = "Comentário de teste"
            };
            string acao = "Adicionar Comentário";

            // Simula erro no repositório (por exemplo, exceção ao salvar)
            _historicoTarefaRepositoryMock.Setup(x => x.SalvarHistoricoTarefaAsync(It.IsAny<HistoricoTarefa>()))
                                          .ThrowsAsync(new Exception("Erro interno"));

            // Act
            var resultado = await _historicoTarefaAppServices.SalvarComentariosTarefaAsync(comentario, acao);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, resultado.StatusCode);
            Assert.IsTrue(resultado.Mensagem.Contains("Erro ao salvar o Tarefa: Erro interno"));
        }
    }
}

