using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MontagemCurriculo.Models;
using System;

namespace MontagemCurriculo.Mapeamento
{
    public class InformacaoLoginMap : IEntityTypeConfiguration<InformacaoLogin>
    {
        public void Configure(EntityTypeBuilder<InformacaoLogin> builder)
        {
            builder.HasKey(i => i.InformacaoLoginId);

            builder.Property(i => i.EnderecoIp).IsRequired();
            builder.Property(i => i.Horario).IsRequired();
            builder.Property(i => i.Data).IsRequired();

            builder.HasOne(i => i.Usuario).WithMany(i => i.InformacoesLogin).HasForeignKey(i => i.UsuarioId);

            builder.ToTable("InformacoesLogin");
        }
    }
}
