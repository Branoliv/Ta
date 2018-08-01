using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using Tarefas.Domain.Entities.Base;
using Tarefas.Domain.Enums;
using Tarefas.Domain.Resources;

namespace Tarefas.Domain.Entities
{
    public class Tarefa : EntitieBase
    {
        protected Tarefa() { }

        public Tarefa(ListaDeTarefas listaDeTarefas, string titulo, string descricao, DateTime dataInicio, DateTime dataConclusao, Usuario usuario)
        {
            ListaDeTarefas = listaDeTarefas;
            Titulo = titulo;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataConclusao = dataConclusao;
            Usuario = usuario;
            Status = EnumStatus.Ativa;

            ValidarEntidade();

            AddNotifications(usuario);

            if(listaDeTarefas != null)
            {
                AddNotifications(listaDeTarefas);
            }

        }


        public void AtualizarTarefa(ListaDeTarefas listaDeTarefas, string titulo, string descricao, DateTime dataInicio, DateTime dataConclusao, Usuario usuario, EnumStatus status)
        {
            ListaDeTarefas = listaDeTarefas;
            Titulo = titulo;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataConclusao = dataConclusao;
            Usuario = usuario;
            Status = status;

            ValidarEntidade();

            AddNotifications(usuario);

            if (listaDeTarefas != null)
            {
                AddNotifications(listaDeTarefas);
            }
        }


        private void ValidarEntidade()
        {
            new AddNotifications<Tarefa>(this)
                .IfNullOrInvalidLength(x => x.Titulo, 1, 100, MSG.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Título", "1", "100"))
                .IfNullOrInvalidLength(x => x.Descricao, 1, 100, MSG.X0_OBRIGATORIA_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Descrição", "1", "500"));
        }


        public ListaDeTarefas ListaDeTarefas { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataConclusao { get; private set; }
        public Usuario Usuario { get; private set; }
        public EnumStatus Status { get; private set; }
        
    }
}
