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
    public partial class DelMitarbeiterWindow : Window
    {
        private MitarbeiterService mitarbeiterService;
        
        public DelMitarbeiterWindow(MitarbeiterService mitarbeiterService)
        {
            InitializeComponent();
            this.mitarbeiterService = mitarbeiterService;
        }

        private void Button_DelMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            // Versuche Eingabe als Integer zu parsen
            if (int.TryParse(txtbox_DelMitarbeiter.Text, out int parsedPersonalNr)) 
            {
                List<Mitarbeiter> mitarbeiterList = mitarbeiterService.GetAllMitarbeiter();
                bool mitarbeiterGefunden = false;

                // Vergleiche Eingabe mit existierenden Mitarbeitern
                foreach(Mitarbeiter mitarbeiter in mitarbeiterList)
                {
                    if(mitarbeiter.Personalnummer == parsedPersonalNr)
                    {
                        mitarbeiterGefunden = true;
                    }
                }
                if (mitarbeiterGefunden )
                {
                    // ToDo: SICHERUNG

                    mitarbeiterService.DelMitarbeiterById(parsedPersonalNr);
                    MessageBox.Show($"Mitarbeiter mit Personalnummer {parsedPersonalNr} erfolgreich gelöscht!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Kein Mitarbeiter mit Personalnummer {parsedPersonalNr} gefunden!");
                }
                // Lösche Mitarbeiter mit übergebener Personalnummer
            }
            // Fehlermeldung
            else
            {
                MessageBox.Show($"Ungültige Eingabe");
            }   
        }
    }
}
