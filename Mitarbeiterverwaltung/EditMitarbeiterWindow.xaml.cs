using Mitarbeiterverwaltung.Objects;
using Mitarbeiterverwaltung.Services;
using System.Windows;
using System.Windows.Controls;

namespace Mitarbeiterverwaltung
{
    public partial class EditMitarbeiterWindow : Window
    {
        private MitarbeiterService mitarbeiterService;
        private AbteilungService abteilungService;
        private Mitarbeiter mitarbeiter;

        /// <summary>
        /// Konstruktor der EditMitarbeiterWindow-Klasse.
        /// Initialisiert das Fenster und die zugehörigen Service-Objekte.
        /// </summary>
        /// <param name="mitarbeiterService">Ein Objekt des Typs MitarbeiterService, das für die Kommunikation mit der Datenbank für Mitarbeiter zuständig ist.</param>
        /// <param name="abteilungService">Ein Objekt des Typs AbteilungService, das für die Kommunikation mit der Datenbank für Abteilungen zuständig ist.</param>
        public EditMitarbeiterWindow(MitarbeiterService mitarbeiterService, AbteilungService abteilungService)
        {
            InitializeComponent();
            this.mitarbeiterService = mitarbeiterService;
            this.abteilungService = abteilungService;
            combobox_abteilung = this.abteilungService.FillAbteilungDropDown(combobox_abteilung);
        }

        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Speichern von Änderungen.
        /// Überprüft die eingegebenen Daten und führt die Bearbeitung des Mitarbeiters durch.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_EditMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            bool checkFlag = true;
            string error = "Fehler! Falscher Input für:\n";

            mitarbeiter.Vorname = txtbox_vorname.Text;
            mitarbeiter.Nachname = txtbox_nachname.Text;

            // Überprüfe Eingabe Abteilung
            if(combobox_abteilung.SelectedItem != null )
            {
                string selectedItem = ((ComboBoxItem)combobox_abteilung.SelectedItem).Content.ToString();

                if (int.TryParse(selectedItem.Substring(0,1),out int parsedAbteilung))
                {
                    mitarbeiter.Abteilung = parsedAbteilung;
                }
            }

            // Überprüfe Eingabe Parkplatznr
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
            else
            {
                mitarbeiter.ParkplatzNr = null;
            }

            if (checkFlag)
            {
                mitarbeiterService.EditMitarbeiter(mitarbeiter);
                MessageBox.Show($"Der Mitarbeiter {mitarbeiter.Vorname} {mitarbeiter.Nachname} mit Personalnummer {mitarbeiter.Personalnummer} wurde erfolgreich bearbeitet!");
                this.Close();
            }
            else
            {
                MessageBox.Show($"{error}");
            }
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Suchen eines Mitarbeiters.
        /// Ruft die Informationen des Mitarbeiters basierend auf der eingegebenen Personalnummer ab.
        /// Füllt die Textboxen mit den Daten.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_GetMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            // Versuche Eingabe als Integer zu parsen
            if (int.TryParse(txtbox_personalnr.Text, out int parsedPersonalNr))
            {
                mitarbeiter = mitarbeiterService.GetMitarbeiterById(parsedPersonalNr);
                
                // Falls Mitarberiter gefunden
                if (mitarbeiter.Vorname != "" && mitarbeiter.Nachname != "")
                {
                    
                    // Textboxen mit Inhalt füllen
                    txtbox_vorname.Text = mitarbeiter.Vorname;
                    txtbox_nachname.Text = mitarbeiter.Nachname;
                    datepicker_geburtstag.SelectedDate = mitarbeiter.Geburtstag;
                    txtbox_parkplatznr.Text = mitarbeiter.ParkplatzNr.ToString();

                    foreach(ComboBoxItem item in combobox_abteilung.Items)
                    {
                        if (item.Content.ToString().Substring(0,1) == mitarbeiter.Abteilung.ToString())
                        {
                            combobox_abteilung.SelectedItem = item;
                        }
                    }

                    // UI-Elemente aktivieren
                    txtbox_vorname.IsReadOnly = false;
                    txtbox_nachname.IsReadOnly = false;
                    txtbox_parkplatznr.IsReadOnly = false;
                    Button_EditMitarbeiter.IsEnabled = true;
                    combobox_abteilung.IsEnabled = true;
                }
                // Falls kein Mitarbeiter gefunden
                else
                {
                    MessageBox.Show($"Mitarbeiter mit Personalnummer {parsedPersonalNr} nicht gefunden!");
                }
            }
            // Falls Personalnummer kein Integer
            else
            {
                MessageBox.Show("Fehler! Falscher Input für:\nPersonalnummer: Es wird eine Ganzzahl erwartet!");
            }
        }
    }
}
