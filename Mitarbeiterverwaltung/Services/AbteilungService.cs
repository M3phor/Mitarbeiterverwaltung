using Mitarbeiterverwaltung.DatabaseAccessObject;
using Mitarbeiterverwaltung.Objects;

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
