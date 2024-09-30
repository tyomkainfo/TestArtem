using System;

namespace nrnUtil
{
    public interface INrnLogger
    {
        string Logpath { get; set; }
        string DefaultArea { get; set; }
        string DefaultLogname { get; set; }
        NrnLogLevel CurrentLevel { get; set; }
        LogEvent OnSendMessages { get; set; }
        string User { get; set; }
        string Workstation { get; set; }
        void Log(string what, NrnLogLevel level = NrnLogLevel.Info,string area="",bool needOwnLog = false);
        void SendNotification(string recipient, string subject, string msg);
        void SendNotification(string sender, string recipient, string subject, string msg);
        void CaptureException(Exception exception);
    }
}