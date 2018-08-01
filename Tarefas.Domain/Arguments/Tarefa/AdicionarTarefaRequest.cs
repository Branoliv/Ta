using System;
using System.Collections.Generic;
using System.Text;

namespace Tarefas.Domain.Arguments.Tarefa
{
    public class AdicionarTarefaRequest
    {
        public Guid IdListaDeTarefas { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataConclusao { get; set; }
        public string Status { get; set; }
    }
}
