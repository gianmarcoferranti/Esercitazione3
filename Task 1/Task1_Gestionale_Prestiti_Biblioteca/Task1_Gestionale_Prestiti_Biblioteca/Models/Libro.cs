using System;
using System.Collections.Generic;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models;

public partial class Libro
{
    public int LibroId { get; set; }

    public string? CodiceLibro { get; set; }

    public string Autore { get; set; } = null!;

    public string Titolo { get; set; } = null!;

    public int AnnoPubblicazione { get; set; }

    public bool Stato { get; set; }

    public virtual ICollection<Prestito> Prestitos { get; set; } = new List<Prestito>();
}
