namespace PetLog.Dominio.Entidades;
public class Compra
{
    public int Id {get; set;}
    public List <Produto> Produtos {get; set;}
    public int Quantidade {get;set;}

    public int ClienteId {get;set;}
    public Cliente Cliente {get; set;}

    public int VendedorId {get; set;}

    public Vendedor Vendedor {get; set;}
    
    public DateTime DataCompra {get; set;}
    public decimal Valor {get; set;}
    public bool Concluido {get; set;}

    public bool Ativo {get; set;}
    public Compra()
    {
        Concluido = false;
        Ativo = true;
    }
}