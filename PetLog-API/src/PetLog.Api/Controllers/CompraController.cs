using Microsoft.AspNetCore.Mvc;
using PetLog.Aplicacao;

namespace PetLog.Api
{
    [ApiController]
    [Route("[controller]")]
    public class CompraController : ControllerBase
    {
        private readonly ICompraAplicacao _compraAplicacao;

        public CompraController(ICompraAplicacao compraAplicacao)
        {
            _compraAplicacao = compraAplicacao;
        }

        [HttpGet]
        [Route("Obter/{compraId}")]
        public async Task<ActionResult> ObterPorId([FromRoute] int compraId)
        {
            try
            {
                var compra = await _compraAplicacao.ObterPorId(compraId);
                return Ok(compra);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> Criar([FromBody] CompraRequisicao compraRequisicao)
        {
            try
            {
                var compraId = await _compraAplicacao.Criar(compraRequisicao);
                return Ok(compraId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> Atualizar([FromBody] CompraAtualizar compraAtualizar)
        {
            try
            {
                await _compraAplicacao.Atualizar(compraAtualizar);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Concluir/{compraId}")]
        public async Task<ActionResult> ConcluirCompra([FromRoute] int compraId)
        {
            try
            {
                await _compraAplicacao.ConcluirCompra(compraId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{compraId}")]
        public async Task<ActionResult> Deletar([FromRoute] int compraId)
        {
            try
            {
                await _compraAplicacao.Deletar(compraId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Restaurar/{compraId}")]
        public async Task<ActionResult> Restaurar([FromRoute] int compraId)
        {
            try
            {
                await _compraAplicacao.Restaurar(compraId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult> Listar([FromQuery] bool ativo = true)
        {
            try
            {
                var compras = await _compraAplicacao.Listar(ativo);
                return Ok(compras);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("Busca")]
        public async Task<ActionResult> Buscar([FromQuery] string filtro, [FromQuery] bool ativo = true)
        {
            try
            {
                var retornoBusca = await _compraAplicacao.Busca(filtro, ativo);
                return Ok(retornoBusca);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}