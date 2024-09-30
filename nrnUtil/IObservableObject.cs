using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace nrnUtil
{
    public interface IObservableObject
    {
        [HideInExcel(true)]
        [NonUpdateableAttribute(true)]
        [DataMember]
        public int rowtype { get; set; }
        [HideInExcel(true)]
        [NonUpdateableAttribute(true)]
        [DataMember]
        public int RowState { get; set; }
        [HideInExcel(true)]
        [NonUpdateableAttribute(true)]
        [DataMember]
        public bool IsEditable { get; set; }
        [HideInExcel(true)]
        [NonUpdateableAttribute(true)]
        [DataMember]
        public bool IsExpanded { get; set; }

        public void Update(object source, PropertyChangedEventHandler propertyChangedEventHandler = null);
        public void Update(string PropertyName, string Value, PropertyChangedEventHandler propertyChangedEventHandler = null);
    }
}
