using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFinancialControl.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

    }
}
