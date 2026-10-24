public abstract class BaseRepositorio
{
    protected readonly PetLogContexto _contexto;

    protected BaseRepositorio(PetLogContexto contexto)
    {
        _contexto = contexto;
    }
}