using Domain.Commands.CtlToken;
using Domain.Contracts.Handlers;
using Domain.Usefuls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;
using Shared.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class TokenController : ControllerBase
    {
        private readonly IHandlerToken _handlerToken;

        public TokenController(IHandlerToken handlerToken)
        {
            _handlerToken = handlerToken;
        }

        /// <summary>
        /// Obtém um token de acesso
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("v1/Obter/Acesso")]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        public IActionResult ObterAcesso([FromBody] CommandObterAcesso command)
        {
            var result = new CommandResult<string>();
            var propertiesLog = HelperLog.GetPropertiesController(nameof(TokenController), nameof(ObterAcesso));

            try
            {
                HelperLog.AddTrackEvent("Validando o command", propertiesLog);
                command.ValidarCommand();

                if (command.IsValid)
                {
                    HelperLog.AddTrackEvent("Chamando a regra de negócio", propertiesLog);
                    result = _handlerToken.ObterAcesso(command);
                }
                else
                {
                    result.Mensagem = command.Notifications.RetornarMensagemParametrosInvalidos("Não foi possível obter o token de acesso");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.AtualizarRetornoError(ex, "Ocorreu um erro ao obter o token de acesso");
                HelperLog.AddTrackEventException(ex, result, propertiesLog);
                return BadRequest(result);
            }
        }


    }
}
