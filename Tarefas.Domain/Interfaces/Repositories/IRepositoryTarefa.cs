using System;
using System.Collections.Generic;
using Tarefas.Domain.Entities;

namespace Tarefas.Domain.Interfaces.Repositories
{
    public interface IRepositoryTarefa
    {
         Tarefa Obter(Guid idTarefa);

        void Adicionar(Tarefa tarefa);

        void Atualizar(Tarefa tarefa);

        IEnumerable<Tarefa> Listar(Guid idUsuario);

        IEnumerable<Tarefa> ListarPorLista(Guid idLista);

        bool ExisteListaAssociada(Guid idTarefa);

        void Excluir(Tarefa tarefa);

    }
}
