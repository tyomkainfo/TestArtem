using globals.Models;
using nrnUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServices.WebApi
{
    public class WebApiBestellungen : WebApiBase<WebApiBestellungen>, IWebApiBestellungen
    {
        public WebApiBestellungen() : base(NrnNoLogger.Instance)
        {
            controller = getControllerName(nameof(WebApiBestellungen));
            defappserver = "https://localhost:7131/";
            defapi = "v1";
        }

        public async Task<List<Bestellung>> GetFromAPI(int lieferant, int startrow, int count)
        {
            object par = new { lieferant, startrow, count };
            List<Bestellung> tmp = await RequestGet<List<Bestellung>>("/list/" + +lieferant + "/" + startrow + "/" + count);
            return tmp;
        }
    }
}
