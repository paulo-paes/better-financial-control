using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFinancialControl.Model
{
    public class Dashboard
    {
        public Decimal Saldo { get; set; }

        public List<Movimentacao> Receitas { get; set; }

        public List<Movimentacao> Despesas { get; set; }
    }
}
