namespace PetLog.Aplicacao
{
    public interface ISugestaoAplicacao
    {
        Task<SugestaoCompraResposta> GerarParaCliente(int clienteId);
    }
}