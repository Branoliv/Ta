using System;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Tarefas.Domain.Arguments.Base;
using Tarefas.Domain.Arguments.Usuario;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Interfaces.Repositories;
using Tarefas.Domain.Interfaces.Services;
using Tarefas.Domain.Resources;
using Tarefas.Domain.ValueObjects;

namespace Tarefas.Domain.Services
{
    public class ServiceUsuario : Notifiable, IServiceUsuario
    {
        private readonly IRepositoryUsuario _repositoryUsuario;

        public ServiceUsuario(IRepositoryUsuario repositoryUsuario)
        {
            _repositoryUsuario = repositoryUsuario;
        }

        public AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request)
        {
            if (request == null)
            {
                AddNotification("AdicionarUsuarioRequest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("AdicionarUsuarioRequest"));
                return null;
            }

            //Cria value objects
            Nome nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            Email email = new Email(request.Email);
            Endereco endereco = new Endereco(request.Logradouro, request.NumeroResid, request.Bairro, request.Cidade, request.Estado, request.Pais, request.Cep);

            //Cria entidade
            Usuario usuario = new Usuario(nome, email, request.Senha, endereco);

            AddNotifications(usuario);

            if (this.IsInvalid()) return null;

            //Persiste no banco de dados
            _repositoryUsuario.Salvar(usuario);
            return new AdicionarUsuarioResponse(usuario.Id);
        }

        public Response AtualizarUsuario(UsuarioRequest request, Guid idUsuario)
        {
            if(request == null)
            {
                AddNotification("AtualizarTarefa", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("RegistarVideoRequest"));
                return null;
            }

            //Cria value objects
            Nome nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            Email email = new Email(request.Email);
            Endereco endereco = new Endereco(request.Logradouro, request.NumeroResid, request.Bairro, request.Cidade, request.Estado, request.Pais, request.Cep);

            var usuario = _repositoryUsuario.Obter(idUsuario);

            if(usuario == null)
            {
                AddNotification("Usuario", MSG.X0_NAO_INFORMADO.ToFormat("Usuário"));
            }

            AddNotifications(usuario);

            if (this.IsInvalid()) return null;

            usuario.AtualizarUsuario(nome, email, endereco);

            _repositoryUsuario.Atualizar(usuario);

            return (Response)usuario;
        }

        public AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            if (request == null)
            {
                AddNotification("AutenticarUsuarioRequest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("AutenticarUsusarioRequest"));
                return null;
            }

            var email = new Email(request.Email);
            var usuario = new Usuario(email, request.Senha);

            AddNotifications(usuario);

            if (this.IsInvalid()) return null;

            usuario = _repositoryUsuario.Obter(usuario.Email.Endereco, usuario.Senha);

            if (usuario == null)
            {
                AddNotification("Usuario", MSG.DADOS_NAO_ENCONTRADOS);
                return null;
            }


            var response = (AutenticarUsuarioResponse)usuario;

            return response;

        }

        public UsuarioResponse Obter(Guid idUsuario)
        {
            if (idUsuario == null)
            {
                AddNotification("ObterUsuario", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Id Usuário"));
            }

            Usuario usuario = _repositoryUsuario.Obter(idUsuario);

            if(usuario == null)
            {
                AddNotification("Usuário", MSG.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            var response = (UsuarioResponse)usuario;

            return response;
        }
    }
}
