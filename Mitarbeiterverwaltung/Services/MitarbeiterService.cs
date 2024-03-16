using Mitarbeiterverwaltung.DatabaseAccessObject;
using Mitarbeiterverwaltung.Objects;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Services
{
    public class MitarbeiterService(string connectionString)
    {
        private readonly DAOConnector databaseObject = new DAOConnector(connectionString);

        // Service Funktion: Alle Mitarbeiter anzeigen
        public List<Mitarbeiter> GetAllMitarbeiter()
        {
            return databaseObject.GetAllMitarbeiter();
        }

        // Service Funktion: Einzelnen Mitarbeiter anzeigen
        public Mitarbeiter GetMitarbeiterById(int id)
        {
            return databaseObject.GetMitarbeiterById(id);
        }

        // Service Funktion: Mitarbeiter löschen
        public void DelMitarbeiterById(int id) 
        {
            databaseObject.DelMitarbeiterById(id);
        }

        //Service Funktion: Mitarbeiter hinzufügen
        public void AddMitarbeiter(Mitarbeiter mitarbeiter)
        {
            databaseObject.AddMitarbeiter(mitarbeiter);
        }

        // Service Funktion: Mitarbeiter exportieren
        public void ExportMitarbeiter() 
        {
            databaseObject.ExportMitarbeiter();
        }

        // Service Funktion: Mitarbeiter bearbeiten
        public void EditMitarbeiter(Mitarbeiter mitarbeiter)
        {
            databaseObject.EditMitarbeiter(mitarbeiter);
        }

    }

}
