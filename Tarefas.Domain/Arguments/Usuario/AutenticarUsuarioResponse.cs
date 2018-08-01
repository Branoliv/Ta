using System;
using Tarefas.Domain.Entities;

namespace Tarefas.Domain.Arguments.Usuario
{
    public class AutenticarUsuarioResponse
    {
        public Guid Id { get; set; }
        public string PrimeiroNome { get; set; }

        public static explicit operator AutenticarUsuarioResponse(Entities.Usuario entidade)
        {
            return new AutenticarUsuarioResponse()
            {
                Id = entidade.Id,
                PrimeiroNome = entidade.Nome.PrimeiroNome
            };
        }
    }
}
