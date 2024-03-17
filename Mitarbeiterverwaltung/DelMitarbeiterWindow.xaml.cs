using Mitarbeiterverwaltung.Objects;
using Mitarbeiterverwaltung.Services;
using System.Windows;

namespace Mitarbeiterverwaltung
{
    public partial class DelMitarbeiterWindow : Window
    {
        private MitarbeiterService mitarbeiterService;

        /// <summary>
        /// Konstruktor der DelMitarbeiterWindow Klasse.
        /// Initialisiert die Benutzeroberfläche des DelMitarbeiterWindow-Fensters.
        /// </summary>
        /// <param name="mitarbeiterService">Ein Objekt des Typs MitarbeiterService, das für die Kommunikation mit der Datenbank für Mitarbeiter zuständig ist.</param>
        public DelMitarbeiterWindow(MitarbeiterService mitarbeiterService)
        {
            InitializeComponent();
            this.mitarbeiterService = mitarbeiterService;
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Löschen des Mitarbeiters.
        /// Überprüft die eingegeben Daten und suche den dazugehörigen Mitarbeiter.
        /// Falls gefunden, öffnet ein neues Fenster zur Bestätigung der Löschung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_DelMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            bool foundMitarbeiter = false;

            // Versuche Eingabe als Integer zu parsen
            if (int.TryParse(txtbox_DelMitarbeiter.Text, out int parsedPersonalNr))
            {
                List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();

                // Vergleiche Eingabe mit existierenden Mitarbeitern
                foreach (Mitarbeiter mitarbeiter in mitarbeiterList)
                {
                    // Falls gefunden, öffne Bestätigungsfenster
                    if (mitarbeiter.Personalnummer == parsedPersonalNr)
                    {
                        foundMitarbeiter = true;
                        DelMitarbeiterCheckWindow delMitarbeiterCheckWindow = new DelMitarbeiterCheckWindow(mitarbeiterService, mitarbeiter);
                        delMitarbeiterCheckWindow.Owner = this;
                        delMitarbeiterCheckWindow.ShowDialog();
                        break;
                    }
                }
                // Falls nicht gefunden, Fehlermeldung
                if (!foundMitarbeiter)
                {
                    MessageBox.Show($"Kein Mitarbeiter mit Personalnummer {parsedPersonalNr} gefunden!");
                }
            }
            // Falls kein Integer, Fehlermeldung
            else
            {
                MessageBox.Show($"Ungültige Eingabe. Es wird eine Ganzzahl erwartet!");
            }
        }
    }
}
