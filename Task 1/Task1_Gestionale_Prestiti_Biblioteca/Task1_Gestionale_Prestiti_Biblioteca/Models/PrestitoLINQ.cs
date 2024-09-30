using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models
{
    internal class PrestitoLINQ
    {
        public int PrestitoId { get; set; }

        public string? CodicePrestito { get; set; }

        public DateOnly? DataPrestito { get; set; }

        public DateOnly? DataRitorno { get; set; }

        public int UtenteRiff { get; set; }

        public int LibroRiff { get; set; }
        public string? UtenteNominativo { get; set; }
        public string? LibroNominativo { get; set; }
    }
}
