using System;
using GPAOnGo.WEBSERVICE;
using GPAOnGo.RFID;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    [XmlRoot(App.XML_PARA_USER)]
    public class Utilisateur : INotifyPropertyChanged
    {
        const string XML_PARA_USER_ID = "user_id";
        const string XML_PARA_USER_PASSWORD = "user_password";
        const string XML_PARA_CODE_GPAO = "user_code_gpao";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Utilisateur()
        {
            this.A_Identification = string.Empty;
            this.A_Password = string.Empty;
            this.A_CodeGPAO = string.Empty;
            this.OScanParametre = new Scan();
            this.OTagListeNegatif = new TagListe();
            this.ORestServiceStatut = new RestServiceStatut();
        }

        public Utilisateur(string pIdentification, string pPassword) : this()
        {
            if (pIdentification.Contains("$"))
            {
                this.A_Identification = pIdentification;
            }
            else
            {
                this.A_Identification = pIdentification.Trim()+ "$dmnbtb";
            }
            this.A_Password = pPassword;
            OnPropertyChanged("Identification");
            OnPropertyChanged("Password");
        }

        public Utilisateur(RestServiceStatut pRestServiceStatut) : this()
        {
            this.ORestServiceStatut = pRestServiceStatut;
        }


        public Utilisateur GetUtilisateur()
        {
            try
            {
                return App._restService.GetUser(this);
            }
            catch (Exception e)
            {
                return new Utilisateur(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Error GetUtilisateur" + e.Message));
            }
        }


        public RestServiceStatut ChangeUserPassword(string pNewPassword)
        {
            try
            {
                return  App._restService.ChangeUserPassword(this, pNewPassword);
            }
            catch (Exception e)
            {
                return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Error ChangeUserPassword" + e.Message);
            }
        }

        // ID de l'utilisateur
        string A_Identification { get; set; }
        string A_Password { get; set; }
        string A_CodeGPAO { get; set; }
        string A_ConnexionBase { get; set; }
        Scan OScanParametre { get; set; }
        
        //ERPObjectGroupeListe oERPObjectGroupeListe { get; set; }

        RestServiceStatut ORestServiceStatut { get; set; }

        TagListe OTagListeNegatif { get; set; }

        // Accès aux variables
        [XmlAttribute(XML_PARA_USER_ID)]
        public string Identification
        {
            get { return A_Identification; }
            set { A_Identification = value; OnPropertyChanged("Identification");
            }
        }

        [XmlAttribute(XML_PARA_USER_PASSWORD)]
        public string Password
        {
            get { return A_Password; }
            set { A_Password = value; OnPropertyChanged("Password");}
        }

        [XmlElement(XML_PARA_CODE_GPAO)]
        public string CodeGPAO
        {
            get { return A_CodeGPAO; }
            set { A_CodeGPAO = value; }
        }

        [XmlIgnore]
        public bool ApplicationOK
        {
            get { return (ORestServiceStatut.etat == EnumRestService.STATUT_APP_OK); }
        }

        [XmlElement(App.XML_RFID_PARA)]
        public Scan ScanParametre
        {
            get { return OScanParametre; }
            set { OScanParametre = value; }
        }

        [XmlElement(App.XML_STATUT)]
        public RestServiceStatut RestServiceStatut
        {
            get { return ORestServiceStatut; }
            set { ORestServiceStatut = value; }
        }

        [XmlIgnore]
        public TagListe TagListeNegatif
        {
            get { return OTagListeNegatif; }
            set { OTagListeNegatif = value; }
        }
    }
}
