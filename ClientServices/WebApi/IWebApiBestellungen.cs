using globals.Models;

namespace ClientServices.WebApi;

public interface IWebApiBestellungen
{
    Task<List<Bestellung>> GetFromAPI(int lieferant, int startrow, int count);
}