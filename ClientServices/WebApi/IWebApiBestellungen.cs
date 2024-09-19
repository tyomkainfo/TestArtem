using globals.Models;

namespace ClientServices.WebApi;

public interface IWebApiBestellungen
{
    List<Bestellung> GetFromAPI(int lieferant, int startrow, int count);
}