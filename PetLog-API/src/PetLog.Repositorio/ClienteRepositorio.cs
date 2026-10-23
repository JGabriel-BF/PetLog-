using PetLog.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using PetLog.Repositorio.Interfaces;

namespace PetLog.Repositorio
{
    public class ClienteRepositorio : BaseRepositorio, IClienteRepositorio
    {
        public ClienteRepositorio(PetLogContexto contexto) : base(contexto)
        {
            

        }

        public async Task <int> Salvar (Cliente cliente)
        {
            _contexto.Clientes.Add(cliente);
            _contexto.SaveChangesAsync();
            return cliente.Id;
        }

        public async Task Atualizar (Cliente cliente)
        {
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();
        }

        public async Task Deletar (Cliente cliente)
        {
            cliente.Ativo = false;
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();
        }

        public async Task <Cliente> Obter (int clienteId)
        {
            return await _contexto.Clientes.Where(u => u.Id == clienteId && u.Ativo).FirstOrDefaultAsync();
        }

        public async Task <Cliente> ObterPorId (int clienteId)
        {
            return await _contexto.Clientes.Where(u => u.Id == clienteId).FirstOrDefaultAsync();
        }

        public async Task <IEnumerable<Cliente>> Listar (bool ativo)
        {
            return await _contexto.Clientes.Where(U => U.Ativo == ativo).ToListAsync();
        }

        public async Task <IEnumerable<Cliente>> Busca (string filtro, bool ativo = true)
        {
            var resultadoBusca = _contexto.Clientes.Where(cliente => cliente.Ativo == ativo);

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroLower = filtro.ToLower();
                resultadoBusca = resultadoBusca.Where(cliente =>
                cliente.Nome.ToLower().Contains(filtroLower) || cliente.SobreNome.ToLower().Contains(filtroLower)
                );
            }
            
            return await resultadoBusca.ToListAsync();
        }
    }
}