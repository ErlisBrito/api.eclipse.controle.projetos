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
    public class ProjetosAppServices_Test
    {
        private Mock<IProjetosRepository> _projetosRepositoryMock;
        private Mock<ITarefaRepository> _tarefaRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private ProjetosAppServices _projetosAppServices;

        [SetUp]
        public void Setup()
        {
            _projetosRepositoryMock = new Mock<IProjetosRepository>();
            _tarefaRepositoryMock = new Mock<ITarefaRepository>();
            _mapperMock = new Mock<IMapper>();

            _projetosAppServices = new ProjetosAppServices(
                _projetosRepositoryMock.Object,
                _tarefaRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Test]
        public async Task ListarProjetosAsync_DeveRetornarOk()
        {
            // Arrange
            var projetosMock = new List<Projetos> { new Projetos(), new Projetos() };
            _projetosRepositoryMock.Setup(repo => repo.ListarProjetosAsync()).ReturnsAsync(projetosMock);
            _mapperMock.Setup(m => m.Map<List<ProjetoViewModel>>(It.IsAny<List<Projetos>>())).Returns(new List<ProjetoViewModel>());

            // Act
            var resultado = await _projetosAppServices.ListarProjetosAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.IsNotNull(resultado.Model);
            Assert.IsInstanceOf<List<ProjetoViewModel>>(resultado.Model);
        }

        [Test]
        public async Task ListarProjetosAsync_DeveRetornarErro()
        {
            // Arrange
            _projetosRepositoryMock.Setup(repo => repo.ListarProjetosAsync()).Throws(new Exception("Erro ao listar projetos"));

            // Act
            var resultado = await _projetosAppServices.ListarProjetosAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, resultado.StatusCode);
            Assert.AreEqual("Erro ao listar os Projetos: Erro ao listar projetos!", resultado.Mensagem);
        }

        [Test]
        public async Task ListarProjetosPorUsuarioAsync_DeveRetornarOk()
        {
            // Arrange
            var usuarioId = 1;
            var projetosMock = new List<Projetos> { new Projetos(), new Projetos() };
            _projetosRepositoryMock.Setup(repo => repo.ListarProjetosPorUsuario(usuarioId)).Returns(projetosMock);
            _mapperMock.Setup(m => m.Map<List<ProjetoViewModel>>(It.IsAny<List<Projetos>>())).Returns(new List<ProjetoViewModel>());

            // Act
            var resultado = await _projetosAppServices.ListarProjetosPorUsuarioAsync(usuarioId);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.IsNotNull(resultado.Model);
        }

        [Test]
        public async Task ListarProjetosPorUsuarioAsync_DeveRetornarErro()
        {
            // Arrange
            var usuarioId = 1;
            _projetosRepositoryMock.Setup(repo => repo.ListarProjetosPorUsuario(usuarioId)).Throws(new Exception("Erro ao listar projetos"));

            // Act
            var resultado = await _projetosAppServices.ListarProjetosPorUsuarioAsync(usuarioId);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, resultado.StatusCode);
            Assert.AreEqual("Erro ao listar os Projetos: Erro ao listar projetos!", resultado.Mensagem);
        }

        [Test]
        public async Task ObterProjetoAsync_DeveRetornarOk()
        {
            // Arrange
            var projetoMock = new Projetos { Id = 1 };
            _projetosRepositoryMock.Setup(repo => repo.ObterProjetoAsync(1)).ReturnsAsync(projetoMock);
            _mapperMock.Setup(m => m.Map<ProjetoViewModel>(It.IsAny<Projetos>())).Returns(new ProjetoViewModel());

            // Act
            var resultado = await _projetosAppServices.ObterProjetoAsync(1);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.IsNotNull(resultado.Model);
        }

        [Test]
        public async Task ObterProjetoAsync_DeveRetornarErro()
        {
            // Arrange
            _projetosRepositoryMock.Setup(repo => repo.ObterProjetoAsync(1)).Throws(new Exception("Erro ao obter projeto"));

            // Act
            var resultado = await _projetosAppServices.ObterProjetoAsync(1);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, resultado.StatusCode);
            Assert.AreEqual("Erro ao obter o Projeto: Erro ao obter projeto!", resultado.Mensagem);
        }

        [Test]
        public async Task SalvarProjetoAsync_DeveRetornarSucesso()
        {
            // Arrange
            var projetoViewModel = new ProjetoViewModel { Id = 1, Titulo = "Novo Projeto" };
            var projetoMock = new Projetos { Id = 1, Titulo = "Novo Projeto" };
            _mapperMock.Setup(m => m.Map<Projetos>(projetoViewModel)).Returns(projetoMock);
            _projetosRepositoryMock.Setup(repo => repo.SalvarProjetoAsync(It.IsAny<Projetos>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _projetosAppServices.SalvarProjetoAsync(projetoViewModel);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual("Projeto cadastrado com sucesso!", resultado.Mensagem);
        }            

        [Test]
        public async Task AtualizarProjetoAsync_DeveRetornarSucesso()
        {
            // Arrange
            var projetoViewModel = new ProjetoViewModel { Id = 1, Titulo = "Projeto Atualizado" };
            var projetoMock = new Projetos { Id = 1, Titulo = "Projeto Atualizado" };
            _mapperMock.Setup(m => m.Map<Projetos>(projetoViewModel)).Returns(projetoMock);
            _projetosRepositoryMock.Setup(repo => repo.AtualizarProjetoAsync(It.IsAny<Projetos>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _projetosAppServices.AtualizarProjetoAsync(projetoViewModel);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual("Projeto atualizado com sucesso!", resultado.Mensagem);
        }

        [Test]
        public async Task AtualizarProjetoAsync_DeveRetornarErro()
        {
            // Arrange
            var model = new ProjetoViewModel { Id = 1, Titulo = "Projeto Atualizado" };

            // Simulando uma falha no repositório para que o erro ocorra
            _projetosRepositoryMock.Setup(repo => repo.AtualizarProjetoAsync(It.IsAny<Projetos>())).Throws(new NullReferenceException("Object reference not set to an instance of an object."));

            // Act
            var resultado = await _projetosAppServices.AtualizarProjetoAsync(model);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, resultado.StatusCode);
            Assert.AreEqual("Erro ao Atualizar o Projeto: Object reference not set to an instance of an object.!", resultado.Mensagem);
        }


        [Test]
        public async Task DeletarProjetoAsync_DeveRetornarSucesso()
        {
            // Arrange
            var projetoMock = new Projetos { Id = 1 };
            _tarefaRepositoryMock.Setup(t => t.ObterTarefaPeloProjetoId(1)).Returns(new List<Tarefa>());
            _projetosRepositoryMock.Setup(repo => repo.ObterProjetoAsync(1)).ReturnsAsync(projetoMock);
            _projetosRepositoryMock.Setup(repo => repo.DeletarProjetoAsync(projetoMock)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _projetosAppServices.DeletarProjetoAsync(1);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual("Projeto Excluído com sucesso!", resultado.Mensagem);
        }       
    }
}
