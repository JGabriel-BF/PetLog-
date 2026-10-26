using System.Collections.Generic;
using System.Threading.Tasks;
using PetLog.Dominio.Entidades;

namespace PetLog.Aplicacao
{
    public interface IVendedorAplicacao
    {
        Task <int> Criar (Vendedor vendedorDTO, int vendedorLoginId);
        Task Atualizar (Vendedor vendedorDTO, int vendedorLoginId);
        Task Deletar (int vendedorId, int vendedorLoginId);
        Task Restaurar (int vendedorId, int vendedorLoginId);
        Task <IEnumerable<Vendedor>> Listar(bool ativo, int vendedorLoginId);
        Task<Vendedor> Obter (int vendedorId, int vendedorLoginId);
        Task <Vendedor> ObterPorId (int vendedorId, int vendedorLoginId);
        Task <IEnumerable<Vendedor>> Busca (string filtro, int usuarioId, bool ativo = true);
        
    }
}