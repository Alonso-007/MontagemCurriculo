using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemCurriculo.Models;

namespace MontagemCurriculo.Mapeamento
{
    public class ExperienciaProfissionalMap : IEntityTypeConfiguration<ExperienciaProfissional>
    {
        public void Configure(EntityTypeBuilder<ExperienciaProfissional> builder)
        {
            builder.HasKey(e => e.ExperienciaProfissionalId);

            builder.Property(e => e.NomeEmpresa).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Cargo).IsRequired().HasMaxLength(50);
            builder.Property(e => e.AnoInicio).IsRequired();
            builder.Property(e => e.AnoFim).IsRequired();
            builder.Property(e => e.DescricaoAtividades).IsRequired().HasMaxLength(500);

            builder.HasOne(e => e.Curriculo).WithMany(e => e.ExperienciaProfissionais).HasForeignKey(e => e.CurriculoId);

            builder.ToTable("ExperienciasProfissionais");
        }
    }
}