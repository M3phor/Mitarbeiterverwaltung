using MySql.Data.MySqlClient;
using System.Windows;

namespace Mitarbeiterverwaltung
{
    public partial class LoginWindow : Window
    {

        private string username;
        private string password;
        private string connectionString;

        /// <summary>
        /// Konstruktor der LoginWindow Klasse.
        /// Initialisiert die Benutzeroberfläche des Login-Fensters.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Ereignishandler für den Klick auf den Login-Button.
        /// Überprüft die eingegebenen Anmeldeinformationen mithilfe der LoginCheck()-Methode.
        /// Öffnet das Hauptfenster, wenn die Anmeldeinformationen gültig sind, andernfalls wird eine Fehlermeldung angezeigt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {

            // Falls Benutzereingabe valide
            if (LoginCheck())
            {
                // Öffne das Hauptfenster und übergebe ConnectionString und Username
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
        /// <summary>
        /// Methode zur Überprüfung der Gültigkeit der eingegebenen Anmeldeinformationen,
        /// indem versucht wird, eine Verbindung zur Datenbank herzustellen.
        /// </summary>
        /// <returns>Ein Bool, welcher der Gültigkeit der eingegebenen Anmeldeinformationen beschreibt</returns>
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
