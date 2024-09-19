using System;
using System.Collections.Generic;
using System.Text;

namespace nrnUtil
{
    public enum BarcodeType
    {
        None,
        EAN,
        Blucom,
        Pruefliste,
        Packliste,
        WALieferschein,
        WARechnung,
        WAEtikett,
        Spezialfunktion
    }
    public enum BlucomDataKind
    {
        MandantKZ,
        Lapnr,
        ccNr,
        Pal,
        Ges
    }

    public class BarcodeUtils
    {
        public const int START_PRUEF = 0;
        public const int END_PRUEF = 9999999;

        public const int START_PACK = 80000000;
        public const int END_PACK = 89999999;

        public const int START_SPEZ = 99990000;
        public const int END_SPEZ = 99999999;

        public const Int64 START_WA = 8013370000;

        public const int WA_KZ = 801337;

        public const int ETIKETT_KZ = 4011;
        public const int LIEFERSCHEIN_KZ = 4012;
        public const int RECHNUNG_KZ = 6015;
        public const int LAP_KZ = 4013;

        public const int BlucomMandantGmbh = 7316;
        public const int BlucomMandantExport = 7317;


        public static int GetBlucomBarcodeData(BlucomDataKind blucomDataKind, string barcode)
        {
            string substringBegin = string.Empty;
            switch (blucomDataKind)
            {
                case BlucomDataKind.MandantKZ:
                     substringBegin = barcode.Substring(0,4);
                    if (Int32.TryParse(substringBegin, out int mandantKz))
                        return mandantKz;
                    break;
                case BlucomDataKind.ccNr:
                    substringBegin = barcode.Substring(4, 10);
                    if (Int32.TryParse(substringBegin, out int ccNr))
                        return ccNr;
                    break;
                case BlucomDataKind.Lapnr:
                     substringBegin = barcode.Substring(14, 8);
                    if (Int32.TryParse(substringBegin, out int Lapnr))
                        return Lapnr;
                    break;
                case BlucomDataKind.Pal:
                    substringBegin = barcode.Substring(22, 5);
                    if (Int32.TryParse(substringBegin, out int Pal))
                        return Pal;
                    break;
                case BlucomDataKind.Ges:
                    substringBegin = barcode.Substring(27, 8);
                    if (Int32.TryParse(substringBegin, out int Ges))
                        return Ges;
                    break;
            }
            return -1;
        }

        public static BarcodeType GetBarcodeType(string Barcode)
        {
            if (!string.IsNullOrWhiteSpace(Barcode))
            {
                if (Barcode.Length == 13)
                    return BarcodeType.EAN;
                if (Barcode.Length == 35)
                    return BarcodeType.Blucom;
                if (Int64.TryParse(Barcode, out Int64 barcodeInt))
                {
                    if (barcodeInt >= BarcodeUtils.START_PRUEF && barcodeInt <= BarcodeUtils.END_PRUEF)
                    {
                        return BarcodeType.Pruefliste;
                    }
                    else if (barcodeInt >= BarcodeUtils.START_PACK && barcodeInt <= BarcodeUtils.END_PACK)
                    {
                        return BarcodeType.Packliste;
                    }
                    else if (barcodeInt >= BarcodeUtils.START_SPEZ && barcodeInt <= BarcodeUtils.END_SPEZ)
                    {
                        return BarcodeType.Spezialfunktion;
                    }
                    else if (barcodeInt >= BarcodeUtils.START_WA)
                    {
                        string substringEnd = Barcode.Substring(Barcode.Length - 4);
                        if (Int32.TryParse(substringEnd, out int numberKind))
                        {
                            if (numberKind == BarcodeUtils.ETIKETT_KZ)
                            {
                                return BarcodeType.WAEtikett;
                            }
                            else if (numberKind == BarcodeUtils.LIEFERSCHEIN_KZ)
                            {
                                return BarcodeType.WALieferschein;
                            }
                            else if (numberKind == BarcodeUtils.RECHNUNG_KZ)
                            {
                                return BarcodeType.WARechnung;
                            }
                        }
                    }
                }
            }
            return BarcodeType.None;
        }


