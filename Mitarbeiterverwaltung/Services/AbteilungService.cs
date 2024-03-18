using Mitarbeiterverwaltung.DatabaseAccessObject;
using Mitarbeiterverwaltung.Objects;
using System.Windows.Controls;

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

        public ComboBox FillAbteilungDropDown(ComboBox comboBox)
        {
            List<Abteilung> abteilungen = GetAllAbteilungen();
            foreach (Abteilung abteilung in abteilungen)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = abteilung.Id + " " + abteilung.Name;
                comboBox.Items.Add(item);
            }

            return comboBox;
        }
    }
}
