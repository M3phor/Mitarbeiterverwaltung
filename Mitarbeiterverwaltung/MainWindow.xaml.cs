using Mitarbeiterverwaltung.DatabaseAccessObject;
using Mitarbeiterverwaltung.Objects;
using Mitarbeiterverwaltung.Services;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mitarbeiterverwaltung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MitarbeiterService mitarbeiterService;
        private AbteilungService abteilungService;
        private string connectionString = "Server=localhost;Database=mitarbeiterverwaltung;username=root;password=;";

        public MainWindow()
        {
            InitializeComponent();
            mitarbeiterService = new MitarbeiterService(connectionString);
            abteilungService = new AbteilungService(connectionString);
            List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();
            MainDataGrid.ItemsSource = mitarbeiterList;
            label_tabelle.Content = "Mitarbeiter";

        }

        private void btn_MitarbeiterAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();
            MainDataGrid.ItemsSource = mitarbeiterList;
            label_tabelle.Content = "Mitarbeiter";
        }

        private void btn_AbteilungenAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            List<Abteilung> abteilungList = abteilungService.GetAllAbteilungen();
            MainDataGrid.ItemsSource= abteilungList;
            label_tabelle.Content = "Abteilungen";
        }

        private void btn_parkanzeigen_Click(object sender, RoutedEventArgs e)
        {
            //ToDo
            label_tabelle.Content = "Parkplätze";
        }

        private void btn_Window_AddMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            AddMitarbeiterWindow addMitarbeiterWindow = new AddMitarbeiterWindow();
            addMitarbeiterWindow.Owner = this;
            addMitarbeiterWindow.ShowDialog();
        }

        private void btn_EditMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            //ToDo
        }

        private void btn_DelMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            //ToDo
        }

        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            //ToDo
        }
    }
}