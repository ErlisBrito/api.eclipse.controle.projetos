using api.eclipse.controle.projetos.Application.Interfaces;
using api.eclipse.controle.projetos.Application.Services;
using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Domain.Interfaces;
using api.eclipse.controle.projetos.Domain.Models;
using AutoMapper;
using Moq;
using System.Net;

namespace api.eclipse.controle.projetos.Tests.Services
{
    [TestFixture]
    public class TarefasAppServices_Test
    {

        private Mock<ITarefaRepository> _tarefaRepositoryMock;
        private Mock<IHistoricoTarefaAppServices> _historicoTarefaAppServicesMock;
        private Mock<IMapper> _mapperMock;
        private TarefasAppServices _tarefasAppServices;

        [SetUp]
        public void SetUp()
        {
            _tarefaRepositoryMock = new Mock<ITarefaRepository>();
            _historicoTarefaAppServicesMock = new Mock<IHistoricoTarefaAppServices>();
            _mapperMock = new Mock<IMapper>();
            _tarefasAppServices = new TarefasAppServices(
                _tarefaRepositoryMock.Object,
                _mapperMock.Object,
                _historicoTarefaAppServicesMock.Object
            );
        }

        [Test]
        public async Task SalvarTarefaAsync_LimiteDeTarefasExcedido_DeveRetornarBadRequest()
        {
            // Arrange
            var model = new TarefaViewModel { ProjetoId = 1 };

            // Simulando que já existem 21 tarefas no projeto
            var tarefasExistentes = new List<Tarefa>
                                   {
                                       new Tarefa(), new Tarefa(), new Tarefa(), new Tarefa(), new Tarefa(),
                                       new Tarefa(), new Tarefa(), new Tarefa(), new Tarefa(), new Tarefa(),
                                       new Tarefa(), new Tarefa(), new Tarefa(), new Tarefa(), new Tarefa(),
                                       new Tarefa(), new Tarefa(), new Tarefa(), new Tarefa(), new Tarefa(),
                                       new Tarefa(), new Tarefa() // Total de 21 tarefas
                                   };

            // Criando mocks
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            var historicoTarefaAppServicesMock = new Mock<IHistoricoTarefaAppServices>();
            var mapperMock = new Mock<IMapper>();

            // Configurando o mock para retornar 21 tarefas no projeto
            tarefaRepositoryMock.Setup(r => r.ListaTarefaPeloProjetoId(model.ProjetoId)).Returns(tarefasExistentes);

            // Configuração do mock para mapear o TarefaViewModel para Tarefa
            mapperMock.Setup(m => m.Map<Tarefa>(model)).Returns(new Tarefa { ProjetoId = model.ProjetoId });

            // Configuração do mock para salvar a tarefa no repositório
            tarefaRepositoryMock.Setup(r => r.SalvarTarefaAsync(It.IsAny<Tarefa>())).Returns(Task.CompletedTask);

            // Criando o serviço com os mocks
            var tarefasAppServices = new TarefasAppServices(tarefaRepositoryMock.Object, mapperMock.Object, historicoTarefaAppServicesMock.Object);

            // Act
            var resultado = await tarefasAppServices.SalvarTarefaAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, resultado.StatusCode);
            Assert.AreEqual("Limite máximo de tarefas atingido: Cada projeto pode conter no máximo 20 tarefas. Não é possível adicionar mais tarefas a este projeto.", resultado.Mensagem);
        }


