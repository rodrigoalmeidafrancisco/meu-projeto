using Domain.Commands.CtlToken;
using Domain.Contracts.Handlers;
using Shared.Commands;
using Shared.Helpers;
using Shared.Settings;

namespace Domain.Handlers
{
    public class HandlerToken : IHandlerToken
    {
        public HandlerToken()
        {

        }

        public CommandResult<string> ObterAcesso(CommandObterAcesso command)
        {
            var result = new CommandResult<string>
            {
                Sucesso = true,
                Mensagem = "Token obtido com sucesso!",
                Data = HelperToken.CreateToken(SettingApp.Aplicacao._Ambiente, ["MeuProjeto.API"])
            };

            return result;
        }

    }
}
