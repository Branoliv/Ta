using System;
using System.Collections.Generic;
using Tarefas.Domain.Arguments.Base;
using Tarefas.Domain.Arguments.Tarefa;
using Tarefas.Domain.Interfaces.Services.Base;

namespace Tarefas.Domain.Interfaces.Services
{
    public interface IServiceTarefa : IServiceBase
    {
        AdicionarTarefaResponse AdicionarTarefa(AdicionarTarefaRequest request, Guid idUsuario);
        Response AtualizarTarefa(TarefaRequest request, Guid idUsuario);
        IEnumerable<TarefaResponse> Listar(Guid idUsuario);
        IEnumerable<TarefaResponse> ListarPorLista(Guid idLista);
        Response ExcluirTarefa(Guid idTarefa);
        TarefaResponse Obter(Guid idTarefa);
    }
}
