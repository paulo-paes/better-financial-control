using BetterFinancialControl.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFinancialControl.Infra
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
        }
    }
}
