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
    public partial class DelMitarbeiterCheckWindow : Window
    {
        private MitarbeiterService mitarbeiterService;
        private Mitarbeiter mitarbeiter;

        public DelMitarbeiterCheckWindow(MitarbeiterService mitarbeiterService, Mitarbeiter mitarbeiter)
        {
            InitializeComponent();
            this.mitarbeiterService = mitarbeiterService;
            this.mitarbeiter = mitarbeiter;
            Label_DelMitarbeiterName.Content = $"{mitarbeiter.Vorname} {mitarbeiter.Nachname}";
        }

        // Bei Klick auf Button Bestätigen
        private void Button_DelMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            // Lösche den Mitarbeiter, zeige Erfolgsmeldung, schließe Fenster
            mitarbeiterService.DelMitarbeiterById(mitarbeiter.Personalnummer);
            MessageBox.Show($"Mitarbeiter mit Personalnummer {mitarbeiter.Personalnummer} erfolgreich gelöscht!");
            this.Close();
            this.Owner.Close();
        }

        // Sicherungsabfrage bevor Löschung
        // Bei Änderung des Textfelds Eingabe
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
