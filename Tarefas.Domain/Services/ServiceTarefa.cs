using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tarefas.Domain.Arguments.Base;
using Tarefas.Domain.Arguments.ListaDeTarefas;
using Tarefas.Domain.Arguments.Tarefa;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Enums;
using Tarefas.Domain.Interfaces.Repositories;
using Tarefas.Domain.Interfaces.Services;
using Tarefas.Domain.Resources;

namespace Tarefas.Domain.Services
{
    public class ServiceTarefa : Notifiable, IServiceTarefa
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryListaDeTarefas _repositoryListaDeTarefas;
        private readonly IRepositoryTarefa _repositoryTarefa;

        public ServiceTarefa(IRepositoryUsuario repositoryUsuario, IRepositoryListaDeTarefas repositoryListaDeTarefas, IRepositoryTarefa repositoryTarefa)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryListaDeTarefas = repositoryListaDeTarefas;
            _repositoryTarefa = repositoryTarefa;
        }

        public AdicionarTarefaResponse AdicionarTarefa(AdicionarTarefaRequest request, Guid idUsuario)
        {
            if (request == null)
            {
                AddNotification("AdicionarTarefa", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("RegistrarVideoRequest"));
                return null;
            }

            Usuario usuario = _repositoryUsuario.Obter(idUsuario);

            if (usuario == null)
            {
                AddNotification("Usuario", MSG.X0_NAO_INFORMADO.ToFormat("Usuário"));
                return null;
            }

            ListaDeTarefas listaDeTarefas = null;
            if (request.IdListaDeTarefas != Guid.Empty)
            {
                listaDeTarefas = _repositoryListaDeTarefas.Obter(request.IdListaDeTarefas);
                if (listaDeTarefas == null)
                {
                    AddNotification("ListaDeTarefas", MSG.X0_NAO_INFORMADA.ToFormat("lista de tarefas"));
                    return null;
                }
            }

            var tarefa = new Tarefa(listaDeTarefas, request.Titulo, request.Descricao, request.DataInicio, request.DataConclusao, usuario);

            AddNotifications(tarefa);

            if (this.IsInvalid()) return null;

            _repositoryTarefa.Adicionar(tarefa);

            return new AdicionarTarefaResponse(tarefa.Id);

        }

        public Response AtualizarTarefa(TarefaRequest request, Guid idUsuario)
        {
            if (request == null)
            {
                AddNotification("AtualizarTarefa", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("RegistrarVideoRequest"));
                return null;
            }

            Usuario usuario = _repositoryUsuario.Obter(idUsuario);

            if (usuario == null)
            {
                AddNotification("Usuario", MSG.X0_NAO_INFORMADO.ToFormat("Usuário"));
                return null;
            }

            ListaDeTarefas listaDeTarefas = null;
            if (request.IdListaDeTarefas != Guid.Empty)
            {
                listaDeTarefas = _repositoryListaDeTarefas.Obter(request.IdListaDeTarefas);
                if (listaDeTarefas == null)
                {
                    AddNotification("ListaDeTarefas", MSG.X0_NAO_INFORMADA.ToFormat("lista de tarefas"));
                    return null;
                }
            }

            var tarefa = _repositoryTarefa.Obter(request.IdTarefa);

            if(tarefa == null)
            {
                AddNotification("Tarefa", MSG.X0_NAO_INFORMADO.ToFormat("Tarefa"));
                return null;
            }

            EnumStatus status = Enum.Parse<EnumStatus>(request.Status);

            AddNotifications(tarefa);

            if (this.IsInvalid()) return null;

            tarefa.AtualizarTarefa(listaDeTarefas, request.Titulo, request.Descricao, request.DataInicio, request.DataConclusao, usuario, status);

             _repositoryTarefa.Atualizar(tarefa);

            return (Response)tarefa;

        }

        public Response ExcluirTarefa(Guid idTarefa)
        {
            Tarefa tarefa = _repositoryTarefa.Obter(idTarefa);

            if (tarefa == null)
            {
                AddNotification("Tarefa", MSG.DADOS_NAO_ENCONTRADOS);
            }

            if (this.IsInvalid()) return null;

            _repositoryTarefa.Excluir(tarefa);

            return new Response() { Message = MSG.OPERACAO_REALIZADA_COM_SUCESSO };
        }

        public IEnumerable<TarefaResponse> Listar(Guid idLista)
        {
            IEnumerable<Tarefa> tarefaCollection = _repositoryTarefa.Listar(idLista);

            var response = tarefaCollection.ToList().Select(entidade => (TarefaResponse)entidade);

            return response;

        }

        public IEnumerable<TarefaResponse> ListarPorLista(Guid idLista)
        {
            IEnumerable<Tarefa> tarefaCollection = _repositoryTarefa.ListarPorLista(idLista);

            var response = tarefaCollection.ToList().Select(entidade => (TarefaResponse)entidade);

            return response;
        }

        public TarefaResponse Obter(Guid idTarefa)
        {
            if (idTarefa == null)
            {
                AddNotification("ObterTarefa", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Id tarefa"));
                return null;
            }

            Tarefa tarefa = _repositoryTarefa.Obter(idTarefa);


            if(tarefa == null)
            {
                AddNotification("Tarefa", MSG.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            var response = (TarefaResponse)tarefa;

            return response;
        }
    }
}
