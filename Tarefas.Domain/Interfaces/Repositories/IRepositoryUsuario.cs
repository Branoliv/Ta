using System;
using Tarefas.Domain.Entities;

namespace Tarefas.Domain.Interfaces.Repositories
{
    public interface IRepositoryUsuario
    {
        Usuario Obter(Guid id);

        Usuario Obter(string email, string senha);

        void Salvar(Usuario usuario);

        bool Existe(string email);

        void Atualizar(Usuario usuario);
    }
}
