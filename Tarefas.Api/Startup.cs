using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using Tarefas.Api.Security;
using Tarefas.Domain.Interfaces.Repositories;
using Tarefas.Domain.Interfaces.Services;
using Tarefas.Domain.Services;
using Tarefas.Infra.Persistence.EF;
using Tarefas.Infra.Persistence.Repositories;
using Tarefas.Infra.Transactions;

namespace Tarefas.Api
{
    public class Startup
    {
        private const string ISSUER = "aeg6esv7";
        private const string AUDIENCE = "bdrsd64sdfe6";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // Adiciona injeção de dependência
            services.AddScoped<TarefasContext, TarefasContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddTransient<IServiceUsuario, ServiceUsuario>();
            services.AddTransient<IServiceListaDeTarefas, ServiceListaDeTarefas>();
            services.AddTransient<IServiceTarefa, ServiceTarefa>();

            // Repositories
            services.AddTransient<IRepositoryUsuario, RepositoryUsuario>();
            services.AddTransient<IRepositoryListaDeTarefas, RepositoryLIstaDeTarefas>();
            services.AddTransient<IRepositoryTarefa, RepositoryTarefa>();

            // Configuração do Token
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations); // AddSingleton => diz que vai existir uma única instância

            var tokenConfigurations = new TokenConfigurations
            {
                Audience = AUDIENCE,
                Issuer = ISSUER,
                Seconds = int.Parse(TimeSpan.FromDays(1).TotalSeconds.ToString())
            };

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.signingCredentials.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token comoo forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddCors();

            // Para todoas as requisições serem necessarias o token, para um endpont não exisgir o token
            // deve colocar o [AllowAnonymous]
            // Caso remova essa linha, para todas as requesições que precisar de token, deve colocar 
            // o atributo [Authorize("Bearer")]
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           

            // Aplicando documentação com suagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Tarefas", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tarefas - v1");
            });
        }
    }
}
