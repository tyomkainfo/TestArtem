using System;
using System.Runtime.Serialization;

namespace nrnUtil
{
    [DataContract]
    public class xbool
    {
        [DataMember]
        public bool value { get; set; }
        [DataMember]
        public string description { get; set; } 
        [DataMember]
        public string oktext { get; set; }
        [DataMember]
        public ErrorCode reason { get; set; }

        public delegate xbool tryFunc();
        public delegate void successFunc();
        public delegate T TypedFunc<T>();

        public static bool operator ==(xbool x, xbool y)
        {
            return x.value == y.value;
        }
        public static bool operator !=(xbool x, xbool y)
        {
            return x.value != y.value;
        }
        public override bool Equals(object obj)
        {
            bool rc = false;
            if (obj is xbool)
            {
                xbool p2 = obj as xbool;
                rc = (this == p2);
            }
            return rc;
        }
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public xbool()
        {
            description = "";
            reason = ErrorCode.general;
        }
        public xbool(Exception e)
        {
            description = e.Message + "\r\n" + e.StackTrace;
            value = false;
            reason = ErrorCode.general;
        }
        public xbool(bool init)
        {
            value = init;
            description = "";
            reason = ErrorCode.general;
        }
        public xbool(string init)
        {
            value = (init == "");
            description = init;
            reason = (value) ? ErrorCode.none : ErrorCode.general;
        }
        public xbool(ErrorCode init)
        {
            value = (init == ErrorCode.none);
            description = (value) ? "" : "Error";
            reason = init;
        }
        public xbool(ErrorCode init, string inittext)
        {
            value = (init == ErrorCode.none);
            description = inittext;
            reason = init;
        }
        public xbool(tryFunc _cmd)
        {
            string init = "";
            try
            {
                xbool res = _cmd();
                value = res.value;
                description = res.description;
                reason = res.reason;

            }
            catch (Exception ex)
            {
                init = ex.Message;
            }
            value = (init == "");
            description = init;
            reason = (value) ? ErrorCode.none : ErrorCode.general;
        }
        public xbool(tryFunc _cmd, successFunc _cmd2)
        {
            string init = "";
            try
            {
                xbool res = _cmd();
                value = res.value;
                description = res.description;
                reason = res.reason;
                _cmd2();
            }
            catch (Exception ex)
            {
                init = ex.Message;
            }
            value = (init == "");
            description = init;
            reason = (value) ? ErrorCode.none : ErrorCode.general;
        }

        public void IfTrue(successFunc _cmd2)
        {
            if (this)
                _cmd2();
        }
        public void Else(successFunc _cmd2)
        {
            if (!this)
                _cmd2();
        }
        public T IfTrue<T>(TypedFunc<T> _cmd2)
        {
            if (this)
                return _cmd2();
            else
                throw new Exception(this.description);
        }
        public T IfTrue<T>(TypedFunc<T> _cmd2, T ErrResult)
        {
            if (this)
                return _cmd2();
            else
                return ErrResult;
        }
        public static implicit operator xbool(bool init)
        {
            return new xbool(init);
        }
        public static implicit operator bool(xbool obj)
        {
            return obj.value;
        }
    }
}