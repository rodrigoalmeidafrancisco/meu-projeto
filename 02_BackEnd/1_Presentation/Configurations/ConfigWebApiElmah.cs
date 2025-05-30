using ElmahCore.Mvc;
using ElmahCore.Sql;
using ElmahCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Settings;

namespace WebApi.Configurations
{
    public static class ConfigWebApiElmah
    {
        public static void AddWebApplicationBuilder_Elmah(this WebApplicationBuilder builder)
        {
            builder.Services.AddElmah<SqlErrorLog>(options =>
            {
                options.Path = @"elmah";
                options.SqlServerDatabaseSchemaName = "Logs";
                options.ApplicationName = $"{SettingApp.Aplicacao.NomeAplicacao} (WebApi)";
                options.OnPermissionCheck = context => SettingApp.Aplicacao._Ambiente.Equals("PROD", StringComparison.OrdinalIgnoreCase) == false;
                options.ConnectionString = SettingApp.ConnectionStrings.Default;
                options.Notifiers.Add(new ElmahNotification());
                options.Filters.Add(new ElmahErrorFilter());
            });
        }

        public class ElmahErrorFilter : ExceptionFilterAttribute, IErrorFilter
        {
            public void OnErrorModuleFiltering(object sender, ExceptionFilterEventArgs args)
            {
                if (args.Exception.GetBaseException() is FileNotFoundException)
                {
                    args.Dismiss();
                }

                if (args.Context is HttpContext httpContext)
                {
                    if (httpContext.Response.StatusCode == 404)
                    {
                        args.Dismiss();
                    }
                }
            }
        }

        public class ElmahNotification : IErrorNotifier
        {
            public string Name => $"{SettingApp.Aplicacao.NomeAplicacao} (WebApi)";

            public void Notify(Error error)
            {

            }
        }
    }
}
