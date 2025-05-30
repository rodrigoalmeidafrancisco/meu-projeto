using Developer.ExtensionCore;

namespace Shared.Commands
{
    public class CommandBaseCrud : CommandBase
    {
        public CommandBaseCrud()
        {

        }

        public CommandBaseCrud(string usuarioLog)
        {
            UsuarioLog = usuarioLog;
        }

        public string UsuarioLog { get; set; }

        public override void ValidarCommand()
        {
            this.Clear();

            if (UsuarioLog.IsNullOrEmptyOrWhiteSpace())
            {
                AddNotification(nameof(UsuarioLog), "Usuário Log é obrigatório.");
            }
        }
    }
}
