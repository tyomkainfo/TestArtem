using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace nrnUtil
{
    public class NoHeaderHandler : IHeaderHandler
    {
        public static IHeaderHandler Instance = new NoHeaderHandler();
        public Dictionary<string, string> GetExtraHeaders()
        {
            return new Dictionary<string, string>();
        }
    }
}
