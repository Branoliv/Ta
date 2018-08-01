using System;
using System.Collections.Generic;
using System.Text;

namespace Tarefas.Domain.Arguments.Tarefa
{
    public class AdicionarTarefaResponse
    {
        public AdicionarTarefaResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
