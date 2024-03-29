﻿using Mitarbeiterverwaltung.Objects;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Windows;
using Formatting = Newtonsoft.Json.Formatting;


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
                    mitarbeiter.Abteilung = reader.GetInt32("Abteilung");
                    mitarbeiter.ParkplatzNr = reader.GetInt32("ParkplatzNr");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
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
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void ExportMitarbeiter()
        {
            //List, weil deren Größe (im gegensatz zum Array) dynamisch angepasst werden kann, zudem Methoden wie .Add(), .Remove() etc.
            List<object> exportedData = new List<object>();

            // ToDo: Path dynmaisch anpassen
            string benutzerPfad = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filename = $"db_export_{DateTime.Now:yyyyMMdd}.json";

            string speicherort = Path.Combine(benutzerPfad, "Desktop", filename);
            
            try
            {
                connection.Open();
                string query = "SELECT mitarbeiter.personalnummer as personalnummer, mitarbeiter.vorname, mitarbeiter.nachname, mitarbeiter.geburtstag, abteilung.name as abteilung, abteilung.kostenstelle, parkplatz.parkplatznr, parkplatz.schatten as parkplatzschatten, parkplatz.stockwerk as parkplatzstockwerk from mitarbeiter LEFT JOIN abteilung on mitarbeiter.abteilung = abteilung.id LEFT JOIN parkplatz on mitarbeiter.parkplatznr = parkplatz.parkplatznr";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var rowData = new
                    {
                        personalnummer = reader["personalnummer"],
                        vorname = reader["vorname"],
                        nachname = reader["nachname"],
                        geburtstag = reader["geburtstag"],
                        abteilung = reader["abteilung"],
                        kostenstelle = reader["kostenstelle"],
                        parkplatznr = reader["parkplatznr"],
                        parkplatzschatten = reader["parkplatzschatten"],
                        parkplatzstockwerk = reader["parkplatzstockwerk"]
                    };

                    exportedData.Add(rowData);
                }

                string jsondata = JsonConvert.SerializeObject(exportedData, Formatting.Indented);
                File.WriteAllText(speicherort, jsondata);

                Console.WriteLine("Daten wurden erfolgreich als json exportiert.");
                MessageBox.Show($"Alle Mitarbeiterdaten wurden exportiert nach: {speicherort}");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void EditMitarbeiter(Mitarbeiter mitarbeiter)
        {
            try
            {
                connection.Open();
                string query = "UPDATE mitarbeiter SET Vorname = @Vorname, Nachname = @Nachname, Abteilung = @Abteilung, ParkplatzNr = @ParkplatzNr WHERE Personalnummer = @Personalnummer";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Vorname", mitarbeiter.Vorname);
                command.Parameters.AddWithValue("@Nachname", mitarbeiter.Nachname);
                command.Parameters.AddWithValue("@Abteilung", mitarbeiter.Abteilung);
                command.Parameters.AddWithValue("@ParkplatzNr", mitarbeiter.ParkplatzNr);
                command.Parameters.AddWithValue("@Personalnummer", mitarbeiter.Personalnummer);
                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        // DAO-Methoden für Abteilungen

        public Abteilung GetAbteilungById(int id)
        {
            Abteilung abteilung = new Abteilung();

            try
            {
                connection.Open();
                string query = "SELECT * FROM abteilung WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    abteilung.Id = reader.GetInt32("Id");
                    abteilung.Name = reader.GetString("Name");
                    abteilung.Kostenstelle = reader.GetInt32("Kostenstelle");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return abteilung;
        }

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
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return abteilungList;
        }

        // Funktionen für Parkplätze

        public List<Parkplatz> GetAllParkplaetze()
        {
            List<Parkplatz> parkplatzList = new List<Parkplatz>();

            try
            {
                connection.Open();
                string query = "SELECT * FROM parkplatz";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Parkplatz parkplatz = new Parkplatz();

                    parkplatz.ParkplatzNr = reader.GetInt32("ParkplatzNr");
                    parkplatz.Schatten = reader.GetBoolean("Schatten");
                    parkplatz.Stockwerk = reader.GetInt32("Stockwerk");

                    parkplatzList.Add(parkplatz);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return parkplatzList;
        }

        public Parkplatz GetParkplatzById(int? id)
        {
            Parkplatz parkplatz = new Parkplatz();

            try
            {
                connection.Open();
                string query = "SELECT * FROM parkplatz WHERE parkplatzNr = @parkplatzNr";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@parkplatzNr", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    parkplatz.ParkplatzNr = reader.GetInt32("parkplatzNr");
                    parkplatz.Schatten = reader.GetBoolean("Schatten");
                    parkplatz.Stockwerk = reader.GetInt32("Stockwerk");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return parkplatz;
        }
    }
}

