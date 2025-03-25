namespace Shared.DTOs
{
    public class DtoConstantes
    {
        public const string ClaimBase = "claimCustom/";

        //Dados do Colaborador Logado, para preencher os dados no login do WebSite
        public static string ClaimNomeCompleto = $"{ClaimBase}/NomeCompleto";
        public static string ClaimMatricula = $"{ClaimBase}/Matricula";
        public static string ClaimEmail = $"{ClaimBase}/Email";
        public static string ClaimRole = $"{ClaimBase}/Role";
        public static string ClaimEstaSimulando = $"{ClaimBase}/EstaSimulando";
        public static string ClaimUsuarioLog = $"{ClaimBase}/UsuarioLog";
        public static string ClaimJsonDadosOriginais = $"{ClaimBase}/JsonDadosOriginais";

        //Chaves
        public const string ChaveCriptografia = "sfhjkhKJHg%dsf;54sf87sd;0^";
        public const string ChaveToken = "21ERu321$324adS6;54fs5f4sd1.cs5f465s4f";
    }
}
