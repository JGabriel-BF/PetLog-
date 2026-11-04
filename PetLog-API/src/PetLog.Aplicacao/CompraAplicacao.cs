using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetLog.Repositorio.Interfaces;
using PetLog.Dominio.Entidades;

namespace PetLog.Aplicacao
{
    public class CompraAplicacao : ICompraAplicacao
    {
        readonly ICompraRepositorio _compraRepositorio;
        readonly IProdutoRepositorio _produtoRepositorio;
        readonly IClienteRepositorio _clienteRepositorio;
        readonly IVendedorRepositorio _vendedorRepositorio;

        public CompraAplicacao(
            ICompraRepositorio compraRepositorio,
            IProdutoRepositorio produtoRepositorio,
            IClienteRepositorio clienteRepositorio,
            IVendedorRepositorio vendedorRepositorio)
        {
            _compraRepositorio = compraRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _vendedorRepositorio = vendedorRepositorio;
        }

        public async Task<int> Criar(CompraRequisicao compraRequisicao)
        {
            if (compraRequisicao == null)
                throw new ArgumentException("Compra não pode ser vazia!");

            ValidarDadosCompra(compraRequisicao.ClienteId, compraRequisicao.VendedorId,
                compraRequisicao.ProdutoIds, compraRequisicao.Quantidade, compraRequisicao.DataCompra);

            await ValidarClienteEVendedor(compraRequisicao.ClienteId, compraRequisicao.VendedorId);

            var produtos = await BuscarProdutosNoBanco(compraRequisicao.ProdutoIds);

            var compra = new Compra()
            {
                ClienteId = compraRequisicao.ClienteId,
                VendedorId = compraRequisicao.VendedorId,
                Produtos = produtos,
                Quantidade = compraRequisicao.Quantidade,
                DataCompra = compraRequisicao.DataCompra,
                Valor = CalcularValor(produtos, compraRequisicao.Quantidade)
            };

            return await _compraRepositorio.Salvar(compra);
        }

        public async Task Atualizar(CompraAtualizar compraAtualizar)
        {
            if (compraAtualizar == null)
                throw new ArgumentException("Compra não pode ser vazia!");

            if (compraAtualizar.Id <= 0)
                throw new ArgumentException("Id da compra inválido!");

            ValidarDadosCompra(compraAtualizar.ClienteId, compraAtualizar.VendedorId,
                compraAtualizar.ProdutoIds, compraAtualizar.Quantidade, compraAtualizar.DataCompra);

            var compra = await _compraRepositorio.ObterPorId(compraAtualizar.Id);

            if (compra == null)
                throw new ArgumentException("Compra não encontrada!");

            if (compra.Concluido)
                throw new InvalidOperationException("Compra concluída não pode ser alterada!");

            await ValidarClienteEVendedor(compraAtualizar.ClienteId, compraAtualizar.VendedorId);

            var produtos = await BuscarProdutosNoBanco(compraAtualizar.ProdutoIds);

            compra.ClienteId = compraAtualizar.ClienteId;
            compra.VendedorId = compraAtualizar.VendedorId;
            compra.Produtos = produtos;
            compra.Quantidade = compraAtualizar.Quantidade;
            compra.DataCompra = compraAtualizar.DataCompra;
            compra.Valor = CalcularValor(produtos, compraAtualizar.Quantidade);

            await _compraRepositorio.Atualizar(compra);
        }

        public async Task Deletar(int compraId)
        {
            if (compraId <= 0)
                throw new ArgumentException("Id da compra inválido!");

            var compra = await _compraRepositorio.ObterPorId(compraId);

            if (compra == null)
                throw new ArgumentException("Compra não encontrada!");

            if (compra.Concluido)
                throw new InvalidOperationException("Compra concluída não pode ser excluída!");

            await _compraRepositorio.Deletar(compra);
        }

        public async Task Restaurar(int compraId)
        {
            if (compraId <= 0)
                throw new ArgumentException("Id da compra inválido!");

            var compra = await _compraRepositorio.ObterPorId(compraId);

            if (compra == null)
                throw new ArgumentException("Compra não encontrada!");

            compra.Ativo = true;

            await _compraRepositorio.Atualizar(compra);
        }

