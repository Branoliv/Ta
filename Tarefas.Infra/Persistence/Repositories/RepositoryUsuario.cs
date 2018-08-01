using System;
using System.Linq;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Interfaces.Repositories;
using Tarefas.Infra.Persistence.EF;
using Tarefas.Infra.Persistence.Mongo;

namespace Tarefas.Infra.Persistence.Repositories
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly TarefasContext _context;
        private readonly TarefaContextMongo _contextMg;

        public RepositoryUsuario(TarefasContext context, TarefaContextMongo contextMg)
        {
            _context = context;
            _contextMg = contextMg;
        }

        public void Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public bool Existe(string email)
        {
            return _context.Usuarios.Any(x => x.Email.Endereco == email);
        }

        public Usuario Obter(Guid id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public Usuario Obter(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.Endereco == email && x.Senha == senha);
        }

        public void Salvar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _contextMg.Usuarios.InsertOne(usuario);
        }
    }
}
