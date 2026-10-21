using PetLog.Dominio.Entidades;

namespace PetLog.Repositorio.Interfaces
{
    public interface IVendedorRepositorio
    {
        Task<int> Salvar (Vendedor vendedor);
        Task Atualizar (Vendedor vendedor);
        Task Deletar (Vendedor vendedor);
        Task <Vendedor> Obter(int vendedorId);
        Task<IEnumerable<Vendedor>> Listar (bool ativo);
        Task<IEnumerable<Vendedor>> Busca (string filtro, bool ativo = true);
    }
}