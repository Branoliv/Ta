using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Tarefas.Domain.Resources;

namespace Tarefas.Domain.ValueObjects
{
    public class Nome : Notifiable
    {
        protected Nome() { }

        public Nome(string primeiroNome, string ultimoNome)
        {
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;

            new AddNotifications<Nome>(this)
                .IfNullOrInvalidLength(x => x.PrimeiroNome, 1, 50, MSG.X0_OBRIGATORIA_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Primeiro nome", 1, 50))
                .IfNullOrInvalidLength(x => x.UltimoNome, 1, 50, MSG.X0_OBRIGATORIA_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Ultimo nome", 1, 50));
        }

        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }
    }
}
