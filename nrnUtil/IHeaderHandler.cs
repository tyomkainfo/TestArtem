using System;
using System.Collections.Generic;
using System.Text;

namespace nrnUtil
{
    public interface IHeaderHandler
    {
		Dictionary<string,string> GetExtraHeaders();
    }
}
