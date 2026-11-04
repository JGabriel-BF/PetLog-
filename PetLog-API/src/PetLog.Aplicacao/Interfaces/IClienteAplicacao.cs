using System.Collections.Generic;
using System.Threading.Tasks;
using PetLog.Aplicacao.DTOs;

namespace PetLog.Aplicacao
{
    public interface IClienteAplicacao
    {
        Task<int> Criar(ClienteRequisicao clienteRequisicao);
        Task Atualizar(ClienteAtualizar clienteAtualizar);
        Task Deletar(int clienteId);
        Task Restaurar(int clienteId);
        Task<IEnumerable<ClienteResposta>> Listar(bool ativo);
        Task<ClienteResposta> ObterPorId(int clienteId);
        Task<IEnumerable<ClienteResposta>> Busca(string filtro, bool ativo = true);
    }
}