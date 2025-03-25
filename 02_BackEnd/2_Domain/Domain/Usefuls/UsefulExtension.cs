using Flunt.Notifications;
using System.Text;

namespace Domain.Usefuls
{
    public static class UsefulExtension
    {
        public static string RetornarMensagemParametrosInvalidos(string mensagem, string parametroErro)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{mensagem}, pois os parâmetros informados estão inválidos!<br />");
            stringBuilder.AppendLine($"• {parametroErro}");

            return stringBuilder.ToString();
        }

        public static string RetornarMensagemParametrosInvalidos(string mensagem, List<string> listaParametrosErro)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{mensagem}, pois os parâmetros informados estão inválidos!");

            listaParametrosErro?.ForEach(item => { stringBuilder.AppendLine($"<br />• {item}"); });

            return stringBuilder.ToString();
        }

        public static string RetornarMensagemParametrosInvalidos(this IReadOnlyCollection<Notification> listaNotificacoes, string mensagem)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{mensagem}, pois os parâmetros informados estão inválidos!");

            IEnumerable<string> parametrosInvalidos = listaNotificacoes.ObterNotificacoesFlunt();

            if (parametrosInvalidos != null && parametrosInvalidos.Any())
            {
                foreach (string parametro in parametrosInvalidos)
                {
                    stringBuilder.AppendLine($"<br />• {parametro}");
                }
            }

            return stringBuilder.ToString();
        }

        public static IEnumerable<string> ObterNotificacoesFlunt(this IReadOnlyCollection<Notification> listaNotificacoes)
        {
            List<string> listaRetorno = new List<string>();

            if (listaNotificacoes != null && listaNotificacoes.Any())
            {
                foreach (Notification notification in listaNotificacoes)
                {
                    if (listaRetorno.Any(x => x.Equals(notification.Message, StringComparison.OrdinalIgnoreCase)) == false)
                    {
                        listaRetorno.Add(notification.Message);
                    }
                }
            }
            return listaRetorno;
        }

    }
}
