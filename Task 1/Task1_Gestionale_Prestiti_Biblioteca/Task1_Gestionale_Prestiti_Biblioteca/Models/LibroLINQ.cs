using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models
{
    internal class LibroLINQ
    {
        public int LibroId { get; set; }

        public string? CodiceLibro { get; set; }

        public string Autore { get; set; } = null!;

        public string Titolo { get; set; } = null!;

        public int AnnoPubblicazione { get; set; }

        public bool Stato { get; set; }
    }
}
