using PetLog.Dominio.Entidades;

namespace PetLog.Repositorio.Interfaces
{
    public interface ProdutoRepositorio
    {
        Task<int> Salvar (Produto produto);
        Task Atualizar (Produto produto);
        Task Deletar (Produto produto);
        Task <Compra> Obter (int produtoId);
        Task <Compra> ObterPorId (int produtoId);
        Task <IEnumerable<Produto>> Listar (bool ativo);
        Task<IEnumerable<Produto>> Busca (string filtro, bool ativo = true);

    }
}