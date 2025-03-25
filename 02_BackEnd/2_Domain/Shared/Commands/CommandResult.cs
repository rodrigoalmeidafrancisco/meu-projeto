using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Shared.Commands
{
    public class CommandResult<T>
    {
        public CommandResult()
        {
            Sucesso = false;
            Mensagem = null;
            Error = false;
            Total = 0;
            Data = default;
            TempoTotalWatch = Stopwatch.StartNew();
        }

        public CommandResult(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Error = false;
            Total = 0;
            Data = default;
            TempoTotalWatch = Stopwatch.StartNew();
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public bool Error { get; set; }
        public int Total { get; set; }
        public T Data { get; set; }
        public string TempoTotalRequisicao => TempoTotalWatch?.Elapsed.ToString(@"hh\:mm\:ss\.fffffff");

        [JsonIgnore]
        public Stopwatch TempoTotalWatch { get; set; }

        public void AtualizarRetorno(CommandResult<T> commandResult)
        {
            Sucesso = commandResult.Sucesso;
            Mensagem = commandResult.Mensagem;
            Error = commandResult.Error;
            Total = commandResult.Total;
            Data = commandResult.Data;
        }

        public void AtualizarRetornoError(Exception ex, string frase, Dictionary<string, string> propertiesLog = null, Dictionary<string, double> metricsLog = null)
        {
            Sucesso = false;
            Mensagem = $"{frase}{(ex != null ? $" => {ex.Message}" : "")}";
            Error = true;
            Total = 0;
            Data = default;
        }

    }
}
