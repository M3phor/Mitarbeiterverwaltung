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
        private string connectionString = "Server=localhost;Database=mitarbeiterverwaltung;username=root;password=;";

        public MainWindow()
        {
            InitializeComponent();
            mitarbeiterService = new MitarbeiterService(connectionString);
            List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();
            MainDataGrid.ItemsSource = mitarbeiterList;

        }




        private void Button_ShowAllMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();
            MainDataGrid.ItemsSource = mitarbeiterList;
        }

        private void Button_Window_AddMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            AddMitarbeiterWindow addMitarbeiterWindow = new AddMitarbeiterWindow();
            addMitarbeiterWindow.Owner = this;
            addMitarbeiterWindow.ShowDialog();
        }

    }
}