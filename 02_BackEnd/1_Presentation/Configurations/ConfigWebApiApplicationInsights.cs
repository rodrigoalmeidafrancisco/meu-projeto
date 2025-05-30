using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using Shared.Helpers;
using Shared.Settings;
using Developer.ExtensionCore;

namespace WebApi.Configurations
{
    public static class ConfigWebApiApplicationInsights
    {
        /// <summary>
        /// Adiciona as dependências da Telemetria
        /// </summary>
        /// <param name="builder"></param>
        public static void AddWebApplicationBuilder_Telemetry(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ITelemetryInitializer, TelemetryInitializer>();
        }

        /// <summary>
        /// Inicializa a Telemetria
        /// </summary>
        /// <param name="app"></param>
        public static void AddWebApplication_Telemetry(this WebApplication app)
        {
            HelperLog.Start(app.Services.GetRequiredService<TelemetryClient>());
        }

        public class TelemetryInitializer : ITelemetryInitializer
        {
            public void Initialize(ITelemetry telemetry)
            {
                if (telemetry.Context != null)
                {
                    if (telemetry.Context.Cloud != null && telemetry.Context.Cloud.RoleName.IsNullOrEmptyOrWhiteSpace())
                    {
                        telemetry.Context.Cloud.RoleName = $"{SettingApp.Aplicacao.NomeAplicacao} (WebApi) - {SettingApp.Aplicacao._Ambiente}";
                    }

                    telemetry.Context.GlobalProperties["Ambiente"] = SettingApp.Aplicacao._Ambiente;
                    telemetry.Context.GlobalProperties["Build"] = SettingApp.Aplicacao._Build;
                    telemetry.Context.GlobalProperties["Release"] = SettingApp.Aplicacao._Release;
                }

                if (telemetry is RequestTelemetry requestTelemetry)
                {
                    //int.TryParse(requestTelemetry.ResponseCode, out int cod);

                    //if (cod == 400)
                    //{
                    //    //Retorno da requisição configurado como Ok e não como falha.
                    //    requestTelemetry.Success = true;
                    //    requestTelemetry.Context.GlobalProperties[$"Overriden{cod}s"] = "true";
                    //}
                }
            }
        }

    }
}
