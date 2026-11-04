using System.Collections.Generic;
using System.Threading.Tasks;
using PetLog.Aplicacao.DTOs;

namespace PetLog.Aplicacao
{
    public interface IVendedorAplicacao
    {
        Task<int> Criar(VendedorRequisicao vendedorRequisicao);
        Task Atualizar(VendedorAtualizar vendedorAtualizar);
        Task Deletar(int vendedorId);
        Task Restaurar(int vendedorId);
        Task<IEnumerable<VendedorRespostaDTO>> Listar(bool ativo);
        Task<VendedorRespostaDTO> ObterPorEmail(string email);
        Task<VendedorRespostaDTO> ObterPorId(int vendedorId);
        Task<IEnumerable<VendedorRespostaDTO>> Busca(string filtro, bool ativo = true);
    }
}