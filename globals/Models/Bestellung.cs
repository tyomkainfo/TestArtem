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
        [DataMember(Name = "lfdNr")]
        public int LfdNr { get; set; }
        [DataMember(Name = "ankaeufer")]
        public string Ankaeufer { get; set; }
        [DataMember(Name = "datum")]
        public DateTime Datum { get; set; }
        [DataMember(Name = "kunde")]
        public string Kunde { get; set; }
        [DataMember(Name = "artikel")]
        public string Artikel { get; set; }
        [DataMember(Name = "vP")]
        public string VP { get; set; }
        [DataMember(Name = "anzahlpaletten")]
        public int Anzahlpaletten { get; set; }
        [DataMember(Name = "stueckJePalette")]
        public int StueckJePalette { get; set; }
        [DataMember(Name = "sesamtanzahlNetto")]
        public int GesamtanzahlNetto { get; set; }
        [DataMember(Name = "einzelpreis")]
        public double Einzelpreis { get; set; }
        [DataMember(Name = "bemerkung")]
        public string Bemerkung { get; set; }
        [DataMember(Name = "container")]
        public string Container { get; set; }
        [DataMember(Name = "besteller")]
        public string Besteller { get; set; }
        [DataMember(Name = "status")]
        public int Status { get; set; }
        [DataMember(Name = "zeit")]
        public DateTime Zeit { get; set; }
        [DataMember(Name = "Menge")]
        public string Menge { get; set; }
        [DataMember(Name = "status_bg")]
        public string Status_bg { get; set; }
        [DataMember(Name = "stkCC")]
        public int StkCC { get; set; }

    }
}
