using Microsoft.AspNetCore.Mvc;
using PetLog.Aplicacao;

namespace PetLog.Api;

[ApiController]
[Route("[controller]")]

public class ClienteController : ControllerBase
{
    private readonly IClienteAplicacao _clienteAplicacao;

    public ClienteController(IClienteAplicacao clienteAplicacao)
    {
        _clienteAplicacao = clienteAplicacao;
    }

    [HttpGet]
    [Route ("obter/{clienteId}")]
    public async Task <ActionResult> ObterPorId ([FromRoute] int clienteId)
    {
        try
        {
            var cliente = await _clienteAplicacao.ObterPorId(clienteId);
            return Ok (cliente); 
        }

        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode (500,ex.Message);
        }   
    }

    [HttpPost]
    [Route("Criar")]
    public async Task <ActionResult> Criar ([FromBody] ClienteRequisicao clienteRequisicao)
    {
        try
        {
            var clienteId = await _clienteAplicacao.Criar(clienteRequisicao);
            return Ok (clienteId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode (500,ex.Message);
        }
    }

    [HttpPut]
    [Route("Atualizar")]
    public async Task <ActionResult> Atualizar ([FromBody] ClienteAtualizar clienteAtualizar)
    {
        try
        {
            await _clienteAplicacao.Atualizar(clienteAtualizar);
            return Ok ();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode (500,ex.Message);
        }
    }

    [HttpDelete]
    [Route("Deletar/{clienteId}")]
    public async Task <ActionResult> Deletar ([FromRoute] int clienteId)
    {
        try
        {
            await _clienteAplicacao.Deletar(clienteId);
            return Ok ();
        }

        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode (500,ex.Message);
        }
    }

    [HttpPut]
    [Route("Restaurar/{clienteId}")]
    public async Task <ActionResult> Restaurar ([FromRoute] int clienteId)
    {
        try
        {
            await _clienteAplicacao.Restaurar(clienteId);
            return Ok ();
        }

        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode (500,ex.Message);
        }
    }

    [HttpGet]
    [Route ("Listar")]
    public async Task <ActionResult> Listar ([FromQuery] bool ativo = true)
    {
        try
        {
            var clientes = await _clienteAplicacao.Listar(ativo);
            return Ok (clientes);
        }

        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode (500,ex.Message);
        }
    }

    [HttpGet]
    [Route("Busca")]
    public async Task <ActionResult> Busca ([FromQuery] string filtro, [FromQuery] bool ativo = true)
    {
        try
        {
            var retornoBusca = await _clienteAplicacao.Busca(filtro, ativo);
            return Ok (retornoBusca);
        }

        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode (500,ex.Message);
        }
    }
}