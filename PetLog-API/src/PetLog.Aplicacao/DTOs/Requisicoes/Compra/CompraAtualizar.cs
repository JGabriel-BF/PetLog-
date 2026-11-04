using System;
using System.Collections.Generic;

namespace PetLog.Aplicacao
{
    public class CompraAtualizar
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public List<int> ProdutoIds { get; set; } = new List<int>();
        public int Quantidade { get; set; }
        public DateTime DataCompra { get; set; }
    }
}