        public async Task ConcluirCompra(int compraId)
        {
            if (compraId <= 0)
                throw new ArgumentException("Id da compra inválido!");

            var compra = await _compraRepositorio.ObterPorId(compraId);

            if (compra == null)
                throw new ArgumentException("Compra não encontrada!");

            if (compra.Concluido)
                throw new InvalidOperationException("Compra já está concluída!");

            if (compra.Produtos == null || !compra.Produtos.Any())
                throw new InvalidOperationException("Compra sem produtos!");

            foreach (var produto in compra.Produtos)
            {
                if (produto.QuantidadeEstoque < compra.Quantidade)
                    throw new InvalidOperationException($"Estoque insuficiente para o produto {produto.Nome}!");
            }

            foreach (var produto in compra.Produtos)
            {
                produto.QuantidadeEstoque -= compra.Quantidade;
                await _produtoRepositorio.Atualizar(produto);
            }

            await _compraRepositorio.ConcluirCompra(compra);
        }

        public async Task<CompraResposta> ObterPorId(int compraId)
        {
            if (compraId <= 0)
                throw new ArgumentException("Id da compra inválido!");

            var compra = await _compraRepositorio.ObterPorId(compraId);

            if (compra == null)
                throw new ArgumentException("Compra não encontrada!");

            return ConverterParaResposta(compra);
        }

        public async Task<IEnumerable<CompraResposta>> Listar(bool ativo)
        {
            var compras = await _compraRepositorio.Listar(ativo);

            return compras.Select(c => ConverterParaResposta(c)).ToList();
        }

        public async Task<IEnumerable<CompraResposta>> Busca(string filtro, bool ativo = true)
        {
            var compras = await _compraRepositorio.Busca(filtro, ativo);

            return compras.Select(c => ConverterParaResposta(c)).ToList();
        }

        #region util
        private static void ValidarDadosCompra(int clienteId, int vendedorId, List<int> produtoIds, int quantidade, DateTime dataCompra)
        {
            if (clienteId <= 0)
                throw new ArgumentException("Para fazer uma compra é necessário ter um cliente!");

            if (vendedorId <= 0)
                throw new ArgumentException("Para fazer uma compra é necessário ter um vendedor!");

            if (produtoIds == null || !produtoIds.Any())
                throw new ArgumentException("A compra precisa ter pelo menos um produto!");

            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero!");

            if (dataCompra == default)
                throw new ArgumentException("A data da compra é obrigatória!");

            if (dataCompra.Date > DateTime.Now.Date)
                throw new ArgumentException("A data da compra não pode ser no futuro!");
        }

        private async Task ValidarClienteEVendedor(int clienteId, int vendedorId)
        {
            var cliente = await _clienteRepositorio.ObterPorId(clienteId);

            if (cliente == null || !cliente.Ativo)
                throw new ArgumentException("Cliente não encontrado!");

            var vendedor = await _vendedorRepositorio.ObterPorId(vendedorId);

            if (vendedor == null || !vendedor.Ativo)
                throw new ArgumentException("Vendedor não encontrado!");
        }

        private async Task<List<Produto>> BuscarProdutosNoBanco(List<int> produtoIds)
        {
            var produtos = new List<Produto>();

            foreach (var produtoId in produtoIds.Distinct())
            {
                var produto = await _produtoRepositorio.Obter(produtoId);

                if (produto == null)
                    throw new ArgumentException($"Produto {produtoId} não encontrado!");

                produtos.Add(produto);
            }

            return produtos;
        }

        private static decimal CalcularValor(List<Produto> produtos, int quantidade)
        {
            return produtos.Sum(p => p.Preco) * quantidade;
        }

        private static CompraResposta ConverterParaResposta(Compra compra)
        {
            return new CompraResposta()
            {
                Id = compra.Id,
                ClienteId = compra.ClienteId,
                ClienteNome = compra.Cliente == null ? string.Empty : $"{compra.Cliente.Nome} {compra.Cliente.SobreNome}",
                VendedorId = compra.VendedorId,
                VendedorNome = compra.Vendedor == null ? string.Empty : compra.Vendedor.Nome,
                Produtos = (compra.Produtos ?? new List<Produto>())
                    .Select(p => new CompraProdutoResposta()
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Preco = p.Preco
                    }).ToList(),
                Quantidade = compra.Quantidade,
                DataCompra = compra.DataCompra,
                Valor = compra.Valor,
                Concluido = compra.Concluido,
                Ativo = compra.Ativo
            };
        }
        #endregion
    }
}
