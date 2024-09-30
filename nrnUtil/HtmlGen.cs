using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrnUtil
{
    public class HtmlGen
    {
        public HtmlGen() { }
        public static string GetHtmlMailHeader(string title)
        {
            return "<html>\r\n"
                   + "<head>\r\n"
                   + "<title>" + title + "</title>\r\n"
                   + "</head>\r\n"
                   + "<body>\r\n";
        }

        public static string GetFirstTdTag(string v, int colspan = 0)
        {
            return "<td " + ((colspan > 0) ? "colspan='" + colspan + "' " : "") + "style='padding-left:16px;padding-top:8px;padding-bottom:8px;text-align: left;display:table-cell;vertical-align:top; white-space: nowrap;'>" + v + "</td>";
        }

        public static string GetTdTag(string v, int colspan = 0)
        {
            return "<td " + ((colspan > 0) ? "colspan='" + colspan + "' " : "") + "style='padding:8px 8px;display:table-cell;text-align:right;vertical-align:top; white-space: nowrap;'>" + v + "</td>";
        }
        public static string EndDataTable()
        {
            return "</table>\r\n";
        }

        public static string GetTrTag(string content, bool even)
        {
            return "<tr height='5' style='border-bottom:1px solid #ddd;" + ((even) ? "background-color:#E7E9EB" : "background-color:#fff")
                                                                         + ";max-height:15px;'>" + content + "</tr>\r\n";
        }

        public static string GetThTag(string v)
        {
            return "<th style='padding:8px 8px;display:table-cell;text-align:right;vertical-align:top; white-space: nowrap;'>" + v + "</th>";
        }

        public static string GetFirstThTag(string v)
        {
            return "<th style='padding-left:16px;padding-top:8px;padding-bottom:8px;display:table-cell;text-align:left;vertical-align:top; white-space: nowrap;'>" + v + "</th>";
        }

        public static string GetFooter()
        {
            return "</td><td style='width:20%'>&nbsp;</td></tr></table>\r\n"
                   + "</body>\r\n</html>\r\n";
        }

        public static string GetDataTable()
        {
            return "<table style='margin:20px 0;border-collapse:collapse;border-spacing:0;width:100%;display:table;border:1px solid #ccc'>\r\n";
        }

        public static string GetHeadLine(string v)
        {
            return "<p>" + v + "</p>\r\n";
        }

        public static string GetOuterTable()
        {
            return "<table style='margin:20px 0;width:100%'>\r\n<tr><td style='width:1%'>&nbsp;</td><td style='width:90%'>\r\n";
        }
    }
}