        [Test]
        public async Task SalvarTarefaAsync_TarefaSalvaComSucesso_DeveRetornarOk()
        {
            // Arrange
            var model = new TarefaViewModel { ProjetoId = 1 };
            _tarefaRepositoryMock.Setup(r => r.ListaTarefaPeloProjetoId(model.ProjetoId)).Returns(new List<Tarefa>());
            _mapperMock.Setup(m => m.Map<Tarefa>(model)).Returns(new Tarefa());
            _tarefaRepositoryMock.Setup(r => r.SalvarTarefaAsync(It.IsAny<Tarefa>())).Returns(Task.CompletedTask);
            //_historicoTarefaAppServicesMock.Setup(h => h.SalvarHistoricoTarefaAsync(It.IsAny<Tarefa>(), It.IsAny<string>())).Returns((Task<Common.Resultado<HistoricoTarefaViewModel>>)Task.CompletedTask);

            // Act
            var resultado = await _tarefasAppServices.SalvarTarefaAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual("Tarefa cadastrada com sucesso!", resultado.Mensagem);
        }

        [Test]
        public async Task AtualizarTarefaAsync_TarefaAtualizadaComSucesso_DeveRetornarOk()
        {
            // Arrange
            var model = new TarefaViewModel { Id = 1, ProjetoId = 1 };
            var tarefaExistente = new Tarefa { Id = 1, ProjetoId = 1 };

            // Mock para obter a tarefa existente
            _tarefaRepositoryMock.Setup(r => r.ObterTarefaAsync(model.Id)).ReturnsAsync(tarefaExistente);

            // Mock para mapear o modelo para a entidade Tarefa
            _mapperMock.Setup(m => m.Map<Tarefa>(model)).Returns(new Tarefa { Id = 1 });

            // Mock para atualizar a tarefa no repositório
            _tarefaRepositoryMock.Setup(r => r.AtualizarTarefaAsync(It.IsAny<Tarefa>())).Returns(Task.CompletedTask);
            // Act
            var resultado = await _tarefasAppServices.AtualizarTarefaAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual("Tarefa Atualizada com sucesso!", resultado.Mensagem);
        }


        [Test]
        public async Task ObterTarefaAsync_TarefaEncontrada_DeveRetornarOk()
        {
            // Arrange
            var model = new TarefaViewModel { Id = 1 };
            var tarefaExistente = new Tarefa { Id = 1 };
            _tarefaRepositoryMock.Setup(r => r.ObterTarefaAsync(model.Id)).ReturnsAsync(tarefaExistente);
            _mapperMock.Setup(m => m.Map<TarefaViewModel>(tarefaExistente)).Returns(model);

            // Act
            var resultado = await _tarefasAppServices.ObterTarefaAsync(model.Id);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual(model, resultado.Model);
        }

        [Test]
        public async Task ListarTarefasAsync_TarefasListadasComSucesso_DeveRetornarOk()
        {
            // Arrange
            var tarefas = new List<Tarefa> { new Tarefa(), new Tarefa() };
            _tarefaRepositoryMock.Setup(r => r.ListarTarefasAsync()).ReturnsAsync(tarefas);
            _mapperMock.Setup(m => m.Map<List<TarefaViewModel>>(tarefas)).Returns(new List<TarefaViewModel>());

            // Act
            var resultado = await _tarefasAppServices.ListarTarefasAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.IsNotNull(resultado.Model);
        }

        [Test]
        public async Task DeletarTarefaAsync_TarefaDeletadaComSucesso_DeveRetornarOk()
        {
            // Arrange
            var tarefaId = 1;
            var tarefaExistente = new Tarefa { Id = tarefaId };
            _tarefaRepositoryMock.Setup(r => r.ObterTarefaAsync(tarefaId)).ReturnsAsync(tarefaExistente);
            _tarefaRepositoryMock.Setup(r => r.AtualizarTarefaAsync(It.IsAny<Tarefa>())).Returns(Task.CompletedTask);
            //_historicoTarefaAppServicesMock.Setup(h => h.SalvarHistoricoTarefaAsync(It.IsAny<Tarefa>(), It.IsAny<string>())).Returns((Task<Common.Resultado<HistoricoTarefaViewModel>>)Task.CompletedTask);

            // Act
            var resultado = await _tarefasAppServices.DeletarTarefaAsync(tarefaId);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual("Tarefa Excluída com sucesso!", resultado.Mensagem);
        }


    }
}
