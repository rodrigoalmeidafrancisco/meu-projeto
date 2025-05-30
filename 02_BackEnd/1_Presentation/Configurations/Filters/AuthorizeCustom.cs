using Developer.ExtensionCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.DTOs;
using System.Security.Claims;

namespace WebApi.Configurations.Filters
{
    public class AuthorizeCustomAttribute : ActionFilterAttribute
    {
        private readonly string _roles;

        public AuthorizeCustomAttribute(string roles)
        {
            _roles = roles;
        }

        //Executa antes de entrar na ação
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Verifica se o usuário está autenticado
            if (context.HttpContext.User.Identity.IsAuthenticated == false)
            {
                context.Result = new UnauthorizedResult();
                base.OnActionExecuting(context);
                return;
            }
            else
            {
                // Obtém o ClaimsPrincipal do contexto HTTP
                bool acessoProibido = false;
                ClaimsPrincipal claimsPrincipal = context.HttpContext.User;

                #region Validações

                // Valida os roles
                if (_roles.IsNullOrEmptyOrWhiteSpace() == false)
                {
                    List<string> listaRolesClaim = [];
                    List<Claim> listClaim = claimsPrincipal.Claims.Where((x) => x.Type == DtoConstantes.ClaimRole).ToList();
                    listClaim?.ForEach(item => { listaRolesClaim.Add(item.Value); });

                    if (_roles.Split(',').Any(role => listaRolesClaim.Contains(role.Trim())) == false)
                        acessoProibido = true;
                }

                #endregion Validações

                // Verifica se o acesso foi proibido
                if (acessoProibido)
                {
                    context.Result = new ForbidResult();
                    base.OnActionExecuting(context);
                    return;
                }
            }
        }

    }
}
