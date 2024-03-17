namespace Mitarbeiterverwaltung.Objects
{
    public class Mitarbeiter
    {
        public int Personalnummer { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtstag { get; set; }
        public int Abteilung { get; set; }
        public int? ParkplatzNr { get; set; }

        public Mitarbeiter()
        {
            this.Vorname = "";
            this.Nachname = "";
            this.Geburtstag = new DateTime();
            this.Abteilung = 0;
            this.ParkplatzNr = null;
        }
    }
}
