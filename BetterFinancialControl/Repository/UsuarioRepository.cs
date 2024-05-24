using BetterFinancialControl.Infra;
using BetterFinancialControl.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFinancialControl.Repository
{
    public class UsuarioRepository
    {
        public async void CreateUser(Usuario user)
        {
            using var context = new AppDbContext();

            await context.Usuarios.AddAsync(user);

            await context.SaveChangesAsync();
        }
    }
}
