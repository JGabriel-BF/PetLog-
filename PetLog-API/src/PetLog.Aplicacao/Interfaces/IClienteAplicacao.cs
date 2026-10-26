using System.Collections.Generic;
using System.Threading.Tasks;
using PetLog.Dominio.Entidades;

namespace PetLog.Aplicacao
{
    public interface IClienteAplicacao
    {
        Task <int> Criar (Cliente cliente);
        Task Atualizar (Cliente cliente);
        Task Deletar (Cliente cliente);
        Task Restaurar (Cliente cliente);
        Task<IEnumerable<Cliente>> Listar (bool ativo);
        Task Obter (int clienteId);
        Task ObterPorId (int clienteId);
        Task <IEnumerable<Cliente>> Busca (string filtro, bool ativo = true);

    }
}