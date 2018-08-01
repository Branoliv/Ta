using System;
using System.Collections.Generic;
using System.Text;
using Tarefas.Domain.Entities;

namespace Tarefas.Domain.Arguments.ListaDeTarefas
{
    public class ListaDeTarefasResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public static explicit operator ListaDeTarefasResponse(Entities.ListaDeTarefas entidade)
        {
            return new ListaDeTarefasResponse()
            {
                Id = entidade.Id,
                Nome = entidade.Nome
            };
        }
    }
}
