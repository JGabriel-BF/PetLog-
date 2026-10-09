namespace PetLog.Dominio.Entidades;
using global::Dominio.PetLog.Entidades;
using PetLog.Dominio.Enumeradores;

public class Produto
{
    public int Id {get; set;}
    public string Nome {get; set;}
    public decimal Valor {get; set;}
    public int QuantidadeEstoque {get; set;}
    public int QuantidadeMinima {get; set;}
    public TipoProdutos TipoProdutos {get; set;}

    public List <Compra> Compras {get; set;}
}