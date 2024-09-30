using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models
{
    internal class UtenteLINQ
    {

        public int UtenteId { get; set; }

        public string? CodiceUtente { get; set; }

        public string Nome { get; set; } = null!;

        public string Cognome { get; set; } = null!;

        public string Email { get; set; } = null!;
        public int ContatorePrestiti { get; set; }
        public override string ToString()
        {
            return $"{UtenteId}";
        }
    }
}
