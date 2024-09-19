using Newtonsoft.Json;
using nrnUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace nrnUtil
{

    public enum AuthenticationType
    {
        Basic, Bearer
    }

    public class WebApiBase<T>
    {
        protected IHeaderHandler _headerHandler;
        public delegate void WebApiProblemHandler(WebApiProblemMessage msg);

        public WebApiProblemHandler OnProblem { get; set; }
        public string controller { get; set; }
        public string defappserver { get; set; }
        public string credentials { get; set; }
        public AuthenticationType authType { get; set; } = AuthenticationType.Basic;

        public string defapi { get; set; }
        protected INrnLogger _logger;
        
        public WebApiBase(INrnLogger logger)
        {
            _logger = logger;
            _headerHandler = new NoHeaderHandler();
        }

        protected async Task<string> RequestGet(string url, string appserver = "", string api = "")
        {
            using (var client = CreateClient(appserver, api, credentials, authType))
            {
                try
                {
                    var response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    var fehler = "Global-WebVerbindungsfehler" + " " + url + ".";
                    throw new Exception(fehler + ex.Message + "\r\n" + ex.StackTrace);
                }
            }
        }

        public T Default<T>()
        {

            if (typeof(T).Name.ToLower().Replace("system.", "") == "string")
                return (T)(object)"";
            else
                return Activator.CreateInstance<T>();
        }

        public S RequestGet<S>(string url, string appserver = "", string api = "", bool isqueryparam = false)
        {
            url = ((controller != "") ? controller + ((url.Substring(0, 1) != "/") ? "/" : "") : "") + url + ((isqueryparam) ? "" : ((url.Substring(url.Length - 1, 1) != "/") ? "/" : ""));
            try
            {
                var result = RequestGet(url, appserver, api).Result;
                return JsonConvert.DeserializeObject<S>(result);
            }
            catch (Exception ex)
            {
                HandleReqExeption(url, ex, "");
                return Default<S>();
            }
        }

        public async Task<S> RequestPostAsync<S>(string url, object data, string appserver = "", string api = "", bool postasform = false)
        {
            url = controller + ((url.Substring(0, 1) != "/") ? "/" : "") + url + ((url.Substring(url.Length - 1, 1) != "/") ? "/" : "");
            string jsonParams = JsonConvert.SerializeObject(data);
            try
            {
                var result = await RequestPost2(url, jsonParams, appserver, api, postasform);
                return JsonConvert.DeserializeObject<S>(result);
            }
            catch (Exception ex)
            {
                HandleReqExeption(url, ex, jsonParams);
                return Default<S>();
            }
        }


        public S RequestPost<S>(string url, object data, string appserver = "", string api = "", bool postasform = false)
        {
            // todo: ddr Message "grau machen"
            url = controller + ((url.Substring(0, 1) != "/") ? "/" : "") + url + ((url.Substring(url.Length - 1, 1) != "/") ? "/" : "");
            string jsonParams = JsonConvert.SerializeObject(data);
            try
            {
                var result = RequestPost(url, jsonParams, appserver, api, postasform).Result;
                return JsonConvert.DeserializeObject<S>(result);
            }
            catch (Exception ex)
            {
                HandleReqExeption(url, ex, jsonParams);
                return Default<S>();
            }
        }

        private void HandleReqExeption(string url, Exception ex, string jsonParams)
        {
            var fehler = "Global-WebVerbindungsfehler" + " " + url + ".";
            if (OnProblem != null)
                OnProblem(new WebApiProblemMessage(fehler + ex.Message + "\r\n" + ex.StackTrace));
            LogError(ex.Message + "\r\nUrl: " + url + "\r\nParams:\r\n" + jsonParams);
        }

        private void LogError(string jsonParams)
        {
            _logger.Log(jsonParams, NrnLogLevel.Error, "webapi");
        }

        public MemoryStream RequestPostNoJson(string url, object data, string appserver = "", string api = "")
        {
            url = controller + ((url.Substring(0, 1) != "/") ? "/" : "") + url + ((url.Substring(url.Length - 1, 1) != "/") ? "/" : "");
            string jsonParams = JsonConvert.SerializeObject(data);
            try
            {
                var result = RequestPostNoJson(url, jsonParams, appserver, api).Result;
                return new MemoryStream(result);
            }
            catch (Exception ex)
            {
                HandleReqExeption(url, ex, jsonParams);
                return null;
            }
        }

        public MemoryStream RequestGetNoJson(string url, string appserver = "", string api = "", int dummy = 0, string cont = "")
        {
            url = ((cont != "") ? cont : controller) + ((url.Substring(0, 1) != "/") ? "/" : "") + url +
                  ((url.Substring(url.Length - 1, 1) != "/") ? "/" : "");
            try
            {
                var result = RequestGetNoJson(url, appserver, api).Result;
                return new MemoryStream(result);
            }
            catch (Exception ex)
            {
                HandleReqExeption(url, ex, "");
                return null;
            }
        }

        private async Task<string> RequestPost2(string url, string parameter, string appserver = "", string api = "", bool postasform = false)
        {
            HttpContent jsonParams;
            if (postasform)
            {
                jsonParams = new FormUrlEncodedContent(JsonConvert.DeserializeObject<Dictionary<string, string>>(parameter));
            }
            else
            {
                jsonParams = new StringContent(parameter, Encoding.UTF8, "application/json");
            }
            using (var client = CreateClient(appserver, api, credentials, authType))
            {
                var response = await client.PostAsync(url, jsonParams);
                try
                {
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        private async Task<string> RequestPost(string url, string parameter, string appserver = "", string api = "", bool postasform = false)
        {
            HttpContent jsonParams;
            if (postasform)
            {
                jsonParams = new FormUrlEncodedContent(JsonConvert.DeserializeObject<Dictionary<string, string>>(parameter));
            }
            else
            {
                jsonParams = new StringContent(parameter, Encoding.UTF8, "application/json");
            }
            using (var client = CreateClient(appserver, api, credentials, authType))
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(client.BaseAddress + url);
                    request.Content = jsonParams;
                    var response = client.SendAsync(request).Result;
                    //var response = client.PostAsync(url, jsonParams).Result;
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        if (response.IsSuccessStatusCode)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }
            }
        }

        private async Task<byte[]> RequestPostNoJson(string url, string parameter, string appserver = "", string api = "")
        {
            var jsonParams = new StringContent(parameter, Encoding.UTF8, "application/json");
            using (var client = CreateClient(appserver, api, credentials, authType))
            {
                var response = client.PostAsync(url, jsonParams).Result;
                try
                {
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        private async Task<byte[]> RequestGetNoJson(string url, string appserver = "", string api = "")
        {
            using (var client = CreateClient(appserver, api, credentials, authType))
            {
                var response = client.GetAsync(url).Result;
                try
                {
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        public HttpClient CreateClient(string appserver, string api = "", string credentials = "", AuthenticationType authType = AuthenticationType.Basic)
        {
            try
            {
                
                var handler = new HttpClientHandler();
                try
                {
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, cetChain, policyErrors) =>
                        {
                            return true;
                        };
                }
                catch { }
                
                var client = new HttpClient(handler);
                client.BaseAddress = new Uri(((appserver == "") ? defappserver : appserver) + ((api == "") ? defapi : api) + "/");
                client.Timeout = TimeSpan.FromSeconds(500);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (fn.str(credentials) != "")
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue((authType == AuthenticationType.Basic) ? "basic" : "Bearer", credentials);
                }
                Dictionary<string, string> extraheaders = _headerHandler.GetExtraHeaders();
                foreach (string key in extraheaders.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, extraheaders[key]);
                }
                return client;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string getControllerName(string clss)
        {
            return clss.Replace("WebApi", "").ToLower();
        }
    }
}
