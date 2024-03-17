using MySql.Data.MySqlClient;
using System.Windows;

namespace Mitarbeiterverwaltung
{
    public partial class LoginWindow : Window
    {

        private string username;
        private string password;
        private string connectionString;


        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {

            // Falls Benutzereingabe valide
            if (LoginCheck())
            {
                // Öffne das Hauptfenster und übergebe den ConnectionString
                MainWindow mainWindow = new MainWindow(connectionString, username);
                mainWindow.Show();

                // Schließe das Login-Fenster
                Close();
            }
            else
            {
                // Zeige eine Fehlermeldung an
                MessageBox.Show("Ungültige Anmeldeinformationen");
            }
        }
        // Überprüfe die Anmeldeinformationen und gib true oder false zurück je nachdem, ob die Anmeldeinformationen gültig sind oder nicht
        private bool LoginCheck()
        {
            username = txtbox_User.Text;
            password = PasswordBox_Passwort.Password;

            // Versuche mit den gegebenen Informationen eine Verbindung zu öffnen, falls erfolgreich -> true
            try
            {
                connectionString = $"Server=localhost;Database=mitarbeiterverwaltung;username={username};password={password};";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                connection.Close();

                return true;
            }

            // Falls Fehlschlag -> false
            catch
            {
                return false;
            }
        }
    }
}
