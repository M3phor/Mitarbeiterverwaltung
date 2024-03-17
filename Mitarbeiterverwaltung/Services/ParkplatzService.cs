using Mitarbeiterverwaltung.DatabaseAccessObject;
using Mitarbeiterverwaltung.Objects;

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
