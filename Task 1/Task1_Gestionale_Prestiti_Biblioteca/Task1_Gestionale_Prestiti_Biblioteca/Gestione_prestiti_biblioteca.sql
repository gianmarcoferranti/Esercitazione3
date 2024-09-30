CREATE TABLE Utente(
	utenteID INT PRIMARY KEY IDENTITY(1,1),
	codiceUtente VARCHAR(250) DEFAULT NEWID(),
	nome VARCHAR(250) NOT NULL,
	cognome VARCHAR(250) NOT NULL,
	email VARCHAR(250) NOT NULL
);
CREATE TABLE Libro(
	libroID INT PRIMARY KEY IDENTITY(1,1),
	codiceLibro VARCHAR(250) DEFAULT NEWID(),
	autore VARCHAR(250) NOT NULL,
	titolo VARCHAR(250) NOT NULL,
	annoPubblicazione INT NOT NULL CHECK(annoPubblicazione <= YEAR(GETDATE())),
	stato BIT NOT NULL DEFAULT 1
);

CREATE TABLE Prestito(
	prestitoID INT PRIMARY KEY IDENTITY(1,1),
	codicePrestito VARCHAR(250) DEFAULT NEWID(),
	dataPrestito DATE DEFAULT GETDATE(),
	dataRitorno DATE DEFAULT NULL,
	utenteRIFF INT NOT NULL,
	libroRIFF INT NOT NULL,
	FOREIGN KEY (utenteRIFF) REFERENCES Utente(utenteID) ON DELETE CASCADE,
	FOREIGN KEY (libroRIFF) REFERENCES Libro(libroID) ON DELETE CASCADE
);


-- Inserimenti nella tabella Utente
INSERT INTO Utente (nome, cognome, email) VALUES ('Mario', 'Rossi', 'mario.rossi@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Luca', 'Bianchi', 'luca.bianchi@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Giulia', 'Verdi', 'giulia.verdi@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Anna', 'Neri', 'anna.neri@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Marco', 'Gialli', 'marco.gialli@example.com');

INSERT INTO Utente (nome, cognome, email) VALUES ('Luigi', 'Verdi', 'luigi.verdi@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Anna', 'Bianchi', 'anna.bianchi@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Giulia', 'Neri', 'giulia.neri@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Sara', 'Rossi', 'sara.rossi@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Paolo', 'Verdi', 'paolo.verdi@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Elena', 'Bianchi', 'elena.bianchi@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Luca', 'Neri', 'luca.neri@example.com');
INSERT INTO Utente (nome, cognome, email) VALUES ('Francesca', 'Gialli', 'francesca.gialli@example.com');



-- Inserimenti nella tabella Libro
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Umberto Eco', 'Il nome della rosa', 1980);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Alessandro Manzoni', 'I promessi sposi', 1827);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Dante Alighieri', 'La Divina Commedia', 1320);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Italo Calvino', 'Le città invisibili', 1972);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Giovanni Boccaccio', 'Il Decameron', 1353);

INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('J.K. Rowling', 'Harry Potter e la Pietra Filosofale', 1997);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('J.R.R. Tolkien', 'Il Signore degli Anelli', 1954);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('George Orwell', '1984', 1949);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Jane Austen', 'Orgoglio e Pregiudizio', 1813);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('F. Scott Fitzgerald', 'Il Grande Gatsby', 1925);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Harper Lee', 'Il Buio Oltre la Siepe', 1960);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Gabriel Garcia Marquez', 'Cent''anni di Solitudine', 1967);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Herman Melville', 'Moby Dick', 1851);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Leo Tolstoy', 'Guerra e Pace', 1869);
INSERT INTO Libro (autore, titolo, annoPubblicazione) VALUES ('Mark Twain', 'Le Avventure di Tom Sawyer', 1876);


-- Inserimenti nella tabella Prestito
INSERT INTO Prestito (dataPrestito, utenteRIFF, libroRIFF) VALUES ('2024-01-15', 1, 1);
INSERT INTO Prestito (dataPrestito, dataRitorno, utenteRIFF, libroRIFF) VALUES ('2024-02-01', '2024-02-15', 2, 2);
INSERT INTO Prestito (dataPrestito, utenteRIFF, libroRIFF) VALUES ('2024-03-10', 3, 3);
INSERT INTO Prestito (dataPrestito, dataRitorno, utenteRIFF, libroRIFF) VALUES ('2024-04-05', '2024-04-20', 4, 4);
INSERT INTO Prestito (dataPrestito, utenteRIFF, libroRIFF) VALUES ('2024-05-12', 5, 5);
INSERT INTO Prestito (dataPrestito, dataRitorno, utenteRIFF, libroRIFF) VALUES ('2024-01-06', '2024-01-20', 6, 6);
INSERT INTO Prestito (dataPrestito, utenteRIFF, libroRIFF) VALUES ('2024-01-07', 7, 7);
INSERT INTO Prestito (dataPrestito, dataRitorno, utenteRIFF, libroRIFF) VALUES ('2024-01-08', '2024-01-22', 8, 8);
INSERT INTO Prestito (dataPrestito, utenteRIFF, libroRIFF) VALUES ('2024-01-09', 9, 9);
INSERT INTO Prestito (dataPrestito, dataRitorno, utenteRIFF, libroRIFF) VALUES ('2024-01-10', '2024-01-24', 10, 10);


