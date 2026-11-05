using Microsoft.AspNetCore.Mvc;
using PetLog.Aplicacao;

namespace PetLog.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoAplicacao _produtoAplicacao;

        public ProdutoController(IProdutoAplicacao produtoAplicacao)
        {
            _produtoAplicacao = produtoAplicacao;
        }

        [HttpGet]
        [Route("Obter/{produtoId}")]
        public async Task<ActionResult> ObterPorId([FromRoute] int produtoId)
        {
            try
            {
                var produto = await _produtoAplicacao.ObterPorId(produtoId);
                return Ok(produto);
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
        public async Task<ActionResult> Criar([FromBody] ProdutoRequisicao produtoRequisicao)
        {
            try
            {
                var produtoId = await _produtoAplicacao.Criar(produtoRequisicao);
                return Ok(produtoId);
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
        public async Task<ActionResult> Atualizar([FromBody] ProdutoAtualizar produtoAtualizar)
        {
            try
            {
                await _produtoAplicacao.Atualizar(produtoAtualizar);
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
        [Route("Deletar/{produtoId}")]
        public async Task<ActionResult> Deletar([FromRoute] int produtoId)
        {
            try
            {
                await _produtoAplicacao.Deletar(produtoId);
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

        [HttpPut]
        [Route("Restaurar/{produtoId}")]
        public async Task<ActionResult> Restaurar([FromRoute] int produtoId)
        {
            try
            {
                await _produtoAplicacao.Restaurar(produtoId);
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
                var produtos = await _produtoAplicacao.Listar(ativo);
                return Ok(produtos);
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
                var retornoBusca = await _produtoAplicacao.Busca(filtro, ativo);
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