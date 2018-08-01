using System;
using Tarefas.Domain.Arguments.Base;
using Tarefas.Domain.Arguments.Usuario;
using Tarefas.Domain.Interfaces.Services.Base;

namespace Tarefas.Domain.Interfaces.Services
{
    public interface IServiceUsuario : IServiceBase
    {
        AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request);
        AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request);
        Response AtualizarUsuario(UsuarioRequest request, Guid idUsuario);
        UsuarioResponse Obter(Guid idUsuario);
    }
}
