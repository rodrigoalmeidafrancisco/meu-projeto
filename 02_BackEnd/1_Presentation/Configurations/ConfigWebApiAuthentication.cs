using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using Shared.Settings;
using System.Text;

namespace WebApi.Configurations
{
    public static class ConfigWebApiAuthentication
    {
        public static void AddWebApplicationBuilder_Authentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Authority = SettingApp.Aplicacao._Ambiente;
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes($"{SettingApp.Aplicacao._Ambiente}{DtoConstantes.ChaveToken}")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Configuração dos "scope" de acesso e "apolicy" na aplicação
            if (SettingApp.Aplicacao.AcessoPolicyLista.Any())
            {
                builder.Services.AddAuthorization(options =>
                {
                    SettingApp.Aplicacao.AcessoPolicyLista.ForEach(item => { options.AddPolicy(item.Key, policy => { policy.RequireClaim("scope", item.Value); }); });
                });
            }
        }
    }
}
