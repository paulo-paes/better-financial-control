using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFinancialControl.Model
{
    [Table("movimentacoes")]
    public class Movimentacao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime Data { get; set; }

        public Decimal Valor { get; set; }

        public int CategoriaId { get; set; }

        [Ignore]
        public Categoria Categoria { get; set; }

        public FormaDePagamento FormaDePagamento { get; set; }

        public Tipo Tipo { get; set; }
    }
}
