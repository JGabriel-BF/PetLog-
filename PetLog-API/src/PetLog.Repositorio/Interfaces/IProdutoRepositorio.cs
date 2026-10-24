using PetLog.Dominio.Entidades;

namespace PetLog.Repositorio.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<int> Salvar (Produto produto);
        Task Atualizar (Produto produto);
        Task Deletar (Produto produto);
        Task <Produto> Obter (int produtoId);
        Task <Produto> ObterPorId (int produtoId);
        Task <IEnumerable<Produto>> Listar (bool ativo);
        Task <IEnumerable<Produto>> Busca (string filtro, bool ativo = true);
    }
}