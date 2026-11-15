using Microsoft.AspNetCore.Mvc;
using PetLog.Aplicacao;

namespace PetLog.Api
{
    [ApiController]
    [Route("[controller]")]
    public class SugestaoController : ControllerBase
    {
        private readonly ISugestaoAplicacao _sugestaoAplicacao;

        public SugestaoController(ISugestaoAplicacao sugestaoAplicacao)
        {
            _sugestaoAplicacao = sugestaoAplicacao;
        }

        [HttpGet]
        [Route("Cliente/{clienteId}")]
        public async Task<ActionResult> GerarParaCliente([FromRoute] int clienteId)
        {
            try
            {
                var sugestao = await _sugestaoAplicacao.GerarParaCliente(clienteId);
                return Ok(sugestao);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
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