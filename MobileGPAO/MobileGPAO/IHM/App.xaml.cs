using GPAOnGo.NGO;
using GPAOnGo.IHM;
using GPAOnGo.IHM.ERP;
using GPAOnGo.IHM.RFID;
using GPAOnGo.IHM.UTILISATEUR;
using GPAOnGo.PISTOLET;
using GPAOnGo.UTILITAIRE;
using GPAOnGo.WEBSERVICE;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GPAOnGo
{
    public struct Constant
    {
        public static double ScreenWidth = Application.Current.MainPage.Width;
        public static double ScreenHeight = Application.Current.MainPage.Height;
    }

    public static class License
    {
        public const string Key = "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";
    }

    public partial class App : Application
    {
        public const string XML_androidGPAO_ENTETE = "cawe_androidGPAO_app";

      
        public const string XML_STATUT = "statut";

        public const string XML_PARA_USER = "user_para";
        
        public const string XML_RFID_PARA = "rfid_para";

        public const string XML_ANDROIDGPAO_MENUS = "menus";
        public const string XML_ANDROIDGPAO_MENUS_LISTE = "menus_liste";
        public const string XML_ANDROIDGPAO_MENU = "menu";

        public const string XML_RFID_APP = "cawe_rfid_app";

        public const string XML_RFID_PARA_BOX = "box_para";

        public const string XML_RFID_NBTAG = "nb_scan";
        public const string XML_RFID_TAGS= "tags";
        public const string XML_RFID_TAGS_LISTE = "tags_liste";
        public const string XML_RFID_TAG = "tag";

        public const string XML_RFID_BOX_DORFID = "box_DoRFID";
        public const string XML_ERPStatutChoix = "ERPStatutChoix";

        public const string XML_PARA_GPAOnGo = "GPAOnGo_para";

        public const string XML_OBJECTERPSTATUT_PARA = "NGOStatutOF";
        public const string XML_OBJECTERPGROUPE_PARA = "objecterpgroupe_para";
        public const string XML_OBJECTERP_PARA = "objecterp_para";

        public const string XML_ERP_STATUTS = "erp_statuts";
        public const string XML_ERP_STATUT = "erp_statut";

        public const string XML_ERP_STATUT_CHOIXS = "erp_statut_choixs";
        public const string XML_ERP_STATUT_CHOIX = "erp_statut_choix";


        public static Utilisateur _utilisateur = new Utilisateur();
        public static Pistolet _pistolet = new Pistolet();
        public static RestService _restService = new RestService();
        
        public static IHMScanRFIDToList pageScanRFIDToList = new IHMScanRFIDToList();
        public static IHMScanRFIDContainer pageScanRFIDContainer = new IHMScanRFIDContainer();
        public static IHMCartonScan pageCartonScan = new IHMCartonScan();
        public static IHMObjectERPScan pageObjectERPScan = new IHMObjectERPScan();
        public static Identification pageIdentification = new Identification();
        public static ChangePassword pageChangePassword = new ChangePassword();
        public static IHMConnexion pageConnexion = new IHMConnexion();
        public static IHMERPStatutListe pageIHMERPStatutListe = new IHMERPStatutListe();
        public static IHMERPStatutChoixListe pageERPStatutChoixListe = new IHMERPStatutChoixListe();
        public static IHMERPStatut_TextLibre pageERPStatut_TextLibre = new IHMERPStatut_TextLibre();
        public static IHMERPStatut_LinearGauge pageERPStatut_LinearGauge = new IHMERPStatut_LinearGauge();
        public static IHMERPList pageERPList = new IHMERPList();
        public static IHMERPListRecherche pageERPListRecherche = new IHMERPListRecherche();
        public static IHMNGOAjoutChamps pageAjoutChamps = new IHMNGOAjoutChamps();
        public static Creation crea = new Creation();

        public const string Subscribe_ScanRFID_Statut= "Subscribe_ScanRFID_Statut";
        public const string Subscribe_ScanRFID_NbTag = "Subscribe_ScanRFID_NbTag";
        public const string Subscribe_ScanRFIDChange = "Subscribe_ScanRFIDChange";
        public const string Subscribe_ScanRFIDChange_Start = "Start";
        public const string Subscribe_ScanRFIDChange_Stop = "Stop";
        public const string Subscribe_ScanRFIDChange_SwitchToAuto = "SwitchToAuto";
        public const string Subscribe_ScanRFIDChange_NewTagListe = "NewTagListe";
        public const string Subscribe_pageScanRFIDToList_NewTagListe = "Subscribe_pageScanRFIDToList_NewTagListe";
        public const string Subscribe_ScanCodeBarre_Tag = "Subscribe_GetScanCodeBarre";
        public const string Subscribe_Connexion_Pistolet = "Subscribe_Connexion_Pistolet";
        public const string Subscribe_Identification_Utilisateur = "Subscribe_Identification_Utilisateur";
        public const string Subscribe_ObjectERPRecherche = "Subscribe_ObjectERPRecherche";

        public const string Type_CAB_Container = "CB";
        public const string Type_CAB_Utilisateur = "USR";
        public const string Type_CAB_OF = "OF";
        
        public const string CONNEXION_BASE_PROD = "Base Production";
        public const string CONNEXION_BASE_TEST = "Base Test";

        public const string Type_CAB_Unknow = "N/A";

        // Interface Manager
        public static IToast _toastManager = DependencyService.Get<IToast>();

        public App()
        { 
            InitializeComponent();

            //C1.Xamarin.Forms.Core.LicenseManager.Key = License.Key;

            MainPage = new CustomNavigationPage(new IHMWelcome());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private new void DescendantAdded()
        {
            //
        }
        private new void DescendantRemoved()
        {
            //
        }

    }
}
