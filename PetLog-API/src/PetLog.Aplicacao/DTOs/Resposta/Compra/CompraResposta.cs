using System;
using System.Collections.Generic;

namespace PetLog.Aplicacao
{
    public class CompraResposta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;
        public int VendedorId { get; set; }
        public string VendedorNome { get; set; } = string.Empty;
        public List<CompraProdutoResposta> Produtos { get; set; } = new List<CompraProdutoResposta>();
        public int Quantidade { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Valor { get; set; }
        public bool Concluido { get; set; }
        public bool Ativo { get; set; }
    }
}