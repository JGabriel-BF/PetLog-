namespace PetLog.Dominio.Entidades;
using PetLog.Dominio.Enumeradores;

public class Produto
{
    public int Id {get; set;}
    public string Nome {get; set;}
    public decimal Preco {get; set;}
    public int QuantidadeEstoque {get; set;}
    public int QuantidadeMinima {get; set;}
    public TipoProdutos Tipo {get; set;}
    public List <Compra> Compras {get; set;}
}