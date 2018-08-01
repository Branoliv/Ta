using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Tarefas.Domain.Arguments.ListaDeTarefas;
using Tarefas.Domain.Arguments.Usuario;
using Tarefas.Domain.Interfaces.Services;
using Tarefas.Infra.Transactions;

namespace Tarefas.Api.Controllers
{
    public class ListaDeTarefasController : Base.ControllerBase
    {
        private readonly IServiceListaDeTarefas _serviceListaDeTarefas;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListaDeTarefasController(IServiceListaDeTarefas serviceListaDeTarefas, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _serviceListaDeTarefas = serviceListaDeTarefas;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("api/v1/ListaDeTarefas/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceListaDeTarefas.Listar(usuarioResponse.Id);
                return await ResponseAsync(response, _serviceListaDeTarefas);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPost]
        [Route("api/v1/ListaDeTarefas/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarListaDeTarefasRequest request)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceListaDeTarefas.AdicionarListaDeTarefas(request, usuarioResponse.Id);
                return await ResponseAsync(response, _serviceListaDeTarefas);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }


        [HttpDelete]
        [Route("api/v1/ListaDeTarefas/Excluir/{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                var response = _serviceListaDeTarefas.ExcluirListaDeTarefas(id);
                return await ResponseAsync(response, _serviceListaDeTarefas);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
    }
}
