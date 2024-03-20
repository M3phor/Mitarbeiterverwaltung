using Mitarbeiterverwaltung.Objects;
using Mitarbeiterverwaltung.Services;
using System.Windows;
using System.Windows.Controls;

namespace Mitarbeiterverwaltung
{
    public partial class AddMitarbeiterWindow : Window
    {
        private MitarbeiterService mitarbeiterService;
        private AbteilungService abteilungService;

        /// <summary>
        /// Konstruktor der AddMitarbeiterWindow-Klasse.
        /// Initialisiert das Fenster und die zugehörigen Service-Objekte.
        /// </summary>
        /// <param name="mitarbeiterService">Ein Objekt des Typs MitarbeiterService, das für die Kommunikation mit der Datenbank für Mitarbeiter zuständig ist.</param>
        public AddMitarbeiterWindow(MitarbeiterService mitarbeiterService, AbteilungService abteilungService)
        {
            InitializeComponent();
            this.mitarbeiterService = mitarbeiterService;
            this.abteilungService = abteilungService;
            combobox_abteilung = this.abteilungService.FillAbteilungDropDown(combobox_abteilung);
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Hinzufügen eines Mitarbeiters.
        /// Erstellt Mitarbeiterobjekt basierend auf Eingaben.
        /// Überprüft Gültigkeit der eingegeben Werte. 
        /// Falls Werte gültig, füge Mitarbeiter der Datenbank hinzu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            // ToDo: Eingabefehler abfangen:    - Namen?
            //                                  - Geburtstag

            Mitarbeiter mitarbeiter = new Mitarbeiter();
            bool checkFlag = true;
            string error = "Fehler! Falscher Input für:\n";

            mitarbeiter.Vorname = txtbox_vorname.Text;
            mitarbeiter.Nachname = txtbox_nachname.Text;

            // Überprüft ob linker Ausdruck = null, falls ja, verwende DateTime.MinValue
            mitarbeiter.Geburtstag = datepicker_geburtstag.SelectedDate ?? DateTime.MinValue;


            // Textbox Parkplatznr nicht null oder leer
            if (!string.IsNullOrEmpty(txtbox_parkplatznr.Text))
            {
                // Versuche Textbox Parkplatznr zu int zu konvertieren
                if (int.TryParse(txtbox_parkplatznr.Text, out int parsedParkplatzNr))
                {
                    mitarbeiter.ParkplatzNr = parsedParkplatzNr;
                }
                else
                {
                    checkFlag = false;
                    error += "Parkplatz: Es wird ein leerer Input oder eine Ganzzahl erwartet!\n";
                }
            }

            if (combobox_abteilung.SelectedItem != null)
            {
                string selectedItem = ((ComboBoxItem)combobox_abteilung.SelectedItem).Content.ToString();

                if (int.TryParse(selectedItem.Substring(0, 1), out int parsedAbteilung))
                {
                    mitarbeiter.Abteilung = parsedAbteilung;
                }
            }

            if (checkFlag)
            {
                mitarbeiterService.AddMitarbeiter(mitarbeiter);
                MessageBox.Show($"Mitarbeiter {mitarbeiter.Vorname} {mitarbeiter.Nachname} wurde erfolgreich erstellt!");
                this.Close();
            }
            else
            {
                MessageBox.Show($"{error}");
            }
        }
    }
}
