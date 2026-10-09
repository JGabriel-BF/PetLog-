using PetLog.Dominio.Entidades;
namespace Dominio.PetLog.Entidades;
public class Compra
{
    public int Id {get; set;}
    public int ProdutoId {get;set;}
    public int ClienteId {get;set;}
    public DateTime DataCompra {get; set;}
    public decimal Valor {get; set;}
    public bool Concluido {get; set;}

    public Compra()
    {
        Concluido = false;
    }
}