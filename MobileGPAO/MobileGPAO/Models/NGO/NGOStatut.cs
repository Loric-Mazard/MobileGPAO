using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    public class NGOStatut : INotifyPropertyChanged
    {
        const string XML_ERP_STATUT_ORIGINE = "Origine";
        const string XML_ERP_STATUT_ROWID = "Rowid";
        const string XML_ERP_STATUT_POSITION = "Position";
        const string XML_ERP_STATUT_DESC = "Desc";
        const string XML_ERP_STATUT_TYPE = "Type";

        const string XML_ERP_STATUT_DATA = "Data";
        const string XML_ERP_STATUT_DATASTRING = "Datastring";
        const string XML_ERP_STATUT_DATE = "Date";
        const string XML_ERP_STATUT_USER = "User";
        const string XML_ERP_STATUT_ENABLE = "Enable";
        const string XML_ERP_STATUT_DISABLEDESC = "Disabledesc";

        public enum enumStatutType
        {
            [XmlEnum(Name = "0")]
            NA = 0,
            [XmlEnum(Name = "1")]
            Boolean = 1,
            [XmlEnum(Name = "2")]
            Pourcentage = 2,
            [XmlEnum(Name = "3")]
            TissusPrincCoupe = 3,
            [XmlEnum(Name = "4")]
            ListeChoix = 4,
            [XmlEnum(Name = "5")]
            TextLibre = 5,
            [XmlEnum(Name = "6")]
            PourcentageLibre = 6,
            [XmlEnum(Name = "7")]
            TissusCoupe = 7,
            [XmlEnum(Name = "8")]
            Emballage = 8
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public NGOStatut() 
        {
            this.A_Origine = string.Empty;
            this.A_ROWID = string.Empty;
            this.A_Position = 0;
            this.A_Description = string.Empty;
            this.A_Type =enumStatutType.NA;
            this.A_Data = string.Empty;
            this.A_DataString = string.Empty;
            this.A_Date = string.Empty;
            this.A_User = string.Empty;
            this.A_Enable = false;
            this.A_DisableDescription = string.Empty;

            this.oERPStatutChoixListe = new NGOStatutChoixListe();
        }

        string A_Origine { get; set; }
        string A_ROWID { get; set; }
        int A_Position { get; set; }
        string A_Description { get; set; }
        enumStatutType A_Type { get; set; }
        string A_Data { get; set; }
        string A_DataString { get; set; }
        String A_Date { get; set; }
        String A_User { get; set; }
        bool A_Enable { get; set; }
        String A_DisableDescription { get; set; }
        NGOStatutChoixListe oERPStatutChoixListe { get; set; }

        // Accès aux variables
        [XmlElement(XML_ERP_STATUT_ORIGINE)]
        public string Origine
        {
            get { return A_Origine; }
            set { A_Origine = value; }
        }

        [XmlElement(XML_ERP_STATUT_ROWID)]
        public string ROWID
        {
            get { return A_ROWID; }
            set { A_ROWID = value; }
        }

        [XmlElement(XML_ERP_STATUT_POSITION)]
        public int Position
        {
            get { return A_Position; }
            set { A_Position = value; }
        }

        [XmlElement(XML_ERP_STATUT_DESC)]
        public string Description
        {
            get { return A_Description; }
            set { A_Description = value; }
        }

        [XmlElement(XML_ERP_STATUT_TYPE)]
        public enumStatutType Type
        {
            get { return A_Type; }
            set { A_Type = value; }
        }

        [XmlElement(XML_ERP_STATUT_DATA)]
        public string Data
        {
            get { return A_Data; }
            set { A_Data = value; OnPropertyChanged("Data");}
        }

        [XmlElement(XML_ERP_STATUT_DATASTRING)]
        public string DataString
        {
            get { return A_DataString; }
            set { A_DataString = value; OnPropertyChanged("DataString");}
        }

        [XmlElement(XML_ERP_STATUT_DATE)]
        public string Date
        {
            get { return A_Date; }
            set { A_Date = value; OnPropertyChanged("Date"); }
        }

        [XmlElement(XML_ERP_STATUT_USER)]
        public string User
        {
            get { return A_User; }
            set { A_User = value; OnPropertyChanged("User");}
        }

        [XmlElement(XML_ERP_STATUT_ENABLE)]
        public bool Enable
        {
            get { return A_Enable; }
            set { A_Enable = value; OnPropertyChanged("Enable");}
        }

        [XmlElement(XML_ERP_STATUT_DISABLEDESC)]
        public string DisableDescription
        {
            get { return A_DisableDescription; }
            set { A_DisableDescription = value; OnPropertyChanged("DisableDescription");}
        }

        [XmlArray(App.XML_ERP_STATUT_CHOIXS)]
        [XmlArrayItem(App.XML_ERP_STATUT_CHOIX, Type = typeof(NGOStatutChoix))]
        public NGOStatutChoixListe ERPStatutChoixListe
        {
            get { return oERPStatutChoixListe; }
            set { oERPStatutChoixListe = value; }
        }
    }
}
