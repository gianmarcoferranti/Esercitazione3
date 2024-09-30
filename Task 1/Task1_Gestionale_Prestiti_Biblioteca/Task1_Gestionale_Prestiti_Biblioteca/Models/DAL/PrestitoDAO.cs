using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models.DAL
{
    internal class PrestitoDAO : IDaoLettura<PrestitoLINQ>
    {
        private static PrestitoDAO? instance;

        public static PrestitoDAO GetInstance()
        {
            if (instance == null)
                instance = new PrestitoDAO();

            return instance;
        }

        private PrestitoDAO() { }
        public List<PrestitoLINQ> GetAll()
        {
            throw new NotImplementedException();
        }

        public PrestitoLINQ GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void GetPrstitosForUserCode(string codUser)
        {
            using (SqlConnection conn = new SqlConnection(Config.credenziali))
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = query;
                cmd.CommandText = "SELECT U.nome, U.cognome, L.titolo, P.dataPrestito FROM Prestito AS P JOIN Utente AS U ON P.utenteRIFF = U.utenteID JOIN Libro AS L ON P.libroRIFF = L.libroID WHERE U.codiceUtente = @cod AND P.dataRitorno  IS NULL;";
                cmd.Parameters.AddWithValue("@cod", codUser);

                try
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PrestitoLINQ temp = new PrestitoLINQ()
                        {
                            DataPrestito =  DateOnly.FromDateTime(reader.GetDateTime(3)),
                            UtenteNominativo = reader.GetString(0) + " " + reader.GetString(1),
                            LibroNominativo = reader.GetString(2)
                        };

                        Console.WriteLine($"{temp.UtenteNominativo} {temp.LibroNominativo} {temp.DataPrestito}");
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
