using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrnUtil
{
    public class DateFromTo
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public DateFromTo()
        {
        }
        public DateFromTo(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
        public void SetToVormonat()
        {
            DateTime tmp = DateTime.Today.AddMonths(-1);
            From = new DateTime(tmp.Year, tmp.Month, 1);
            To = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
        }
        public void SetToVormonatOf(int month,int year)
        {
            DateTime tmp = new DateTime(year, month, 1).AddMonths(-1);
            From = new DateTime(tmp.Year, tmp.Month, 1);
            To = From.AddMonths(1).AddDays(-1);
        }
    }
}
