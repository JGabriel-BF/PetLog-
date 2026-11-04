using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetLog.Repositorio.Interfaces;
using PetLog.Dominio.Entidades;
using PetLog.Aplicacao.DTOs;

namespace PetLog.Aplicacao
{
    public class ClienteAplicacao : IClienteAplicacao
    {
        readonly IClienteRepositorio _clienteRepositorio;

        public ClienteAplicacao(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<int> Criar(ClienteRequisicao clienteRequisicao)
        {
            if (clienteRequisicao == null)
                throw new ArgumentException("Cliente não pode ser vazio!");

            ValidarInformacoesCliente(clienteRequisicao);

            var cliente = new Cliente()
            {
                Nome = clienteRequisicao.Nome,
                SobreNome = clienteRequisicao.SobreNome,
                Telefone = clienteRequisicao.Telefone
            };

            return await _clienteRepositorio.Salvar(cliente);
        }

        public async Task Atualizar(ClienteAtualizar clienteAtualizar)
        {
            if (clienteAtualizar == null)
                throw new ArgumentException("Cliente não pode ser vazio!");

            ValidarInformacoesAtualizar(clienteAtualizar);

            var cliente = await _clienteRepositorio.ObterPorId(clienteAtualizar.Id);

            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado!");

            cliente.Nome = clienteAtualizar.Nome;
            cliente.SobreNome = clienteAtualizar.Sobrenome;
            cliente.Telefone = clienteAtualizar.Telefone;

            await _clienteRepositorio.Atualizar(cliente);
        }

        public async Task Deletar(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("Id do cliente inválido!");

            var cliente = await _clienteRepositorio.ObterPorId(clienteId);

            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado!");

            await _clienteRepositorio.Deletar(cliente);
        }

        public async Task Restaurar(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("Id do cliente inválido!");

            var cliente = await _clienteRepositorio.ObterPorId(clienteId);

            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado!");

            cliente.Ativo = true;

            await _clienteRepositorio.Atualizar(cliente);
        }

        public async Task<ClienteResposta> ObterPorId(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("Id do cliente inválido!");

            var cliente = await _clienteRepositorio.ObterPorId(clienteId);

            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado!");

            return ConverterParaResposta(cliente);
        }

        public async Task<IEnumerable<ClienteResposta>> Listar(bool ativo)
        {
            var clientes = await _clienteRepositorio.Listar(ativo);

            return clientes.Select(c => ConverterParaResposta(c)).ToList();
        }

        public async Task<IEnumerable<ClienteResposta>> Busca(string filtro, bool ativo = true)
        {
            var clientes = await _clienteRepositorio.Busca(filtro, ativo);

            return clientes.Select(c => ConverterParaResposta(c)).ToList();
        }

        #region util
        private static void ValidarInformacoesCliente(ClienteRequisicao clienteRequisicao)
        {
            ValidarDadosCliente(clienteRequisicao.Nome, clienteRequisicao.SobreNome, clienteRequisicao.Telefone);
        }

        private static void ValidarInformacoesAtualizar(ClienteAtualizar clienteAtualizar)
        {
            if (clienteAtualizar.Id <= 0)
                throw new ArgumentException("Id do cliente inválido!");

            ValidarDadosCliente(clienteAtualizar.Nome, clienteAtualizar.Sobrenome, clienteAtualizar.Telefone);
        }

        private static void ValidarDadosCliente(string nome, string sobrenome, long telefone)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio!");

            if (string.IsNullOrWhiteSpace(sobrenome))
                throw new ArgumentException("Sobrenome não pode ser vazio!");

            if (telefone <= 0)
                throw new ArgumentException("Telefone não pode ser vazio!");

            var digitos = telefone.ToString().Length;

            if (digitos < 10 || digitos > 11)
                throw new ArgumentException("Telefone inválido! Informe DDD + número.");
        }

        private static ClienteResposta ConverterParaResposta(Cliente cliente)
        {
            return new ClienteResposta()
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Sobrenome = cliente.SobreNome,
                Telefone = cliente.Telefone,
                Ativo = cliente.Ativo
            };
        }
        #endregion
    }
}