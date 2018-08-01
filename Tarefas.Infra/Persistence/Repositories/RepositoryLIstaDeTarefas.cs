using System;
using System.Collections.Generic;
using System.Linq;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Interfaces.Repositories;
using Tarefas.Infra.Persistence.EF;

namespace Tarefas.Infra.Persistence.Repositories
{
    public class RepositoryLIstaDeTarefas : IRepositoryListaDeTarefas
    {
        private readonly TarefasContext _context;

        public RepositoryLIstaDeTarefas(TarefasContext context)
        {
            _context = context;
        }

        public ListaDeTarefas Adicionar(ListaDeTarefas listaDeTarefas)
        {
            _context.Add(listaDeTarefas);

            return listaDeTarefas;
        }

        public void Excluir(ListaDeTarefas listaDeTarefas)
        {
            _context.ListaDeTarefas.Remove(listaDeTarefas);
        }

        public IEnumerable<ListaDeTarefas> Listar(Guid idUsuario)
        {
            return _context.ListaDeTarefas.Where(x => x.Usuario.Id == idUsuario).ToList();
        }

        public ListaDeTarefas Obter(Guid idLista)
        {
            return _context.ListaDeTarefas.FirstOrDefault(x => x.Id == idLista);
        }
    }
}
