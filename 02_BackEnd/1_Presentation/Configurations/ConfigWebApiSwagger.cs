using Microsoft.OpenApi.Models;
using Shared.Settings;
using System.Reflection;

namespace WebApi.Configurations
{
    public static class ConfigWebApiSwagger
    {
        public static void AddWebApplicationBuilder_Swagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = SettingApp.Aplicacao.NomeAplicacao,
                    Version = "v1",
                    Description = "Documentação da API"
                });

                //Configura o Swagger para que possa fazer requisição com a API, incluíndo um Bearer Token para as chamadas direto na tela.
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Insira o token JWT desta forma: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         Array.Empty<string>()
                    }
                });

                //Permite que apareça o Summary do método da controller
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });
        }

        public static void AddWebApplication_Swagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.DefaultModelsExpandDepth(-1);
                x.SwaggerEndpoint("v1/swagger.json", SettingApp.Aplicacao.NomeAplicacao);
                x.DocumentTitle = SettingApp.Aplicacao.NomeAplicacao;
            });
        }

    }
}
