using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PetLog.Dominio.Entidades;
using PetLog.Dominio.Enumeradores;
using PetLog.Repositorio.Interfaces;

namespace PetLog.Aplicacao
{
    public class VendedorAplicacao : IVendedorAplicacao
    {
        readonly IVendedorRepositorio _vendedorRepositorio;

        public VendedorAplicacao(IVendedorRepositorio vendedorRepositorio)
        {
            _vendedorRepositorio = vendedorRepositorio;
        }

        public async Task<int> Criar (Vendedor vendedor, int vendedorLoginId)
        {
            if (vendedor == null)
                throw new Exception("Vendedor não pode ser vazio");

            ValidarInformacoesVendedor(vendedor);

            if (string.IsNullOrEmpty(vendedor.Senha))
            throw new Exception ("Senha não pode ser vazia");

            if (vendedor.Senha.Length < 6)
            throw new Exception("A senha deve ter no mínimo 6 caracteres");

            return await _vendedorRepositorio.Salvar(vendedor);
        }

        public async Task Atualizar (Vendedor vendedor, int vendedorLoginId)
        {
            var vendedorDominio = await _vendedorRepositorio.Obter(vendedor.Id);

            if (vendedorDominio == null)
            throw new Exception("Vendedor não encontrado");

            ValidarInformacoesVendedor(vendedorDominio);

            vendedorDominio.Nome = vendedor.Nome;
            vendedorDominio.Email = vendedor.Email;

            await _vendedorRepositorio.Atualizar(vendedorDominio);
        }

        public async Task <Vendedor> Obter (int vendedorId, int vendedorLoginId)
        {
            var vendedorDominio = await _vendedorRepositorio.Obter(vendedorId);

            if (vendedorDominio == null)
            throw new Exception("Vendedor não encontrado");

            return vendedorDominio;
        }

        public async Task <Vendedor> ObterPorId (int vendedorId, int vendedorLoginId)
        {
            var vendedorDominio = await _vendedorRepositorio.ObterPorId(vendedorId);

            if (vendedorDominio == null)
            throw new Exception("Vendedor não encontrado");

            return vendedorDominio;
        }

        public async Task Restaurar (int vendedorId, int vendedorLoginId)
        {
            var vendedorDominio = await _vendedorRepositorio.ObterPorId(vendedorId);

            if (vendedorDominio == null)
            throw new Exception ("Vendedor não encontrado");
            vendedorDominio.Restaurar();
            await _vendedorRepositorio.Atualizar(vendedorDominio);
        }

        public async Task Deletar (int vendedorId, int vendedorLoginId)
        {
            var vendedorDominio = await _vendedorRepositorio.Obter(vendedorId);

            if (vendedorDominio == null)
            throw new Exception("Vendedor não encontrado");

            vendedorDominio.Deletar();
            await _vendedorRepositorio.Atualizar(vendedorDominio);
        }

        public async Task<IEnumerable<Vendedor>> Listar (bool ativo, int vendedorId)
        {
            return await _vendedorRepositorio.Listar(ativo);
        }

        public async Task <IEnumerable<Vendedor>> Busca (string filtro, int vendedorId, bool ativo = true)
        {
            return await _vendedorRepositorio.Listar(ativo);
        }













        #region baguals
        private static void ValidarInformacoesVendedor(Vendedor vendedor)
        {
            if (string.IsNullOrEmpty(vendedor.Nome))
            throw new Exception("Nome não pode ser vazio");

            if (string.IsNullOrEmpty(vendedor.Email))
            throw new Exception("E-Mail não pode ser vazio");
        }
        #endregion
    }
}

