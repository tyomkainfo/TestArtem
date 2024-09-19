using System;
using nrnUtil;
using System.Diagnostics;
using System.IO;

namespace nrnUtil
{

    public class OfficeService : IOfficeService
    {
        public bool isofficeinstalled { get; private set; }

        public OfficeService()
        {
            isofficeinstalled = true;
            /*
            Type officeType = Type.GetTypeFromProgID("Excel.Application");
            if (officeType == null)
            {
                isofficeinstalled = false;
            }
            else
            {
                isofficeinstalled = true;
            }
            */
        }
        public bool OpenPdf(string pdfName)
        {
            if (isofficeinstalled)
            {
                var ps = new ProcessStartInfo(pdfName)
                {
                    UseShellExecute = true,
                    Verb = "open"
                };
                Process.Start(ps);
                return true;
            }
            else
            {
                return false;
                //oder alternativen pdf viewer besorgen
            }
        }

        public bool OpenExcel(string excelName)
        {
            if (isofficeinstalled)
            {
                var ps = new ProcessStartInfo(excelName)
                {
                    UseShellExecute = true,
                    Verb = "open"
                };
                Process.Start(ps);
                return true;
            }
            else
            {
                return false;
                //oder alternativen excel viewer besorgen
            }
        }

        public void OpenExcel(MemoryStream mstream, string dateiname)
        {
            if (Path.GetDirectoryName(dateiname) == "")
                dateiname = Path.Combine(@"c:\faktura2\temp\", dateiname);
            string DateiNameBase = dateiname;
            string DateiName = DateiNameBase;
            int kopie = 0;
            bool klappt = false;
            FileStream fileStream = null;
            while (!klappt)
            {
                try
                {
                    if (kopie > 0)
                    {
                        DateiName = Path.GetFileNameWithoutExtension(DateiNameBase) + "_Kopie" + kopie + Path.GetExtension(DateiNameBase);
                    }
                    fileStream = File.Create(DateiName);
                    klappt = true;
                    kopie++;
                }
                catch (Exception ex)
                {
                    kopie++;
                }
            }
            if (fileStream != null)
            {
                mstream.WriteTo(fileStream);
                mstream.Close();
                fileStream.Close();
                OpenExcel(DateiName);
            }
        }

    }
}
