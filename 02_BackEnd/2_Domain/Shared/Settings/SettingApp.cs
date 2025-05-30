using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Settings
{
    public static class SettingApp
    {
        /*====================================================================================================================
        | ********************* Declaração da propriedade ********************                                               |                  
        | *  public static SettingsAplicacao Aplicacao { get; set; }                                                         |
        | ********************************************************************                                               |
        |                                                                                                                    |
        | -> Para obter uma seção inteira do json, usar dessa forma:                                                         |
        | Aplicacao = new SettingsAplicacao();                                                                               |
        | configuration.GetSection("Aplicacao").Bind(Aplicacao);                                                             |
        |                                                                                                                    |
        | ------------------------------------------------------------------------------------------------------------------ |
        |                                                                                                                    |
        | -> Para obter cada informação individual, usar dessa forma:                                                        |
        | Aplicacao = new SettingsAplicacao() { GuidIdAplicacaoAPI = configuration["Aplicacao:GuidIdAplicacaoAPI"] };        |
        |                                                                                                                    |
        ====================================================================================================================*/

        public static void Start(IConfiguration configuration, string webRootPath)
        {
            Aplicacao = new SettingAppAplicacao();
            configuration.GetSection("Aplicacao").Bind(Aplicacao);

            ApplicationInsights = new SettingAppApplicationInsights();
            configuration.GetSection("ApplicationInsights").Bind(ApplicationInsights);

            AppSettings = new SettingAppAppSettings();
            configuration.GetSection("AppSettings").Bind(AppSettings);

            ConnectionStrings = new SettingAppConnectionStrings();
            configuration.GetSection("ConnectionStrings").Bind(ConnectionStrings);
        }

        public static SettingAppAplicacao Aplicacao { get; set; }
        public static SettingAppApplicationInsights ApplicationInsights { get; set; }
        public static SettingAppAppSettings AppSettings { get; set; }
        public static SettingAppConnectionStrings ConnectionStrings { get; set; }

    }
}
