using System;
using System.Collections.Generic;
using Tarefas.Domain.Arguments.Base;
using Tarefas.Domain.Arguments.ListaDeTarefas;
using Tarefas.Domain.Arguments.Usuario;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Interfaces.Services.Base;

namespace Tarefas.Domain.Interfaces.Services
{
    public interface IServiceListaDeTarefas : IServiceBase
    {
        IEnumerable<ListaDeTarefasResponse> Listar(Guid idUsuario);
        ListaDeTarefasResponse AdicionarListaDeTarefas(AdicionarListaDeTarefasRequest request, Guid idUsuario);
        Response ExcluirListaDeTarefas(Guid idLista);
    }
}
