using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiterverwaltung.Objects
{
    public class MitarbeiterGesamt
    {
        public int Personalnummer { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtstag { get; set; }
        public int? Abteilung { get; set; }
        public string Abteilungsname { get; set; }
        public int Kostenstelle { get; set; }
        public int? ParkplatzNr { get; set; }
        public bool Schatten { get; set; }
        public int? Stockwerk { get; set; }
    }
}
