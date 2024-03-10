using Mitarbeiterverwaltung.Objects;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.DatabaseAccessObject
{
    public class DAOConnector
    {
        private readonly string connectionString;
        private readonly MySqlConnection connection;

        // Konstruktor für DAO
        public DAOConnector(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new MySqlConnection(this.connectionString);
        }

        // DAO-Methoden für Mitarbeiter

        public Mitarbeiter GetMitarbeiterById(int id)
        {
            Mitarbeiter mitarbeiter = new Mitarbeiter();
            
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM mitarbeiter WHERE Personalnummer = @Id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        mitarbeiter.Personalnummer = reader.GetInt32("Personalnummer");
                        mitarbeiter.Vorname = reader.GetString("Vorname");
                        mitarbeiter.Nachname = reader.GetString("Nachname");
                        mitarbeiter.Geburtstag = reader.GetDateTime("Geburtstag");
                        mitarbeiter.Abteilung = reader.GetInt32("AbteilungId");
                        mitarbeiter.ParkplatzNr = reader.GetInt32("ParkplatzId");
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            return mitarbeiter;
        }

        public List<Mitarbeiter> GetAllMitarbeiter()
        {
            List<Mitarbeiter> mitarbeiterList = new List<Mitarbeiter>();

            try
            {
                connection.Open();
                string query = "SELECT * FROM mitarbeiter";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Mitarbeiter mitarbeiter = new Mitarbeiter();

                    mitarbeiter.Personalnummer = reader.GetInt32("Personalnummer");
                    mitarbeiter.Vorname = reader.GetString("Vorname");
                    mitarbeiter.Nachname = reader.GetString("Nachname");
                    mitarbeiter.Geburtstag = reader.GetDateTime("Geburtstag");
                    mitarbeiter.Abteilung = reader.GetInt32("Abteilung");

                    if (!reader.IsDBNull(reader.GetOrdinal("Parkplatznr")))
                    {
                        mitarbeiter.ParkplatzNr = reader.GetInt32("Parkplatznr");
                    }
                    else
                    {
                        mitarbeiter.ParkplatzNr = null;
                    }

                    mitarbeiterList.Add(mitarbeiter);
                }
                connection.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return mitarbeiterList;
        }

        public void DelMitarbeiterById(int id)
        {
            try 
            {
                connection.Open();
                string query = "DELETE FROM mitarbeiter WHERE Personalnummer = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }

            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void AddMitarbeiter(Mitarbeiter mitarbeiter) 
        {
            try
            {
                
                connection.Open();
                string query = "INSERT INTO mitarbeiter (Vorname, Nachname, Geburtstag, Abteilung, ParkplatzNr) VALUES (@Vorname, @Nachname, @Geburtstag, @Abteilung, @ParkplatzNr)";
                int year = mitarbeiter.Geburtstag.Year;
                int month = mitarbeiter.Geburtstag.Month;
                int day = mitarbeiter.Geburtstag.Day;
                string geburtstag = $"{year}-{month}-{day}";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Vorname", mitarbeiter.Vorname);
                command.Parameters.AddWithValue("@Nachname", mitarbeiter.Nachname);
                command.Parameters.AddWithValue("@Geburtstag", geburtstag);
                command.Parameters.AddWithValue("@Abteilung", mitarbeiter.Abteilung);
                command.Parameters.AddWithValue("@ParkplatzNr", mitarbeiter.ParkplatzNr);
                command.ExecuteNonQuery();
                connection.Close();
            }

            catch( Exception ex )
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }



        // DAO-Methoden für Abteilungen

        //ToDo: GetAbteilungById

        public List<Abteilung> GetAllAbteilungen()
        {
            List<Abteilung> abteilungList = new List<Abteilung>();

            try
            {
                connection.Open();
                string query = "SELECT * FROM abteilung";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Abteilung abteilung = new Abteilung();

                    abteilung.Id = reader.GetInt32("Id");
                    abteilung.Name = reader.GetString("Name");
                    abteilung.Kostenstelle = reader.GetInt32("Kostenstelle");

                    abteilungList.Add(abteilung);
                }
                connection.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return abteilungList;
        }


        // Funktionen für Parkplätze

        //ToDo: GetParkplatzById

        //ToDo: GetAllParkplatz


    }


}

