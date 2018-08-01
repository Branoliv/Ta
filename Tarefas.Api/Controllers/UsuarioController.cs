using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Tarefas.Api.Security;
using Tarefas.Domain.Arguments.Usuario;
using Tarefas.Domain.Interfaces.Services;
using Tarefas.Infra.Transactions;

namespace Tarefas.Api.Controllers
{
    public class UsuarioController : Base.ControllerBase
    {
        private readonly IServiceUsuario _serviceUsuario;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioController(IServiceUsuario serviceUsuario, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _serviceUsuario = serviceUsuario;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/Usuario/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarUsuarioRequest request)
        {
            try
            {
                var response = _serviceUsuario.AdicionarUsuario(request);
                return await ResponseAsync(response, _serviceUsuario);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/Usuario/Autenticar")]
        public object Autenticar(
            [FromBody]AutenticarUsuarioRequest request,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenconfigurations)
        {
            bool credenciaisValidas = false;
            AutenticarUsuarioResponse response = _serviceUsuario.AutenticarUsuario(request);

            credenciaisValidas = response != null;

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(response.Id.ToString(), "Id"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim("Usuario", JsonConvert.SerializeObject(response))
                    }

                    );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenconfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenconfigurations.Issuer,
                    Audience = tokenconfigurations.Audience,
                    SigningCredentials = signingConfigurations.signingCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    primeiroNome = response.PrimeiroNome,
                    idUsuario = response.Id
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    _serviceUsuario.Notifications
                };
            }
        }


        [HttpPost]
        [Route("api/v1/Usuario/Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody]UsuarioRequest request)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceUsuario.AtualizarUsuario(request, usuarioResponse.Id);
                return await ResponseAsync(request, _serviceUsuario);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }


        [HttpGet]
        [Route("api/v1/Usuario/Obter/{idUsuario:Guid}")]
        public async Task<IActionResult> Obter (Guid idUsuario)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                UsuarioResponse response = _serviceUsuario.Obter(idUsuario);

                return await ResponseAsync(response, _serviceUsuario);

            }
            catch(Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
    }
}
