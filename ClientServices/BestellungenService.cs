using globals.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ClientServices.WebApi;

namespace ClientServices
{
    public class BestellungenService
    {
        private readonly IWebApiBestellungen _webApiBestellungen;
        public BestellungenService(IWebApiBestellungen webApiBestellungen)
        {
            _webApiBestellungen = webApiBestellungen;
        }
        
        public List<Bestellung> GetDemoData()
        {
            List<Bestellung> _bestellungen = new List<Bestellung>();

            _bestellungen.Add(new Bestellung
            {
                LfdNr = 382218,
                Ankaeufer = "elbers",
                Datum = DateTime.Now,
                Zeit = DateTime.Now,
                Kunde = "6",
                Artikel = "Call vulg. GG Sunset Trio 11 cm Trio",
                VP = "DN",
                Menge = "80 X 8",
                StueckJePalette = 8,
                GesamtanzahlNetto = 640,
                Einzelpreis = 1.5,
                Bemerkung = "Bemerkung für diese Bestellung",
                Container = "Tag 5",
                Besteller = "Daniel",
                Status_bg = "gedruckt",
                StkCC = 40
            });
            _bestellungen.Add(new Bestellung
            {
                LfdNr = 382219,
                Ankaeufer = "elbers",
                Datum = DateTime.Now,
                Zeit = DateTime.Now,
                Kunde = "22",
                Artikel = "* Ampelpflanzen  xyz",
                VP = "DN",
                Menge = "42 X 4",
                StueckJePalette = 4,
                GesamtanzahlNetto = 40,
                Einzelpreis = 2.5,
                Bemerkung = "Bemerkung für diese Bestellung",
                Container = "Tag 2",
                Besteller = "Daniel",
                Status_bg = "bestätigt",
                StkCC = 20
            });
            _bestellungen.Add(new Bestellung
            {
                LfdNr = 382219,
                Ankaeufer = "elbers",
                Datum = DateTime.Now,
                Zeit = DateTime.Now,
                Kunde = "22",
                Artikel = "* Ampelpflanzen  xyz",
                VP = "DN",
                Menge = "42 X 4",
                StueckJePalette = 4,
                GesamtanzahlNetto = 40,
                Einzelpreis = 2.5,
                Bemerkung = "Bemerkung für diese Bestellung",
                Container = "Tag 2",
                Besteller = "Daniel",
                Status_bg = "bestätigt",
                StkCC = 20
            });

            _bestellungen.Add(new Bestellung
            {
                LfdNr = 382220,
                Ankaeufer = "test test",
                Datum = DateTime.Now,
                Zeit = DateTime.Now,
                Kunde = "777",
                Artikel = "Item",
                VP = "DN",
                Menge = "7 X 5",
                StueckJePalette = 4,
                GesamtanzahlNetto = 40,
                Einzelpreis = 2.5,
                Bemerkung = "Bemerkung für diese Bestellung",
                Container = "Tag 10",
                Besteller = "John Doi",
                Status_bg = "neu",
                StkCC = 20
            });
            _bestellungen.Add(new Bestellung
            {
                LfdNr = 382220,
                Ankaeufer = "test test",
                Datum = DateTime.Now,
                Zeit = DateTime.Now,
                Kunde = "777",
                Artikel = "Item",
                VP = "DN",
                Menge = "7 X 5",
                StueckJePalette = 4,
                GesamtanzahlNetto = 40,
                Einzelpreis = 2.5,
                Bemerkung = "Bemerkung für diese Bestellung",
                Container = "Tag 10",
                Besteller = "John Doi",
                Status_bg = "neu",
                StkCC = 20
            });
            return _bestellungen;
        }

        public async Task<List<Bestellung>> GetFromAPI(int lieferant, int startrow, int count)
        {
            return await _webApiBestellungen.GetFromAPI(lieferant, startrow, count);
        }
    }
}
