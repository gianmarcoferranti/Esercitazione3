using Microsoft.EntityFrameworkCore;
using System;
using Task1_Gestionale_Prestiti_Biblioteca.Models;
using Task1_Gestionale_Prestiti_Biblioteca.Models.DAL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task1_Gestionale_Prestiti_Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool insAbi = true;
            while (insAbi)
            {
                Console.WriteLine("Digita il comando per scegliere l'operazione\n" +
                    "I - Inserimento\n" +
                    "S - Stampa\n" +
                    "M - Modifica\n" +
                    "C - Cancella\n" +
                    "R - Ricerche Speciali\n" +
                    "Q - Esci");

                string? inputUtente = Console.ReadLine();

                switch (inputUtente)
                {
                    case "I":           //----------------INSERIMENTO--------------------//
                        Console.WriteLine("Cosa vuoi inserire?\n" +
                                    "U - Utente\n" +
                                    "L - Libro\n" +
                                    "P - Prestito\n" +
                                    "Q - Indietro");
                        string? inputUtente2 = Console.ReadLine();
                        switch (inputUtente2)
                        {
                            case "U":   // AGGIUNGI UTENTE

                                Console.WriteLine("Inserisci il nome dell'utente");
                                string? inNome = Console.ReadLine();
                                Console.WriteLine("Inserisci il cognome dell'utente");
                                string? inCognome = Console.ReadLine();
                                Console.WriteLine("Inserisci l'email dell'utente");
                                string? inEmail = Console.ReadLine();

                                if (inNome != "" && inCognome != "" && inEmail != "")
                                {
                                    using (var ctx = new GestionePrestitiBibliotecaContext())
                                    {
                                        try
                                        {
                                            Utente user = new Utente()
                                            {
                                                Nome = inNome is not null ? inNome : "N.D.",
                                                Cognome = inCognome is not null ? inCognome : "N.D.",
                                                Email = inEmail is not null ? inEmail : "N.D."
                                            };
                                            ctx.Utentes.Add(user);
                                            ctx.SaveChanges();
                                            Console.WriteLine("Utente Aggiunto con successo!");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Qualcosa è andato storto nella creazione dell'utente!");
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Controlla i campi");
                                }
                                break;
                            case "L":   // AGGIUNGI LIBRO

                                Console.WriteLine("Inserisci il titolo del libro");
                                string? inTitolo = Console.ReadLine();
                                Console.WriteLine("Inserisci il nome dell'autore");
                                string? inAutore = Console.ReadLine();
                                Console.WriteLine("Inserisci l'anno di uscita");
                                string inAnno = Console.ReadLine();
                                int inAnno1;

                                if (inTitolo != "" && inAutore != "" && Int32.TryParse(inAnno, out inAnno1) != false)
                                {
                                    using (var ctx = new GestionePrestitiBibliotecaContext())
                                    {
                                        try
                                        {
                                            Libro libro = new Libro()
                                            {
                                                Titolo = inTitolo is not null ? inTitolo : "N.D.",
                                                Autore = inAutore is not null ? inAutore : "N.D.",
                                                AnnoPubblicazione = inAnno1,
                                                Stato = true
                                            };
                                            ctx.Libros.Add(libro);
                                            ctx.SaveChanges();
                                            Console.WriteLine("Libro Aggiunto con successo!");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Qualcosa è andato storto nella creazione del libro!");
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Controlla i campi");
                                }
                                break;
                            case "P":   // AGGIUNGI PRESTITO

                                Console.WriteLine("Inserisci Codice tessera Utente");
                                string? inCodiceUtente = Console.ReadLine();
                                Console.WriteLine("Inserisci Codice Libro");
                                string? inCodiceLibro = Console.ReadLine();



                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {
                                    try
                                    {

                                        var utente2 = ctx.Utentes.Single(p => p.CodiceUtente == inCodiceUtente);
                                        var libro2 = ctx.Libros.Single(p => p.CodiceLibro == inCodiceLibro);


                                        var prestito = new Prestito()
                                        {
                                            UtenteRiffNavigation = utente2,
                                            LibroRiffNavigation = libro2,
                                            DataPrestito = DateOnly.FromDateTime(DateTime.Now)
                                        };

                                        // prendo tutte le copie con prestiti attivi
                                        var prestitoAppoggio = ctx.Prestitos.Where(p => p.LibroRiffNavigation.CodiceLibro == inCodiceLibro && p.DataRitorno == null).Select(p => new { p.DataRitorno }).ToList();
                                        //prendo il libro in questione
                                        var libroStatus = ctx.Libros.Single(p => p.CodiceLibro == inCodiceLibro);



                                        if (prestitoAppoggio.Count == 0)
                                        {
                                            libroStatus.Stato = false;  //rende non diponibile il libro preso in prestito
                                            ctx.Libros.Update(libroStatus);
                                            ctx.Prestitos.Add(prestito);
                                            ctx.SaveChanges();
                                            Console.WriteLine("Prestito Aggiunto con successo!");

                                        }
                                        else
                                        {
                                            Console.WriteLine("Prestito non disponibile!");

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Qualcosa è andato storto nella creazione del prestito!");
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                break;
                            default:
                                Console.WriteLine("Non conosco questo comando!");
                                break;
                        }



                        break;
                    case "S":
                        //----------------------STAMPA----------------------------------//

                        Console.WriteLine("Cosa vuoi vedere?\n" +
                                    "U - Utente\n" +
                                    "L - Libro\n" +
                                    "P - Prestito\n" +
                                    "Q - Indietro");
                        string? inputUtente3 = Console.ReadLine();
                        switch (inputUtente3)
                        {
                            case "U":   // VISUALIZZA UTENTI

                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {
                                    var elencoUtenti = ctx.Utentes.ToList();

                                    foreach (var utenti in elencoUtenti)
                                    {
                                        Console.WriteLine($"{utenti.Nome} {utenti.Cognome} {utenti.Email} {utenti.CodiceUtente}");
                                    }
                                }

                                break;
                            case "L":   // VISUALIZZA LIBRI

                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {
                                    var elencoLibri = ctx.Libros.ToList();

                                    foreach (var libri in elencoLibri)
                                    {
                                        Console.WriteLine($"{libri.Titolo} {libri.Autore} {libri.CodiceLibro}");
                                    }
                                }

                                break;
                            case "P":   // VISUALIZZA PRESTITI

                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {

                                    var elencoPrestiti = ctx.Prestitos
                                                                            .Join(
                                                                                ctx.Utentes,
                                                                                entryPoint => entryPoint.UtenteRiff,
                                                                                entry => entry.UtenteId,
                                                                                (entryPoint, entry) => new { entryPoint, entry }
                                                                            )
                                                                            .Join(
                                                                                ctx.Libros,
                                                                                combinedEntry => combinedEntry.entryPoint.LibroRiff,
                                                                                Libro => Libro.LibroId,
                                                                                (combinedEntry, libro) => new
                                                                                {
                                                                                    UID = combinedEntry.entry.Nome,
                                                                                    TID = combinedEntry.entry.Cognome,
                                                                                    EID = combinedEntry.entryPoint.DataPrestito,
                                                                                    EIDF = combinedEntry.entryPoint.DataRitorno,
                                                                                    Title = libro.Titolo
                                                                                }
                                                                            ).ToList();



                                    foreach (var prestiti in elencoPrestiti)
                                    {
                                        Console.WriteLine($"[NOME]    {prestiti.UID} [COGNOME]    {prestiti.TID} [TITOLO]    {prestiti.Title} [DATA PRESTITO]    {prestiti.EID} [DATA RICONSEGNA]    {prestiti.EIDF}");
                                    }
                                }

                                break;
                            default:
                                Console.WriteLine("Non conosco questo comando!");
                                break;
                        }

                        break;

                    case "M":
                        //----------------------MODIFICA----------------------------------//

                        Console.WriteLine("Cosa vuoi aggiornare?\n" +
                                    "U - Utente\n" +
                                    "L - Libro\n" +
                                    "P - Prestito\n" +
                                    "Q - Indietro");
                        string? inputUtente4 = Console.ReadLine();
                        switch (inputUtente4)
                        {
                            case "U":   // MODIFICA DATI UTENTI

                                Console.WriteLine("Inserisci il codice utente dell'utente che vuoi modificare");
                                string? inCodice = Console.ReadLine();



                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {
                                    try
                                    {
                                        var utente = ctx.Utentes.Single(p => p.CodiceUtente == inCodice);


                                        Console.WriteLine("Inserisci un nuovo nome");
                                        string? inNome = Console.ReadLine();

                                        Console.WriteLine("Inserisci un nuovo cognome");
                                        string? inCognome = Console.ReadLine();

                                        Console.WriteLine("Inserisci una nuova email");
                                        string? inEmail = Console.ReadLine();

                                        utente.Nome = inNome is not null ? inNome : "N.D.";
                                        utente.Cognome = inCognome is not null ? inCognome : "N.D.";
                                        utente.Email = inEmail is not null ? inEmail : "N.D.";

                                        ctx.Utentes.Update(utente);
                                        ctx.SaveChanges();
                                        Console.WriteLine("Utente modificato con successo!");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Qualcosa è andato storto nella modifica dell'utente!");
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                break;
                            case "L":   // MODIFICA DATI LIBRI


                                Console.WriteLine("Inserisci il codice libro del libro che vuoi modificare");
                                string? inCodiceLibro = Console.ReadLine();



                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {
                                    try
                                    {
                                        var libro = ctx.Libros.Single(p => p.CodiceLibro == inCodiceLibro);


                                        Console.WriteLine("Inserisci un nuovo titolo");
                                        string? inTitolo = Console.ReadLine();

                                        Console.WriteLine("Inserisci un nuovo autore");
                                        string? inAutore = Console.ReadLine();

                                        Console.WriteLine("Inserisci una nuova data pubblicazione");
                                        string? inData = Console.ReadLine();
                                        int inAnno2;

                                        if (Int32.TryParse(inData, out inAnno2) != false)
                                        {


                                            libro.Titolo = inTitolo is not null ? inTitolo : "N.D.";
                                            libro.Autore = inAutore is not null ? inAutore : "N.D.";
                                            libro.AnnoPubblicazione = inAnno2;

                                            ctx.Libros.Update(libro);
                                            ctx.SaveChanges();
                                            Console.WriteLine("Libro modificato con successo!");
                                        }
                                        else { Console.WriteLine("Controlla i campi"); }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Qualcosa è andato storto nella modifica del libro!");
                                        Console.WriteLine(ex.Message);
                                    }
                                }


                                break;
                            case "P":   // MODIFICA DATA RESTITUZIONE PRESTITI  --> modifica status libro a disponibile


                                Console.WriteLine("Inserisci il codice libro del libro che è stato riconsegnato");
                                string? inCodiceLibroRestituito = Console.ReadLine();

                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {
                                    try
                                    {

                                        var prestito = ctx.Prestitos.Where(p => p.DataRitorno == null).Include(p => p.LibroRiffNavigation)
                                                .SingleOrDefault(p => p.LibroRiffNavigation.CodiceLibro == inCodiceLibroRestituito);
                                        //prendo il libro in questione
                                        var libroStatus1 = ctx.Libros.Single(p => p.CodiceLibro == inCodiceLibroRestituito);



                                        if (prestito != null && libroStatus1 != null)
                                        {

                                            libroStatus1.Stato = true;  //rende  diponibile il libro restituito
                                            prestito.DataRitorno = DateOnly.FromDateTime(DateTime.Now);

                                            ctx.Libros.Update(libroStatus1);
                                            ctx.Prestitos.Update(prestito);
                                            ctx.SaveChanges();
                                            Console.WriteLine("Libro modificato con successo!");

                                        }
                                        else
                                        {
                                            Console.WriteLine("Qualcosa è andato storto nella modifica del libro!");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Qualcosa è andato storto nella modifica del libro!");
                                        Console.WriteLine(ex.Message);
                                    }
                                }


                                break;
                            default:
                                Console.WriteLine("Non conosco questo comando!");
                                break;
                        }

                        break;

                    case "C":
                        //----------------------CANCELLA----------------------------------//

                        Console.WriteLine("Cosa vuoi cancellare?\n" +
                                    "U - Utente\n" +
                                    "L - Libro\n" +
                                    "P - Prestito\n" +
                                    "Q - Indietro");
                        string? inputUtente5 = Console.ReadLine();
                        switch (inputUtente5)
                        {
                            case "U":   // CANCELLA UTENTE

                                Console.WriteLine("Inserisci il codice utente dell'utente che vuoi eliminare");
                                string? inCodice = Console.ReadLine();



                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {
                                    try
                                    {
                                        var utente = ctx.Utentes.Single(p => p.CodiceUtente == inCodice);
                                        var libroStatus2 = ctx.Prestitos.Where(p => p.UtenteRiffNavigation.CodiceUtente.Equals(inCodice))
                                                                            .Join(
                                                                                ctx.Utentes,
                                                                                entryPoint => entryPoint.UtenteRiffNavigation.UtenteId,
                                                                                entry => entry.UtenteId,
                                                                                (entryPoint, entry) => new { entryPoint, entry }
                                                                            )
                                                                            .Join(
                                                                                ctx.Libros,
                                                                                combinedEntry => combinedEntry.entryPoint.LibroRiffNavigation.LibroId,
                                                                                Libro => Libro.LibroId,
                                                                                (combinedEntry, libro) => new
                                                                                {
                                                                                    titolo = libro.Titolo,
                                                                                    autore = libro.Autore,
                                                                                    stato = libro.Stato,
                                                                                    id = libro.LibroId
                                                                                }
                                                                            ).ToList();


                                        foreach (var libro in libroStatus2)
                                        {
                                            var libroToUpdate = ctx.Libros.Find(libro.id);

                                            if (libroToUpdate != null)
                                            {
                                                libroToUpdate.Stato = true;  // Rende disponibile il libro in prestito poiché l'utente è cancellato
                                                ctx.Libros.Update(libroToUpdate);
                                                Console.WriteLine("Libri in prestito dell'utente cancellato restituiti!");

                                            }

                                        }

                                        ctx.Utentes.Remove(utente);
                                        ctx.SaveChanges();
                                        Console.WriteLine("Utente eliminato con successo!");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Qualcosa è andato storto nell'eliminazione dell'utente!");
                                        Console.WriteLine(ex.Message);
                                    }
                                }

                                break;
                            case "L":   // CANCELLA LIBRO

                                Console.WriteLine("Inserisci il codice libro del libro che vuoi eliminare");
                                string? inCodiceLibro = Console.ReadLine();



                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {
                                    try
                                    {
                                        var libro = ctx.Libros.Single(p => p.CodiceLibro == inCodiceLibro);
                                        var prestito = ctx.Prestitos.Where(p => p.LibroRiffNavigation.CodiceLibro.Equals(inCodiceLibro))
                                                                            .Join(
                                                                                ctx.Libros,
                                                                                prestito => prestito.LibroRiffNavigation.LibroId,
                                                                                Libro => Libro.LibroId,
                                                                                (prestito, libro) => new
                                                                                {
                                                                                    titolo = libro.Titolo,
                                                                                    autore = libro.Autore,
                                                                                    stato = libro.Stato,
                                                                                    id = libro.LibroId
                                                                                }
                                                                            ).ToList();


                                        foreach (var prest in prestito)
                                        {
                                            var prestitoToRemove = ctx.Prestitos.SingleOrDefault(p => p.LibroRiffNavigation.LibroId == prest.id);
                                            if (prestitoToRemove != null)
                                            {
                                                ctx.Prestitos.Remove(prestitoToRemove);
                                                ctx.SaveChanges();

                                            }
                                        }
                                        ctx.Libros.Remove(libro);
                                        ctx.SaveChanges();
                                        Console.WriteLine("Libro riosso con successo!");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Qualcosa è andato storto nell'eliminazione del libro!");
                                        Console.WriteLine(ex.Message);
                                    }
                                }


                                break;
                            case "P":   // CANCELLA PRESTITO

                                Console.WriteLine("Inserisci il codice prestito del prestito che vuoi eliminare");
                                string? inCodicePrestito = Console.ReadLine();

                                // il libro coinvolto torna disponibile

                                using (var ctx = new GestionePrestitiBibliotecaContext())
                                {
                                    try
                                    {
                                        var prestito = ctx.Prestitos.Single(p => p.CodicePrestito == inCodicePrestito);
                                        var libro = ctx.Libros.Join(
                                                                    ctx.Prestitos,
                                                                    libros => libros.LibroId,
                                                                    prestito => prestito.LibroRiffNavigation.LibroId,
                                                                    (libro, prestito) => new
                                                                    {
                                                                        idlibro = libro.LibroId,
                                                                        stato = libro.Stato
                                                                    }
                                                                );


                                        foreach (var lib in libro)
                                        {
                                            var libroToUpdate = ctx.Libros.SingleOrDefault(p => p.LibroId == lib.idlibro);
                                            if (libroToUpdate != null)
                                            {
                                                libroToUpdate.Stato = true;
                                                ctx.Libros.Update(libroToUpdate);
                                                ctx.SaveChanges();
                                            }
                                        }

                                        ctx.Prestitos.Remove(prestito);
                                        ctx.SaveChanges();
                                        Console.WriteLine("Prestito rimosso con successo!");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Qualcosa è andato storto nell'eliminazione del prestito!");
                                        Console.WriteLine(ex.Message);
                                    }
                                }

                                break;
                            default:
                                Console.WriteLine("Non conosco questo comando!");
                                break;
                        }

                        break;
                    case "R":
                        Console.WriteLine("Quale richiesta vuoi fare?\n" +
                                "L - Ricerca Libri Disponibili per il Prestito\n" +
                                "P - Prestiti in corso per un determinato utente\n" +
                                "U - Lista Utenti con maggiori prestiti all'attivo\n" +
                                "Q - Esci");
                        string? inInput = Console.ReadLine();
                        switch (inInput)
                        {
                            case "L":   // Libri disponibili per il prestito

                                LibroDAO.GetInstance().GetLibrosForPrestito();
                                

                                break;
                            case "P":   // Prestiti in corso di un certo utente
                                Console.WriteLine("Inserisci codice utente");
                                string? inInput2 = Console.ReadLine();
                                PrestitoDAO.GetInstance().GetPrstitosForUserCode(inInput2);
                                break;
                            case "U":   // Utenti con tanti prestiti
                                UtenteDAO.GetInstance().GetUserForPrestitos();

                                break;
                            default:
                                Console.WriteLine("Non conosco questo comando!");
                                break;
                        }
                        break;
                    case "Q":
                        insAbi = false;
                        break;
                    default:
                        Console.WriteLine("Non conosco questo comando!");
                        break;
                }
            }
        }
    }
}
