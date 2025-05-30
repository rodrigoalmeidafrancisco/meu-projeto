using Shared.Usefuls;

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
            Data = Data.IsList() ? Activator.CreateInstance<T>() : default;
        }

        public CommandResult(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Error = false;
            Total = 0;
            Data = Data.IsList() ? Activator.CreateInstance<T>() : default;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public bool Error { get; set; }
        public int Total { get; set; }
        public T Data { get; set; }

        public void AtualizarRetorno(bool sucesso, string mensagem, bool error, int total, T data)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Error = error;
            Total = total;
            Data = data;
        }

        public void AtualizarRetornoTrue(string mensagem, T data, int total)
        {
            Sucesso = true;
            Mensagem = mensagem;
            Error = false;
            Total = total;
            Data = data;
        }

        public void AtualizarRetornoFalse(string mensagem)
        {
            Sucesso = false;
            Mensagem = mensagem;
            Error = false;
            Total = 0;
            Data = default;
        }

        public void AtualizarRetornoError(Exception ex, string frase)
        {
            Sucesso = false;
            Mensagem = $"{frase}{(ex != null ? $" => {ex.Message}" : "")}";
            Error = true;
            Total = 0;
            Data = Data.IsList() ? Activator.CreateInstance<T>() : default;
        }

        public void AtualizarRetornoError(string frase)
        {
            Sucesso = false;
            Mensagem = frase;
            Error = true;
            Total = 0;
            Data = Data.IsList() ? Activator.CreateInstance<T>() : default;
        }

        public void AtualizarRetornoCommand(CommandResult<T> command)
        {
            Sucesso = command.Sucesso;
            Mensagem = command.Mensagem;
            Error = command.Error;
            Total = command.Total;
            Data = command.Data;
        }

    }
}
