﻿using Mitarbeiterverwaltung.DatabaseAccessObject;
using Mitarbeiterverwaltung.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Services
{
    public class MitarbeiterService
    {
        private readonly DAOConnector databaseObject;

        public MitarbeiterService(string connectionString)
        {
            databaseObject = new DAOConnector(connectionString);
        }

        // Service Funktion: Alle Mitarbeiter anzeigen
        public List<Mitarbeiter> GetAllMitarbeiter()
        {
            return databaseObject.GetAllMitarbeiter();
        }

        // Service Funktion: Einzelnen Mitarbeiter anzeigen
        public Mitarbeiter GetMitarbeiter(int id)
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


    }

}