using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace nrnUtil
{
    public interface I_settings
    {
        INrnLogger _logger { get; set; }
        int Fontsize { get; }
        double ZoomFaktor { get; set; }
        string Workstation { get; }
        string User { get; }
        int UserId { get; set; }
        int LoginId { get; set; }
        string Winuser { get; }
        string Password { get; }
        string HashWert { get; }
        bool Ishalle { get; }
        bool IsMaarten { get; }
        bool Isonemodule { get; }
        string Modulename { get; set; }
        bool Singleton { get; }
        bool Uebernahmemodus { get; set; }
        string Overrideprinter { get; }
        bool Forceprintpreview { get; }
        bool IskalcMode { get; }
        string Appserver { get; set; }
        string Messagingurl { get; set; }
        string MessagingurlSignalR { get; set; }
        string WebShopUrl { get; set; }
        int StellenEinzelpreis { get; set; }
        int Mandant { get; set; }
        string pfadtoppoint { get; set; }
        string Configfile { get; set; }



        /// <summary>
        ///  Globale Einstellung die anzahl der 
        ///  nachkommastellen beim runden von einzelpreisen
        /// </summary>
        int RoundEinzelpreisDecimals { get; }

        string Credentials { get; set; }
        int JumpPoint { get; }
        int JumpValue { get; }
        void LoadFromIni();
        int angebot_jahr { get; set; }
        Guid myGuid { get; set; }

        void SetSettings(int numdigits_einzelpreis, string MessagingServer, string MessagingServerSignalR,
            string _webShopUrl, string rmqexchange, string rabbitserver, string rabbituser, string rabbitpass,string mailserver);

        string dbserver { get; set; }
        string UserEmail { get; set; }
        string RMQExchange { get; set; }

        string RabbitUser { get; set; }
        string RabbitPassword { get; set; }
        string RabbitServer { get; set; }
        string BestellBemerkung { get; set; }
        string BestellZeit { get; set; }

        string MailServer { get; set; }

    }

}
