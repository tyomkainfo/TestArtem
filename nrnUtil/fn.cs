using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace nrnUtil
{
    public class fn
    {
        static string[] Monatsname = new string[] {"Januar", "Februar", "März", "April", "Mai", "Juni",
            "Juli", "August", "September", "Oktober", "November", "Dezember" };

        public static byte[] TrimTailingZeros(byte[] arr)
        {
            if (arr == null || arr.Length == 0)
                return arr;
            return arr.Reverse().SkipWhile(x => x == 0).Reverse().ToArray();
        }

        public delegate void WebApiProblemHandler(WebApiProblemMessage msg);

        
        public static bool IsInDesignMode()
        {
            return (GetMyEntryAssembly() == null);
            //bool bModeCheck = (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);
        }
        
        public static Assembly GetMyEntryAssembly()
        {
            Assembly result = null;
            try
            {
                //if ((System.Web.HttpContext.Current == null) || (System.Web.HttpContext.Current.Handler == null))
                return Assembly.GetEntryAssembly(); // Not a web application
                //return System.Web.HttpContext.Current.Handler.GetType().BaseType.Assembly;
            }
            catch
            {

            }
            return result;
        }

        public static string formatDatumIso(DateTime value)
        {
            if (value != null)
                return value.Year + "-" + ((value.Month < 10) ? "0" : "") + value.Month + "-" + ((value.Day < 10) ? "0" : "") + value.Day;
            else
                return "";
        }


        public static string isodate()
        {
            DateTime tmp = DateTime.Now;
            string res = Convert.ToString(tmp.Year) + "-"
                                                    + ((tmp.Month < 10) ? "0" : "") + Convert.ToString(tmp.Month) + "-"
                                                    + ((tmp.Day < 10) ? "0" : "") + Convert.ToString(tmp.Day) + "T"
                                                    + ((tmp.Hour < 10) ? "0" : "") + Convert.ToString(tmp.Hour) + ":"
                                                    + ((tmp.Minute < 10) ? "0" : "") + Convert.ToString(tmp.Minute) + ":"
                                                    + ((tmp.Second < 10) ? "0" : "") + Convert.ToString(tmp.Second) + "Z";
            return res;
        }
        
        public static string SepaID()
        {
            DateTime tmp = DateTime.Now;
            string res = "EFLSEP" + Convert.ToString(tmp.Year % 2000) + Convert.ToString(tmp.Month) + Convert.ToString(tmp.Day)
                         + Convert.ToString(tmp.Hour) + Convert.ToString(tmp.Minute) + Convert.ToString(tmp.Second);
            return res;
        }

        public static string checkUstID(string UstID)
        {
            if (string.IsNullOrEmpty(UstID) || string.IsNullOrWhiteSpace(UstID))
                return "zu wenig Zeichen";
            if (string.IsNullOrEmpty(UstID) || string.IsNullOrWhiteSpace(UstID))
                return "zu wenig Zeichen";
            if (!fn.IsAlphaNum(UstID))
                return "nur Buchstaben und Ziffern erlaubt";
            string land = UstID.Substring(0, 2);
            if (fn.IsNumeric(land.Substring(0, 1)) || (fn.IsNumeric(land.Substring(1, 1))))
                return "2-Buchstabige Länderkennung erforderlich!";
            UstID = UstID.Substring(2, UstID.Length - 2);
            if (land == "BE")
            {
                if (UstID.Length != 10)
                    return "muss 10 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "DK")
            {
                if (UstID.Length != 8)
                    return "muss 8 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "DE")
            {
                if (UstID.Length != 9)
                    return "muss 9 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "EE")
            {
                if (UstID.Length != 9)
                    return "muss 9 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "FI")
            {
                if ((UstID.Length != 8) && (UstID.Length != 9))
                    return "muss 8 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "FR")
            {
                if (UstID.Length != 11)
                    return "muss 11 Ziffern haben";
                if (!(fn.IsNumeric(UstID.Substring(3, UstID.Length - 3))))
                    return "ab der dritten Stelle nur Ziffern erlaubt";
            }
            else if (land == "EL")
            {
                if (UstID.Length != 9)
                    return "muss 9 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "IE")
            {
                if (UstID.Length != 8)
                    return "muss 8 Ziffern haben";
                if ((!(fn.IsNumeric(UstID.Substring(0, 1))) || (!(fn.IsNumeric(UstID.Substring(3, 4))) || ((fn.IsNumeric(UstID.Substring(UstID.Length - 2, 1)))))))
                    return "die zweite Stelle kann und die letzte Stelle muss ein Buchstabe sein!";
            }
            else if (land == "IT")
            {
                if (UstID.Length != 11)
                    return "muss 11 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "LV")
            {
                if (UstID.Length != 11)
                    return "muss 11 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "LT")
            {
                if ((UstID.Length != 9) && (UstID.Length != 12))
                    return "muss 9 oder 12 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "LU")
            {
                if (UstID.Length != 8)
                    return "muss 8 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "MT")
            {
                if (UstID.Length != 8)
                    return "muss 8 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "NL")
            {
                if (UstID.Length != 12)
                    return "muss 12 Stellen haben";
                if (!((fn.IsNumeric(UstID.Substring(0, 9)))))
                    return "die ersten 9 Stellen müssen numerisch sein!";
                if (!(fn.IsNumeric(UstID.Substring(UstID.Length - 2, 2))))
                    return "die letzten beiden Stellen müssen numerisch sein!";
                if ((UstID.Substring(9, 1) != "B"))
                    return "die drittletzte Stelle muss der Buchstabe 'B' sein";
            }
            else if (land == "AT")
            {
                if (UstID.Length != 9)
                    return "muss 9 Stellen haben";
                if (UstID.Substring(0, 1) != "U")
                    return "die erste Stelle muss der Buchstabe 'U' sein!";
            }
            else if (land == "PL")
            {
                if (UstID.Length != 10)
                    return "muss 10 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "PT")
            {
                if (UstID.Length != 9)
                    return "muss 9 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "SE")
            {
                if (UstID.Length != 12)
                    return "muss 12 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
                if (UstID.Substring(UstID.Length - 2, 2) != "01")
                    return "die beiden letzen Stellen bestehen immer aus der Ziffernkombination '01'";
            }
            else if (land == "SK")
            {
                if (UstID.Length != 10)
                    return "muss 10 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "SI")
            {
                if (UstID.Length != 8)
                    return "muss 8 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "ES")
            {
                if (UstID.Length != 9)
                    return "muss 9 Ziffern haben";
                if (!(fn.IsNumeric(UstID.Substring(2, 7))))
                    return "Buchstaben nur an der ersten und letzten Stelle erlaubt!";
            }
            else if (land == "CZ")
            {
                if ((UstID.Length < 8) || (UstID.Length > 10))
                    return "muss 8-10 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "HU")
            {
                if (UstID.Length != 8)
                    return "muss 8 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
            }
            else if (land == "GB")
            {
                if ((UstID.Length != 9) && (UstID.Length != 12))
                    return "muss 9 oder 12 Ziffern haben";
                if (!(fn.IsNumeric(UstID)))
                    return "nur Ziffern erlaubt";
                //für Verwaltungen und Gesundheitswesen: fünf, die ersten zwei Stellen GD oder HA
            }
            else if (land == "SM")
            {
                if ((UstID.Length != 5))
                    return "muss 5 Ziffern haben";
            }
            else if (land == "CY")
            {
                if (UstID.Length != 9)
                    return "muss 9 Ziffern haben";
                if ((fn.IsNumeric(UstID.Substring(UstID.Length - 1, 1))))
                    return "die letzte Stelle muss ein Buchstabe sein!";
            }
            else if (land == "HR")
            {
                if ((UstID.Length != 11))
                    return "muss 11 Ziffern haben";
            }
            else
                return "unbekannte Länderkennung!";
            return "";
        }

        private static bool IsAlphaNum(string ustID)
        {
            int i;
            for (i = 0; i < ustID.Length; i++)
                if (!(((ustID[i] >= '0') && (ustID[i] <= '9')) || ((ustID[i] >= 'A') && (ustID[i] <= 'Z')))) return false;
            return true;
        }
        public static string HttpGet(string url, string credentials = "",bool ignoressl = false)
        {
            WebClient client = new WebClient();
            RemoteCertificateValidationCallback? tmpcallback = null;
            if (ignoressl)
            {
                try
                {
                    tmpcallback = ServicePointManager.ServerCertificateValidationCallback;
                    //Change SSL checks so that all checks pass
                    ServicePointManager.ServerCertificateValidationCallback =
                        new RemoteCertificateValidationCallback(
                            delegate { return true; }
                        );
                }
                catch (Exception ex)
                {

                }
            }
            // Add a user agent header in case the 
            // requested URI contains a query.
            if (credentials != "")
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            if (ignoressl)
            {
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = tmpcallback;
                }
                catch (Exception ex)
                {

                }
            }

            return s;
        }
        public static string CheckUstQX(string srcId, string trgId, string Firma, string Ort, string Plz, string Str, string dummy)
        {
            string url = "https://evatr.bff-online.de/evatrRPC?UstId_1=" + srcId + "&UstId_2=" + trgId + "&Firmenname=" + Firma + "&Ort=" + Ort + "&PLZ=" + Plz + "&" + Str + "&Druck=nein";
            string tmp = HttpGet(url);



            return tmp;
        }
        public static int CheckUstQ(string srcId, string trgId, string Firma, string Ort, string Plz, string Str, string dummy)
        {
            string url = "https://evatr.bff-online.de/evatrRPC?UstId_1=" + srcId + "&UstId_2=" + trgId + "&Firmenname=" + Firma + "&Ort=" + Ort + "&PLZ=" + Plz + "&" + Str + "&Druck=nein";
            string tmp = HttpGet(url);

            string ErrorCode = ValueFromUstXml(tmp, "ErrorCode");
            /*
            string Datum = ValueFromUstXml(tmp, "Datum");
            string Uhrzeit = ValueFromUstXml(tmp, "Uhrzeit");
            string Gueltig_ab = ValueFromUstXml(tmp, "Gueltig_ab");
            string Gueltig_bis = ValueFromUstXml(tmp, "Gueltig_bis");
            */
            return Convert.ToInt32(ErrorCode);
        }


        public static string ValueFromUstXml(string xml, string name)
        {
            int p1 = xml.IndexOf(name);
            if (p1 < 0) return "";
            int p2 = xml.IndexOf("<string>", p1) + 8;
            int p3 = xml.IndexOf("</string>", p2);
            if ((p2 < 0) || (p3 < 0)) return "";
            return xml.Substring(p2, p3 - p2);
        }
        public static int BGRun(string cmdLine)
        {
            string exe = cmdLine.Substring(0, cmdLine.IndexOf(" "));
            string par = cmdLine.Substring(cmdLine.IndexOf(" ") + 1, cmdLine.Length - cmdLine.IndexOf(" ") - 1);
            //var handle = Process.GetCurrentProcess().MainWindowHandle;
            Process tmp = Process.Start(exe, par);
            tmp.WaitForExit();
            return tmp.ExitCode;
        }

        public static int BGRunNoParams(string cmdLine)
        {
            //var handle = Process.GetCurrentProcess().MainWindowHandle;
            Process tmp = Process.Start(cmdLine);
            tmp.WaitForExit();
            return tmp.ExitCode;
        }

        public static int spar(string nummer)
        {
            if (IsNumeric(nummer))
                return Convert.ToInt32(nummer);
            else
                return -1;
        }

        public static string formatLong(double value)
        {
            int tmp = Convert.ToInt32(value);
            return Convert.ToString(tmp);
        }
        public static string formatLong(object value)
        {
            int tmp = fn.nint(value);
            return Convert.ToString(tmp);
        }
        public static string formatDoubleTs(double value, int stellen = 2)
        {
            string tmp = Math.Round(value, stellen, MidpointRounding.AwayFromZero).ToString($"F{stellen}").Replace(".", ",");
            return tmp;//{0:c}
        }

        public static string formatCurrency(double value)
        {
            string tmp = Math.Round(value, 2, MidpointRounding.AwayFromZero).ToString($"C");
            return tmp;
        }

        public static string formatDouble(double value, int stellen = 2)
        {
            double tmpRound = Math.Round(value, stellen, MidpointRounding.AwayFromZero);
            return tmpRound.ToString($"F{stellen}").Replace(".", ",");
        }
        public static string formatDecimal(decimal value, int stellen = 2)
        {
            decimal tmpRound = Decimal.Round(value, stellen, MidpointRounding.AwayFromZero);
            return tmpRound.ToString($"F{stellen}").Replace(".", ",");
        }
        public static string formatDouble(object value, int stellen = 2)
        {
            double v2 = fn.ndbl(value);
            return Math.Round(v2, stellen, MidpointRounding.AwayFromZero).ToString().Replace(".", ",");
        }
        public static int FGRun(string cmdLine)
        {
            //var handle = Process.GetCurrentProcess().MainWindowHandle;
            Process tmp = Process.Start(cmdLine);
            tmp.WaitForExit();
            return tmp.ExitCode;
        }
        public static int FGRun(string directory,string cmdLine,string arguments)
        {
            cmdLine = Path.Combine(directory, cmdLine);
            ProcessStartInfo _processStartInfo = new ProcessStartInfo();
            _processStartInfo.WorkingDirectory = directory;
            _processStartInfo.FileName = cmdLine;
            _processStartInfo.Arguments = arguments;
            Process myProcess = Process.Start(_processStartInfo);
            myProcess.WaitForExit();
            return myProcess.ExitCode;
        }
        public static string str_crop(string value, int length)
        {
            if (value.Length > length) value = fn.Left(value, length);
            return value;
        }
        public static string str_rfill(string value, int length)
        {
            while (value.Length < length) value += " ";
            if (value.Length > length) value = fn.Left(value, length);
            return value;
        }

        public static string str_lfill(string value, int length)
        {
            while (value.Length < length) value = " " + value;
            if (value.Length > length) value = fn.Right(value, length);
            return value;
        }

        public static string formatDatumSepa(DateTime value)
        {
            string result = Convert.ToString(value.Year) + "-" + Convert.ToString(value.Month) + "-" + Convert.ToString(value.Day);
            return result;
        }
        public static string formatDatumSepa2(DateTime value)
        {
            string result = Convert.ToString(value.Year) + "-"
                                                         + ((value.Month < 10) ? "0" : "")
                                                         + Convert.ToString(value.Month)
                                                         + "-" + ((value.Day < 10) ? "0" : "")
                                                         + Convert.ToString(value.Day);
            return result;
        }
        public static string xmlstr(string value)
        {
            return value.Replace("&", "&amp;");
        }
        public static string onlyalphanum(string value)
        {
            value = value.Replace(" ", "");
            value = value.Replace("-", "");
            value = value.Replace("/", "");
            value = value.Replace(".", "");
            value = value.Replace("#", "");
            value = value.Replace("'", "");
            value = value.Replace("´", "");
            value = value.Replace("\n", "");
            value = value.Replace("\r", "");
            return value;
        }

        public static string removespec(string value)
        {
            value = value.Replace("#", " ");
            value = value.Replace("'", " ");
            value = value.Replace("´", " ");
            value = value.Replace("\n", "");
            value = value.Replace("\r", "");
            return value;
        }

        public static int b2i(bool value)
        {
            return (value) ? 1 : 0;
        }

        public static string UmlautErsatz(string text)
        {
            text = text.Replace("ü", "ue");
            text = text.Replace("Ü", "UE");
            text = text.Replace("ä", "ae");
            text = text.Replace("Ä", "AE");
            text = text.Replace("ö", "oe");
            text = text.Replace("Ö", "OE");
            text = text.Replace("ß", "ss");
            text = text.Replace("é", "e");
            return text;
        }

        public static void dbv(DbDataReader rd, string name, IntSetter setter)
        {
            setter(nint(rd[name]));
        }
        public static void dbv(DbDataReader rd, string name, StrSetter setter)
        {
            setter(str(rd[name]));
        }

        public static void dbv(DbDataReader rd, string name, DblSetter setter)
        {
            setter(ndbl(rd[name]));
        }

        public static string RemoveTrailingZeros(string v)
        {
            while ((v.Length > 0) && (v[0] == '0'))
                v = v.Substring(1);
            return v;
        }

        public static void dbd(DbDataReader rd, string name, DblSetter setter)
        {
            setter(ndbl(rd[name]));
        }
        public static void dbv(DbDataReader rd, string name, out DateTime result)
        {
            result = ndate(rd[name]);
        }

        public static void dbv(DbDataReader rd, string name, DtmSetter setter)
        {
            setter(ndate(rd[name]));
        }

        public static string str(object value)
        {
            if (value == null)
                return "";
            else if (value.GetType().Name == "DBNull")
            {
                return "";
            }
            else
                return Convert.ToString(value);
        }

        public static int nint(object value)
        {
            if (value == null)
                return 0;
            try
            {
                if (value.GetType().Name == "DBNull")
                {
                    return 0;
                }
                else
                    return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }

        }

        public static int getfirstnumericvalueinstring(string tmp)
        {
            try
            {
                string ex2 = "0";
                int cnt = 0;
                int i = tmp.IndexOf(",", 0);
                int j = 0;
                while (i > -1)
                {
                    cnt++;
                    string tmp2 = tmp.Substring(j, i - j);
                    if (cnt == 10)
                        ex2 = tmp2;
                    j = i + 1;
                    i = tmp.IndexOf(",", i + 1);
                }
                return Convert.ToInt32(ex2);
            }
            catch
            {
                return 0;
            }
        }

        public static int nMint(object value)
        {
            if (value == null)
                return -1;
            try
            {
                if (value.GetType().Name == "DBNull")
                {
                    return -1;
                }
                else
                    return Convert.ToInt32(value);
            }
            catch
            {
                return -1;
            }

        }
        public static long nint64(object value)
        {
            if (value == null || value.GetType().Name == "DBNull")
            {
                return 0;
            }
            else
            {
                if (long.TryParse(Convert.ToString(value), out long longValue))
                {
                    return longValue;

                }
                return (long)value;
            }
        }

        public static object GermanColorName(string name)
        {
            // Transparent = -1 , Black = 0 , Yellow = 1, Orange = 2, Gray = 3, Green = 4, Blue = 5 
            if (name == "Black")
                return "Schwarz";
            else if (name == "Yellow")
                return "Gelb";
            else if (name == "Gray")
                return "Grau";
            else if (name == "Green")
                return "Grün";
            else if (name == "Blue")
                return "Blau";
            else
            {
                return name;
            }
        }

        public static bool nbool(object value)
        {
            if (value.GetType().Name == "DBNull")
            {
                return false;
            }
            else
                return (((int)value) == 1);
        }

        public static bool sbool(object value)
        {
            if (value == null)
                return false;
            if (value.GetType().Name == "DBNull")
            {
                return false;
            }
            else
                return (Convert.ToString(value).ToLower() == "true");
        }

        public static double ndbldot(object value)
        {
            string tmp = fn.str(value);
            try
            {
                return Convert.ToDouble(tmp);
            }
            catch
            {
                return 0;
            }
        }

        public static double ndbl(object value)
        {
            try
            {

            
            if (value == null)
                return 0;
            else if (value.GetType().Name == "DBNull")
            {
                return 0;
            }
            else if (Convert.ToString(value) == "")
                return 0;
            else
                return Convert.ToDouble(value);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static int ndbli(object value)
        {
            if (value.GetType().Name == "DBNull")
            {
                return 0;
            }
            else
                return Convert.ToInt32((double)value);
        }
        //
        public static DateTime ndatedot(object value)
        {
            try
            {
                if (value.GetType().Name == "DBNull")
                {
                    return default;
                }
                else
                    return DateTime.ParseExact(fn.str(value), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                return default;
            }
        }

        public static DateTime ndate(object value)
        {
            if (value == null)
                return default;
            try
            {
                if (value.GetType().Name == "DBNull")
                {
                    return default;
                }
                else
                    return Convert.ToDateTime(value);
            }
            catch
            {
                return default;
            }
        }

        public static string inttostr(int value)
        {
            return Convert.ToString(value);
        }
        public static string sql_str(string value)
        {
            if (value == null)
                return "''";
            return "'" + value.Replace("'", "''") + "'";
        }
        public static string sql_str(int value)
        {
            return "'" + Convert.ToString(value) + "'";
        }



        public static string sql_date(DateTime? value)
        {
            if (value == null)
                return "null";
            else if (value.Value.Date == DateTime.MinValue)
                return "null";
            else
                return "convert(datetime,'" + Convert.ToString(value.Value) + "',104)";
        }
        public static string FileSize(long size)
        {
            if (size < 1000)
            {
                return Convert.ToString(size) + " Bytes";
            }
            else if (size < 1000000)
            {
                return Convert.ToString(size / 1000) + " KB";
            }
            else if (size < 1000000000)
            {
                return Convert.ToString(size / 1000000) + " MB";
            }
            else
            {
                return Convert.ToString(size / 1000000000) + " GB";
            }
        }

        public static string ConvertToFileNameReady(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace(" ", "_");
                value = value.Replace("/", "_");
                value = value.Replace("\\", "_");
                value = value.Replace(":", "_");
                value = value.Replace("*", "_");
                value = value.Replace("?", "_");
                value = value.Replace("\"", "_");
                value = value.Replace("<", "_");
                value = value.Replace(">", "_");
                value = value.Replace("|", "_");
                value = value.Replace("+", "_");
                value = value.Replace(",", "_");
            }
            return value;
        }

        public static string sql_date(DateTime value)
        {
            if (Convert.ToString(value).Substring(0, 10) == "01.01.0001")
                return "null";
            else return "convert(datetime,'" + Convert.ToString(value) + "',104)";
        }
        public static string sql_date(string value)
        {
            DateTime v2 = Convert.ToDateTime(value);
            return sql_date(v2);
        }
        public static string sql_num(int value)
        {
            return Convert.ToString(value);
        }
        public static string sql_num(long value)
        {
            return Convert.ToString(value);
        }
        public static string sql_num(string value)
        {
            if (value == "")
                return "0";
            return Convert.ToString(Convert.ToInt64(value));
        }
        public static string sql_num(bool value)
        {
            if (value)
                return "1";
            return "0";
        }
        public static string sql_float(double value)
        {
            return Convert.ToString(value).Replace(",", ".");
        }
        public static string sql_float(string value)
        {
            if (value == "")
                return "0";
            return Convert.ToString(value).Replace(",", ".");
        }
        public static string sql_fk(int value)
        {
            if (value == 0)
                return "null";
            else
                return Convert.ToString(value);
        }
        public static string sql_fk(object ovalue)
        {
            int value = fn.nint(ovalue);
            if (value == 0)
                return "null";
            else
                return Convert.ToString(value);
        }
        public static bool ispkeyvalid(int pkey)
        {
            if (pkey >= 0)
                return true;
            else
                return false;
        }
        public static string sqlnow()
        {
            return "getdate()";
        }
        public static string sqltoday()
        {
            return "CONVERT(DATE, GetDate(), 101)";
        }
        public static string intto2digit(int value)
        {
            string result = Convert.ToString(value + 100);
            result = fn.Right(result, 2);
            return result;
        }

        public static string GetTimestampString()
        {
            return DateTime.Today.Year + ((DateTime.Today.Month < 10) ? "0" : "")
                                             + DateTime.Today.Month + ((DateTime.Today.Day < 10) ? "0" : "") +
                                             DateTime.Today.Day + "_"
                                             + (100 * DateTime.Now.Hour) + DateTime.Now.Minute;
        }

        public static string Left(string param, int length)
        {
            if (length < 0) return "";
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            if (param.Length <= length)
                return param;
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            if (param.Length <= length)
                return param;
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            if (startIndex < 0) return param;
            if (param.Length <= startIndex)
                return "";
            if (length > (param.Length - startIndex))
                length = param.Length - startIndex;
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            if (param.Length <= startIndex)
                return "";
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }

        public static string Delete(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            if (startIndex < 0) return param;
            if (param.Length <= startIndex)
                return "";
            if (length > (param.Length - startIndex))
                length = param.Length - startIndex;
            string result = param.Substring(0, startIndex)
                            + param.Substring(startIndex + length, param.Length - startIndex - length);
            //return the result of the operation
            return result;
        }


        public static string TrimLeft(string value)
        {
            while (Left(value, 1) == " ") value = Mid(value, 1);//evtl 1?
            return value;
        }
        public static string TrimRight(string value)
        {
            while (Right(value, 1) == " ") value = Left(value, value.Length - 1);
            return value;
        }

        public static double ctddef(string value)
        {
            if (!double.TryParse(value, out double dummy)) return 0;
            double result;
            try
            {
                result = Convert.ToDouble(value);
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        public static int ctidef(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return 0;
            if (!double.TryParse(value, out double dummy)) return 0;
            int result;
            try
            {
                result = Convert.ToInt32(value);
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        public static int GetCalendarWeek2(DateTime date)
        {
#if NET6_0_OR_GREATER
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
#else
            double a = Math.Floor(Convert.ToDouble((14 - (date.Month)) / 12));
            double y = date.Year + 4800 - a;
            double m = (date.Month) + (12 * a) - 3;

            double jd = date.Day + Math.Floor(((153 * m) + 2) / 5) +
                (365 * y) + Math.Floor(y / 4) - Math.Floor(y / 100) +
                Math.Floor(y / 400) - 32045;

            double d4 = (jd + 31741 - (jd % 7)) % 146097 % 36524 %
                        1461;
            double L = Math.Floor(d4 / 1460);
            double d1 = ((d4 - L) % 365) + L;

            // Kalenderwoche ermitteln
            int calendarWeek = (int)Math.Floor(d1 / 7) + 1;

            // Das Jahr der Kalenderwoche ermitteln
            int year = date.Year;
            if (calendarWeek == 1 && date.Month == 12)
            {
                calendarWeek = 53;
                year++;
            }
            if (calendarWeek >= 52 && date.Month == 1)
                year--;

            // Die ermittelte Kalenderwoche zurückgeben
            return calendarWeek;
#endif
        }

        public static int GetAnzahlWochen(int jahr)
        {
            DateTime baseDate = new DateTime(jahr, 12, 31);
            int calendarWeek = GetCalendarWeek2(baseDate);

            // Wenn dieser Tag in die Woche 1 des neuen Jahres fällt, die Kalenderwoche 
            // des um eine Woche reduzierten Datums ermitteln
            if (calendarWeek == 1)
                return GetCalendarWeek2(baseDate.AddDays(-7));

            // Ergebnis zurückgeben
            return calendarWeek;
        }
        public static string sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        public static bool testnetwork(string _host)
        {
            Ping p = new Ping();
            String host = _host;
            byte[] buffer = new byte[32];
            int timeout = 1000;
            PingOptions pingOptions = new PingOptions();
            try
            {
                PingReply reply = p.Send(host, timeout, buffer, pingOptions);

                if (reply.Status == IPStatus.Success)
                {
                    // erfolgreich
                    return true;
                }
                else
                {
                    // keine Antwort innerhalb <timeout> ms
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckIfAProcessIsRunning(string processname)
        {
            return Process.GetProcessesByName(processname).Length > 0;
        }

        public static int ExtractNumberFromFileName(string FileName)
        {
            try
            {
                bool firstnumfound = false;
                string tmp = "";
                foreach (char x in FileName)
                {
                    if (!firstnumfound)
                    {
                        if (ctidef(Convert.ToString(x)) != 0)
                        {
                            firstnumfound = true;
                            tmp = Convert.ToString(x);
                        }
                    }
                    else
                    {
                        if (Char.IsNumber(x))
                        {
                            tmp += x;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return Convert.ToInt32(tmp);
            }
            catch
            {
                return 0;
            }
        }

        public void CreateRecurringTask()
        {
            /*
            Outlook.TaskItem task = Application.CreateItem(
                Outlook.OlItemType.olTaskItem) as Outlook.TaskItem;
            task.Subject = "Tax Preparation";
            task.StartDate = DateTime.Parse("4/1/2007 8:00 AM");
            task.DueDate = DateTime.Parse("4/15/2007 8:00 AM");
            Outlook.RecurrencePattern pattern =
                task.GetRecurrencePattern();
            pattern.RecurrenceType = Outlook.OlRecurrenceType.olRecursYearly;
            pattern.PatternStartDate = DateTime.Parse("4/1/2007");
            pattern.NoEndDate = true;
            task.ReminderSet = true;
            task.ReminderTime = DateTime.Parse("4/1/2007 8:00 AM");
            task.Save();
             */
        }

        public static int NumWeeksThisYear()
        {
            return NumWeeksOfYear(DateTime.Now.Year);
        }
        public static int NumWeeksLastYear()
        {
            return NumWeeksOfYear(DateTime.Now.Year - 1);
        }
        public static int NumWeeksOfYear(int jahr)
        {
            DateTime tmp = new DateTime(jahr + 1, 1, 1);
            if (tmp.DayOfWeek > DayOfWeek.Wednesday)
                return 53;
            else
                return 52;
        }
        public static double Round2(double epr)
        {
            return Math.Round(epr, 2);
        }
        public static Decimal Round2(Decimal epr)
        {
            return Math.Round(epr, 2);
        }
        public static double Round(double epr, int anzahlStellen)
        {
            return Math.Round(epr, anzahlStellen);
        }
        public static decimal RoundNumber(decimal number, int decimals)
        {
            if (decimals < 0 || decimals > 8)
                throw new ArgumentOutOfRangeException("decimals must be between 0 and 8", "decimals");

            //Komma verschieben
            int shiftFactor = (int)Math.Pow(10, decimals);
            decimal tmp = number * shiftFactor;

            //Runden: 0.5 addieren/subtrahieren und dann Nachkommastellen abschneiden
            decimal diff = (tmp >= 0 ? 0.5m : -0.5m);
            tmp = (long)(tmp + diff);

            //Komma wieder verschieben
            return (tmp / shiftFactor);
        }

        public static string GetUrlResponse(string url)
        {
            return GetUrlResponse(url, null, null);
        }

        public static string GetUrlResponse(string url, string username, string password)
        {
            string content;

            WebRequest webRequest = WebRequest.Create(url);

            if (username == null || password == null)
            {
                NetworkCredential networkCredential = new NetworkCredential(username, password);
                webRequest.PreAuthenticate = true;
                webRequest.Credentials = networkCredential;
            }

            WebResponse webResponse = webRequest.GetResponse();

            StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.ASCII);

            content = sr.ReadToEnd();

            return content;
        }

        /*
        public static Color generateRampFrom2Colors(Color c1, Color c2, int count,int current)
        {
            if (current < 0) current = 0;
            Color result = new Color();
            float r = c1.R;
            float g = c1.G;
            float b = c1.B;
            float cR = (r - c2.R) / count;
            float cG = (g - c2.G) / count;
            float cB = (b - c2.B) / count;
            r -= (cR * current); 
            g -= (cG * current); 
            b -= (cB * current);
            try
            {
                result = Color.FromArgb((int)r, (int)g, (int)b);
            }
            catch (Exception exx) {
                throw;
            }
            return result;
        }

        public static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
        */
        public static void OpenMail(string empfaenger)
        {
            var ps = new ProcessStartInfo("mailto://" + empfaenger)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);

        }
        public static xbool Dial(string number)
        {
            if (File.Exists("C:\\faktura\\octocall\\octocall.exe"))
            {
                Process.Start("C:\\faktura\\octocall\\octocall.exe", "\"0" + number + "\"");
                return true;
            }
            else if (File.Exists(@"C:\Program Files (x86)\Alcatel_PIMphony\aocphone.exe"))
            {
                Process.Start(@"C:\Program Files (x86)\Alcatel_PIMphony\aocphone.exe", "\"0" + number + "\"");
                return true;
            }
            else if (File.Exists(@"C:\Program Files\Alcatel_PIMphony\aocphone.exe"))
            {
                Process.Start(@"C:\Program Files\Alcatel_PIMphony\aocphone.exe", "\"0" + number + "\"");
                return true;
            }
            return new xbool("Die Software der Telefonanlage wurde nicht gefunden.\r\nBitte Installieren Sie diese zuerst.");
        }

        public static bool IsNumeric(string value)
        {
            try
            {
                Int64 dummy = Convert.ToInt64(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        public static bool CheckFolderEmpty(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            var folder = new DirectoryInfo(path);
            if (folder.Exists)
            {
                return folder.GetFileSystemInfos().Length == 0;
            }

            return true;
        }
        public static string make3digitno(int jobnr)
        {
            string res = Convert.ToString(jobnr);
            while (res.Length < 3)
                res = "0" + res;
            return res;
        }
        public static string dsq(string param)
        {
            return param.Replace("'", "''");
        }
        public static string makesafesqlstr(string p)
        {
            return p.Replace("'", "''");
        }

        public static string Trim(string p)
        {
            return TrimLeft(TrimRight(p));
        }

        public static string FormatDouble(double Umsatz)
        {
            return Umsatz.ToString("N2");
        }

        public static string formatProzent(double skonto)
        {
            return skonto.ToString("N2") + " %";
        }

        public static string nospace(string x)
        {
            return x.Replace(" ", "");
        }


        public static bool Zip(string filename)
        {
            string destname = filename.Replace(Path.GetExtension(filename), ".zip");
            using (ZipArchive tmp = ZipFile.Open(destname, ZipArchiveMode.Create))
                tmp.CreateEntryFromFile(filename, Path.GetFileName(filename));
            return true;
        }

        public static bool ZipFolderFlat(string foldername)
        {
            string destname = foldername + ".zip";
            ZipFile.CreateFromDirectory(foldername, destname);
            return true;
        }

        public static string formatDatumShort(DateTime value)
        {
            if (value != null)
                return ((value.Day < 10) ? "0" : "") + value.Day + "." + ((value.Month < 10) ? "0" : "") + value.Month + "." + value.Year;
            else
                return "";
        }

        public static string formatDatumLong(DateTime value)
        {
            if (value != null)
                return ((value.Day < 10) ? "0" : "") + value.Day + "." + ((value.Month < 10) ? "0" : "")
                       + value.Month + "." + value.Year + " " + ((value.Hour < 10) ? "0" : "") + value.Hour + ":"
                       + ((value.Minute < 10) ? "0" : "") + value.Minute;
            else
                return "";
        }

        public static string PrettyFileName(string mDRFile)
        {
            string retval = mDRFile.Replace(":", "_");
            return Path.GetFileNameWithoutExtension(retval).Replace(":", "_").Replace(".", "_") + Path.GetExtension(retval);
        }

        public static bool IsEANValid(Int64 EAN)
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

        public static bool CheckEmail(string mail)
        {
            if (mail.IndexOf("@") == -1)
                return false;
            else if (hasuml(mail))
            {
                return false;
            }
            else if (!hasvaliddomain(mail))
            {
                return false;
            }
            else if (mail.IndexOf(" ") > -1)
                return false;
            return true;
        }

        public static bool hasvaliddomain(string mail)
        {
            string[] tmp = mail.Split('@');
            if (tmp[1].Split('.').Count() < 2)
                return false;
            return true;
        }

        public static bool hasuml(string mail)
        {
            if (mail.IndexOf("ä") != -1)
                return true;
            if (mail.IndexOf("ö") != -1)
                return true;
            if (mail.IndexOf("ü") != -1)
                return true;
            if (mail.IndexOf("ß", StringComparison.OrdinalIgnoreCase) != -1)
                return true;
            if (mail.IndexOf("Ä") != -1)
                return true;
            if (mail.IndexOf("Ö") != -1)
                return true;
            if (mail.IndexOf("Ü") != -1)
                return true;
            if (mail.IndexOf("?") != -1)
                return true;
            return false;
        }

        public static byte[] nbytes(object v)
        {
            try
            {
                return (byte[])v;
            }
            catch
            {
                return null;
            }
        }
        public static string DatumtoEightDigitInt(DateTime datum)
        {
            return Convert.ToString(datum.Day + (100 * datum.Month) + (10000 * datum.Year));
        }
        public static int DatumToInt(DateTime datum)
        {
            if (datum.Year == 1)
                return 0;
            try
            {
                return Convert.ToInt32(((DateTimeOffset)datum).ToUnixTimeSeconds());
            }
            catch
            {
                return 0;
            }
            //return datum.Day + (100 * datum.Month) + (10000 * datum.Year);
        }

        public static string bildname(string v)
        {
            return v.Replace(" ", "_").Replace("ß", "ss").Replace(",", "").Replace(".", "").Replace("&", "_").Replace("´", "_")
                .Replace("ü", "ue").Replace("ä", "ae").Replace("ö", "oe").Replace("(", "").Replace(")", "")
                .Replace("/", "").Replace("*", "").Replace("+", "").Replace("'", "").Replace("%", "").Replace("\\", "")
                .Replace("\r", "").Replace("\n", "").Replace(" ", "_").Replace("Ø", "_dm_").Replace("|", "-")
                .Replace(":","").Replace("Ô","O").Replace("é", "e").Replace("-", "").Replace("<", "").Replace(">", "");
        }

        public static DateTime StrToDate(string dt)
        {
            if (dt==null)
                return new DateTime(1, 1, 1);
            string[] cmp = dt.Split('.');
            int tag = Convert.ToInt32(cmp[0]);
            int monat = Convert.ToInt32(cmp[1]);
            int jahr = Convert.ToInt32(cmp[2]);
            return new DateTime(jahr, monat, tag);
        }

        public static string GetAppsrv()
        {
            IPHostEntry host = Dns.GetHostEntry("autoappsrv");
            return "https://" + host.HostName + "/";
        }

        public static string GetAppsrvBackend()
        {
            IPHostEntry host = Dns.GetHostEntry("autoappsrvbackend");
            return "https://" + host.HostName + "/";
        }

        public static string GetMonthName(DateTime dt)
        {
            return Monatsname[dt.Month - 1];
        }

        public static int GetDaysInMonth(int month,int year)
        {
            DateTime start = new DateTime(year, month, 1);
            DateTime end = start.AddMonths(1);
            return end.Subtract(start).Days;

        }

        public static int GetCalendarWeek(DateTime dt)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekNum;
        }
        public static List<string> SplitMessages(string x)
        {
            List<string> tmp = new List<string>();

            while (x.IndexOf("{\"type\"") >= 0)
            {
                string x2 = x.Substring(1, x.Length - 1);
                if (x2.IndexOf("{\"type\"") == -1)
                {
                    tmp.Add(x);
                    x = "";
                }
                else
                {
                    tmp.Add(x.Substring(0, x2.IndexOf("{\"type\"") + 1));
                    x = x2.Substring(x2.IndexOf("{\"type\""), x2.Length - x2.IndexOf("{\"type\""));
                }
                //string vsp = x.Substring(0)
            }

            return tmp;
        }

        public static string WorkingFilename(string illegal)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                illegal = illegal.Replace(c.ToString(), "");
            }

            return illegal;
        }
        /// <summary>
        /// Split Values "value 1", " value,2" where the values can contain ','
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string[] GetCsvValues(string content)
        {
            List<string> tmp = new List<string>();
            if (!string.IsNullOrEmpty(content) && !string.IsNullOrWhiteSpace(content))
            {
                bool found = false;
                List<char> word = new List<char>();
                for (int i = 0; i < content.Length; i++)
                {
                    char c = content[i];
                    if (c != '\"' && c != ',')
                    {
                        word.Add(c);
                        found = false;

                    }
                    else if (c == '\"')
                    {
                        found = true;
                        if (i == content.Length - 1)
                            tmp.Add(new String(word.ToArray()));
                    }
                    else if (c == ',')
                    {
                        if (found)
                        {

                            tmp.Add(new String(word.ToArray()));
                            word = new List<char>();
                        }
                        found = false;
                    }
                }
            }
            return tmp.ToArray();
        }

        public static int xlchar2num(string v)
        {
            int result = 0;
            foreach (char x in v)
            {
                result *= 26;
                result += (x - 64);
            }

            return result;
        }

        public static DateTime? DateOnly(DateTime? datum)
        {
            if (datum == null)
                return null;
            return new DateTime(datum.Value.Year, datum.Value.Month, datum.Value.Day);
        }

        public static DateTime DateTimeFromUnix(int v)
        {
            return new DateTime(1970, 1, 1).AddSeconds(v); //.AddHours(2) //hier wird nichts draufgerechnet weil die Zeit im Shop normiert wird.
        }

        public static string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((nic.OperationalStatus == OperationalStatus.Up) && (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback))
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddresses;
        }

        public static string casewhen(string feld, string felddefault)
        {
            return "case when isnull("+feld+",0) > 0 then "+feld+" else "+felddefault+" end";
        }
    }
}