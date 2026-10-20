using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetLog.Dominio.Entidades;

namespace PetLog.Repositorio.Configuracoes;

public class CompraConfiguracoes : IEntityTypeConfiguration<Compra>
{
    public void Configure(EntityTypeBuilder<Compra> builder)
    {
        builder.ToTable("Compras").HasKey(compra => compra.Id);

        builder.Property(compra => compra.Id).HasColumnName("CompraId").IsRequired(true);
        builder.Property(compra => compra.Produtos).HasColumnName("TipoProdutos").IsRequired(true);
        builder.Property(compra => compra.Quantidade).HasColumnName("Quantidade").IsRequired(true);
        builder.Property(compra => compra.ClienteId).HasColumnName("ClienteId").IsRequired(true);
        builder.Property(compra => compra.VendedorId).HasColumnName("VendedorId").IsRequired(true);
        builder.Property(compra => compra.DataCompra).HasColumnName("DataCompra").IsRequired(true);
        builder.Property(compra => compra.Valor).HasColumnName("Valor").IsRequired(true);
        builder.Property(compra => compra.Concluido).HasColumnName("Concluido").IsRequired(true);

        builder
        .HasOne (compra => compra.Cliente)
        .WithMany(cliente => cliente.Compras)
        .HasForeignKey(compra => compra.ClienteId);

        builder
        .HasMany(compra => compra.Produtos)
        .WithMany(produto => produto.Compras);

        builder
        .HasOne (compra => compra.Vendedor)
        .WithMany(vendedor => vendedor.Compras);
    }
}