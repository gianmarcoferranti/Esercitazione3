using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models.DAL
{
    internal class UtenteDAO : IDaoLettura<UtenteLINQ>
    {
        private static UtenteDAO? instance;

        public static UtenteDAO GetInstance()
        {
            if (instance == null)
                instance = new UtenteDAO();

            return instance;
        }

        private UtenteDAO() { }

        List<UtenteLINQ> IDaoLettura<UtenteLINQ>.GetAll()
        {
            throw new NotImplementedException();
        }

        UtenteLINQ IDaoLettura<UtenteLINQ>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void GetUserForPrestitos()
        {
            using (SqlConnection conn = new SqlConnection(Config.credenziali))
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = query;
                cmd.CommandText = "SELECT U.utenteID, U.nome, U.cognome, COUNT(*) FROM Utente AS U JOIN Prestito AS P ON U.utenteID = P.utenteRIFF WHERE U.utenteID = 1 AND P.dataRitorno IS NULL GROUP BY U.utenteID, U.nome, U.cognome ORDER BY COUNT(*);";
                

                try
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UtenteLINQ temp = new UtenteLINQ()
                        {
                            UtenteId = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Cognome = reader.GetString(2),
                            ContatorePrestiti = reader.GetInt32(3)
                            
                        };

                        Console.WriteLine($"{temp.Nome} {temp.Cognome} {temp.ContatorePrestiti}");
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
