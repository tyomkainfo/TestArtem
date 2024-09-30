using System;

namespace nrnUtil
{
    public class xint
    {
        public int value;
        public bool isempty;
        public string description;
        public ErrorCode reason;

        public static bool operator ==(xint x, xint y)
        {
            return ((x.value == y.value) && (x.isempty == y.isempty));
        }
        public static bool operator !=(xint x, xint y)
        {
            return x.value != y.value;
        }

        public static bool operator !(xint x)
        {
            return x.value < 1;
        }
        public override bool Equals(object obj)
        {
            bool rc = false;
            if (obj is xint)
            {
                xint p2 = obj as xint;
                rc = (this == p2);
            }
            return rc;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public xint()
        {
            value = 0;
            isempty = false;
            description = "";
            reason = ErrorCode.general;
        }
        public xint(int init)
        {
            value = init;
            isempty = false;
            description = "";
            reason = ErrorCode.general;
        }
        public xint(string init)
        {
            isempty = (init == "");
            if (!isempty)
                try
                {
                    value = Convert.ToInt32(init);
                }
                catch 
                {
                    
                }
               
            description = init;
            reason = ErrorCode.general;
        }
        public xint(bool init)
        {
            value = 0;
            isempty = init;
            description = "";
            reason = ErrorCode.general;
        }

        public xint(ErrorCode init)
        {
            isempty = init != ErrorCode.none;
            description = (isempty) ? "" : "Error";
            reason = init;
        }
        public xint(ErrorCode init, string inittext)
        {
            isempty = (init == ErrorCode.none);
            description = inittext;
            reason = init;
        }
        public static implicit operator xint(int init)
        {
            return new xint(init);
        }
        public static implicit operator int(xint obj)
        {
            return obj.value;
        }
    }
}