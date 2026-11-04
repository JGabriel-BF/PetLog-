using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetLog.Aplicacao
{
    public interface ICompraAplicacao
    {
        Task<int> Criar(CompraRequisicao compraRequisicao);
        Task Atualizar(CompraAtualizar compraAtualizar);
        Task Deletar(int compraId);
        Task Restaurar(int compraId);
        Task ConcluirCompra(int compraId);
        Task<IEnumerable<CompraResposta>> Listar(bool ativo);
        Task<CompraResposta> ObterPorId(int compraId);
        Task<IEnumerable<CompraResposta>> Busca(string filtro, bool ativo = true);
    }
}