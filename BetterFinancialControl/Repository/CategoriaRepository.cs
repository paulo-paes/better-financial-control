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
    public class CategoriaRepository
    {
        private SQLiteConnection conn;
        public CategoriaRepository()
        {
            conn = new SQLiteConnection(Constantes.PathDB);
        }
        public void Criar(Categoria categoria, int userId)
        {
            categoria.UserId = userId;
            conn.Insert(categoria);
        }

        public List<Categoria> Buscar(int userId)
        {
            return conn.Table<Categoria>().Where(c => c.UserId == userId).ToList();
        }

        public void Atualizar(Categoria categoria)
        {
            if (categoria == null) return;
            var dbCat = conn.Find<Categoria>(categoria.Id);
            if (dbCat == null)
            {
                throw new Exception("Categoria não encontrada");
            }
            conn.Update(categoria);

        }

        public void Excluir(int id)
        {
            var dbCat = conn.Find<Categoria>(id);
            if (dbCat == null)
            {
                throw new Exception("Categoria não encontrada");
            }
            conn.Delete<Categoria>(id);
        }
    }
}
