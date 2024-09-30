using System.Collections.Generic;
using System.Windows.Input;

namespace nrnUtil
{
    public class NrnErrorType
    {
        public static NrnErrorType GetWAError(string res = "")
        {
            if (res != "")
                res = "\r\n" + res;
            NrnErrorType error = new NrnErrorType("Fehler beim Serverzugriff" + res, "ERROR_WEBAPI", Severity.SERVER_ERROR);
            error.AddBtn(ErrorButton.GetTerminationButton());
            return error;
        }


        public NrnErrorType(string validationMessage, string msg_key, Severity severity = Severity.VALIDATION_ERROR)
        {
            this.ValidationMessage = validationMessage;
            this.ToolTipText = validationMessage;
            this.MessageBoxText = validationMessage;

            this.Severity = severity;
            this.MSG_KEY = msg_key;
        }

        public void AddBtn(ErrorButton btn)
        {
            btns.Add(btn.Bezeichnung, btn);
        }
        public void AddBtn(string bezeichnung, ICommand command)
        {
            btns.Add(bezeichnung, new ErrorButton(bezeichnung, command));
        }
        public void RemoveBtn(string bezeichnung)
        {
            if (btns != null)
            {
                if (btns.ContainsKey(bezeichnung))
                {
                    btns.Remove(bezeichnung);
                }
            }
        }
        public Dictionary<string, ErrorButton> btns = new Dictionary<string, ErrorButton>();
        public string MSG_KEY { get; private set; }
        public Severity Severity { get; private set; }
        // todo: gefällt mir noch nicht
        public object message { get; set; }

        public string ValidationMessage { get; private set; }
        public string MessageBoxText { get; set; }
        public object ToolTipText { get; set; }


    }
}