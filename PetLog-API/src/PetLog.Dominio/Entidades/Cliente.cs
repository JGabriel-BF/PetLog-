namespace PetLog.Dominio.Entidades;
public class Cliente
{
    public int Id {get; set;}
    public string Nome {get; set;}
    public string SobreNome {get; set;}
    public long Telefone {get; set;}
    public bool Ativo {get; set;}
    public List<Compra> Compras {get; set;}

    public Cliente()
    {
        Ativo = true;
    }
}