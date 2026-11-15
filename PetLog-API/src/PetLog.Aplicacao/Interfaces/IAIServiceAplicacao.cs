namespace PetLog.Aplicacao
{
    public interface IAIService
    {
        Task<string> GerarRespostaAsync(string prompt);
    }
}