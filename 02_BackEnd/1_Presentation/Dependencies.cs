using Data.Contexts;
using Domain.Contracts.Handlers;
using Domain.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Settings;

namespace WebApi
{
    public static class Dependencies
    {
        //services.AddDbContext -> já faz a mesma função do AddScoped.
        //services.AddSingleton -> Provem uma intancia do objeto para aplicação toda, sempre ativa.
        //services.AddScoped -> Busca sempre da memoria caso exista, se não cria.
        //services.AddTransient -> Cada requisição cria uma nova instância.

        public static void Start(IServiceCollection services)
        {
            //*************************** Contexto Aplicação ***************************
            services.AddDbContext<ContextDefault>(options => options.UseSqlServer(SettingApp.ConnectionStrings.Default)
               .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            );

            //Faz a dependência do ImemoryCache, caso seja necessário utilizar no projeto
            services.AddSingleton<IMemoryCache, MemoryCache>();

            Domain(services);
            Data(services);
            EventBus(services);
        }

        private static void Domain(IServiceCollection services)
        {
            //Deixar em ordem alfabética
            services.AddScoped<IHandlerToken, HandlerToken>();
        }

        private static void Data(IServiceCollection services)
        {
            //Deixar em ordem alfabética
            //services.AddScoped<IRepository..., Repository...>();
        }

        private static void EventBus(IServiceCollection services)
        {

        }

    }
}
