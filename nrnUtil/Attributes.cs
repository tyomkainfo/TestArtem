using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace nrnUtil
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class TabTitleAttribute : Attribute
    {
        public string Text { get; set; }
    }
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class HelpAttribute : Attribute
    {
        public string HelpId { get; set; }

    }
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class NonUpdateableAttribute : Attribute
    {
        public NonUpdateableAttribute(bool _Updatable)
        {
            this.NonUpdateable = _Updatable;
        }
        public bool NonUpdateable { get; set; }

    }
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class HideInExcelAttribute : Attribute
    {
        public HideInExcelAttribute(bool _Hide)
        {
            this.HideInExcel = _Hide;
        }
        public bool HideInExcel { get; set; }

    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NoAutoGenerateAttribute : Attribute
    {
        public bool NoAutoGenerate { get; set; }

    }
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class IdFieldAttribute : Attribute
    {
        public bool IdField { get; set; }

    }
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DefautValueAttribute : Attribute
    {
        public DefautValueAttribute(string _val)
        {
            this.Val = _val;
        }
        public string Val { get; set; }

    }
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string _val)
        {
            this.Val = _val;
        }
        public string Val { get; set; }

    }
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ForeignKeyAttribute : Attribute
    {
        public ForeignKeyAttribute(bool _val)
        {
            this.IsForeignKey = _val;
        }
        public bool IsForeignKey { get; set; } = false;

    }

    public class AttributeHelper
    {
        public static bool IsForeignKeyProperty(object who, string propertyName)
        {
            try
            {
                PropertyInfo pInfo = who.GetType().GetProperty(propertyName);
                if (pInfo != null)
                {
                    Attribute attribute = Attribute.GetCustomAttribute(pInfo, typeof(ForeignKeyAttribute));
                    if (attribute is ForeignKeyAttribute nonUpdateableAttribute)
                    {
                        return nonUpdateableAttribute.IsForeignKey;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public static bool IsUpdateProperty(object DataClass, string PropertyName)
        {
            if (DataClass != null)
            {
                Type DataClassType = AttributeHelper.GetDataType(DataClass, PropertyName);
                if (DataClassType != null)
                {
                    PropertyInfo pInfo = DataClassType.GetProperty(PropertyName);
                    if (pInfo != null)
                    {
                        Attribute attribute = Attribute.GetCustomAttribute(pInfo, typeof(NonUpdateableAttribute));
                        if (attribute is NonUpdateableAttribute nonUpdateableAttribute)
                        {
                            return !nonUpdateableAttribute.NonUpdateable;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool IsHideInExcel(object DataClass, string PropertyName)
        {
            if (DataClass != null)
            {
                Type DataClassType = AttributeHelper.GetDataType( DataClass,  PropertyName);
                if (DataClassType != null)
                {
                    PropertyInfo pInfo = DataClassType.GetProperty(PropertyName);
                    if (pInfo != null)
                    {
                        Attribute attribute = Attribute.GetCustomAttribute(pInfo, typeof(HideInExcelAttribute));
                        if (attribute is HideInExcelAttribute nonUpdateableAttribute)
                        {
                            return nonUpdateableAttribute.HideInExcel;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }
        private static Type GetDataType(object DataClass, string PropertyName)
        {
            Type DataClassType = null;
            if (DataClass != null)
            {
                if (PropertyName.Contains("."))
                {
                    int index = PropertyName.IndexOf('.');
                    if (index > 0)
                    {
                        index++;
                        string property = PropertyName.Substring((index), (PropertyName.Length - index));
                        string propertyClass = PropertyName.Substring(0, (PropertyName.Length - (property.Length + 1)));
                        if (DataClass is PropertyByString pbs)
                        {
                            PropertyInfo pInfo1 = DataClass.GetType().GetProperty(propertyClass);
                            DataClassType = pInfo1.PropertyType;
                            PropertyName = property;
                        }
                    }
                    if (DataClass == null)
                    {
                        return null;
                    }
                }
                if (DataClassType == null)
                    DataClassType = DataClass.GetType();
                return DataClassType;
            }
            return null;
        }
        //private bool IsExelProperty(object DataClass, string PropertyName)
        //{
        //    Type DataClassType = null;
        //    if (DataClass != null)
        //    {
        //        if (PropertyName.Contains("."))
        //        {
        //            int index = PropertyName.IndexOf('.');
        //            if (index > 0)
        //            {
        //                index++;
        //                string property = PropertyName.Substring((index), (PropertyName.Length - index));
        //                string propertyClass = PropertyName.Substring(0, (PropertyName.Length - (property.Length + 1)));
        //                if (DataClass is PropertyByString pbs)
        //                {
        //                    PropertyInfo pInfo1 = DataClass.GetType().GetProperty(propertyClass);
        //                    DataClassType = pInfo1.PropertyType;
        //                    PropertyName = property;
        //                }
        //            }
        //            if (DataClass == null)
        //            {
        //                return false;
        //            }
        //        }
        //        if (DataClassType == null)
        //            DataClassType = DataClass.GetType();
        //        if (DataClassType != null)
        //        {
        //            PropertyInfo pInfo = DataClassType.GetProperty(PropertyName);
        //            if (pInfo != null)
        //            {

        //                Attribute attribute = Attribute.GetCustomAttribute(pInfo, typeof(HideInExcelAttribute));
        //                if (attribute is HideInExcelAttribute nonUpdateableAttribute)
        //                {
        //                    return !nonUpdateableAttribute.HideInExcel;
        //                }
        //                else
        //                {
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}
