using System;
using System.Collections.Generic;
using System.Text;
using Tarefas.Domain.Entities;

namespace Tarefas.Domain.Interfaces.Repositories
{
    public interface IRepositoryListaDeTarefas
    {
        IEnumerable<ListaDeTarefas> Listar(Guid idUsuario);
        ListaDeTarefas Obter(Guid idLista);
        ListaDeTarefas Adicionar(ListaDeTarefas listaDeTarefas);
        void Excluir(ListaDeTarefas listaDeTarefas);

    }
}
