using Flunt.Notifications;

namespace Shared.Commands
{
    public abstract class CommandBase : Notifiable<Notification>
    {
        public abstract void ValidarCommand();
    }
}
