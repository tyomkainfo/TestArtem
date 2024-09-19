using nrnUtil;

namespace nrnUtil
{
    public class UniversalRequest : BaseRequest
    {
        public int id { get; set; } = -1;
        public int id2 { get; set; } = -1;
        public int[] ids { get; set; }
        public int mandant { get; set; } = -1;
        public string field { get; set; } = "";
        public string value { get; set; } = "";
        public string oldvalue { get; set; } = "";
        //public clItem obj { get; set; }
        public UniversalRequest() : base()
        { }
        public UniversalRequest(UniversalRequest uniReq) : base(uniReq)
        {
            id = uniReq.id;
            mandant = uniReq.mandant;
            field = uniReq.field;
            value = uniReq.value;
            oldvalue = uniReq.oldvalue;
        }
        public void SetBaseReq(UniversalRequest uniReq)
        {
            id = uniReq.id;
            mandant = uniReq.mandant;
            field = uniReq.field;
            value = uniReq.value;
            oldvalue = uniReq.oldvalue;
        }
    }
}