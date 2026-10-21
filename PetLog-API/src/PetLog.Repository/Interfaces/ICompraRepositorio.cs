using PetLog.Dominio.Entidades;

namespace PetLog.Repositorio.Interfaces
{
    public interface ICompraRepositorio
    {
        Task<int> Salvar (Compra compra);
        Task Atualizar (Compra compra);
        Task Deletar (Compra compra);
        Task <IEnumerable<Compra>> Listar (bool ativo);
        Task<IEnumerable<Compra>> Busca (string filtro, bool ativo = true);
        Task ConcluirCompra (Compra compra);
    }
}