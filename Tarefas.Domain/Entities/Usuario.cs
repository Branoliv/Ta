using prmToolkit.NotificationPattern;
using Tarefas.Domain.Entities.Base;
using Tarefas.Domain.ValueObjects;
using YouLearn.Domain.Extensions;

namespace Tarefas.Domain.Entities
{
    public class Usuario : EntitieBase
    {
        protected Usuario() { }

        public Usuario(Nome nome, Email email, string senha, Endereco endereco)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Endereco = endereco;

            new AddNotifications<Usuario>(this)
                .IfNullOrInvalidLength(x => x.Senha, 3, 32);

            Senha = Senha.ConvertToMD5();

            AddNotifications(nome, email, endereco);
        }

        public Usuario(Email email, string senha)
        {
            Email = email;
            Senha = senha;

            Senha = Senha.ConvertToMD5();
            AddNotifications(email);
        }

        public void AtualizarUsuario(Nome nome, Email email, Endereco endereco)
        {
            Nome = nome;
            Email = email;
            Endereco = endereco;

            new AddNotifications<Usuario>(this);

            AddNotifications(nome, email, endereco);
        }



        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public string Senha { get; private set; }
        public Endereco Endereco { get; private set; }
    }
}
