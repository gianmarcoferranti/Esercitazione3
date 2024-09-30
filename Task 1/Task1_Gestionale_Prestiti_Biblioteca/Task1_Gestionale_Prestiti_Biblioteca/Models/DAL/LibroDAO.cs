using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models.DAL
{
    internal class LibroDAO : IDaoLettura<LibroLINQ>
    {
        private static LibroDAO? instance;

        public static LibroDAO GetInstance()
        {
            if (instance == null)
                instance = new LibroDAO();

            return instance;
        }

        private LibroDAO() { }
        public List<LibroLINQ> GetAll()
        {
            throw new NotImplementedException();
        }

        public LibroLINQ GetById(int id)
        {
            throw new NotImplementedException();
        }


        public void GetLibrosForPrestito()
        {
            using (SqlConnection conn = new SqlConnection(Config.credenziali))
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = query;
                cmd.CommandText = "SELECT L.titolo, L.autore FROM Libro AS L WHERE L.stato = 1;";


                try
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LibroLINQ temp = new LibroLINQ()
                        {
                            Titolo = reader.GetString(0),
                            Autore = reader.GetString(1)
                        };

                        Console.WriteLine($"{temp.Titolo} {temp.Autore} Disponibile");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
        }

    }
}
