using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetLog.Repositorio.Interfaces;
using PetLog.Dominio.Entidades;
using PetLog.Dominio.Enumeradores;

namespace PetLog.Aplicacao
{
    public class ProdutoAplicacao : IProdutoAplicacao
    {
        readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoAplicacao(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<int> Criar(ProdutoRequisicao produtoRequisicao)
        {
            if (produtoRequisicao == null)
                throw new ArgumentException("Produto não pode ser vazio!");

            ValidarDadosProduto(produtoRequisicao.Nome, produtoRequisicao.Preco,
                produtoRequisicao.QuantidadeEstoque, produtoRequisicao.QuantidadeMinima, produtoRequisicao.Tipo);

            var produto = new Produto()
            {
                Nome = produtoRequisicao.Nome,
                Preco = produtoRequisicao.Preco,
                QuantidadeEstoque = produtoRequisicao.QuantidadeEstoque,
                QuantidadeMinima = produtoRequisicao.QuantidadeMinima,
                Tipo = (TipoProdutos)produtoRequisicao.Tipo
            };

            return await _produtoRepositorio.Salvar(produto);
        }

        public async Task Atualizar(ProdutoAtualizar produtoAtualizar)
        {
            if (produtoAtualizar == null)
                throw new ArgumentException("Produto não pode ser vazio!");

            if (produtoAtualizar.Id <= 0)
                throw new ArgumentException("Id do produto inválido!");

            ValidarDadosProduto(produtoAtualizar.Nome, produtoAtualizar.Preco,
                produtoAtualizar.QuantidadeEstoque, produtoAtualizar.QuantidadeMinima, produtoAtualizar.Tipo);

            var produto = await _produtoRepositorio.ObterPorId(produtoAtualizar.Id);

            if (produto == null)
                throw new ArgumentException("Produto não encontrado!");

            produto.Nome = produtoAtualizar.Nome;
            produto.Preco = produtoAtualizar.Preco;
            produto.QuantidadeEstoque = produtoAtualizar.QuantidadeEstoque;
            produto.QuantidadeMinima = produtoAtualizar.QuantidadeMinima;
            produto.Tipo = (TipoProdutos)produtoAtualizar.Tipo;

            await _produtoRepositorio.Atualizar(produto);
        }

        public async Task Deletar(int produtoId)
        {
            if (produtoId <= 0)
                throw new ArgumentException("Id do produto inválido!");

            var produto = await _produtoRepositorio.ObterPorId(produtoId);

            if (produto == null)
                throw new ArgumentException("Produto não encontrado!");

            await _produtoRepositorio.Deletar(produto);
        }

        public async Task Restaurar(int produtoId)
        {
            if (produtoId <= 0)
                throw new ArgumentException("Id do produto inválido!");

            var produto = await _produtoRepositorio.ObterPorId(produtoId);

            if (produto == null)
                throw new ArgumentException("Produto não encontrado!");

            produto.Ativo = true;

            await _produtoRepositorio.Atualizar(produto);
        }

        public async Task<ProdutoResposta> ObterPorId(int produtoId)
        {
            if (produtoId <= 0)
                throw new ArgumentException("Id do produto inválido!");

            var produto = await _produtoRepositorio.ObterPorId(produtoId);

            if (produto == null)
                throw new ArgumentException("Produto não encontrado!");

            return ConverterParaResposta(produto);
        }

        public async Task<IEnumerable<ProdutoResposta>> Listar(bool ativo)
        {
            var produtos = await _produtoRepositorio.Listar(ativo);

            return produtos.Select(p => ConverterParaResposta(p)).ToList();
        }

        public async Task<IEnumerable<ProdutoResposta>> Busca(string filtro, bool ativo = true)
        {
            var produtos = await _produtoRepositorio.Busca(filtro, ativo);

            return produtos.Select(p => ConverterParaResposta(p)).ToList();
        }

        #region util
        private static void ValidarDadosProduto(string nome, decimal preco, int estoque, int minima, TipoProduto tipo)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do produto não pode ser vazio!");

            if (preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero!");

            if (estoque < 0)
                throw new ArgumentException("Quantidade em estoque não pode ser negativa!");

            if (minima < 0)
                throw new ArgumentException("Quantidade mínima não pode ser negativa!");

            if (!Enum.IsDefined(typeof(TipoProduto), tipo))
                throw new ArgumentException("Tipo de produto inválido!");
        }

        private static ProdutoResposta ConverterParaResposta(Produto produto)
        {
            return new ProdutoResposta()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                QuantidadeMinima = produto.QuantidadeMinima,
                Tipo = (TipoProduto)produto.Tipo,
                Ativo = produto.Ativo
            };
        }
        #endregion
    }
}