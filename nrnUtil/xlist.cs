using System.Collections.Generic;

namespace nrnUtil
{
    public class xlist<T> //where T:class
    {
        private List<T> _values;
        public List<T> values { 
            get 
            {
                if (_values == null)
                    _values = new List<T>();
                return _values;
            }
            set
            {
                if (_values == value)
                    return;
                _values = value;
            }
        } 
        public ErrorCode reason;
        public string description;
        public bool valid;


    }
}