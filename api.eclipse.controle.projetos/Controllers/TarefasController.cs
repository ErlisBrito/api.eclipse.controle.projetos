using Microsoft.AspNetCore.Mvc;

namespace api.eclipse.controle.projetos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {

        [HttpPost("CriarTarefaAsync")]
        public async Task<IActionResult> CriarTarefaAsync()
        {
            return Ok();
        }
    }
}
