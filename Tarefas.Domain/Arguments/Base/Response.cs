using System;
using Tarefas.Domain.Entities;

namespace Tarefas.Domain.Arguments.Base
{
    public class Response
    {
        public string Message { get; set; }

        public static explicit operator Response(Entities.Tarefa entidade)
        {
            return new Response()
            {
                 Message =  Resources.MSG.OPERACAO_REALIZADA_COM_SUCESSO 
            };
        }

        public static explicit operator Response(Entities.Usuario entidade)
        {
            return new Response()
            {
                Message = Resources.MSG.OPERACAO_REALIZADA_COM_SUCESSO
            };
        }
    }
}
