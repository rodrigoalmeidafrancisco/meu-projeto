namespace Shared.DTOs
{
    public class DtoArquivoGenerico
    {
        public DtoArquivoGenerico()
        {

        }

        public string Nome { get; set; }
        public string ContentType { get; set; }
        public string Tipo { get; set; }
        public byte[] Bytes { get; set; }
        public string Bytes64 { get; set; }
        public Stream ImagemStream { get; set; }

        public void AlterarNomeArquivo()
        {
            DateTime data = DateTime.Now;
            string nomeArquivo = $"{data.Year}{data.Month}{data.Day}_{data.Hour}{data.Minute}{data.Second}{data.Millisecond}";
            string extensao = Nome?.Split('.').LastOrDefault();
            Nome = $"{nomeArquivo}.{extensao}";
        }
    }
}
