using System;
using System.Collections.Generic;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models;

public partial class Utente
{
    public int UtenteId { get; set; }

    public string? CodiceUtente { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Prestito> Prestitos { get; set; } = new List<Prestito>();
    public override string ToString()
    {
        return $"{UtenteId}";
    }
}