        public static int GetWAABarcodeKind(string barcode)
        {
            if (IsWAA(barcode))
            {
                string substringEnd = barcode.Substring(barcode.Length - ETIKETT_KZ.ToString().Length);
                if (Int32.TryParse(substringEnd, out int kz))
                {
                    return kz;
                }
            }
            return -1;
        }
        public static bool IsPruefliste(string barcode)
        {
            if (!string.IsNullOrEmpty(barcode) && barcode.Length == START_PRUEF.ToString().Length)
            {
                if (Int32.TryParse(barcode, out int barcodeInt))
                {
                    if (barcodeInt >= START_PRUEF && barcodeInt <= END_PRUEF)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static xbool IsPackliste(string barcode)
        {
            if (!string.IsNullOrEmpty(barcode) && barcode.Length == START_PACK.ToString().Length)
            {
                if (Int32.TryParse(barcode, out int barcodeInt))
                {
                    if (barcodeInt >= START_PACK && barcodeInt <= END_PACK)
                    {
                        return true;
                    }
                }
                new xbool("Ungültiger Barcode");
            }
            return new xbool("Leerer Barcode");
        }
        public static xbool IsWAA(string barcode)
        {
            return (BarcodeUtils.GetBarcodeType(barcode) == BarcodeType.WAEtikett ||
                BarcodeUtils.GetBarcodeType(barcode) == BarcodeType.WALieferschein ||
                BarcodeUtils.GetBarcodeType(barcode) == BarcodeType.WARechnung ||
                BarcodeUtils.GetBarcodeType(barcode) == BarcodeType.Blucom);
        }
        public static int GetEtikettId(string barcode)
        {
            if (IsWAA(barcode))
            {
                string et = barcode.Substring(WA_KZ.ToString().Length, barcode.Length - (WA_KZ.ToString().Length + ETIKETT_KZ.ToString().Length));
                if (!string.IsNullOrWhiteSpace(et))
                {
                    if (Int32.TryParse(et, out int etikettID))
                    {
                        return etikettID;
                    }
                }
            }
            return -1;
        }
        public static int GetLieferscheinId(string barcode)
        {
            if (IsWAA(barcode))
            {
                string et = barcode.Substring(WA_KZ.ToString().Length, barcode.Length - (WA_KZ.ToString().Length + LIEFERSCHEIN_KZ.ToString().Length));
                if (!string.IsNullOrWhiteSpace(et))
                {
                    if (Int32.TryParse(et, out int lieferscheinID))
                    {
                        return lieferscheinID;
                    }
                }
            }
            return -1;
        }
        public static int GetRechungsId(string barcode)
        {
            return BarcodeUtils.GetLieferscheinId(barcode);
        }
        public static string GetBarcodeByLieferscheinID(int Lieferschien_ID)
        {
            return WA_KZ.ToString() + Lieferschien_ID + LIEFERSCHEIN_KZ.ToString();
        }





        public static bool EAN_isValid(string EAN)
        {
            string strEAN = EAN.ToString();
            if (strEAN.Length == 13)
            {
                int Multiplikator = 1;
                int checknumber = 0;
                int number;
                for (int i = 0; i < 12; i++)
                {
                    if (Int32.TryParse(strEAN.Substring(i, 1), out number))
                    {
                        checknumber += ((number * Multiplikator) % 10);
                        Multiplikator = (Multiplikator == 1 ? 3 : 1);
                    }
                }
                int prufziffer = -1;
                int ergebnis = (10 - (checknumber % 10));

                //neu
                if (ergebnis == 10)
                    ergebnis = 0;
                //

                if (Int32.TryParse(strEAN.Substring(strEAN.Length - 1), out prufziffer))
                {
                    return (ergebnis == prufziffer);
                }
            }
            return false;
        }
    }
}
