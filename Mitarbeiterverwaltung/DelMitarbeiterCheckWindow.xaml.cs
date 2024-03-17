using Mitarbeiterverwaltung.Objects;
using Mitarbeiterverwaltung.Services;
using System.Windows;
using System.Windows.Controls;

namespace Mitarbeiterverwaltung
{

    public partial class DelMitarbeiterCheckWindow : Window
    {
        private MitarbeiterService mitarbeiterService;
        private Mitarbeiter mitarbeiter;

        /// <summary>
        /// Konstruktor der DelMitarbeiterCheckWindow-Klasse.
        /// Initialisiert die Benutzeroberfläche des DelMitarbeiterCheckWindow-Fensters.
        /// </summary>
        /// <param name="mitarbeiterService">Ein Objekt des Typs MitarbeiterService, das für die Kommunikation mit der Datenbank für Mitarbeiter zuständig ist.</param>
        /// <param name="mitarbeiter">Ein Objekt des Typs Mitarbeiter, das den zu löschenden Mitarbeiter darstellt.</param>
        public DelMitarbeiterCheckWindow(MitarbeiterService mitarbeiterService, Mitarbeiter mitarbeiter)
        {
            InitializeComponent();
            this.mitarbeiterService = mitarbeiterService;
            this.mitarbeiter = mitarbeiter;
            Label_DelMitarbeiterName.Content = $"{mitarbeiter.Vorname} {mitarbeiter.Nachname}";
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button Bestätigen.
        /// Löscht Mitarbeiter aus Datenbank, zeigt Erfolgsmeldung und schließt die Fenster.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_DelMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            // Lösche den Mitarbeiter, zeige Erfolgsmeldung, schließe Fenster
            mitarbeiterService.DelMitarbeiterById(mitarbeiter.Personalnummer);
            MessageBox.Show($"Mitarbeiter mit Personalnummer {mitarbeiter.Personalnummer} erfolgreich gelöscht!");
            this.Close();
            this.Owner.Close();
        }
        /// <summary>
        /// Ereignishandler, der ausgelöst wird, wenn sich der Inhalt der Bestätigungs-Textbox ändert.
        /// Überprüft den eingegebenen Text auf Gleichheit mit Sicherungsstring
        /// Aktivert den Button bei Gleichheit, deaktiert den Button bei Unterschied
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_DelMitarbeiterName_TextChanged(object sender, TextChangedEventArgs e)
        {

            // Falls Eingabe == Vor/-Nachname des Mitarbeiter
            if (txtbox_DelMitarbeiterName.Text == Label_DelMitarbeiterName.Content.ToString())
            {
                // Aktiviere Button
                Button_DelMitarbeiterBestätigen.IsEnabled = true;
            }
            else
            {
                // Deaktiviere Button
                Button_DelMitarbeiterBestätigen.IsEnabled = false;
            }
        }

        // Bei Klick auf Abbruch
        private void Button_CancelDelMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            // Schließe Fenster
            this.Close();
        }
    }
}
