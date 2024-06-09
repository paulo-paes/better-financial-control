using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFinancialControl.Model
{
    public class DashboardModel
    {
        public Decimal Saldo { get; set; }

        public List<Movimentacao> Movimentacoes { get; set; }
    }
}
