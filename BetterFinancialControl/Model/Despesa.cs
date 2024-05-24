using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFinancialControl.Model
{
    public class Despesa
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime DataDespesa { get; set; }

        public Decimal Valor { get; set; }

        public Categoria Categoria { get; set; }
    }
}
