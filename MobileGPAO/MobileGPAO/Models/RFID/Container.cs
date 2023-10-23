using System;
using GPAOnGo.WEBSERVICE;
using GPAOnGo.NGO;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace GPAOnGo.RFID
{
    [XmlRoot(App.XML_RFID_PARA_BOX)]
    public class Container : INotifyPropertyChanged
    {
        const string XML_RFID_PARA_BOX_ID = "box_id";
        const string XML_RFID_PARA_BOX_QTY = "box_qty";
        const string XML_RFID_PARA_BOX_QTY_SCAN = "box_qty_scan";
        const string XML_RFID_PARA_BOX_MODEAPP = "box_modeapp";
        const string XML_RFID_PARA_BOX_MODEAPP_DESC = "box_modeappdesc";
        const string XML_RFID_PARA_BOX_DEJAAPP = "box_dejaapp";
        const string XML_RFID_PARA_BOX_ECARTOK = "box_ecartok";

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Container() 
        {
            this.A_Identification = "";
            this.A_QttAttendue = 0;
            this.A_Appairage_Mode = 0;
            this.A_Appairage_Mode_Description = string.Empty;
            this.A_Appairage_Mode_DejaEffectue = false;
            this.A_Appairage_Mode_DejaEffectue_Forcer = false;
            this.A_Appairage_Mode_EcartOK = false;
            this.A_Appairage_Mode_EcartOK_Forcer = false;
            this.OTagListe = new TagListe();
            this.ORestServiceStatut = new RestServiceStatut();
        }

        public Container(string pIdentification) :this()
        {
            this.A_Identification = pIdentification;
        }

        public Container(RestServiceStatut pRestServiceStatut) : this()
        {
            this.ORestServiceStatut = pRestServiceStatut;
        }

        // ID du container
        string A_Identification { get; set; }

        int A_QttAttendue { get; set; }

        int A_Appairage_Mode { get; set; }

        string A_Appairage_Mode_Description { get; set; }

        bool A_Appairage_Mode_DejaEffectue { get; set; }

        bool A_Appairage_Mode_DejaEffectue_Forcer { get; set; }

        bool A_Appairage_Mode_EcartOK { get; set; }

        bool A_Appairage_Mode_EcartOK_Forcer { get; set; }

        TagListe OTagListe { get; set; }

        RestServiceStatut ORestServiceStatut { get; set; }

        [XmlAttribute(XML_RFID_PARA_BOX_ID)]
        public string Identification
        {
            get { return A_Identification; }
            set { A_Identification = value; OnPropertyChanged("Identification"); }
        }

        [XmlIgnore]
        public bool AutorisationOK
        {
            get { return (ORestServiceStatut.etat == EnumRestService.STATUT_APP_OK ); }
        }

        [XmlElement(XML_RFID_PARA_BOX_QTY)]
        public int QttAttendue
        {
            get { return A_QttAttendue; }
            set { A_QttAttendue = value; }
        }

        [XmlElement(XML_RFID_PARA_BOX_QTY_SCAN)]
        public int QttScan
        {
            get {return OTagListe.Count(); }
        }

        [XmlElement(XML_RFID_PARA_BOX_MODEAPP)]
        public int Appairage_Mode
        {
            get { return A_Appairage_Mode; }
            set { A_Appairage_Mode = value; }
        }

        [XmlElement(XML_RFID_PARA_BOX_MODEAPP_DESC)]
        public string Appairage_Mode_Description
        {
            get { return A_Appairage_Mode_Description; }
            set { A_Appairage_Mode_Description = value; }
        }

        [XmlElement(XML_RFID_PARA_BOX_DEJAAPP)]
        public bool Appairage_Mode_DejaEffectue
        {
            get { return A_Appairage_Mode_DejaEffectue; }
            set { A_Appairage_Mode_DejaEffectue = value; }
        }

        [XmlIgnore]
        public bool Appairage_Mode_DejaEffectue_Forcer
        {
            get { return A_Appairage_Mode_DejaEffectue_Forcer; }
            set { A_Appairage_Mode_DejaEffectue_Forcer = value; }
        }

        [XmlElement(XML_RFID_PARA_BOX_ECARTOK)]
        public bool Appairage_Mode_EcartOK
        {
            get { return A_Appairage_Mode_EcartOK; }
            set { A_Appairage_Mode_EcartOK = value; }
        }

        [XmlIgnore]
        public bool Appairage_Mode_EcartOK_Forcer
        {
            get { return A_Appairage_Mode_EcartOK_Forcer; }
            set { A_Appairage_Mode_EcartOK_Forcer = value; }
        }

        [XmlIgnore]
        public TagListe TagListe
        {
            get { return OTagListe; }
            set { OTagListe = value;
                OnPropertyChanged("QttScan");
            }
        }

        [XmlElement(App.XML_STATUT)]
        public RestServiceStatut RestServiceStatut
        {
            get { return ORestServiceStatut; }
            set { ORestServiceStatut = value; }
        }


        public Container GetContainer(int pAppairage_Mode,string pAppairage_Desc, Utilisateur pUtilisateur)
        {
            try
            {
                return App._restService.GetContainer(this, pAppairage_Mode, pAppairage_Desc, pUtilisateur);
            }
            catch (Exception e)
            {
                return new Container(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Error GetContainer" + e.Message));
            }
        }

        public RestServiceStatut PostContainer(Utilisateur pUtilisateur)
        {
            try
            {
                return App._restService.PostContainer(this, pUtilisateur);
            }
            catch (Exception e)
            {
                return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, e.Message);
            }
        }
    }
}
