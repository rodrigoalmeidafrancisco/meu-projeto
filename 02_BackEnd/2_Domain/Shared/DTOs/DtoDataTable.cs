using Developer.ExtensionCore;
using Shared.Enums;

namespace Shared.DTOs
{
    public class DtoDataTable
    {
        public int Draw { get; set; }
        public int Start { get; set; } //Skip
        public DtoDataTablePesquisa Search { get; set; } = new DtoDataTablePesquisa();
        public IList<DtoDataTableColunaOrdenacao> Order { get; set; } = [];
        public IList<DtoDataTableColunas> Columns { get; set; } = [];
        public int Length { get; set; } //Take
        public int Total { get; set; }

        public string DataTableObterValorPesquisa()
        {
            return Search?.Value?.Trim();
        }

        public int DataTableObterIndiceColunaOrdenacao()
        {
            if (Order.Any())
            {
                return Order.First().Column.ToInt();
            }

            return 0;
        }

        public string DataTableObterColunaOrdenacaoNome()
        {
            if (Columns.Any())
            {
                return Columns[DataTableObterIndiceColunaOrdenacao()].Data;
            }

            return null;
        }

        public EnumTipoOrdenacaoDataTable DataTableObterColunaOrdenacaoTipo()
        {
            EnumTipoOrdenacaoDataTable tipoRetorno;
            string valor = Order?.First().Dir.ToString() ?? EnumTipoOrdenacaoDataTable.Asc.ToString();

            switch (valor.ToLower())
            {
                case "asc":
                    tipoRetorno = EnumTipoOrdenacaoDataTable.Asc;
                    break;
                case "desc":
                    tipoRetorno = EnumTipoOrdenacaoDataTable.Desc;
                    break;
                default:
                    tipoRetorno = EnumTipoOrdenacaoDataTable.Asc;
                    break;
            }

            return tipoRetorno;
        }

        public int DataTableObterSkip()
        {
            return Start;
        }

        public int DataTableObterTake()
        {
            return Length == 0 ? 10 : Length;
        }
    }

    public class DtoDataTableColunaOrdenacao
    {
        public string Column { get; set; }
        public EnumTipoOrdenacaoDataTable Dir { get; set; }
    }

    public class DtoDataTableColunas
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DtoDataTablePesquisa Search { get; set; } = new DtoDataTablePesquisa();
    }

    public class DtoDataTablePesquisa
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }
}
