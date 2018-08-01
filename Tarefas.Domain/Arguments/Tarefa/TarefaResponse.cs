using System;

namespace Tarefas.Domain.Arguments.Tarefa
{
    public class TarefaResponse
    {
        public Guid IdTarefa { get; set; }
        public Guid? IdListaDeTarefas { get; set; }
        public string NomeListaDeTarefas { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataConclusao { get; set; }
        public int Status { get; set; }

        public static explicit operator TarefaResponse(Entities.Tarefa entidade)
        {
            return new TarefaResponse()
            {
                IdTarefa = entidade.Id,
                IdListaDeTarefas = entidade.ListaDeTarefas?.Id,
                NomeListaDeTarefas = entidade.ListaDeTarefas?.Nome,
                Titulo = entidade.Titulo,
                Descricao = entidade.Descricao,
                DataInicio = entidade.DataInicio,
                DataConclusao = entidade.DataConclusao,
                Status = (int) entidade.Status
            };
        }
    }
}
