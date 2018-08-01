using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarefas.Domain.Arguments.Base;
using Tarefas.Domain.Arguments.ListaDeTarefas;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Interfaces.Repositories;
using Tarefas.Domain.Interfaces.Services;
using Tarefas.Domain.Resources;

namespace Tarefas.Domain.Services
{
    public class ServiceListaDeTarefas : Notifiable, IServiceListaDeTarefas
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryListaDeTarefas _repositoryListaDeTarefas;
        private readonly IRepositoryTarefa _repositoryTarefa;

        public ServiceListaDeTarefas(IRepositoryUsuario repositoryUsuario, IRepositoryListaDeTarefas repositoryListaDeTarefas, IRepositoryTarefa repositoryTarefa)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryListaDeTarefas = repositoryListaDeTarefas;
            _repositoryTarefa = repositoryTarefa;
        }

        public ListaDeTarefasResponse AdicionarListaDeTarefas(AdicionarListaDeTarefasRequest request, Guid idUsuario)
        {
            Usuario usuario = _repositoryUsuario.Obter(idUsuario);

            ListaDeTarefas listaDeTarefas = new ListaDeTarefas(request.Nome, usuario);

            AddNotifications(listaDeTarefas);

            if (this.IsInvalid()) return null;

            listaDeTarefas = _repositoryListaDeTarefas.Adicionar(listaDeTarefas);

            return (ListaDeTarefasResponse)listaDeTarefas;
        }

        public Response ExcluirListaDeTarefas(Guid idLista)
        {
            bool existe = _repositoryTarefa.ExisteListaAssociada(idLista);

            if (existe)
            {
                AddNotification("ListaDeTarefas", MSG.NAO_E_POSSIVEL_EXCLUIR_UMA_X0_ASSOCIADA_A_UMA_X1.ToFormat("Lista de tarefas", "tarefa"));

                return null;
            }

            ListaDeTarefas listaDeTarefas = _repositoryListaDeTarefas.Obter(idLista);

            if (listaDeTarefas == null)
            {
                AddNotification("ListaDeTarefas", MSG.DADOS_NAO_ENCONTRADOS);
            }

            if (this.IsInvalid()) return null;

            _repositoryListaDeTarefas.Excluir(listaDeTarefas);

            return new Response() { Message = MSG.OPERACAO_REALIZADA_COM_SUCESSO };
        }

        public IEnumerable<ListaDeTarefasResponse> Listar(Guid idUsuario)
        {
            IEnumerable<ListaDeTarefas> listaDeTarefasConllection = _repositoryListaDeTarefas.Listar(idUsuario);

            var response = listaDeTarefasConllection.ToList().Select(entidade => (ListaDeTarefasResponse)entidade);

            return response;
        }
    }
}
