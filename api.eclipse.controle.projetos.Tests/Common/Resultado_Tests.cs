using api.eclipse.controle.projetos.Common;
using System.Net;

namespace api.eclipse.controle.projetos.Tests.Common
{
    [TestFixture]
    public class Resultado_Tests
    {
        [Test]
        public void SucessoMensagem_DeveRetornarStatusCodeOk()
        {
            // Arrange
            var mensagem = "Operação bem-sucedida";

            // Act
            var resultado = Resultado<string>.SucessoMensagem(mensagem);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, resultado.StatusCode);
            Assert.AreEqual(mensagem, resultado.Mensagem);
        }

        [Test]
        public void InformacaoMensagem_DeveRetornarStatusCodeBadRequest()
        {
            // Arrange
            var mensagem = "Informação recebida com problemas";

            // Act
            var resultado = Resultado<string>.InformacaoMensagem(mensagem);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, resultado.StatusCode);
            Assert.AreEqual(mensagem, resultado.Mensagem);
        }

        [Test]
        public void ErroMensagem_DeveRetornarStatusCodeInternalServerError()
        {
            // Arrange
            var mensagem = "Erro interno no servidor";

            // Act
            var resultado = Resultado<string>.ErroMensagem(mensagem);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, resultado.StatusCode);
            Assert.AreEqual(mensagem, resultado.Mensagem);
        }

        [Test]
        public void Resultado_DevePossuirModeloCorreto()
        {
            // Arrange
            var modeloEsperado = "Modelo de Teste";
            var resultado = new Resultado<string>
            {
                StatusCode = HttpStatusCode.OK,
                Mensagem = "Operação bem-sucedida",
                Model = modeloEsperado
            };

            // Act & Assert
            Assert.AreEqual(modeloEsperado, resultado.Model);
        }
              
        [Test]
        public void Resultado_DeveFuncionarComDiferentesTiposDeModelo()
        {
            // Arrange
            var mensagem = "Operação de sucesso";
            var resultadoString = Resultado<string>.SucessoMensagem(mensagem);
            var resultadoInt = Resultado<int>.SucessoMensagem(mensagem);

            // Act & Assert
            Assert.AreEqual(HttpStatusCode.OK, resultadoString.StatusCode);
            Assert.AreEqual(mensagem, resultadoString.Mensagem);
            Assert.AreEqual(HttpStatusCode.OK, resultadoInt.StatusCode);
            Assert.AreEqual(mensagem, resultadoInt.Mensagem);
        }
    }
}
