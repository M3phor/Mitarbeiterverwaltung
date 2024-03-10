using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Objects
{
    public class Mitarbeiter
    {
        public int Personalnummer { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtstag { get; set; }
        public int? Abteilung { get; set; }
        public int? ParkplatzNr { get; set; }

        public Mitarbeiter() {
            this.Vorname = null;
            this.Nachname = null;
            this.Geburtstag = new DateTime();
            this.Abteilung = null;
            this.ParkplatzNr = null;
        }


        public Mitarbeiter(string Vorname, string Nachname, DateTime Geburtstag, int Abteilung, int? ParkplatzNr)
        {
            this.Vorname = Vorname;
            this.Nachname = Nachname;
            this.Geburtstag = Geburtstag;
            this.Abteilung = Abteilung;
            this.ParkplatzNr = ParkplatzNr;
        }

    }

    

}
