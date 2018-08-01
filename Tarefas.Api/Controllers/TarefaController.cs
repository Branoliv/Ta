using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Tarefas.Domain.Arguments.Tarefa;
using Tarefas.Domain.Arguments.Usuario;
using Tarefas.Domain.Interfaces.Services;
using Tarefas.Infra.Transactions;

namespace Tarefas.Api.Controllers
{
    public class TarefaController : Base.ControllerBase
    {
        private readonly IServiceTarefa _serviceTarefa;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TarefaController(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IServiceTarefa serviceTarefa) : base(unitOfWork)
        {
            _serviceTarefa = serviceTarefa;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("api/v1/Tarefa/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceTarefa.Listar(usuarioResponse.Id);
                return await ResponseAsync(response, _serviceTarefa);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
        
        [HttpPost]
        [Route("api/v1/Tarefa/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarTarefaRequest request)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceTarefa.AdicionarTarefa(request, usuarioResponse.Id);
                return await ResponseAsync(request, _serviceTarefa);

            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPost]
        [Route("api/v1/Tarefa/Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody]TarefaRequest request)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceTarefa.AtualizarTarefa(request, usuarioResponse.Id);
                return await ResponseAsync(request, _serviceTarefa);

            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpDelete]
        [Route("api/v1/Tarefa/Excluir/{idTarefa:Guid}")]
        public async Task<IActionResult> Excluir(Guid idTarefa)
        {
            try
            {
                string usuarioclaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioclaims);

                var response = _serviceTarefa.ExcluirTarefa(idTarefa);
                return await ResponseAsync(response, _serviceTarefa);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }


        [HttpGet]
        [Route("api/v1/Tarefa/Obter/{idTarefa:Guid}")]
        public async Task<IActionResult> Obter(Guid idTarefa)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                TarefaResponse response = _serviceTarefa.Obter(idTarefa);

                return await ResponseAsync(response, _serviceTarefa);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpGet]
        [Route("api/v1/Tarefa/ListarPorLista/{idLista:Guid}")]
        public async Task<IActionResult> ListarPorLista(Guid idLista)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceTarefa.ListarPorLista(idLista);

                return await ResponseAsync(response, _serviceTarefa);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }


    }
}
