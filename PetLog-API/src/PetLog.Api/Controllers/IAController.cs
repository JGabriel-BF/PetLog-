using Microsoft.AspNetCore.Mvc;
using PetLog.Aplicacao;

namespace PetLog.Api
{
    [ApiController]
    [Route("[controller]")]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost]
        [Route("Completar")]
        public async Task<ActionResult> Completar([FromBody] string prompt)
        {
            try
            {
                var resposta = await _aiService.GerarRespostaAsync(prompt);
                return Ok(resposta);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}