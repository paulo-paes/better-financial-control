using BetterFinancialControl.Model;
using BetterFinancialControl.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFinancialControl.Repository
{
    public class UsuarioRepository
    {

        private SQLiteConnection conn;
        public UsuarioRepository() {
            conn = new SQLiteConnection(Constantes.PathDB);
        }
        public void CriarUsuario(Usuario user)
        {
            conn.Insert(user);
        }

        public bool VerificarUsuario(string email, string senha)
        {
            string[] args =
            [
                email,
                senha
            ];

            var user = conn.FindWithQuery<Usuario>("SELECT * FROM usuarios WHERE email = ? and senha = ?", args);


            return !(user == null);

        }
    }
}
