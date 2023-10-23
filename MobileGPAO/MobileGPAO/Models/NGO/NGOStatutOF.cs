using System;
using GPAOnGo.WEBSERVICE;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    [XmlRoot("NGOStatutOF")]
    public class NGOStatutOF : INotifyPropertyChanged
    {
        const string XML_ERP_STATUT_ORIGINE = "Origine";
        const string XML_ERP_STATUT_ROWID = "Rowid";
        const string XML_ERP_STATUT_TITRE = "Titre";
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public NGOStatutOF()
        {
            this.A_Origine = string.Empty;
            this.A_ROWID = string.Empty;
            this.A_Titre = string.Empty;
            this.A_NGOStatutListe = new NGOStatutListe();
        }

        public NGOStatutOF(string pOrigine, string pROWID) :this()
        {
            this.A_Origine = pOrigine;
            this.A_ROWID = pROWID;
        }

        public NGOStatutOF(RestServiceStatut pRestServiceStatut) : this()
        {
            this.ORestServiceStatut = pRestServiceStatut;
        }


        public NGOStatutOF GetObjectNGOStatut()
        {
            try
            {
                return App._restService.GetNGOStatut(this);
            }
            catch (Exception e)
            {
                return new NGOStatutOF(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Error GetObjectERPStatut" + e.Message));
            }
        }

        string A_Origine { get; set; }
        string A_ROWID { get; set; }
        string A_Titre { get; set; }

        NGOStatutListe A_NGOStatutListe { get; set; }

        RestServiceStatut ORestServiceStatut { get; set; }

        // Accès aux variables
        [XmlAttribute(XML_ERP_STATUT_ORIGINE)]
        public string Origine
        {
            get { return A_Origine; }
            set { A_Origine = value; }
        }


        [XmlAttribute(XML_ERP_STATUT_ROWID)]
        public string ROWID
        {
            get { return A_ROWID; }
            set { A_ROWID = value; }
        }

        [XmlAttribute(XML_ERP_STATUT_TITRE)]
        public string Titre
        {
            get { return A_Titre; }
            set { A_Titre = value; }
        }

        [XmlArray(App.XML_ERP_STATUTS)]
        [XmlArrayItem(App.XML_ERP_STATUT, Type = typeof(NGOStatut))]
        public NGOStatutListe NGOStatutListe
        {
            get { return A_NGOStatutListe; }
            set { A_NGOStatutListe = value; }
        }

        [XmlElement(App.XML_STATUT)]
        public RestServiceStatut RestServiceStatut
        {
            get { return ORestServiceStatut; }
            set { ORestServiceStatut = value; }
        }

        [XmlIgnore]
        public bool AutorisationOK
        {
            get { return ORestServiceStatut.etat == EnumRestService.STATUT_APP_OK; }
        }
    }
}
