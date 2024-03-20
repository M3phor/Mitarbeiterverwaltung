using Mitarbeiterverwaltung.Objects;
using Mitarbeiterverwaltung.Services;
using System.Windows;

namespace Mitarbeiterverwaltung
{
    public partial class MainWindow : Window
    {
        private MitarbeiterService mitarbeiterService;
        private AbteilungService abteilungService;
        private ParkplatzService parkplatzService;

        /// <summary>
        /// Konstruktor der MainWindow Klasse.
        /// Initialisiert die Benutzeroberfläche des MainWindow-Fensters.
        /// </summary>
        /// <param name="connectionString">Die Verbindungszeichenfolge zur Datenbank.</param>
        /// <param name="username">Der Benutzername, der in der Benutzeroberfläche angezeigt werden soll.</param>
        public MainWindow(string connectionString, string username)
        {
            InitializeComponent();
            label_Username.Content = username;

            // Initialisierung der Services
            mitarbeiterService = new MitarbeiterService(connectionString);
            abteilungService = new AbteilungService(connectionString);
            parkplatzService = new ParkplatzService(connectionString);
            loadDataGridMitarbeiter();
        }
        /// <summary>
        /// Lädt die Mitarbeiterdaten aus der Datenbank und zeigt sie in der Datentabelle an.
        /// </summary>
        public void loadDataGridMitarbeiter()
        {
            List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();
            mainDataGrid.ItemsSource = mitarbeiterList;
            label_Tabelle.Content = "Mitarbeiter";
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Anzeigen von Mitarbeitern.
        /// Ruft loadDataGridMitarbeiter() auf, um Mitarbeiterdaten anzuzeigen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MitarbeiterAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            loadDataGridMitarbeiter();
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Anzeigen von Abteilungen.
        /// Lädt Daten zu Abteilungen von der Datenbank und zeigt sie in der Datentabelle an.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AbteilungenAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            List<Abteilung> abteilungList = abteilungService.GetAllAbteilungen();
            mainDataGrid.ItemsSource = abteilungList;
            label_Tabelle.Content = "Abteilungen";
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Anzeigen von Parkplätzen.
        /// Lädt Daten zu Parkplätzen von der Datenbank und zeigt sie in der Datentabelle an.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ParkplatzAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            List<Parkplatz> parkplatzList = parkplatzService.GetAllParkplaetze();
            mainDataGrid.ItemsSource = parkplatzList;
            label_Tabelle.Content = "Parkplätze";
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Anzeigen der Exportdaten.
        /// Lädt Daten zu Mitarbeitern von der Datenbank.
        /// Vergleicht die Einträge pro Mitarbeitermit korrespondierenden Daten in den Tabellen Abteilung und Parkplatz.
        /// Zeigt die Daten in der Datentabelle an.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ExportAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();
            List<MitarbeiterGesamt> mitarbeiterGesamtList = new List<MitarbeiterGesamt>();

            foreach (Mitarbeiter mitarbeiter in mitarbeiterList)
            {
                MitarbeiterGesamt mitarbeiterGesamt = new MitarbeiterGesamt();
                Abteilung abteilung = abteilungService.GetAbteilungById(mitarbeiter.Abteilung);

                mitarbeiterGesamt.Personalnummer = mitarbeiter.Personalnummer;
                mitarbeiterGesamt.Vorname = mitarbeiter.Vorname;
                mitarbeiterGesamt.Nachname = mitarbeiter.Nachname;
                mitarbeiterGesamt.Geburtstag = mitarbeiter.Geburtstag;
                mitarbeiterGesamt.Abteilung = mitarbeiter.Abteilung;
                mitarbeiterGesamt.Abteilungsname = abteilung.Name;
                mitarbeiterGesamt.Kostenstelle = abteilung.Kostenstelle;
                mitarbeiterGesamt.ParkplatzNr = mitarbeiter.ParkplatzNr;
                if (mitarbeiter.ParkplatzNr != null)
                {
                    Parkplatz parkplatz = parkplatzService.GetParkplatzById(mitarbeiter.ParkplatzNr);
                    mitarbeiterGesamt.Schatten = parkplatz.Schatten;
                    mitarbeiterGesamt.Stockwerk = parkplatz.Stockwerk;
                }
                mitarbeiterGesamtList.Add(mitarbeiterGesamt);
            }
            mainDataGrid.ItemsSource = mitarbeiterGesamtList;
            label_Tabelle.Content = "Export Tabelle";
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Hinzufügen eines Mitarbeiters.
        /// Öffnet ein neues Fenster zum Hinzufügen eines Mitarbeiters und aktualisiert dann die Datentabelle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Window_AddMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            AddMitarbeiterWindow addMitarbeiterWindow = new AddMitarbeiterWindow(mitarbeiterService, abteilungService);
            addMitarbeiterWindow.Owner = this;
            addMitarbeiterWindow.ShowDialog();
            loadDataGridMitarbeiter();
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Bearbeiten eines Mitarbeiters.
        /// Öffnet ein neues Fenster zum Bearbeiten eines Mitarbeiters und aktualisiert dann die Datentabelle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            EditMitarbeiterWindow editMitarbeiterWindow = new EditMitarbeiterWindow(mitarbeiterService, abteilungService);
            editMitarbeiterWindow.Owner = this;
            editMitarbeiterWindow.ShowDialog();
            loadDataGridMitarbeiter();
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Löschen eines Mitarbeiters.
        /// Öffnet ein neues Fenster zum Löschen eines Mitarbeiters und aktualisiert dann die Datentabelle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Window_DelMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            DelMitarbeiterWindow delMitarbeiterWindow = new DelMitarbeiterWindow(mitarbeiterService);
            delMitarbeiterWindow.Owner = this;
            delMitarbeiterWindow.ShowDialog();
            loadDataGridMitarbeiter();
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Button zum Exportieren von Mitarbeiterdaten.
        /// Führt den Export der Mitarbeiterdaten aus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Export_Click(object sender, RoutedEventArgs e)
        {
            mitarbeiterService.ExportMitarbeiter();
        }
    }
}