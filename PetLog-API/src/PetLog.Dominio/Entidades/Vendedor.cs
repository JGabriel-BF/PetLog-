namespace PetLog.Dominio.Entidades;
public class Vendedor
{
    public int ID {get; set; }
    public string Nome {get; set;}
    public string Email {get; set;}
    public string Senha {get; set;}
    public bool Ativo {get; set;}
    public List <Compra> Compras {get; set;}

    //MUDAR DE USUARIO PARA VENDEDOR

    public Vendedor()
    {
        Ativo = true;
    }

    public void Deletar()
    {
        Ativo = false;
    }

    public void Restaurar()
    {
        Ativo = true;
    }
}