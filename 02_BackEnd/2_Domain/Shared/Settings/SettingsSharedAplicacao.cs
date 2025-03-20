namespace Shared.Settings
{
    public class SettingsSharedAplicacao
    {
        public SettingsSharedAplicacao()
        {
            AcessoPolicy = [];
        }

        public string _Build { get; set; }
        public string _Release { get; set; }
        public List<string> AcessoPolicy { get; set; }
        public List<KeyValuePair<string, string[]>> AcessoPolicyLista => SelecionarListaPolicyAcesso();
        public string NomeAplicacao { get; set; }
        public string WebUri { get; set; }

        private List<KeyValuePair<string, string[]>> SelecionarListaPolicyAcesso()
        {
            var listaRetorno = new List<KeyValuePair<string, string[]>>();

            if (AcessoPolicy != null && AcessoPolicy.Any())
            {
                string[] listaPolicyParametroSplit;
                string chave;
                string[] valor;

                foreach (var item in AcessoPolicy)
                {
                    listaPolicyParametroSplit = item.Split('|');
                    chave = listaPolicyParametroSplit[0];
                    valor = listaPolicyParametroSplit[1].Split(' ');

                    for (int i = 0; i < valor.Length; i++)
                    {
                        valor[i] = valor[i].Trim();
                    }

                    listaRetorno.Add(new KeyValuePair<string, string[]>(chave.Trim(), valor));
                }
            }

            return listaRetorno;
        }

    }
}
