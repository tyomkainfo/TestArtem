using globals.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FloritradeApi.Controllers
{
    [ApiController]
    public class BestellungenController : ControllerBase
    {
        [HttpGet]
        [Route("v1/Bestellungen/list/{supplier}/{startrow}/{count}")]
        public virtual List<Bestellung> GetBestellungenList(int supplier, int startrow, int count)
        {
           
            List<Bestellung> res = new List<Bestellung>();
            Bestellung tmp = new Bestellung();
            tmp.LfdNr = 1;
            tmp.Ankaeufer = "Ankaeufer";
            tmp.Datum = DateTime.Now;
            tmp.Kunde = "6";
            tmp.Artikel = "Calluna BL 12cm Rot";
            tmp.VP = "DN";
            tmp.Anzahlpaletten = 80;
            tmp.StueckJePalette = 8;
            tmp.GesamtanzahlNetto = 640;
            tmp.Einzelpreis = 1.5;
            tmp.Bemerkung = "Bemerkung für diese Bestellung";
            tmp.Container = "Tag 5";
            tmp.Besteller = "Daniel";
            tmp.Status = 1;
            tmp.StkCC = 40;
            res.Add(tmp);

            /*
           try
           {
               dc_clList<dc_clArtikelReservierung> aal = _clReservierung.GetArtikelReservierungListShop(startrow, count, 1);
               foreach (var aa in aal.lst)
               {
                   int fk_kunde_lfd_nr = _clReservierung.GetReservierungById(aa.Fk_reservierung_lfd_nr, 1).Fk_p_lfd_nr;
                   Product tmp = _productHelper.GetProductById(aa.Fk_anbaumeldung_id, false,
                       Request.HttpContext.Connection.RemoteIpAddress.ToString(), false);
                   res.Add(new ReservationPosition(aa, tmp, fk_kunde_lfd_nr));
               }
           }
           catch (Exception ex)
           {

           }
           */
            return res;
        }
    }
}
