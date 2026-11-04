using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetLog.Aplicacao
{
    public interface IProdutoAplicacao
    {
        Task<int> Criar(ProdutoRequisicao produtoRequisicao);
        Task Atualizar(ProdutoAtualizar produtoAtualizar);
        Task Deletar(int produtoId);
        Task Restaurar(int produtoId);
        Task<IEnumerable<ProdutoResposta>> Listar(bool ativo);
        Task<ProdutoResposta> ObterPorId(int produtoId);
        Task<IEnumerable<ProdutoResposta>> Busca(string filtro, bool ativo = true);
    }
}