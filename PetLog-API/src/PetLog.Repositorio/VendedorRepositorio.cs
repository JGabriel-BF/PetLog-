using PetLog.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using PetLog.Repositorio.Interfaces;

namespace PetLog.Repositorio
{
public class VendedorRepositorio : BaseRepositorio, IVendedorRepositorio
{

    public VendedorRepositorio(PetLogContexto contexto) : base(contexto)
    {
        
    }

    public async Task<int> Salvar (Vendedor vendedor)
    {
        _contexto.Vendedores.Add(vendedor);
        _contexto.SaveChangesAsync();
        return vendedor.Id;
    }

    public async Task Atualizar (Vendedor vendedor)
    {
        _contexto.Vendedores.Update(vendedor);
        await _contexto.SaveChangesAsync();
    }

    public async Task Deletar (Vendedor vendedor)
    {
        vendedor.Ativo = false;
        _contexto.Vendedores.Update(vendedor);
        await _contexto.SaveChangesAsync();
    }

    public async Task<Vendedor> Obter (int vendedorId)
    {
        return await _contexto.Vendedores.Where(u => u.Id == vendedorId && u.Ativo).FirstOrDefaultAsync();
    }

    public async Task <Vendedor> ObterPorId(int vendedorId)
    {
        return await _contexto.Vendedores.Where(u => u.Id == vendedorId)
                                    .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Vendedor>> Listar (bool ativo)
    {
        return await _contexto.Vendedores.Where(U => U.Ativo == ativo).ToListAsync();
    }

    public async Task<IEnumerable<Vendedor>> Busca(string filtro, bool ativo = true)
    {
        var resultadoBusca = _contexto.Vendedores.Where(vendedor => vendedor.Ativo == ativo);
        
        if (!string.IsNullOrWhiteSpace(filtro))
        {
            var filtroLower = filtro.ToLower();
            resultadoBusca = resultadoBusca.Where(vendedor =>
            vendedor.Nome.ToLower().Contains(filtroLower) || vendedor.Email.ToLower().Contains(filtroLower)
            );
        }

        return await resultadoBusca.ToListAsync();
    }
}
}