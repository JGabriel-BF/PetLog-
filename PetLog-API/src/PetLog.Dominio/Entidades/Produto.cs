namespace PetLog.Dominio.Entidades;
using PetLog.Dominio.Enumeradores;

public class Produto
{
    public int Id {get; set;}
    public string Nome {get; set;}
    public decimal Valor {get; set;}
    public TipoProdutos TipoProdutos {get; set;}
}