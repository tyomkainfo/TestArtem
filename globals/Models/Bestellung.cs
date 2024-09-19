using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace globals.Models
{
    [DataContract]
    public class Bestellung

    {
        [DataMember(Name = "LfdNr")]
        public int LfdNr { get; set; }
        [DataMember(Name = "Ankaeufer")]
        public string Ankaeufer { get; set; }
        [DataMember(Name = "Datum")]
        public DateTime Datum { get; set; }
        [DataMember(Name = "Kunde")]
        public string Kunde { get; set; }
        [DataMember(Name = "Artikel")]
        public string Artikel { get; set; }
        [DataMember(Name = "VP")]
        public string VP { get; set; }
        [DataMember(Name = "Anzahlpaletten")]
        public int Anzahlpaletten { get; set; }
        [DataMember(Name = "StueckJePalette")]
        public int StueckJePalette { get; set; }
        [DataMember(Name = "GesamtanzahlNetto")]
        public int GesamtanzahlNetto { get; set; }
        [DataMember(Name = "Einzelpreis")]
        public double Einzelpreis { get; set; }
        [DataMember(Name = "Bemerkung")]
        public string Bemerkung { get; set; }
        [DataMember(Name = "Container")]
        public string Container { get; set; }
        [DataMember(Name = "Besteller")]
        public string Besteller { get; set; }
        [DataMember(Name = "Status")]
        public int Status { get; set; }
        [DataMember(Name = "StkCC")]
        public DateTime Zeit { get; set; }
        public string Menge { get; set; }
        [DataMember(Name = "Menge")]
        public int StkCC { get; set; }
    }
}
