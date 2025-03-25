using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using Shared.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shared.Helpers
{
    public static class HelperToken
    {
        public static string CreateToken(List<string> listaEscopo, Dictionary<string, object> listaClaimsPersonalisada = null, int minutosExpiracao = 60)
        {
            //Caso tenha uma lista de escopos, adiciona ela na configuração do Token
            var listaSubject = new List<Claim>() { new("scope", "openid"), new("scope", "profile") };

            listaEscopo.ForEach(item =>
            {
                if (item.Trim().Equals("openid", StringComparison.CurrentCultureIgnoreCase) == false &&
                    item.Trim().Equals("profile", StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    listaSubject.Add(new Claim("scope", item.Trim()));
                }
            });

            listaClaimsPersonalisada ??= [];

            //Configura o Token
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = SettingsShared.Aplicacao._Ambiente,
                Subject = new ClaimsIdentity(listaSubject),
                Claims = listaClaimsPersonalisada,
                Expires = DateTime.UtcNow.AddMinutes(minutosExpiracao),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes($"{SettingsShared.Aplicacao._Ambiente}{DtoConstantes.ChaveToken}")), SecurityAlgorithms.HmacSha256Signature),
            };

            //Gera o Token de acesso
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(tokenDescriptor);
            string token = handler.WriteToken(securityToken);
            return token;
        }

    }
}
