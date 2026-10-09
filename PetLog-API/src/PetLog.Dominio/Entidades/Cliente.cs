namespace PetLog.Dominio.Entidades;
using global::Dominio.PetLog.Entidades;
public class Cliente
{
    public int Id {get; set;}
    public string Nome {get; set;}
    public string Email {get; set;}

    public List<Compra> Compras {get; set;}
}