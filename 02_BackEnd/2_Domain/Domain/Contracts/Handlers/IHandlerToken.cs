using Domain.Commands.CtlToken;
using Shared.Commands;

namespace Domain.Contracts.Handlers
{
    public interface IHandlerToken
    {
        CommandResult<string> ObterAcesso(CommandObterAcesso command);
    }
}
