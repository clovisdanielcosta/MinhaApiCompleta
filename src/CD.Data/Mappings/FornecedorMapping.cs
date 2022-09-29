using CD.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CD.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedores");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).IsRequired().HasColumnType("varchar(200)");
            builder.Property(p => p.Documento).IsRequired().HasColumnType("varchar(14)");
            builder.HasOne(p => p.Endereco).WithOne(p => p.Fornecedor);
            builder.HasMany(f => f.Produtos).WithOne(p => p.Fornecedor).HasForeignKey(p => p.FornecedorId);
        }
    }
}