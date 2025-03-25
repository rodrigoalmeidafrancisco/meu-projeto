using Flunt.Validations;
using Shared.Commands;

namespace Domain.Commands.CtlToken
{
    public class CommandObterAcesso : CommandBase
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public override void ValidarCommand()
        {
            AddNotifications(new Contract<CommandObterAcesso>().Requires()
                .IsNotNullOrWhiteSpace(Login, nameof(Login), "Login é obrigatório.")
                .IsNotNullOrWhiteSpace(Senha, nameof(Senha), "Senha é obrigatório.")
            );
        }
    }
}
