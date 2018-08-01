using prmToolkit.NotificationPattern;
using Tarefas.Domain.Entities.Base;

namespace Tarefas.Domain.Entities
{
    public class ListaDeTarefas : EntitieBase
    {
        protected ListaDeTarefas() { }

        public ListaDeTarefas(string nome, Usuario usuario)
        {
            Nome = nome;
            Usuario = usuario;

            new AddNotifications<ListaDeTarefas>(this)
                .IfNullOrInvalidLength(x => x.Nome, 1, 100);
            AddNotifications(usuario);
        }

        public string Nome { get; private set; }
        public Usuario Usuario { get; private set; }
    }
}
