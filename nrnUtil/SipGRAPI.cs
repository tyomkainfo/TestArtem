using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace nrnUtil
{
    public class SipGRAPI
    {
        //private const string sipgateuser = "michael.droll@elbers-eurofleurs.de";
        //private const string sipgatepassword = "Krupp050";
        private const string sipgatetoken = "token-AA4SC5:b3d7b418-9ea7-4e70-b5d9-9536b7aa9f7a";
        private const string URLFAX = "https://api.sipgate.com/v2/sessions/fax";
        private const string URLSMS = "https://api.sipgate.com/v2/sessions/sms";
        private const string URLLOG = "https://api.sipgate.com/v2/history?limit=50";

        private static string enc = Base64Encode(sipgatetoken);
        //Base64Encode(sipgateuser + ":" + sipgatepassword);

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static sgresult SendFax(string pdf, string recipient,string sender,int docid,int extradata1, int extradata2)
        {
            recipient = recipient.Replace(" ", "").Replace("-", "").Replace("/", "").Replace("(", "").Replace(")", "");
            /*
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Ssl3
                | System.Net.SecurityProtocolType.Tls12
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls;
            */
            sgresult result = new sgresult();
            string betreff = Path.GetFileName(pdf);
            result.sessionid = 0;
            result.error = "";
            try
            {              
                recipient = recipient.Replace("+", "00").Replace(" ", "");
                string fax2 = Convert.ToBase64String(File.ReadAllBytes(pdf));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URLFAX);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization: Basic " + enc);
                //goto l1;
                string data = " { \r\n"
                    + "  \"faxlineId\": \"f1\",\r\n"
                    + "  \"recipient\": \"" + recipient + "\",\r\n"
                    + "  \"filename\": \""+betreff+"\",\r\n"
                    + "  \"base64Content\": \"" + fax2 + "\"\r\n"
                    + "}";

                request.ContentLength = data.Length;

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                requestWriter.Write(data);
                requestWriter.Close();


                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                Console.Out.WriteLine(response);
                responseReader.Close();

                JObject json = JObject.Parse(response);
                result.sessionid = Convert.ToInt64(json["sessionId"]);
            }
            catch (Exception ex)
            {
                
                result.error = ex.Message;
            }
            
           
            return result;
        }

    

        public static xbool SendSms(string sms, string recipient)
        {
            // Mit .net5 muss man das nicht mehr setzen bzw. bekommt sonst auch einen Fehler.
            /*
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Ssl3
                | System.Net.SecurityProtocolType.Tls12
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls;
            */
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(URLSMS);
                request2.Method = "POST";
                request2.ContentType = "application/json";
                request2.Headers.Add("Authorization: Basic " + enc);

                string data2 = " { \r\n"
                    + "  \"smsId\": \"s0\",\r\n"
                    + "  \"recipient\": \"" + recipient + "\",\r\n"
                    + "  \"message\": \"" + sms + "\"\r\n"
                    //+ " "sendAt": 1546850272\"\
                    + "}";

                request2.ContentLength = data2.Length;

                StreamWriter requestWriter2 = new StreamWriter(request2.GetRequestStream(), System.Text.Encoding.ASCII);
                requestWriter2.Write(data2);
                requestWriter2.Close();


                WebResponse webResponse = request2.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                Console.Out.WriteLine(response);
                responseReader.Close();
            }
            catch (Exception ex)
            {
                return new xbool(ex.Message);
            }
            return true;
        }

        public static string CheckLog()
        {
            /*
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Ssl3
                | System.Net.SecurityProtocolType.Tls12
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls;
            */
            xbool result = true;
          
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URLLOG);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization: Basic " + enc);
            
            int cnt = 0;
            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                //Console.Out.WriteLine(response);
                responseReader.Close();
                return response;

           
            }
            catch (Exception e)
            {
                return e.Message + "\r\n" + e.StackTrace;
            }
            
        }
    }

    public class sgresult
    {
        public Int64 sessionid;
        public string error;
    }
}
