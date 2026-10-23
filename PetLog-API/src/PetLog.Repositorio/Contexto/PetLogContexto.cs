using Microsoft.EntityFrameworkCore;
using PetLog.Dominio.Entidades;
using PetLog.Repositorio.Configuracoes;

public class PetLogContexto : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Compra>  Compras { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }

    public PetLogContexto(DbContextOptions options) : base (options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClienteConfiguracoes());
        modelBuilder.ApplyConfiguration(new CompraConfiguracoes());
        modelBuilder.ApplyConfiguration(new ProdutoConfiguracoes());
        modelBuilder.ApplyConfiguration(new VendedorConfiguracoes());
    }
}