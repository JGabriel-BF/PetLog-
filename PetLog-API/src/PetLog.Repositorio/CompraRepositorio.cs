using PetLog.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using PetLog.Repositorio.Interfaces;

namespace PetLog.Repositorio
{
    public class CompraRepositorio : BaseRepositorio, ICompraRepositorio
    {
        
        public CompraRepositorio(PetLogContexto contexto) : base(contexto)
        {
            

        }

        public async Task <int> Salvar (Compra compra)
        {
            _contexto.Compras.Add(compra);
            _contexto.SaveChangesAsync();
            return compra.Id;
        }

        public async Task Atualizar (Compra compra)
        {
            _contexto.Compras.Update(compra);
            await _contexto.SaveChangesAsync();
        }

        public async Task Deletar (Compra compra)
        {
            compra.Ativo = false;
            _contexto.Compras.Update(compra);
            await _contexto.SaveChangesAsync();
        }

        public async Task <Compra> Obter (int compraId)
        {
            return await _contexto.Compras.Where(u => u.Id == compraId && u.Ativo).FirstOrDefaultAsync();
        } 

        public async Task <Compra> ObterPorId (int compraId)
        {
            return await _contexto.Compras.Where(u => u.Id == compraId).FirstOrDefaultAsync();
        }

        public async Task <IEnumerable<Compra>> Listar (bool ativo)
        {
            return await _contexto.Compras.Where(U => U.Ativo ==ativo).ToListAsync();
        }

        public async Task<IEnumerable<Compra>> Busca (string filtro, bool ativo = true)
        {
            var resultadoBusca = _contexto.Compras.Where(compra => compra.Ativo == ativo);

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroLower = filtro.ToLower();
                resultadoBusca = resultadoBusca.Where(compra =>
                compra.Cliente.ToString().ToLower().Contains(filtroLower)
                );
            }

            return await resultadoBusca.ToListAsync();
        }

        public async Task ConcluirCompra (Compra compra)
        {
            compra.Concluido = true;
            _contexto.Update(compra);
            await _contexto.SaveChangesAsync();
        }
    }

}