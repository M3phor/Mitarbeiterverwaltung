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
        private bool isTxtboxAbteilungsnameEnabled;

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
            isTxtboxAbteilungsnameEnabled = false;
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
            mitarbeiter.Vorname = txtbox_vorname.Text;
            mitarbeiter.Nachname = txtbox_nachname.Text;

            // Überprüfe Eingabe Abteilung
            if (!string.IsNullOrEmpty(txtbox_abteilung.Text))
            {
                // Versuche Textbox Abteilung zu int zu konvertieren
                if (int.TryParse(txtbox_abteilung.Text, out int parsedAbteilung))
                {
                    mitarbeiter.Abteilung = parsedAbteilung;
                }
                else
                {
                    checkFlag = false;
                }
            }
            else
            {
                checkFlag = false;
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
                MessageBox.Show("Überprüfen Sie die Eingabe");
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
                Abteilung abteilung = abteilungService.GetAbteilungById(parsedPersonalNr);

                // Falls Mitarberiter gefunden
                if (mitarbeiter.Vorname != null && mitarbeiter.Nachname != null)
                {
                    //Aktiviere Abteilungsnamenabgleich
                    isTxtboxAbteilungsnameEnabled = true;

                    // Textboxen mit Inhalt füllen
                    txtbox_vorname.Text = mitarbeiter.Vorname;
                    txtbox_nachname.Text = mitarbeiter.Nachname;
                    datepicker_geburtstag.SelectedDate = mitarbeiter.Geburtstag;
                    txtbox_abteilung.Text = mitarbeiter.Abteilung.ToString();
                    txtbox_parkplatznr.Text = mitarbeiter.ParkplatzNr.ToString();

                    // UI-Elemente aktivieren
                    txtbox_vorname.IsReadOnly = false;
                    txtbox_nachname.IsReadOnly = false;
                    txtbox_abteilung.IsReadOnly = false;
                    txtbox_parkplatznr.IsReadOnly = false;
                    Button_EditMitarbeiter.IsEnabled = true;
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
                MessageBox.Show("Ungültige Eingabe!");
            }
        }
        /// <summary>
        /// Ereignishandler, der ausgelöst wird, wenn sich der Inhalt der Abteilungs-Textbox ändert.
        /// Aktualisiert den Namen der Abteilung, wenn eine gültige Abteilungsnummer eingegeben wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txtbox_abteilung_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isTxtboxAbteilungsnameEnabled)
            {
                if (int.TryParse(txtbox_abteilung.Text, out int parsedAbteilung))
                {
                    Abteilung abteilung = abteilungService.GetAbteilungById(parsedAbteilung);
                    if (abteilung.Name != null)
                    {
                        txtbox_abteilungsname.Text = abteilung.Name;
                    }
                    else
                    {
                        MessageBox.Show($"Abteilung mit Id {parsedAbteilung} nicht gefunden!");
                        txtbox_abteilung.Text = mitarbeiter.Abteilung.ToString();
                    }
                }

                else
                {
                    if (string.IsNullOrEmpty(txtbox_abteilung.Text))
                    {
                        txtbox_abteilungsname.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Ungültige Eingabe!");
                        txtbox_abteilung.Text = mitarbeiter.Abteilung.ToString();
                    }
                }
            }
        }
    }
}
