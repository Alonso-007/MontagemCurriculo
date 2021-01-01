using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemCurriculo.Models;
using System;

namespace MontagemCurriculo.Mapeamento
{
    public class IdiomaMap : IEntityTypeConfiguration<Idioma>
    {
        public void Configure(EntityTypeBuilder<Idioma> builder)
        {
            builder.HasKey(i => i.IdiomaId);

            builder.Property(i => i.Nome).IsRequired().HasMaxLength(50);
            builder.HasIndex(i => i.Nome).IsUnique();

            builder.Property(i => i.Nivel).IsRequired().HasMaxLength(50);

            builder.HasOne(i => i.Curriculo).WithMany(i => i.Idiomas).HasForeignKey(i => i.CurriculoId);

            builder.ToTable("idiomas");
        }
    }
}
