using Mitarbeiterverwaltung.Objects;
using Mitarbeiterverwaltung.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mitarbeiterverwaltung
{
    /// <summary>
    /// Interaktionslogik für AddMitarbeiterWindow.xaml
    /// </summary>
    public partial class AddMitarbeiterWindow : Window
    {
        private MitarbeiterService mitarbeiterService;
        private string connectionString = "Server=localhost;Database=mitarbeiterverwaltung;username=root;password=;";

        public AddMitarbeiterWindow()
        {
            InitializeComponent();
            mitarbeiterService = new MitarbeiterService(connectionString);
        }

        // ToDo: Errorhandling falls unerwartete Eingaben (Bsp. Text als Abteilung)
        private void Button_AddMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            string vorname = txtbox_vorname.Text;
            string nachname = txtbox_nachname.Text;

            // Überprüft ob linker Ausdruck = null, falls ja, verwende DateTime.MinValue
            DateTime geburtstag = datepicker_geburtstag.SelectedDate ?? DateTime.MinValue;

            // TODO: Eingabefehler abfangen
            int abteilung = Convert.ToInt32(txtbox_abteilung.Text);

            int? parkplatzNr = null;
            // Textbox Parkplatznr nicht null oder leer
            if (!string.IsNullOrEmpty(txtbox_parkplatznr.Text))
            {
                // Versuche Textbox Parkplatznr zu int zu konvertieren
                if (int.TryParse(txtbox_parkplatznr.Text, out int parsedParkplatzNr))
                {
                    parkplatzNr = parsedParkplatzNr;
                }
            }

            // Erstelle Objekt Mitarbeiter mit eingelesenen Daten
            Mitarbeiter mitarbeiter = new Mitarbeiter(vorname, nachname, geburtstag, abteilung, parkplatzNr);

            // Führe Servicefunktion Addmitarbeiter aus
            mitarbeiterService.AddMitarbeiter(mitarbeiter);
        }
    
    }
}
