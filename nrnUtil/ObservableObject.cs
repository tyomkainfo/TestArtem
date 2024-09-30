using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Input;

namespace nrnUtil
{
    /// <summary>
    /// Encapsulates INotifyPropertyChanged implementation for View Models to use
    /// </summary>
    [DataContract]
    public class ObservableObject : PropertyByString, INotifyPropertyChanged, IObservableObject
    {
        private readonly INrnLogger _logger;
        public ObservableObject()
        {
            _logger = NrnNoLogger.Instance;
        }

        [HideInExcel(true)]
        [NonUpdateableAttribute(true)]
        [DataMember]
        public int rowtype { get; set; }

        private int _RowState = 0;
        [HideInExcel(true)]
        [NonUpdateableAttribute(true)]
        [DataMember]
        public int RowState
        {
            get
            {
                return _RowState;
            }
            set
            {
                if (value == _RowState) return;
                _RowState = value;
                RaisePropertyChanged(() => this.RowState);
            }
        }
        private bool _IsEditable = true;
        [HideInExcel(true)]
        [NonUpdateableAttribute(true)]
        [DataMember]
        public bool IsEditable
        {
            get
            {
                return _IsEditable;
            }
            set
            {
                if (value == _IsEditable) return;
                _IsEditable = value;
                RaisePropertyChanged(() => this.IsEditable);
            }
        }
        private bool _IsExpanded = false;
        [HideInExcel(true)]
        [NonUpdateableAttribute(true)]
        [DataMember]
        public bool IsExpanded
        {
            get
            {
                return _IsExpanded;
            }
            set
            {
                if (value == _IsExpanded) return;
                _IsExpanded = value;
                RaisePropertyChanged(() => this.IsExpanded);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed
        /// </summary>
        /// <param name="propertyExpression">property</param>
        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            var body = propertyExpression.Body as MemberExpression;
            if (body != null)
            {
                var property = body.Member as PropertyInfo;
                if (property != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property.Name));
                }
            }
        }
        public void RaisePropertyChange(string PropertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected void MultiPropChange(string[] props)
        {
            foreach (string prop in props)
                OnPropertyChanged(prop);
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public void Update(object source, PropertyChangedEventHandler propertyChangedEventHandler = null)
        {
            if (source == null)
                return;
            if (source.GetType() != this.GetType())
                throw new ArgumentException("Der Parameter source muss von gleichen Type sein");
            PropertyInfo[] myObjectFields = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo fi in myObjectFields)
            {

                if (propertyChangedEventHandler != null)
                {
                    this.PropertyChanged -= propertyChangedEventHandler;
                }
                if (fi.Name.Equals("Item"))
                    continue;
                if (fi.CanWrite)
                    fi.SetValue(this, fi.GetValue(source));
                if (propertyChangedEventHandler != null)
                {
                    this.PropertyChanged += propertyChangedEventHandler;
                }
            }
        }

        public void Update(string PropertyName, string Value, PropertyChangedEventHandler propertyChangedEventHandler = null)
        {
            if (propertyChangedEventHandler != null)
            {
                this.PropertyChanged -= propertyChangedEventHandler;
            }
            PropertyInfo pinfo = this.GetType().GetProperty(PropertyName);
            if (pinfo != null)
            {
                pinfo.SetValue(this, Convert.ChangeType(Value, pinfo.PropertyType), null);
            }
            if (propertyChangedEventHandler != null)
            {
                this.PropertyChanged += propertyChangedEventHandler;
            }
        }

    }
}
