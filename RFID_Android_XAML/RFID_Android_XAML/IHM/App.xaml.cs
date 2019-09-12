using RFID_Android_XAML.WEBSERVICE;
using RFID_Android_XAML.UTILITAIRE;
using RFID_Android_XAML.GPAO;
using RFID_Android_XAML.PISTOLET;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RFID_Android_XAML
{
    public partial class App : Application
    {

        public static Page page_Connexion;
        public static Page page_Identification;
        public static Page page_ModeChoix;
        public static Page page_CartonScan;
        public static Page page_ScanRFIDContainer;
        public static Page page_ScanRFIDToList;

        public static Utilisateur _utilisateur = new Utilisateur();
        public static Pistolet _pistolet = new Pistolet();

        public const string Subscribe_NbTag = "Subscribe_NbTag";
        public const string Subscribe_Statut = "Subscribe_Statut";
        public const string Subscribe_ScanRFIDEnd = "Subscribe_ScanRFIDEnd";
        public const string Subscribe_ScanRFIDStart = "Subscribe_ScanRFIDStart";
        public const string Subscribe_ScanRFID_TagListe = "Subscribe_ScanRFID_TagListe";
        public const string Subscribe_ScanRFID_TagListe_FromPage = "Subscribe_ScanRFID_TagListe_FromPage";
        public const string Subscribe_ScanCodeBarre_Tag = "Subscribe_GetScanCodeBarre";
        public const string Subscribe_Connexion_Pistolet = "Subscribe_Connexion_Pistolet";
        public const string Subscribe_Identification_Utilisateur = "Subscribe_Identification_Utilisateur";

        public const string Type_CAB_Container = "CB";
        public const string Type_CAB_Utilisateur = "USR";
        public const string Type_CAB_Unknow = "N/A";

        // Interface Manager
        public static IToast _toastManager = DependencyService.Get<IToast>();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Welcome());
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
