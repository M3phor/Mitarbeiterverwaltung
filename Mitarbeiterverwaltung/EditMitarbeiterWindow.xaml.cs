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
    /// Interaction logic for EditMitarbeiterWindow.xaml
    /// </summary>
    public partial class EditMitarbeiterWindow : Window
    {
        private MitarbeiterService mitarbeiterService;
        public Mitarbeiter mitarbeiter;

        public EditMitarbeiterWindow(MitarbeiterService mitarbeiterService)
        {
            InitializeComponent();
            this.mitarbeiterService = mitarbeiterService;
        }

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
                checkFlag = false;
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

        private void Button_GetMitarbeiter_Click(object sender, RoutedEventArgs e)
        {

            // Versuche Eingabe als Integer zu parsen
            if (int.TryParse(txtbox_personalnr.Text, out int parsedPersonalNr))
            {
                mitarbeiter = mitarbeiterService.GetMitarbeiterById(parsedPersonalNr);
                
                // Falls Mitarberiter gefunden
                if (mitarbeiter.Vorname != null && mitarbeiter.Nachname != null) 
                {
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
        }
    }
}
