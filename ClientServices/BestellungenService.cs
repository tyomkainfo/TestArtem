using globals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServices
{
    public class BestellungenService
    {
        public List<Bestellung> GetDemoData()
        {
            List<Bestellung> _bestellungen = new List<Bestellung>();
            Bestellung tmp = new Bestellung();
            tmp.LfdNr = 382218;
            tmp.Ankaeufer = "elbers";
            tmp.Datum = DateTime.Now;
            tmp.Zeit = DateTime.Now;
            tmp.Kunde = "6";
            tmp.Artikel = "Call vulg. GG Sunset Trio  11 cm Trio";
            tmp.VP = "DN";
            tmp.Menge = "80 X 8";
            tmp.StueckJePalette = 8;
            tmp.GesamtanzahlNetto = 640;
            tmp.Einzelpreis = 1.5;
            tmp.Bemerkung = "Bemerkung für diese Bestellung";
            tmp.Container = "Tag 5";
            tmp.Besteller = "Daniel";
            tmp.Status = 1;
            tmp.StkCC = 40;
            _bestellungen.Add(tmp);
            return _bestellungen;
        }
    }
}
