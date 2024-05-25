using BetterFinancialControl.Model;
using BetterFinancialControl.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFinancialControl.Repository
{
    public class MovimentacaoRepository
    {
        private SQLiteConnection conn;
        public MovimentacaoRepository() { 
            conn = new SQLiteConnection(Constantes.PathDB);
        }

        public void Criar(Movimentacao mov)
        {
            conn.Insert(mov);
        }

        public List<Movimentacao> Buscar()
        {
            return conn.Table<Movimentacao>().ToList();
        }

        public void Atualizar(Movimentacao mov)
        {
            if (mov == null) return;
            var movDb = conn.Find<Movimentacao>(mov.Id);

            if (movDb == null)
            {
                throw new Exception("Movimentação não encontrada");
            }

            conn.Update(mov);
        }

        public async void Excluir(int id)
        {
            var movDb = conn.Find<Movimentacao>(id);

            if (movDb == null)
            {
                throw new Exception("Movimentação não encontrada");
            }

            conn.Delete<Movimentacao>(id);
        }

        public List<Movimentacao> BuscarPorTipo(Tipo tipo)
        {
            return conn.Table<Movimentacao>().ToList().Where(m => m.Tipo == tipo).ToList();
        }
    }
}
