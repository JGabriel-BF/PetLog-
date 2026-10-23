using PetLog.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using PetLog.Repositorio.Interfaces;

namespace PetLog.Repositorio
{
    public class ProdutoRepositorio : BaseRepositorio, IProdutoRepositorio
    {
        public ProdutoRepositorio(PetLogContexto contexto) : base(contexto)
        {
            

        }

        public async Task <int> Salvar (Produto produto)
        {
            _contexto.Produtos.Add(produto);
            _contexto.SaveChangesAsync();
            return produto.Id;
        }

        public async Task Atualizar (Produto produto)
        {
            _contexto.Produtos.Update(produto);
            await _contexto.SaveChangesAsync();
        }

        public async Task Deletar (Produto produto)
        {
            produto.Ativo = false;
            _contexto.Produtos.Update(produto);
            await _contexto.SaveChangesAsync();
        }
        
        public async Task <IEnumerable<Produto>> Listar (bool ativo)
        {
            return await _contexto.Produtos.Where(U => U.Ativo == ativo).ToListAsync();
        }

        public async Task <Produto> Obter (int produtoId)
        {
            return await _contexto.Produtos.Where(u => u.Id == produtoId && u.Ativo).FirstOrDefaultAsync();
        }

        public async Task <Produto> ObterPorId(int produtoId)
        {
            return await _contexto.Produtos.Where(u => u.Id == produtoId).FirstOrDefaultAsync();
        }

        public async Task <IEnumerable<Produto>> Busca (string filtro, bool ativo = true)
        {
            var resultadoBusca = _contexto.Produtos.Where(produto => produto.Ativo == ativo);

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroLower = filtro.ToLower();
                resultadoBusca = resultadoBusca.Where(produto =>
                produto.Nome.ToLower().Contains(filtroLower)
                );
            }
            return await resultadoBusca.ToListAsync();
        }
    }
}
