using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Interfaces.Repositories;
using Tarefas.Infra.Persistence.EF;

namespace Tarefas.Infra.Persistence.Repositories
{
    public class RepositoryTarefa : IRepositoryTarefa
    {
        private readonly TarefasContext _context;

        public RepositoryTarefa(TarefasContext context)
        {
            _context = context;
        }

        public void Adicionar(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
        }

        public void Atualizar(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
        }

        public void Excluir(Tarefa tarefa)
        {
            _context.Remove(tarefa);
        }

        public bool ExisteListaAssociada(Guid idTarefa)
        {
            return _context.Tarefas.Any(x => x.ListaDeTarefas.Id == idTarefa);
        }

        public IEnumerable<Tarefa> Listar(Guid idUsuario)
        {
            return _context.Tarefas.Include(x => x.ListaDeTarefas).Include(x => x.Usuario)
                .Where(x => x.Usuario.Id == idUsuario).ToList();
        }

        public IEnumerable<Tarefa> ListarPorLista(Guid idLista)
        {
            return _context.Tarefas.Include(x => x.ListaDeTarefas).Include(x => x.Usuario)
                .Where(x => x.ListaDeTarefas.Id == idLista).ToList();
        }

        public Tarefa Obter(Guid idTarefa)
        {
            var tarefa = _context.Tarefas
                .Include(x => x.ListaDeTarefas)
                .Include(x => x.Usuario)
                .Where(x => x.Id == idTarefa);
            return tarefa.Single(); ;
        }
    }
}
