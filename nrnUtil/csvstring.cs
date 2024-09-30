using System;
using System.Text;

namespace nrnUtil
{
    public class csvstring
    {
        private StringBuilder builder;
        private bool first;
        public string content {
            get
            {
                return builder.ToString();
            }
        }
        public csvstring()
        {
            builder = new StringBuilder();
            first = true;
        }
        public void CsvAdd(string what)
        {
            if (what == null)
                what = "";
            if (!first)
                builder.Append(";");
            first = false;
            builder.Append(what);
        }
        public void CsvAdd(int what)
        {
            CsvAdd(Convert.ToString(what));
        }
        public void StartLineWith(string what)
        {
            builder.Append(what);
            first = false;
        }
        public void EndLine()
        {
            builder.Append("\r\n");
            first = true;
        }

        public void SaveToFile(string itmsfile, bool isutf8 = true)
        {
            if (isutf8)
                System.IO.File.WriteAllText(itmsfile, builder.ToString());
            else
                System.IO.File.WriteAllText(itmsfile, builder.ToString(), Encoding.GetEncoding(1252));
        }
    }
}