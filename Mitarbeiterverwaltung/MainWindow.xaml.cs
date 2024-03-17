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

        public MainWindow(string connectionString, string username)
        {
            InitializeComponent();
            label_username.Content = username;
            mitarbeiterService = new MitarbeiterService(connectionString);
            abteilungService = new AbteilungService(connectionString);
            parkplatzService = new ParkplatzService(connectionString);
            loadDataGridMitarbeiter();
        }

        public void loadDataGridMitarbeiter()
        {
            List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();
            MainDataGrid.ItemsSource = mitarbeiterList;
            label_tabelle.Content = "Mitarbeiter";
        }

        private void btn_MitarbeiterAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            loadDataGridMitarbeiter();
        }

        private void btn_AbteilungenAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            List<Abteilung> abteilungList = abteilungService.GetAllAbteilungen();
            MainDataGrid.ItemsSource = abteilungList;
            label_tabelle.Content = "Abteilungen";
        }

        private void btn_parkanzeigen_Click(object sender, RoutedEventArgs e)
        {
            List<Parkplatz> parkplatzList = parkplatzService.GetAllParkplaetze();
            MainDataGrid.ItemsSource = parkplatzList;
            label_tabelle.Content = "Parkplätze";
        }

        private void btn_Window_AddMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            AddMitarbeiterWindow addMitarbeiterWindow = new AddMitarbeiterWindow(mitarbeiterService);
            addMitarbeiterWindow.Owner = this;
            addMitarbeiterWindow.ShowDialog();
            loadDataGridMitarbeiter();
        }

        private void btn_EditMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            EditMitarbeiterWindow editMitarbeiterWindow = new EditMitarbeiterWindow(mitarbeiterService, abteilungService);
            editMitarbeiterWindow.Owner = this;
            editMitarbeiterWindow.ShowDialog();
            loadDataGridMitarbeiter();
        }

        private void btn_Window_DelMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            DelMitarbeiterWindow delMitarbeiterWindow = new DelMitarbeiterWindow(mitarbeiterService);
            delMitarbeiterWindow.Owner = this;
            delMitarbeiterWindow.ShowDialog();
            loadDataGridMitarbeiter();
        }

        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            mitarbeiterService.ExportMitarbeiter();
        }

        private void btn_ExportAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();
            List<MitarbeiterGesamt> mitarbeiterGesamtList = new List<MitarbeiterGesamt>();

            foreach (Mitarbeiter mitarbeiter in mitarbeiterList)
            {
                MitarbeiterGesamt mitarbeiterGesamt = new MitarbeiterGesamt();
                Abteilung abteilung = abteilungService.GetAbteilungById(mitarbeiter.Personalnummer);

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
            MainDataGrid.ItemsSource = mitarbeiterGesamtList;
            label_tabelle.Content = "Export Tabelle";
        }
    }
}