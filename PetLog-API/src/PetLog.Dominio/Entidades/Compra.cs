namespace PetLog.Dominio.Entidades;
public class Compra
{
    public int Id {get; set;}
    public int ProdutoId {get;set;}
    public Produto Produto {get; set;} = new ();
    public int Quantidade {get;set;}

    public int ClienteId {get;set;}
    public Cliente Cliente {get; set;}

    public int UsuarioId {get; set;}
    public Usuario Usuario {get; set;}
    
    public DateTime DataCompra {get; set;}
    public decimal Valor {get; set;}
    public bool Concluido {get; set;}

    public Compra()
    {
        Concluido = false;
    }
}