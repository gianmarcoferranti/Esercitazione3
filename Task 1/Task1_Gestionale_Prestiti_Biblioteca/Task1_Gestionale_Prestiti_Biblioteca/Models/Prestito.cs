using System;
using System.Collections.Generic;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models;

public partial class Prestito
{
    public int PrestitoId { get; set; }

    public string? CodicePrestito { get; set; }

    public DateOnly? DataPrestito { get; set; }

    public DateOnly? DataRitorno { get; set; }

    public int UtenteRiff { get; set; }

    public int LibroRiff { get; set; }

    public virtual Libro LibroRiffNavigation { get; set; } = null!;

    public virtual Utente UtenteRiffNavigation { get; set; } = null!;
}
