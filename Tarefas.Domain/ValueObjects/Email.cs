using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Tarefas.Domain.Resources;

namespace Tarefas.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        protected Email() { }

        public Email(string endereco)
        {
            Endereco = endereco;

            new AddNotifications<Email>(this)
                .IfNotEmail(x => x.Endereco, MSG.X0_INVALIDO.ToFormat("Email"));
        }

        public string Endereco { get; private set; }
    }
}
