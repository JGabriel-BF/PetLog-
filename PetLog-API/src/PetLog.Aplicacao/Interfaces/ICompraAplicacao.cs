using System.Collections.Generic;
using System.Threading.Tasks;
using PetLog.Dominio.Entidades;

namespace PetLog.Aplicacao
{
    public interface ICompraAplicacao
    {
        Task <int> Criar (Compra compra);
        Task Atualizar (Compra compra);
        Task Deletar (Compra compra);
        Task Restaurar (Compra compra);
        Task<IEnumerable<Compra>> Listar (bool ativo);
        Task Obter (int compraId);
        Task ObterPorId (int compraId);
        Task <IEnumerable<Compra>> Busca (string filtro, bool ativo = true);
        Task ConcluirCompra (Compra compra);

    }
}