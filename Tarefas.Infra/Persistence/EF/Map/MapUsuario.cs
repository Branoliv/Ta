using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarefas.Domain.Entities;
using Tarefas.Domain.ValueObjects;

namespace Tarefas.Infra.Persistence.EF.Map
{
    public class MapUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //Nome da tabela
            builder.ToTable("Usuario");

            //Chave primaria
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Senha).HasMaxLength(36).IsRequired();

            //Mapeando objetos de valor
            builder.OwnsOne<Nome>(x => x.Nome, cb =>
            {
                cb.Property(x => x.PrimeiroNome).HasMaxLength(50).HasColumnName("PrimeiroNome").IsRequired();
                cb.Property(x => x.UltimoNome).HasMaxLength(50).HasColumnName("UltimoNome").IsRequired();
            });

            builder.OwnsOne<Email>(x => x.Email, cb =>
            {
                cb.Property(x => x.Endereco).HasMaxLength(200).HasColumnName("Email").IsRequired();
            });

            builder.OwnsOne<Endereco>(x => x.Endereco, cb =>
            {
                cb.Property(x => x.Logradouro).HasMaxLength(400).HasColumnName("Logradouro");
                cb.Property(x => x.NumeroResid).HasColumnName("Numero_Residencia");
                cb.Property(x => x.Bairro).HasMaxLength(200).HasColumnName("Bairoo");
                cb.Property(x => x.Cidade).HasMaxLength(300).HasColumnName("Cidade");
                cb.Property(x => x.Estado).HasMaxLength(300).HasColumnName("Estado");
                cb.Property(x => x.Pais).HasMaxLength(100).HasColumnName("Pais");
                cb.Property(x => x.Cep).HasMaxLength(200).HasColumnName("Cep");
            });
        }
    }
}
