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

        public void Criar(Movimentacao mov, int userId)
        {
            mov.UserId = userId;
            conn.Insert(mov);
        }

        public List<Movimentacao> Buscar(int userId)
        {
            return conn.Table<Movimentacao>().Where(m => m.UserId == userId).ToList();
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

        public List<Movimentacao> BuscarPorTipo(Tipo tipo, int userId)
        {
            return conn.Table<Movimentacao>().ToList().Where(m => m.Tipo == tipo && m.UserId == userId).ToList();
        }
    }
}
