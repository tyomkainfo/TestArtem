using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace nrnUtil
{
    public class ObservableErrorObject : INotifyDataErrorInfo
    {
        private readonly INrnLogger _logger;


        private Dictionary<String, List<NrnErrorType>> errors = new Dictionary<string, List<NrnErrorType>>();
        private ReaderWriterLockSlim dictLock = new ReaderWriterLockSlim();


        public ObservableErrorObject()
        {
            _logger = NrnNoLogger.Instance;
        }

        // Adds the specified error to the errors collection if it is not 
        // already present, inserting it in the first position if isWarning is 
        // false. Raises the ErrorsChanged event if the collection changes. 
        public void AddError(string propertyName, NrnErrorType error)
        {
            try
            {
                dictLock.EnterWriteLock();
                if (!errors.ContainsKey(propertyName))
                    errors.Add(propertyName, new List<NrnErrorType>());
                if (error.Severity == Severity.UPDATE_ERROR)
                {
                    NrnErrorType errType = errors[propertyName].FirstOrDefault(x => x != null && x.Severity == Severity.UPDATE_ERROR);
                    if (errType != null)
                    {
                        errors[propertyName].Remove(errType);
                        errors[propertyName].Insert(0, error);
                        RaiseErrorsChanged(propertyName);
                    }
                    else
                    {
                        errors[propertyName].Insert(0, error);
                        RaiseErrorsChanged(propertyName);
                    }
                }
                else
                {
                    NrnErrorType errType = errors[propertyName].FirstOrDefault(x => x != null && x.MSG_KEY == error.MSG_KEY);
                    if (errType == null)
                    {
                        errors[propertyName].Insert(0, error);
                        RaiseErrorsChanged(propertyName);
                    }
                    else
                    {
                        errors[propertyName].Remove(errType);
                        errors[propertyName].Insert(0, error);
                        RaiseErrorsChanged(propertyName);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                dictLock.ExitWriteLock();
            }
        }

        // Removes the specified error from the errors collection if it is
        // present. Raises the ErrorsChanged event if the collection changes.
        public void RemoveError(string propertyName, string msg_key)
        {
            try
            {
                if (!string.IsNullOrEmpty(propertyName))
                {
                    if (errors.ContainsKey(propertyName))
                    {
                        List<NrnErrorType> tmpList = errors[propertyName];
                        if (tmpList != null && tmpList.Count > 0)
                        {
                            NrnErrorType cErrorType = errors[propertyName].FirstOrDefault(x => x != null && x.MSG_KEY.Equals(msg_key));
                            if (cErrorType != null)
                            {
                                errors[propertyName].Remove(cErrorType);
                                if (errors.ContainsKey(propertyName) && errors[propertyName].Count == 0) errors.Remove(propertyName);
                                RaiseErrorsChanged(propertyName);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #region INotifyDataErrorInfo Members

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) ||
                !errors.ContainsKey(propertyName)) return null;
            return errors[propertyName];
        }
        public NrnErrorType GetUpdateError(string propertyName)
        {
            if (errors.ContainsKey(propertyName))
            {
                NrnErrorType cErrorType = errors[propertyName].FirstOrDefault(x => x != null && x.Severity == Severity.UPDATE_ERROR);
                if (cErrorType != null)
                {
                    return cErrorType;
                }
            }
            return null;
        }
        public List<NrnErrorType> GetAllErrors()
        {
            List<NrnErrorType> List = new List<NrnErrorType>();
            foreach (KeyValuePair<String, List<NrnErrorType>> errorList in errors)
            {
                if (errorList.Value is List<NrnErrorType> errorList2)
                {
                    foreach (NrnErrorType errorType in errorList2)
                    {
                        List.Add(errorType);
                    }
                }
            }
            return List;
        }
        public bool HasErrors
        {
            get
            {
                return errors.Any();
            }
        }

        #endregion
        /// <summary>
        /// Gibt alle Fehler einer Property zurück.
        /// </summary>
        /// <param name="Propertyname"></param>
        /// <returns></returns>
        public bool HasAnyError(string Propertyname)
        {
            if (String.IsNullOrEmpty(Propertyname) || !errors.ContainsKey(Propertyname))
            {
                return false;
            }
            List<NrnErrorType> errList = errors[Propertyname];
            if (errList == null)
            {
                return false;
            }
            return (errList.Count > 0);
        }
        /// <summary>
        /// Gibt nur Fehler zurück die mit Severity.UPDATE gekennzeichnet wurden.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool HasUpdateErrors(string propertyName)
        {
            return HasError(propertyName, Severity.UPDATE_ERROR);
        }
        public bool HasValidationError(string PropertyName)
        {
            if (errors.ContainsKey(PropertyName))
            {
                NrnErrorType cErrorType = errors[PropertyName].FirstOrDefault(x => x != null && x.Severity == Severity.VALIDATION_ERROR || x.Severity == Severity.VALIDATION_WARNING);
                if (cErrorType != null)
                    return true;
            }
            return false;
        }
        public bool HasValidationError()
        {
            foreach (string property in errors.Keys)
            {
                if (errors[property].Where(x=>x.Severity != Severity.VALIDATION_INFO).Any())
                    return true;
            } 
            return false;
        }
        public bool HasError(string PropertyName, Severity severity)
        {
            try
            {
                if (!string.IsNullOrEmpty(PropertyName))
                {
                    if (errors.ContainsKey(PropertyName))
                    {
                        List<NrnErrorType> tmpList = errors[PropertyName];
                        if (tmpList != null && tmpList.Count > 0)
                        {
                            NrnErrorType cErrorType = tmpList.FirstOrDefault(x => x != null && x.Severity == severity);
                            if (cErrorType != null)
                                return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

    }
}
