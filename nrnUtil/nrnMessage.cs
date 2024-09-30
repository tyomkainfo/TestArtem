using System;
using Newtonsoft.Json;

namespace nrnUtil
{
    public class nrnMessage
    {
        [JsonProperty(Order = -13)]
        public string type;

        [JsonProperty(Order = -12)]
        public actions action;

        [JsonProperty(Order = -11)]
        public string field;

        [JsonProperty(Order = -10)]
        public string value;

        [JsonProperty(Order = -9)]
        public string oldValue;

        [JsonProperty(Order = -8)]
        public int id;

        [JsonProperty(Order = -7)]
        public int[] ids;

        [JsonProperty(Order = -6)]
        public string senderinstance;

        [JsonProperty(Order = -5)]
        public string senderclass;

        [JsonProperty(Order = -4)]
        public int mandant;

        [JsonProperty(Order = -3)]
        public string user;

        [JsonProperty(Order = -2)]
        public int userId;

        [JsonProperty(Order = -1)]
        public string workstation;

        [JsonProperty(Order = -1)]
        public Guid guid;

        [JsonProperty(Order = -1)]
        public DateTime TimeStamp;

        public enum actions
        {
            Add,
            Del,
            Edit,
            Change,
            Reload,
            Heartbeat
        }
        public nrnMessage()
        {
            Type t = this.GetType();
            if (t != null)
            {
                this.type = t.Name;
            }
            else
            {
                throw new InvalidOperationException("nrnMessage: der Type der Erweiterungsklasse konnte nicht bestimmt werden.... ");
            }
            TimeStamp = DateTime.Now;
        }
        public nrnMessage(actions action) : this()
        {
            this.action = action;
            TimeStamp = DateTime.Now;
        }
        public nrnMessage(UniversalRequest universalRequest, actions action) : this(action)
        {
            if (universalRequest != null)
            {
                if (universalRequest.inst != null)
                    senderinstance = universalRequest.inst;
                if (universalRequest.senderclass != null)
                    senderclass = universalRequest.senderclass;
                if (universalRequest.user != null)
                    user = universalRequest.user;
                if (universalRequest.workstation != null)
                    workstation = universalRequest.workstation;
                if (universalRequest.field != null)
                    field = universalRequest.field;
                if (universalRequest.value != null)
                    value = universalRequest.value;
                if (universalRequest.oldvalue != null)
                    oldValue = universalRequest.oldvalue;
                ids = universalRequest.ids;
                mandant = universalRequest.mandant;
                id = universalRequest.id;
                userId = universalRequest.userId;
            }
            TimeStamp = DateTime.Now;
        }
    }
}