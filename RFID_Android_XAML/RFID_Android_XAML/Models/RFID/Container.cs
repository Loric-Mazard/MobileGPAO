using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RFID_Android_XAML.WEBSERVICE;
using RFID_Android_XAML.GPAO;


namespace RFID_Android_XAML.RFID
{
    public class Container
    {
        public Container()
        {
            this.identificateur = "";
            this.peuxAccederApplication = false;
            this.type = "";
            this.qttAttendue = 0;
            this.tagListe = new TagListe();
        }
        public Container(string pidentificateur,bool ppeuxAccederApplication, string ptype, int pqttAttendue)
        {
            this.identificateur = pidentificateur;
            this.peuxAccederApplication = ppeuxAccederApplication;
            this.type = ptype;
            this.qttAttendue = pqttAttendue;
            this.tagListe = new TagListe();
        }

        // ID du container
        string identificateur { get; set; }
        bool peuxAccederApplication { get; set; }

        // Type de 
        string type { get; set; }

        int qttAttendue { get; set; }


        // Liste des tag dans le container
        TagListe tagListe { get; set; }

        // Accès aux variables
        public string Identificateur
        {
            get { return identificateur; }
        }
        public bool PeuxAccederApplication
        {
            get { return peuxAccederApplication; }
            set { peuxAccederApplication = value; }
        }

        public string Type
        {
            get { return type; }
        }
        public int QttAttendue
        {
            get { return qttAttendue; }
        }

        public TagListe TagListe
        {
            get { return tagListe; }
            set { tagListe = value; }
        }

        public async Task<Container> CreerContainer(Utilisateur  putilisateur,ModeAppairage pmodeAppairage, string pContainerID)
        {
            try
            {
                RestService restService = new RestService();
                return await restService.CreerContainer(putilisateur,pmodeAppairage, pContainerID);
            }
            catch (System.Net.WebException e)
            {
                return new Container();
            }
        }

        public async Task<bool> PostContainer(ModeAppairage pmodeAppairage)
        {
            try
            {
                if(!this.tagListe.IsConfigured)
                {
                    App._toastManager.LongMessage("LIST SCAN NOT INIT");
                    return false;
                }
                else if(pmodeAppairage.Valeur == 1 && this.qttAttendue !=this.tagListe.Collection.Count)
                {
                    App._toastManager.LongMessage("NB SCAN ATTENDU <> NB SCAN REEL");
                    return false;
                }
                else
                {
                    RestService restService = new RestService();
                    return await restService.PostContainer(pmodeAppairage, this);
                }
            }
            catch (System.Net.WebException e)
            {
                return false;
            }
        }
    }
}
