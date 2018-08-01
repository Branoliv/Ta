using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarefas.Domain.Entities;

namespace Tarefas.Infra.Persistence.EF.Map
{
    public class MapListaDeTarefas : IEntityTypeConfiguration<ListaDeTarefas>
    {
        public void Configure(EntityTypeBuilder<ListaDeTarefas> builder)
        {
            //Nome tabela
            builder.ToTable("ListaDeTarefas");

            //ForeingKey
            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey("IdUsuario");

            //Chave primaria
            builder.HasKey(x => x.Id);

            //Propriedade
            builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();
        }
    }
}
