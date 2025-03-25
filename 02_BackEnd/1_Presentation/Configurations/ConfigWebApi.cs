using ElmahCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Logging;
using Shared.Settings;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Configurations
{
    public static class ConfigWebApi
    {
        public static void InitializerWebApplicationBuilder(this WebApplicationBuilder builder)
        {
            //habilitar a visualização de logs de PII
            IdentityModelEventSource.ShowPII = true;

            //Configura para utilizar o IIS, quando publicar.
            builder.WebHost.UseIISIntegration();

            //Configura para exibir os logs no console ao debugar a aplicação.
            builder.Logging.ClearProviders().AddConsole();

            //Obtendo as configurações da API "appsettings"
            SettingsShared.Start(builder.Configuration, builder.Services, builder.Environment.WebRootPath);

            //Configura os parâmetros do System.Text.Json para o Retorno da API   
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(x => x.AddPolicy("AllowAll", y => { y.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            //Permite fazer a validação do ComponentModel.Annotations
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Configurando o proxy
            //HttpClient.DefaultProxy = new WebProxy(new Uri(""), true)
            //{
            //    UseDefaultCredentials = false,
            //    Credentials = CredentialCache.DefaultCredentials
            //};

            //Configura a utilização do Application Insights
            //builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions()
            //{
            //    ConnectionString = "" //Chave de conexão do Application Insights
            //});

            //Comprime o Json no Retorno da API, diminuindo o seu tamanho
            builder.Services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            //Configuração para que o IMemoryCache seja distribuido entre os servidores no balance. 
            builder.Services.AddDistributedMemoryCache();

            //Inicializa as dependências do projeto
            Dependencies.Start(builder.Services);
        }

        public static void InitializerWebApplication(this WebApplication app)
        {
            //Informo que irei utilizar arquivos estáticos (wwwroot)
            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Padrão de rotas do MVC
            app.UseRouting();
            app.MapControllers();

            //Força a API responder apenas em HTTPS
            app.UseHttpsRedirection();

            //Poder realizar chamadas localhost em tempo de desenvolvimento
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication(); // Autenticação
            app.UseAuthorization(); // Roles

            //Informa a utilização do Elmah
            app.UseElmah();
        }

    }
}
