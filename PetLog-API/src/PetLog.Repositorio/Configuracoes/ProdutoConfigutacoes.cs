using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetLog.Dominio.Entidades;

namespace PetLog.Repositorio.Configuracoes;

public class ProdutoConfiguracoes : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos").HasKey(produto => produto.Id);

        builder.Property(produto => produto.Id).HasColumnName("ProdutoId").IsRequired(true);
        builder.Property(produto => produto.Nome).HasColumnName("Nome").IsRequired(true).HasMaxLength(100);
        builder.Property(produto => produto.Preco).HasColumnName("Preço").IsRequired(true);
        builder.Property(produto => produto.QuantidadeEstoque).HasColumnName("QuantidadeEstoque").IsRequired(true);
        builder.Property(produto => produto.QuantidadeMinima).HasColumnName("QuantidadeMinima").IsRequired(true);
        builder.Property(produto => produto.Tipo).HasColumnName("TipoProdutoId").IsRequired(true);
        builder.Property(produto => produto.Ativo).HasColumnName("Ativo").IsRequired(true);

        builder
        .HasMany(produto => produto.Compras)
        .WithMany(compras => compras.Produtos);
    }
}