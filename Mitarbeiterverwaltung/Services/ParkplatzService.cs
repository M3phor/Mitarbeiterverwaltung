using Mitarbeiterverwaltung.DatabaseAccessObject;
using Mitarbeiterverwaltung.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Services
{
    public class ParkplatzService(string connectionString)
    {
        private readonly DAOConnector databaseObject = new DAOConnector(connectionString);

        public List<Parkplatz> GetAllParkplaetze()
        {
            return databaseObject.GetAllParkplaetze();
        }

        public Parkplatz GetParkplatzById(int? id)
        {
            return databaseObject.GetParkplatzById(id);
        }
    }
}
