using PetLog.Dominio.Entidades;

namespace PetLog.Repositorio.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<int> Salvar (Cliente cliente);
        Task Atualizar (Cliente cliente);
        Task <Cliente> Obter (int clienteId);
        Task Deletar (Cliente cliente);
        Task <IEnumerable<Cliente>> Listar (bool ativo);
        Task <IEnumerable<Cliente>> Busca (string filtro, bool ativo = true);
    }
}