using System;
using System.Xml.Serialization;
using GPAOnGo.WEBSERVICE;
using Xamarin.Forms;

namespace GPAOnGo.UTILITAIRE
{
    [XmlRoot(App.XML_PARA_GPAOnGo)]
    public class AppGPAOnGo
    {
        const string XML_PARA_GPAOnGo_VERSION = "GPAOnGo_Version";

        public AppGPAOnGo()
        {
            this.A_Version = string.Empty;
            this.ORestServiceStatut = new RestServiceStatut();
        }

        public AppGPAOnGo(RestServiceStatut pRestServiceStatut) : this()
        {
            this.ORestServiceStatut = pRestServiceStatut;
        }

        string A_Version { get; set; }

        RestServiceStatut ORestServiceStatut { get; set; }

        [XmlElement(XML_PARA_GPAOnGo_VERSION)]
        public string Version
        {
            get { return A_Version; }
            set { A_Version = value; }
        }

        [XmlElement(App.XML_STATUT)]
        public RestServiceStatut RestServiceStatut
        {
            get { return ORestServiceStatut; }
            set { ORestServiceStatut = value; }
        }

        [XmlIgnore]
        public bool ApplicationOK
        {
            get { return (ORestServiceStatut.etat == EnumRestService.STATUT_APP_OK); }
        }

        public AppGPAOnGo GetGPAOnGo()
        {
            try
            {
                return App._restService.GetGPAOnGo();
            }
            catch (Exception e)
            {
                return new AppGPAOnGo(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Error GetGPAOnGo" + e.Message));
            }
        }

        public string GetApkVersion()
        {
            return DependencyService.Get<IAppVersion>().GetVersion();
        }
       
    }
}
