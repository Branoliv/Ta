namespace Tarefas.Domain.Arguments.Usuario
{
    public class AdicionarUsuarioRequest
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Logradouro { get; set; }
        public string NumeroResid { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
    }
}
