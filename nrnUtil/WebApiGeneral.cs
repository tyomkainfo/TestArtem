using Newtonsoft.Json.Linq;

namespace nrnUtil
{

    public class WebApiGeneral : WebApiBase<WebApiGeneral>
    {
        public WebApiGeneral(INrnLogger logger, I_settings settings) : base(logger)
        {
            _logger = logger;
            controller = ""; //Any Controller can be called. Add the Name to the URL
            defappserver = settings.Appserver;
            defapi = "v1";
        }

        public object GeneralPost(string url, JObject parameters)
        {
            return RequestPost<object>(url, parameters);
        }
        public object GeneralGet(string url)
        {
            return RequestGet<object>(url);
        }
     
    }
}
