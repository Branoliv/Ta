using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarefas.Domain.Entities;

namespace Tarefas.Infra.Persistence.EF.Map
{
    public class MapTarefa : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {

            //Nome tabela
            builder.ToTable("Tarefa");

            //ForeiKey
            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey("IdUsuario");
            builder.HasOne(x => x.ListaDeTarefas).WithMany().HasForeignKey("IdListaDeTarefa");

            //Chave primaria
            builder.HasKey(x => x.Id);

            //Propriedades
            builder.Property(x => x.Titulo).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Descricao).HasMaxLength(500).IsRequired();
            builder.Property(x => x.DataInicio).IsRequired().HasColumnName("DataInicio").HasColumnType("Date");
            builder.Property(x => x.DataConclusao).IsRequired().HasColumnName("DataConclusao").HasColumnType("Date");
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
