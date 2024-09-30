using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models;

public partial class GestionePrestitiBibliotecaContext : DbContext
{
    public GestionePrestitiBibliotecaContext()
    {
    }

    public GestionePrestitiBibliotecaContext(DbContextOptions<GestionePrestitiBibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestito> Prestitos { get; set; }

    public virtual DbSet<Utente> Utentes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACADEMY2024-22\\SQLEXPRESS;Database=gestione_prestiti_biblioteca;User Id=academy;Password=academy!;\nMultipleActiveResultSets=true;\nEncrypt=false;\nTrustServerCertificate=false\n");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.LibroId).HasName("PK__Libro__18C65F4BA8F6C5E3");

            entity.ToTable("Libro");

            entity.Property(e => e.LibroId).HasColumnName("libroID");
            entity.Property(e => e.AnnoPubblicazione).HasColumnName("annoPubblicazione");
            entity.Property(e => e.Autore)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("autore");
            entity.Property(e => e.CodiceLibro)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("codiceLibro");
            entity.Property(e => e.Stato)
                .HasDefaultValue(true)
                .HasColumnName("stato");
            entity.Property(e => e.Titolo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("titolo");
        });

        modelBuilder.Entity<Prestito>(entity =>
        {
            entity.HasKey(e => e.PrestitoId).HasName("PK__Prestito__7E579A751944A470");

            entity.ToTable("Prestito");

            entity.Property(e => e.PrestitoId).HasColumnName("prestitoID");
            entity.Property(e => e.CodicePrestito)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("codicePrestito");
            entity.Property(e => e.DataPrestito)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("dataPrestito");
            entity.Property(e => e.DataRitorno)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("dataRitorno");
            entity.Property(e => e.LibroRiff).HasColumnName("libroRIFF");
            entity.Property(e => e.UtenteRiff).HasColumnName("utenteRIFF");

            entity.HasOne(d => d.LibroRiffNavigation).WithMany(p => p.Prestitos)
                .HasForeignKey(d => d.LibroRiff)
                .HasConstraintName("FK__Prestito__libroR__1AD3FDA4");

            entity.HasOne(d => d.UtenteRiffNavigation).WithMany(p => p.Prestitos)
                .HasForeignKey(d => d.UtenteRiff)
                .HasConstraintName("FK__Prestito__utente__19DFD96B");
        });

        modelBuilder.Entity<Utente>(entity =>
        {
            entity.HasKey(e => e.UtenteId).HasName("PK__Utente__CA5C22536BD63A15");

            entity.ToTable("Utente");

            entity.Property(e => e.UtenteId).HasColumnName("utenteID");
            entity.Property(e => e.CodiceUtente)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("codiceUtente");
            entity.Property(e => e.Cognome)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("cognome");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
