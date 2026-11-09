using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetLog.Aplicacao.DTOs;
using PetLog.Dominio.Entidades;
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

        public async Task<int> Criar(VendedorRequisicao vendedorRequisicao)
        {
            if (vendedorRequisicao == null)
                throw new ArgumentException("Vendedor não pode ser vazio!");

            ValidarInformacoesVendedor(vendedorRequisicao);

            if (await _vendedorRepositorio.ObterPorEmail(vendedorRequisicao.Email) != null)
                throw new InvalidOperationException("Vendedor já cadastrado!");

            var vendedor = new Vendedor()
            {
                Nome = vendedorRequisicao.Nome,
                Email = vendedorRequisicao.Email,
                Senha = vendedorRequisicao.Senha
            };

            return await _vendedorRepositorio.Salvar(vendedor);
        }

        public async Task Atualizar(VendedorAtualizar vendedorAtualizar)
        {
            if (vendedorAtualizar == null)
                throw new ArgumentException("Vendedor não pode ser vazio!");

            ValidarInformacoesAtualizar(vendedorAtualizar);

            var vendedor = await _vendedorRepositorio.ObterPorId(vendedorAtualizar.Id);

            if (vendedor == null)
                throw new ArgumentException("Vendedor não encontrado!");

            vendedor.Nome = vendedorAtualizar.Nome;
            vendedor.Email = vendedorAtualizar.Email;

            await _vendedorRepositorio.Atualizar(vendedor);
        }

        public async Task Deletar(int vendedorId)
        {
            if (vendedorId <= 0)
                throw new ArgumentException("Id do vendedor inválido!");

            var vendedor = await _vendedorRepositorio.ObterPorId(vendedorId);

            if (vendedor == null)
                throw new ArgumentException("Vendedor não encontrado!");

            vendedor.Deletar();

            await _vendedorRepositorio.Atualizar(vendedor);
        }

        public async Task Restaurar(int vendedorId)
        {
            if (vendedorId <= 0)
                throw new ArgumentException("Id do vendedor inválido!");

            var vendedor = await _vendedorRepositorio.ObterPorId(vendedorId);

            if (vendedor == null)
                throw new ArgumentException("Vendedor não encontrado!");

            vendedor.Restaurar();

            await _vendedorRepositorio.Atualizar(vendedor);
        }

 public async Task<VendedorRespostaDTO> ObterPorEmail(string email)
{
    if (string.IsNullOrWhiteSpace(email))
        throw new ArgumentException("E-mail não pode ser vazio!");

    if (!email.Contains("@"))
        throw new ArgumentException("E-mail deve conter @!");

    var vendedor = await _vendedorRepositorio.ObterPorEmail(email);

    if (vendedor == null)
        throw new ArgumentException("Vendedor não encontrado!");

    return ConverterParaResposta(vendedor);
}

        public async Task<VendedorRespostaDTO> ObterPorId(int vendedorId)
        {
            if (vendedorId <= 0)
                throw new ArgumentException("Id do vendedor inválido!");

            var vendedor = await _vendedorRepositorio.ObterPorId(vendedorId);

            if (vendedor == null)
                throw new ArgumentException("Vendedor não encontrado!");

            return ConverterParaResposta(vendedor);
        }

        public async Task<IEnumerable<VendedorRespostaDTO>> Listar(bool ativo)
        {
            var vendedores = await _vendedorRepositorio.Listar(ativo);

            return vendedores.Select(v => ConverterParaResposta(v)).ToList();
        }

        public async Task<IEnumerable<VendedorRespostaDTO>> Busca(string filtro, bool ativo = true)
        {
            var vendedores = await _vendedorRepositorio.Listar(ativo);

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroMinusculo = filtro.ToLower();

                vendedores = vendedores.Where(v =>
                    v.Nome.ToLower().Contains(filtroMinusculo) ||
                    v.Email.ToLower().Contains(filtroMinusculo));
            }

            return vendedores.Select(v => ConverterParaResposta(v)).ToList();
        }

        public async Task<VendedorRespostaDTO> Login(VendedorLogin vendedorLogin)
        {
            if (vendedorLogin == null)
            throw new ArgumentException("Dados de login não podem ser vazios!");

        if (string.IsNullOrWhiteSpace(vendedorLogin.Email))
        throw new ArgumentException("E-mail não pode ser vazio!");

        if (string.IsNullOrWhiteSpace(vendedorLogin.Senha))
        throw new ArgumentException("Senha não pode ser vazia!");

        var vendedor = await _vendedorRepositorio.ObterPorEmail(vendedorLogin.Email);

        if (vendedor == null || vendedor.Senha != vendedorLogin.Senha)
        throw new UnauthorizedAccessException("E-mail ou senha inválidos!");

        return ConverterParaResposta(vendedor);
}   

        #region util
        private static void ValidarInformacoesVendedor(VendedorRequisicao vendedorRequisicao)
        {
            if (string.IsNullOrWhiteSpace(vendedorRequisicao.Nome))
                throw new ArgumentException("Nome não pode ser vazio!");

            if (string.IsNullOrWhiteSpace(vendedorRequisicao.Email))
                throw new ArgumentException("E-mail não pode ser vazio!");

            if (!vendedorRequisicao.Email.Contains("@"))
                throw new ArgumentException("E-mail deve conter @!");

            if (string.IsNullOrWhiteSpace(vendedorRequisicao.Senha))
                throw new ArgumentException("Senha não pode ser vazia!");

            if (vendedorRequisicao.Senha.Length < 6)
                throw new ArgumentException("A senha deve ter no mínimo 6 caracteres!");
        }

        private static void ValidarInformacoesAtualizar(VendedorAtualizar vendedorAtualizar)
        {
            if (vendedorAtualizar.Id <= 0)
                throw new ArgumentException("Id do vendedor inválido!");

            if (string.IsNullOrWhiteSpace(vendedorAtualizar.Nome))
                throw new ArgumentException("Nome não pode ser vazio!");

            if (string.IsNullOrWhiteSpace(vendedorAtualizar.Email))
                throw new ArgumentException("E-mail não pode ser vazio!");

            if (!vendedorAtualizar.Email.Contains("@"))
                throw new ArgumentException("E-mail deve conter @!");
        }

        private static VendedorRespostaDTO ConverterParaResposta(Vendedor vendedor)
        {
            return new VendedorRespostaDTO()
            {
                Id = vendedor.Id,
                Nome = vendedor.Nome,
                Email = vendedor.Email,
                Ativo = vendedor.Ativo
            };
        }
        #endregion
    }
}