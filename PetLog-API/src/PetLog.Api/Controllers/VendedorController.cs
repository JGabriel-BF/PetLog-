using Microsoft.AspNetCore.Mvc;
using PetLog.Aplicacao;

namespace PetLog.Api;

[ApiController]
[Route("[controller]")]

public class VendedorController : ControllerBase
{
    private readonly IVendedorAplicacao _vendedorAplicacao;

    public VendedorController(IVendedorAplicacao vendedorAplicacao)
    {
        _vendedorAplicacao = vendedorAplicacao;
    }

    [HttpGet]
    [Route("obter/{vendedorId}")]
    public async Task<ActionResult> ObterPorId ([FromRoute] int vendedorId)
    {
        try
        {
            var vendedor = await _vendedorAplicacao.ObterPorId(vendedorId);
            return Ok(vendedor);
        }

        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("obterporemail")]
    public async Task<ActionResult> ObterPorEmail([FromQuery] string email)
    {
        try
        {
            var vendedor = await _vendedorAplicacao.ObterPorEmail(email);
            return Ok(vendedor);
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
    public async Task<ActionResult>Criar([FromBody] VendedorRequisicao vendedorRequisicao)
    {
        try
        {
            var vendedorId = await _vendedorAplicacao.Criar(vendedorRequisicao);
            return Ok (vendedorId);
        }

        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized (ex.Message);
        }
        
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Atualizar")]
    public async Task <ActionResult> Atualizar ([FromBody] VendedorAtualizar vendedorAtualizar)
    {
        try
        {
            await _vendedorAplicacao.Atualizar(vendedorAtualizar);
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
    [Route("Deletar/{vendedorId}")]
    public async Task <ActionResult> Deletar ([FromRoute] int vendedorId)
    {
        try
        {
            await _vendedorAplicacao.Deletar(vendedorId);
            return Ok ();
        }

        catch (ArgumentException ex)
        {
            return BadRequest (ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }

    [HttpPut]
    [Route("Restaurar/{vendedorId}")]
    public async Task <ActionResult> Restaurar ([FromRoute] int vendedorId)
    {
        try
        {
            await _vendedorAplicacao.Restaurar (vendedorId);
            return Ok ();
        }

        catch (ArgumentException ex)
        {
            return BadRequest (ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode (500, ex.Message);
        }
    }

    [HttpGet]
    [Route("Listar")]
    public async Task <ActionResult> Listar ([FromQuery] bool ativo)
    {
        try
        {
            var vendedores = await _vendedorAplicacao.Listar(ativo);
            return Ok (vendedores);
        }

        catch (ArgumentException ex)
        {
            return BadRequest (ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("Busca")]
    public async Task <ActionResult> Buscar ([FromQuery] string filtro, [FromQuery] bool ativo = true)
    {
        try
        {
            var retornoBusca = await _vendedorAplicacao.Busca(filtro);
            return Ok (retornoBusca);
        }

        catch (ArgumentException ex)
        {
            return BadRequest (ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode (500, ex.Message);
        }
    }
}

