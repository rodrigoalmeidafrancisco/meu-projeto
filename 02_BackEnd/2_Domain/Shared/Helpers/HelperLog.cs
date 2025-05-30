using Microsoft.ApplicationInsights;
using Shared.Commands;

namespace Shared.Helpers
{
    public static class HelperLog
    {
        private static TelemetryClient _telemetryClient { get; set; }

        public static void Start(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        #region Métodos para Geração do LOG no Azure Devops

        public static void AddTrackEvent(string phrase, Dictionary<string, string> propertiesLog = null, Dictionary<string, double> metricsLog = null)
        {
            metricsLog = metricsLog ?? [];
            propertiesLog = propertiesLog ?? [];
            _telemetryClient?.TrackEvent($"{phrase}", propertiesLog, metricsLog);
        }

        public static void AddTrackEvent<Then>(string phrase, CommandResult<Then> commandResult, Dictionary<string, string> propertiesLog = null, Dictionary<string, double> metricsLog = null)
        {
            metricsLog = metricsLog ?? [];
            propertiesLog = propertiesLog ?? [];

            if (commandResult != null)
            {
                propertiesLog["Result.Sucesso"] = commandResult.Sucesso.ToString();
                propertiesLog["Result.Mensagem"] = commandResult.Mensagem;
            }

            AddTrackEvent($"{phrase}", propertiesLog, metricsLog);
        }

        public static void AddTrackEventException<Then>(string phrase, CommandResult<Then> commandResult, Dictionary<string, string> propertiesLog = null, Dictionary<string, double> metricsLog = null)
        {
            metricsLog = metricsLog ?? [];
            propertiesLog = propertiesLog ?? [];

            if (commandResult != null)
            {
                propertiesLog["Result.Sucesso"] = commandResult.Sucesso.ToString();
                propertiesLog["Result.Mensagem"] = commandResult.Mensagem;
            }

            _telemetryClient?.TrackException(new Exception(phrase), propertiesLog, metricsLog);
        }

        public static void AddTrackEventException<Then>(Exception exception, CommandResult<Then> commandResult, Dictionary<string, string> propertiesLog = null, Dictionary<string, double> metricsLog = null)
        {
            metricsLog = metricsLog ?? [];
            propertiesLog = propertiesLog ?? [];

            if (commandResult != null)
            {
                propertiesLog["Result.Sucesso"] = commandResult.Sucesso.ToString();
                propertiesLog["Result.Mensagem"] = commandResult.Mensagem;
            }

            if (exception != null)
            {
                propertiesLog["Exception.Message"] = exception.Message?.ToString();
                propertiesLog["Exception.InnerException"] = exception.InnerException?.ToString();
                propertiesLog["Exception.StackTrace"] = exception.StackTrace?.ToString();
            }

            _telemetryClient?.TrackException(exception, propertiesLog, metricsLog);
        }

        #endregion Métodos para Geração do LOG no Azure Devops

        #region Métodos para criar as propriedades de log básicas

        public static Dictionary<string, string> GetPropertiesController(string controller, string metodo, string colabLog = null)
        {
            var propriedadesLog = new Dictionary<string, string>()
            {
                { "Controller", controller },
                { "Método", metodo },
                { "ColabLog", colabLog },
            };

            return propriedadesLog;
        }

        public static Dictionary<string, string> GetPropertiesHandler(string handler, string metodo, string colabLog = null)
        {
            var propriedadesLog = new Dictionary<string, string>()
            {
                { "Handler", handler },
                { "Método", metodo },
                { "ColabLog", colabLog },
            };

            return propriedadesLog;
        }

        public static Dictionary<string, string> GetPropertiesRepository(string repository, string metodo, string colabLog = null)
        {
            var propriedadesLog = new Dictionary<string, string>()
            {
                { "Repository", repository },
                { "Método", metodo },
                { "ColabLog", colabLog },
            };

            return propriedadesLog;
        }

        public static Dictionary<string, string> GetPropertiesService(string repository, string metodo, string colabLog = null)
        {
            var propriedadesLog = new Dictionary<string, string>()
            {
                { "Servico", repository },
                { "Método", metodo },
                { "ColabLog", colabLog },
            };

            return propriedadesLog;
        }

        public static void AtualizarColabLog(this Dictionary<string, string> propriedadesLog, string colabLog)
        {
            propriedadesLog["ColabLog"] = colabLog;
        }

        #endregion Métodos para criar as propriedades de log básicas

    }
}
