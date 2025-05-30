namespace Shared.DTOs
{
    public class DtoColaboradorLogado
    {
        public DtoColaboradorLogado()
        {
        }

        public DtoColaboradorLogado(string nome, string matricula, string email, bool estaSimulando, string jsonDadosOriginais, List<string> roles)
        {
            Nome = nome;
            Matricula = matricula;
            Email = email;
            EstaSimulando = estaSimulando;
            JsonDadosOriginais = jsonDadosOriginais;
            Roles = roles;
        }

        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = [];
        public bool EstaSimulando { get; set; } = false;
        public string JsonDadosOriginais { get; set; } = string.Empty;
    }
}
