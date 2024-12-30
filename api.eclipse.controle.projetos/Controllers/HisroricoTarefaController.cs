using api.eclipse.controle.projetos.Application.Interfaces;
using api.eclipse.controle.projetos.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.eclipse.controle.projetos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HisroricoTarefaController : ControllerBase
    {
        private readonly IHistoricoTarefaAppServices _historicoTarefaAppServices;

        public HisroricoTarefaController(IHistoricoTarefaAppServices historicoTarefaAppServices)
        {
            _historicoTarefaAppServices = historicoTarefaAppServices;
        }

        [HttpPost("SalvarComentariosTarefaAsync")]
        public async Task<IActionResult> SalvarComentariosTarefaAsync(HistoricoTarefaViewModel model)
        {
            var result = await _historicoTarefaAppServices.SalvarComentariosTarefaAsync(model , "Salvando Comentario");
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }
    }
}
