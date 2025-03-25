using Domain.Commands.CtlToken;
using Shared.Commands;

namespace Domain.Contracts.Handlers
{
    public interface IHandlerToken
    {
        Task<CommandResult<string>> ObterAcessoAsync(CommandObterAcesso command);
    }
}
