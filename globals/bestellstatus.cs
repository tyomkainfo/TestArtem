using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace globals
{
    public class Bestellstatus
    {
        public const int alle = -1;
        public const int neu = 0;
        public const int gesehen = 1;
        public const int bestaetigt = 2;
        public const int gedruckt = 4;

        public static string GetAsString(int status)
        {
            switch (status)
            {
                case 0:
                    return "Neu";
                case 1:
                    return "Gesehen";
                case 2:
                    return "Bestätigt";
                case 4:
                    return "Gedruckt";
                default:
                    return "Unbekannt";
            }
        }
    }
}
