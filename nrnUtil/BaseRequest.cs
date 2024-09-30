namespace nrnUtil
{
    public class BaseRequest 
    {
        public string inst = "";
        public string senderclass = "";
        public string user = "";
        public int userId = -1;
        public int loginId = -1;
        public string workstation = "";
        public string hashWert = "";
        public BaseRequest()
        { }
        public BaseRequest(BaseRequest baseReq)
        {
            inst = baseReq.inst;
            senderclass = baseReq.senderclass;

            user = baseReq.user;
            userId = baseReq.userId;

            workstation = baseReq.workstation;

            hashWert = baseReq.hashWert;
        }

    }
}