using Domain.Commands.CtlToken;
using Domain.Contracts.Handlers;
using Domain.Usefuls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;

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
        public async Task<IActionResult> ObterAcessoAsync([FromBody] CommandObterAcesso command)
        {
            var result = new CommandResult<string>();
            command.ValidarCommand();

            if (command.IsValid)
            {
                var retornoRegra = await _handlerToken.ObterAcessoAsync(command);
                result.AtualizarRetorno(retornoRegra);
            }
            else
            {
                result.Mensagem = command.Notifications.RetornarMensagemParametrosInvalidos("Não foi possível obter o token de acesso");
            }

            return Ok(result);
        }


    }
}
