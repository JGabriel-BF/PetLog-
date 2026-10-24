using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetLog.Dominio.Entidades;

namespace PetLog.Repositorio.Configuracoes;

public class VendedorConfiguracoes : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.ToTable("Vendedor").HasKey(x => x.Id);

        builder.Property(nameof(Vendedor.Id)).HasColumnName("VendedorId");
        builder.Property(nameof(Vendedor.Nome)).HasColumnName("Nome").IsRequired(true).HasMaxLength(100);
        builder.Property(nameof(Vendedor.Email)).HasColumnName("Email").IsRequired(true);
        builder.Property(nameof(Vendedor.Senha)).HasColumnName("Senha").IsRequired(true);
        builder.Property(nameof(Vendedor.Ativo)).HasColumnName("Ativo").IsRequired(true);
    }
}