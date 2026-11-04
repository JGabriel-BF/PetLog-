namespace PetLog.Aplicacao.DTOs;

public class ClienteResposta
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
    public long Telefone { get; set; }
    public bool Ativo { get; set; }
}