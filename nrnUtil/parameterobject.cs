using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrnUtil
{
    public class parameterobject : object
    {
        public object parameter;

        public parameterobject(object par)
        {
            parameter = par;
        }
        public object GetVal(string propertyName)
        {
            try
            {
                return parameter.GetType().GetProperty(propertyName).GetValue(parameter, null);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int GetIntVal(string propertyName)
        {
            try
            {
                return Convert.ToInt32(parameter.GetType().GetProperty(propertyName).GetValue(parameter, null));
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public bool GetBoolVal(string propertyName)
        {
            try
            {
                return Convert.ToBoolean(parameter.GetType().GetProperty(propertyName).GetValue(parameter, null));
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
