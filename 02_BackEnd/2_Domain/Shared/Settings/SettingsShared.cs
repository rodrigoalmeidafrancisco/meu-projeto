using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Settings
{
    public static class SettingsShared
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

        private static IServiceCollection _serviceCollection;

        public static void Start(IConfiguration configuration, IServiceCollection services, string webRootPath)
        {
            _serviceCollection = services;

            Aplicacao = new SettingsSharedAplicacao();
            configuration.GetSection("Aplicacao").Bind(Aplicacao);

            AppSettings = new SettingsSharedAppSettings();
            configuration.GetSection("AppSettings").Bind(AppSettings);

            ConnectionStrings = new SettingsSharedConnectionStrings();
            configuration.GetSection("ConnectionStrings").Bind(ConnectionStrings);
        }

        public static T GetService<T>()
        {
            ServiceProvider servicesProvider = _serviceCollection.BuildServiceProvider();

            using (var scope = servicesProvider.CreateScope())
            {
                return scope.ServiceProvider.GetService<T>();
            }
        }

        public static T GetRequiredService<T>()
        {
            ServiceProvider servicesProvider = _serviceCollection.BuildServiceProvider();

            using (var scope = servicesProvider.CreateScope())
            {
                return scope.ServiceProvider.GetRequiredService<T>();
            }
        }

        public static SettingsSharedAplicacao Aplicacao { get; set; }
        public static SettingsSharedAppSettings AppSettings { get; set; }
        public static SettingsSharedConnectionStrings ConnectionStrings { get; set; }

    }
}
