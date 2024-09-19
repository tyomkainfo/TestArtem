namespace nrnUtil
{
    using System;
    using System.IO;

    public interface IOfficeService
    {
        bool isofficeinstalled { get; }
        bool OpenPdf(string pdfName);
        bool OpenExcel(string excelName);
        void OpenExcel(MemoryStream mstream, string dateiname);
    }
}