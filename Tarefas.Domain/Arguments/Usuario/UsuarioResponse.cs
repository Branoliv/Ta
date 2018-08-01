using System;
using Tarefas.Domain.Entities;

namespace Tarefas.Domain.Arguments.Usuario
{
    public class UsuarioResponse
    {
        public Guid IdUsuario {get  ; set;}
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string Logradouro { get; set; }
        public string NumeroResid { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }

        public static explicit operator UsuarioResponse(Entities.Usuario entidade)
        {
            return new UsuarioResponse()
            {
                IdUsuario = entidade.Id,
                PrimeiroNome = entidade.Nome.PrimeiroNome,
                UltimoNome = entidade.Nome.UltimoNome,
                Email = entidade.Email.Endereco,
                Logradouro = entidade.Endereco.Logradouro,
                NumeroResid = entidade.Endereco.NumeroResid,
                Bairro = entidade.Endereco.Bairro,
                Cidade = entidade.Endereco.Cidade,
                Estado = entidade.Endereco.Estado,
                Pais = entidade.Endereco.Pais,
                Cep = entidade.Endereco.Cep
            };
        }
    }
}
