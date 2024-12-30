using api.eclipse.controle.projetos.Application.Interfaces;
using api.eclipse.controle.projetos.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.eclipse.controle.projetos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {

        private readonly ITarefasAppServices _tarefasAppServices;

        public TarefasController(ITarefasAppServices tarefasAppServices)
        {
            _tarefasAppServices = tarefasAppServices;
        }


        [HttpPost("CriarTarefaAsync")]
        public async Task<IActionResult> CriarTarefaAsync(TarefaViewModel model)
        {
            var result = await _tarefasAppServices.SalvarTarefaAsync(model);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpGet("ObterTarefaAsync")]
        public async Task<IActionResult> ObterTarefaAsync(int id)
        {
            var result = await _tarefasAppServices.ObterTarefaAsync(id);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result.Model);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpGet("ListarTarefaAsync")]
        public async Task<IActionResult> ListarTarefaAsync()
        {
            var result = await _tarefasAppServices.ListarTarefasAsync();
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpGet("ListarTarefaPorProjetoAsync")]
        public async Task<IActionResult> ListarTarefaPorProjetoAsync(int projetoId)
        {
            var result = await _tarefasAppServices.ListarTarefasAsync();
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpGet("GerarRelatorioDesempenhoAsync")]
        public async Task<IActionResult> GerarRelatorioDesempenhoAsync()
        {
            var result = await _tarefasAppServices.GerarRelatorioDesempenhoAsync();
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpPost("EditarTarefaAsync")]
        public async Task<IActionResult> EditarTarefaAsync(TarefaViewModel model)
        {
            var result = await _tarefasAppServices.AtualizarTarefaAsync(model);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpDelete("DeletarTarefaAsync")]
        public async Task<IActionResult> DeletarTarefaAsync(int id)
        {
            var result = await _tarefasAppServices.DeletarTarefaAsync(id);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

    }
}
