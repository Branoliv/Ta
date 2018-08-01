using prmToolkit.NotificationPattern;
using System;

namespace Tarefas.Domain.Entities.Base
{
    public abstract class EntitieBase : Notifiable
    {
        public EntitieBase()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
    }
}
