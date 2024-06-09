using BetterFinancialControl.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterFinancialControl.Utils;
namespace BetterFinancialControl.Repository
{
    public class DashboardRepository
    {

        private SQLiteConnection conn;
        public DashboardRepository() {
            conn = new SQLiteConnection(Constantes.PathDB);
        }

        public DashboardModel Buscar(DateTime date, int userId)
        {
            DateTime startDate = new DateTime(date.Year, date.Month, 1, 0, 0, 0);
            DateTime endDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59);

            var movs = conn.Table<Movimentacao>();
            var filteredMovs = movs
                .Where(m => m.Data >= startDate && m.Data <= endDate && m.UserId == userId)
                .OrderByDescending(m => m.Data).ToList();
            //var despesas = movs.Where(m => m.Tipo == Tipo.Despesa).ToList();
            //var receitas = movs.Where(m => m.Tipo == Tipo.Receita).ToList();
            var categorias = conn.Table<Categoria>();

            DashboardModel dash = new();
            dash.Movimentacoes = filteredMovs;
            Decimal saldo = 0;
            Decimal balanco = 0;
            foreach ( var item in movs )
            {
                if (item.Tipo == Tipo.Receita)
                {
                    saldo += item.Valor;
                } else { saldo -= item.Valor; }
            }

            foreach (var item in filteredMovs)
            {
                item.Categoria = categorias.Where(m => m.Id == item.CategoriaId).First();
                if (item.Tipo == Tipo.Receita)
                {
                    balanco += item.Valor;
                }
                else { balanco -= item.Valor; }
            }

            dash.Saldo = saldo;
            dash.Balanco = balanco;

            return dash;

        }
    }
}
