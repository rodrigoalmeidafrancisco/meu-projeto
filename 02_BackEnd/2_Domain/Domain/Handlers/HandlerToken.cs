using Domain.Commands.CtlToken;
using Domain.Contracts.Handlers;
using Shared.Commands;
using Shared.Helpers;

namespace Domain.Handlers
{
    public class HandlerToken : IHandlerToken
    {
        public HandlerToken()
        {

        }

        public async Task<CommandResult<string>> ObterAcessoAsync(CommandObterAcesso command)
        {
            var result = new CommandResult<string>
            {
                Sucesso = true,
                Mensagem = "Token obtido com sucesso!",
                Data = HelperToken.CreateToken(["MeuProjeto.API"])
            };

            return result;
        }

    }
}
