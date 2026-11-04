namespace PetLog.Aplicacao{
    public class ProdutoResposta
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int QuantidadeMinima { get; set; }
        public TipoProduto Tipo { get; set; }
        public bool Ativo { get; set; }
    }
}