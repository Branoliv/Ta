using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using Tarefas.Domain.Entities;
using Tarefas.Domain.ValueObjects;
using Tarefas.Infra.Persistence.EF.Map;
using Tarefas.Shared;

namespace Tarefas.Infra.Persistence.EF
{
    public class TarefasContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<ListaDeTarefas> ListaDeTarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.ConnectionString);
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseMySql(Settings.ConnectionStringMySql);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Classe ignoradas no banco de dados

            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Email>();
            modelBuilder.Ignore<Nome>();
            modelBuilder.Ignore<Endereco>();

            //aplicar configurações
            modelBuilder.ApplyConfiguration(new MapUsuario());
            modelBuilder.ApplyConfiguration(new MapTarefa());
            modelBuilder.ApplyConfiguration(new MapListaDeTarefas());

            base.OnModelCreating(modelBuilder);


        }
    }
}
