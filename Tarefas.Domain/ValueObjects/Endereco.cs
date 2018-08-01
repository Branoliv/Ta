using prmToolkit.NotificationPattern;

namespace Tarefas.Domain.ValueObjects
{
    public class Endereco : Notifiable
    {
        protected Endereco() { }

        public Endereco(string logradouro, string numeroResid, string bairro, string cidade, string estado, string pais, string cep)
        {
            Logradouro = logradouro;
            NumeroResid = numeroResid;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Cep = cep;

            new AddNotifications<Endereco>(this);
        }

        public string Logradouro { get; private set; }
        public string NumeroResid { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
        public string Cep { get; private set; }
    }
}
