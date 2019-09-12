using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RFID_Android_XAML.WEBSERVICE;
using RFID_Android_XAML.RFID;

namespace RFID_Android_XAML.GPAO
{
    public class Utilisateur
    {
        public Utilisateur()
        {
            this.identification = "";
            this.peuxAccederApplication = false;
            this.listeModeAppairage = new List<ModeAppairage>();
            this.scanParametre = new Scan();
            this.tagListeNegatif = new TagListe();
        }

        public Utilisateur(string pidentification, bool ppeuxAccederApplication, ref List<ModeAppairage> plisteModeAppairage, ref Scan pScanParametre)
        {
            this.identification = pidentification;
            this.peuxAccederApplication = ppeuxAccederApplication;
            this.listeModeAppairage = plisteModeAppairage;
            this.ScanParametre = pScanParametre;
            this.tagListeNegatif = new TagListe();
        }


        public Utilisateur(ref Utilisateur utilisateur)
        {
            this.identification = utilisateur.Identification;
            this.peuxAccederApplication = utilisateur.peuxAccederApplication;
            this.listeModeAppairage = utilisateur.ListeModeAppairage;
            this.tagListeNegatif = utilisateur.tagListeNegatif;
        }

        // ID de l'utilisateur
        string identification { get; set; }

        bool peuxAccederApplication { get; set; }

        // Liste des modes disponibles pour cet utilisateur
        List<ModeAppairage> listeModeAppairage { get; set; }

        Scan scanParametre { get; set; }

        TagListe tagListeNegatif { get; set; }


        // Accès aux variables
        public string Identification
        {
            get { return identification; }
            set { identification = value; }
        }
        public bool PeuxAccederApplication
        {
            get { return peuxAccederApplication; }
            set { peuxAccederApplication = value; }
        }

        public List<ModeAppairage> ListeModeAppairage
        {
            get { return listeModeAppairage; }
            set { listeModeAppairage = value; }
        }

        public Scan ScanParametre
        {
            get { return scanParametre; }
            set { scanParametre = value; }
        }


        public TagListe TagListeNegatif
        {
            get { return tagListeNegatif; }
            set { tagListeNegatif = value; }
        }

        async public Task<Utilisateur> CreerUtilisateur(string pIdentification)
        {
            try
            {
                RestService restService = new RestService();
                return await restService.CreerUtilisateur(pIdentification);
            }
            catch (System.Net.WebException e)
            {
                return new Utilisateur();
            }
        }
    }
}
