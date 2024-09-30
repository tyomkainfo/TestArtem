using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.Serialization;

namespace nrnUtil
{
    [DataContract]
    public class PropertyByString : ObservableErrorObject
    {
        public object this[string propertyName]
        {
            get { return this.GetType()?.GetProperty(propertyName)?.GetValue(this, null); }
            set
            {
                PropertyInfo pinfo = this.GetType().GetProperty(propertyName);
                if (pinfo != null)
                {
                    pinfo.SetValue(this, Convert.ChangeType(value, pinfo.PropertyType), null);
                }
                else
                {
                    throw new Exception("Property not found");
                }
            }
        }

        
    
    }

    public interface Zoomable
    {
        double GetZoomFaktor();
    }
    public interface Closeable
    {
        void DoClose();
    }
    public interface Activateable
    {
        void DoActivate();
    }
    public interface INrnMultiComboBoxItem
    {
        int cmbBoxid { get; }
        int cmbBoxIsChecked { set; get; }
    }
    public interface INrnComboBox2ColItem
    {
        string bezeichnung2 { set; get; }
    }
    public interface IColorsStateItem
    {
        int colorsstate { get; set; }

    }
    public interface IColorsStateViewmodel
    {
        int ColorsStateValue { get; set; }

    }
    public interface clItem
    {
        int rowtype { get; set; }
        void SetValuesFromRd(DbDataReader rd);
        void SetExampleValues();
        int GetId();
        string GetIdField();
        string GetTableName();

    }
    public interface sqlupdateable
    {
        string getInsertSql();
        string getUpdateSql();

    }

    public interface clItemList<T> where T : class, clItem
    {
        SortableBindingList<T> lst { get; set; }
    }
    [DataContract]
    public class dc_clList<T> : clItemList<T>, IDisposable where T : class, clItem, new()
    {
        [DataMember] //so you can use WCF!
        public SortableBindingList<T> lst { get; set; }
        public dc_clList()
        {
            lst = new SortableBindingList<T>();
        }

        public static dc_clList<T> GetExample()
        {
            dc_clList<T> tmp = new dc_clList<T>();
            for (int i = 0; i < 100; i++)
            {
                T obj = new T();
                obj.SetExampleValues();
                tmp.lst.Add(obj);
            }

            return tmp;
        }
        public int getIndexOfId(int id)
        {
            int cnt = -1;
            foreach (T obj in lst)
            {
                cnt++;
                if (obj.GetId() == id)
                    return cnt;
            }
            return -1;
        }
        public T getById(int id)
        {
            int cnt = -1;
            foreach (T obj in lst)
            {
                cnt++;
                if (obj.GetId() == id)
                    return obj;
            }
            return null;
        }

        public void Dispose()
        {
            
        }
    }

}