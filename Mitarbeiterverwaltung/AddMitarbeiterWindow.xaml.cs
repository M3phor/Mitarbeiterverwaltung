using Mitarbeiterverwaltung.Objects;
using Mitarbeiterverwaltung.Services;
using System.Windows;

namespace Mitarbeiterverwaltung
{
    public partial class AddMitarbeiterWindow : Window
    {
        private MitarbeiterService mitarbeiterService;


        public AddMitarbeiterWindow(MitarbeiterService mitarbeiterService)
        {
            InitializeComponent();
            this.mitarbeiterService = mitarbeiterService;
        }

        private void Button_AddMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            // ToDo: Eingabefehler abfangen:    - Namen?
            //                                  - Geburtstag

            Mitarbeiter mitarbeiter = new Mitarbeiter();
            bool checkFlag = true;

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
                    MessageBox.Show("Ungültige Eingabe bei Parkplatz. Es wird kein Input oder eine Ganzzahl erwartet!");
                    checkFlag = false;
                }
            }

            // Textbox Abteilung nicht null oder leer
            if (!string.IsNullOrEmpty(txtbox_abteilung.Text))
            {
                if (int.TryParse(txtbox_abteilung.Text, out int abteilung))
                {
                    mitarbeiter.Abteilung = abteilung;
                }
                else
                {
                    MessageBox.Show("Ungültige Eingabe bei Abteilung. Es wird eine Ganzzahl erwartet!");
                    checkFlag = false;
                }
            }
            else
            {
                MessageBox.Show("Ungültige Eingabe bei Abteilung. Das Feld darf nicht leer sein!");
                checkFlag = false;
            }

            if (checkFlag)
            {
                mitarbeiterService.AddMitarbeiter(mitarbeiter);
                MessageBox.Show($"Mitarbeiter {mitarbeiter.Vorname} {mitarbeiter.Nachname} wurde erfolgreich erstellt!");
                this.Close();
            }
        }
    }
}
