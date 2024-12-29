using Microsoft.AspNetCore.Mvc;

namespace api.eclipse.controle.projetos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjetoController : ControllerBase
    {


        [HttpGet("CriarProjetoAsync")]
        public async Task<IActionResult> CriarProjetoAsync()
        {
            return Ok();
        }
    }
}
