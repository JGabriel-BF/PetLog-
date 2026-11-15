using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetLog.Repositorio.Interfaces;

namespace PetLog.Aplicacao
{
    public class SugestaoAplicacao : ISugestaoAplicacao
    {
        readonly ICompraRepositorio _compraRepositorio;
        readonly IClienteRepositorio _clienteRepositorio;
        readonly IAIService _aiService;

        public SugestaoAplicacao(
            ICompraRepositorio compraRepositorio,
            IClienteRepositorio clienteRepositorio,
            IAIService aiService)
        {
            _compraRepositorio = compraRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _aiService = aiService;
        }

        public async Task<SugestaoCompraResposta> GerarParaCliente(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("Id do cliente inválido!");

            var cliente = await _clienteRepositorio.ObterPorId(clienteId);

            if (cliente == null || !cliente.Ativo)
                throw new ArgumentException("Cliente não encontrado!");

            var compras = await _compraRepositorio.ListarPorCliente(clienteId);

            if (!compras.Any())
                throw new InvalidOperationException("Cliente sem compras concluídas para analisar.");

            var historico = new List<string>();
            var estoque = new Dictionary<int, string>();

            foreach (var compra in compras)
            {
                foreach (var produto in compra.Produtos)
                {
                    historico.Add($"- {compra.DataCompra:dd/MM/yyyy}: {produto.Nome}, quantidade {compra.Quantidade}");
                    estoque[produto.Id] = $"- {produto.Nome}: estoque atual {produto.QuantidadeEstoque}, mínimo {produto.QuantidadeMinima}";
                }
            }

            var prompt =
                "Você é o assistente de uma casa de rações. Abaixo estão o histórico de compras de um cliente " +
                "e o estoque atual dos produtos que ele compra. Analise com que frequência e em que quantidade " +
                "ele costuma comprar cada produto, estime quando ele deve comprar de novo e avise se o estoque atual " +
                "não cobre a quantidade que ele costuma levar. Responda em português, em no máximo 5 linhas, " +
                "para o vendedor. Use somente os dados abaixo, sem inventar nada.\n\n" +
                $"Data de hoje: {DateTime.Now:dd/MM/yyyy}\n\n" +
                "Histórico de compras:\n" + string.Join("\n", historico) + "\n\n" +
                "Estoque atual:\n" + string.Join("\n", estoque.Values);

            var sugestao = await _aiService.GerarRespostaAsync(prompt);

            return new SugestaoCompraResposta
            {
                ClienteId = clienteId,
                Sugestao = sugestao
            };
        }
    }
}