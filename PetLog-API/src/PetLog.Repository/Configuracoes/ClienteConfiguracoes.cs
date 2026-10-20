using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetLog.Dominio.Entidades;

namespace PetLog.Repositorio.Configuracoes;

public class ClienteConfiguracoes : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes").HasKey(cliente => cliente.Id);

        builder.Property(cliente => cliente.Id).HasColumnName("ClienteId").IsRequired(true);
        builder.Property(cliente => cliente.Nome).HasColumnName("Nome").IsRequired(true).HasMaxLength(50);
        builder.Property(cliente => cliente.SobreNome).HasColumnName("Sobrenome").IsRequired(true).HasMaxLength(100);
        builder.Property(cliente => cliente.Telefone).HasColumnName("Telefone").IsRequired(true).HasMaxLength(15);
        builder.Property(cliente => cliente.Compras).HasColumnName("Compras").IsRequired(true);
        
    }
}