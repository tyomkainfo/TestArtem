using System;
using nrnUtil;
using System.Diagnostics;
using System.IO;

namespace WebApiClient.Services
{

    public class EmptyOfficeService : IOfficeService
    {
        public static IOfficeService Instance { get; } = new EmptyOfficeService();
        public bool isofficeinstalled { get; private set; }

        public EmptyOfficeService()
        {
           isofficeinstalled = false;
        }
        public bool OpenPdf(string pdfName)
        {
            return true;
        }

        public bool OpenExcel(string excelName)
        {
            return true;
        }

        public void OpenExcel(MemoryStream mstream, string dateiname)
        {
            return;
        }
    }
}
