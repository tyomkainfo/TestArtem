using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace nrnUtil
{
    public enum NrnLogLevel
    {
        Critical, Error, Warning, Info
    }
    public delegate void LogEvent(string what, NrnLogLevel level, string area, string user, string workstation);
    public class NrnLogger : INrnLogger
    {
        private readonly object Lock = new object();

        public string Logpath { get; set; }
        public NrnLogLevel CurrentLevel { get; set; }
        public LogEvent OnSendMessages { get; set; }
        public string User { get; set; }
        public string Workstation { get; set; }
        public string DefaultArea { get; set; }
        public string DefaultLogname { get; set; }
        public NrnLogger(string path, NrnLogLevel level)
        {
            Logpath = path;
            if (!Directory.Exists(Logpath))
                Directory.CreateDirectory(Logpath);
            CurrentLevel = level;
            DefaultArea = "general";
            DefaultLogname = "Log";
            DelOldLogs();
        }

        private void DelOldLogs()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string[] files = Directory.GetFiles(Logpath);
                    foreach (var filePath in files)
                    {
                        string filename = Path.GetFileNameWithoutExtension(filePath);
                        if (!string.IsNullOrEmpty(filename) && filename.Length > 10)
                        {
                            string date = filename.Substring(filename.Length - 10, 10);
                            if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out DateTime datum))
                            {
                                if (datum < DateTime.Now.AddDays(-30))
                                {
                                    File.Delete(filePath);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            });
        }
        public void Log(string what, NrnLogLevel level = NrnLogLevel.Info, string area = "", bool needOwnLog = false)
        {
            lock (Lock)
            {
                try
                {
                    string datum = "_" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2");
                    string logdatei =
                        Path.Combine(Logpath, ((area == "") || (needOwnLog == false)) ? DefaultLogname : area) +
                        ((User != null) ? User : "") + datum + ".csv";
                    foreach (string zeile in what.Replace("\r", "").Split('\n'))
                    {
                        string content = DateTime.Now.ToString() + ";" + Workstation + ";" + User + ";" +
                                         ((area == "") ? DefaultArea : area) + ";" + Convert.ToString(level) + ";" +
                                         zeile + "\r\n";
                        File.AppendAllText(logdatei, content);
                    }

                    if (OnSendMessages != null)
                        OnSendMessages(what, level, area, User, Workstation);
                }
                catch
                {

                }
            }
        }

        public virtual void SendNotification(string recipient, string subject, string msg)
        {
        }

        public virtual void CaptureException(Exception exception)
        {
        }

        public virtual void SendNotification(string sender, string recipient, string subject, string msg)
        {
            
        }
    }

    public class NrnNoLogger : INrnLogger
    {
        public static INrnLogger Instance = new NrnNoLogger("", NrnLogLevel.Info);
        private readonly object Lock = new object();
        public string Logpath { get; set; }
        public NrnLogLevel CurrentLevel { get; set; }
        public LogEvent OnSendMessages { get; set; }
        public string User { get; set; }
        public string Workstation { get; set; }
        public string DefaultArea { get; set; }
        public string DefaultLogname { get; set; }

        public NrnNoLogger(string path, NrnLogLevel level)
        {
            Logpath = path;
            CurrentLevel = level;
        }

        public void Log(string what, NrnLogLevel level = NrnLogLevel.Info, string area = "", bool needOwnLog = false)
        {

        }

        public void SendNotification(string recipient, string subject, string msg)
        {
            
        }

        public void CaptureException(Exception exception)
        {
            throw exception;
        }

        public void SendNotification(string sender, string recipient, string subject, string msg)
        {
            //
        }
    }
}
