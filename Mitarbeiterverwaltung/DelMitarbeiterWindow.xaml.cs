using Mitarbeiterverwaltung.Objects;
using Mitarbeiterverwaltung.Services;
using System.Windows;

namespace Mitarbeiterverwaltung
{
    public partial class DelMitarbeiterWindow : Window
    {
        private MitarbeiterService mitarbeiterService;

        public DelMitarbeiterWindow(MitarbeiterService mitarbeiterService)
        {
            InitializeComponent();
            this.mitarbeiterService = mitarbeiterService;
        }
        // Bei Klick auf Button Mitarbeiter Löschen
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
