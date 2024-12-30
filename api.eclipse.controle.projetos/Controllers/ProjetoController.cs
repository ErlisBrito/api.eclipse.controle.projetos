using api.eclipse.controle.projetos.Application.Interfaces;
using api.eclipse.controle.projetos.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.eclipse.controle.projetos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetosAppServices _projetosAppServices;
        public ProjetoController(IProjetosAppServices projetosAppServices)
        {
            _projetosAppServices = projetosAppServices;
        }

        [HttpGet("ListarProjetosAsync")]
        public async Task<IActionResult> ListarProjetosAsync()
        {
            var result = await _projetosAppServices.ListarProjetosAsync();
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpGet("ListarProjetosPorUsuarioAsync")]
        public async Task<IActionResult> ListarProjetosPorUsuarioAsync(int usuarioId)
        {
            var result = await _projetosAppServices.ListarProjetosPorUsuarioAsync(usuarioId);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpPost("CriarProjetoAsync")]
        public async Task<IActionResult> CriarProjetoAsync(ProjetoViewModel model)
        {
            var result = await _projetosAppServices.SalvarProjetoAsync(model);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result.Mensagem);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpGet("ObterProjetosAsync")]
        public async Task<IActionResult> ObterProjetosAsync(int id)
        {
            var result = await _projetosAppServices.ObterProjetoAsync(id);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result.Model);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpPost("EditarProjetosAsync")]
        public async Task<IActionResult> EditarProjetosAsync(ProjetoViewModel model)
        {
            var result = await _projetosAppServices.AtualizarProjetoAsync(model);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result.Mensagem);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }

        [HttpDelete("DeletarProjetosAsync")]
        public async Task<IActionResult> DeletarProjetosAsync(int id)
        {
            var result = await _projetosAppServices.DeletarProjetoAsync(id);
            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                return Ok(result.Mensagem);
            }
            return StatusCode((int)result.StatusCode, new { Mensagem = result.Mensagem });
        }
    }
}
