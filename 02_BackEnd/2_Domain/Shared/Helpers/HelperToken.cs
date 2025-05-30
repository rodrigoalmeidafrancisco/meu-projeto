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
        public static string CreateToken(string ambienteAplicacao, List<string> listaEscopo, Dictionary<string, object> listaClaimsPersonalisada = null, int minutosExpiracao = 60)
        {
            try
            {
                //Caso tenha uma lista de escopos, adiciona ela na configuração do Token
                var listaSubject = new List<Claim>() { new Claim("scope", "openid"), new Claim("scope", "profile") };

                listaEscopo.ForEach(item =>
                {
                    if (item.Trim().Equals("openid", StringComparison.CurrentCultureIgnoreCase) == false &&
                        item.Trim().Equals("profile", StringComparison.CurrentCultureIgnoreCase) == false)
                    {
                        listaSubject.Add(new Claim("scope", item.Trim()));
                    }
                });

                listaClaimsPersonalisada = listaClaimsPersonalisada != null ? listaClaimsPersonalisada : new Dictionary<string, object>();

                //Configura o Token
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Issuer = $"MeuProjeto_{ambienteAplicacao.ToUpper()}",
                    Subject = new ClaimsIdentity(listaSubject),
                    Claims = listaClaimsPersonalisada,
                    Expires = DateTime.UtcNow.AddMinutes(minutosExpiracao),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes($"{ambienteAplicacao.ToUpper()}{DtoConstantes.ChaveToken}")), SecurityAlgorithms.HmacSha256Signature)
                };

                //Gera o Token de acesso
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(tokenDescriptor);
                string token = handler.WriteToken(securityToken);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao obter o token de acesso => {ex.Message}");
            }
        }

    }
}
