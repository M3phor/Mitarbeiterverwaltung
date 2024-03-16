using Mitarbeiterverwaltung.DatabaseAccessObject;
using Mitarbeiterverwaltung.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Services
{
    public class AbteilungService(string connectionString)
    {
        private readonly DAOConnector databaseObject = new DAOConnector(connectionString);

        public List<Abteilung> GetAllAbteilungen()
        {
            return databaseObject.GetAllAbteilungen();
        }

        public Abteilung GetAbteilungById(int id) 
        {
            return databaseObject.GetAbteilungById(id);
        }
    }
}
