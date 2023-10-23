using System;
using System.Xml.Serialization;
using GPAOnGo.WEBSERVICE;

namespace GPAOnGo.NGO
{
    public class NGOStatutChoix
    {
        const string XML_ERP_STATUT_CHOIX_ORIGINE = "erp_statut_choix_origine";
        const string XML_ERP_STATUT_CHOIX_ROWID = "erp_statut_choix_rowid";
        const string XML_ERP_STATUT_CHOIX_POSITION = "erp_statut_choix_pos";
        const string XML_ERP_STATUT_CHOIX_TYPE = "erp_statut_choix_type";
        const string XML_ERP_STATUT_CHOIX_VALEUR = "erp_statut_choix_valeur";
        const string XML_ERP_STATUT_CHOIX_VALEURSTRING = "erp_statut_choix_valeurstring";
        const string XML_ERP_STATUT_CHOIX_ENABLE = "erp_statut_choix_enable";
        const string XML_ERP_STATUT_CHOIX_DISABLEDESC = "erp_statut_choix_disabledesc";
        const string XML_ERP_STATUT_CHOIX_VALEUR_MIN = "erp_statut_choix_valeur_min";
        const string XML_ERP_STATUT_CHOIX_VALEUR_MAX = "erp_statut_choix_valeur_max";

        public NGOStatutChoix()
        {
            this.A_Origine = string.Empty;
            this.A_ROWID = string.Empty;
            this.A_Position = 0;
            this.A_Type = 0;
            this.A_Valeur = string.Empty;
            this.A_ValeurString = string.Empty;
            this.A_Enable = false;
            this.A_DisableDescription = string.Empty;
            this.A_Valeur_Min = false;
            this.A_Valeur_Min = false;
        }


        public NGOStatutChoix(string pOrigine, string pROWID, int pPosition,string pValeur) :base()
        {
            this.A_Origine = pOrigine;
            this.A_ROWID = pROWID;
            this.A_Position = pPosition;
            this.A_Valeur = pValeur;
        }


        string A_Origine { get; set; }
        string A_ROWID { get; set; }
        int A_Position { get; set; }
        int A_Type { get; set; }
        string A_Valeur { get; set; }
        string A_ValeurString { get; set; }
        bool A_Enable { get; set; }
        String A_DisableDescription { get; set; }
        bool A_Valeur_Min { get; set; }
        bool A_Valeur_Max { get; set; }

        // Accès aux variables
        [XmlElement(XML_ERP_STATUT_CHOIX_ORIGINE)]
        public string Origine
        {
            get { return A_Origine; }
            set { A_Origine = value; }
        }

        [XmlElement(XML_ERP_STATUT_CHOIX_ROWID)]
        public string ROWID
        {
            get { return A_ROWID; }
            set { A_ROWID = value; }
        }

        [XmlElement(XML_ERP_STATUT_CHOIX_POSITION)]
        public int Position
        {
            get { return A_Position; }
            set { A_Position = value; }
        }

        [XmlElement(XML_ERP_STATUT_CHOIX_TYPE)]
        public int Type
        {
            get { return A_Type; }
            set { A_Type = value; }
        }


        [XmlElement(XML_ERP_STATUT_CHOIX_VALEUR)]
        public string Valeur
        {
            get { return A_Valeur; }
            set { A_Valeur = value; }
        }

        [XmlElement(XML_ERP_STATUT_CHOIX_VALEURSTRING)]
        public string ValeurString
        {
            get { return A_ValeurString; }
            set { A_ValeurString = value; }
        }

        [XmlElement(XML_ERP_STATUT_CHOIX_VALEUR_MIN)]
        public bool Valeur_Min
        {
            get { return A_Valeur_Min; }
            set { A_Valeur_Min = value; }
        }

        [XmlElement(XML_ERP_STATUT_CHOIX_VALEUR_MAX)]
        public bool Valeur_Max
        {
            get { return A_Valeur_Max; }
            set { A_Valeur_Max = value; }
        }

        [XmlElement(XML_ERP_STATUT_CHOIX_ENABLE)]
        public bool Enable
        {
            get { return A_Enable; }
            set { A_Enable = value; }
        }

        [XmlElement(XML_ERP_STATUT_CHOIX_DISABLEDESC)]
        public string DisableDescription
        {
            get { return A_DisableDescription; }
            set { A_DisableDescription = value; }
        }

        public RestServiceStatut PostERPStatutChoix(Utilisateur pUtilisateur)
        {
            try
            {
                return App._restService.PostNGOStatutChoix(this, pUtilisateur);
            }
            catch (Exception e)
            {
                return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, e.Message);
            }
        }
    }
}
