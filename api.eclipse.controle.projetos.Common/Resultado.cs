using System.Net;

namespace api.eclipse.controle.projetos.Common
{
    public class Resultado<T>
    {
        public T Model { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Mensagem { get; set; }

        public static Resultado<T> SucessoMensagem(string mensagem)
        {
            return new Resultado<T>() { StatusCode = HttpStatusCode.OK, Mensagem = mensagem };
        }

        public static Resultado<T> InformacaoMensagem(string mensagem)
        {
            return new Resultado<T>() { StatusCode = HttpStatusCode.BadRequest, Mensagem = mensagem };
        }

        public static Resultado<T> ErroMensagem(string mensagem)
        {
            return new Resultado<T>() { StatusCode = HttpStatusCode.InternalServerError, Mensagem = mensagem };
        }

    }

}
