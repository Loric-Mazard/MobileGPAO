using GPAOnGo.RFID;
using GPAOnGo.UTILITAIRE;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPAOnGo.IHM.RFID
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IHMScanRFIDToList : ContentPage
	{

        private TagListe tagliste;

        public BindingEvenement bindingEvenement_Statut_Scan = new BindingEvenement(string.Empty);
        public BindingEvenement bindingEvenement_Statut_NbTagScan = new BindingEvenement("0");

        bool IsScanEnCours = false;

        public IHMScanRFIDToList()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;

            tagliste = new TagListe();

            Subscribe();

            bindingEvenement_Statut_Scan.Message = string.Empty;
            bindingEvenement_Statut_NbTagScan.Message = "0";

            lbl_Statut_Scan.SetBinding(Label.TextProperty, new Binding("Message", source: bindingEvenement_Statut_Scan, mode: BindingMode.OneWay, stringFormat: "{0}"));
            lbl_Statut_NbTagScan.SetBinding(Label.TextProperty, new Binding("Message", source: bindingEvenement_Statut_NbTagScan, mode: BindingMode.OneWay, stringFormat: "Qtt Tag Scan: {0}"));

            App._pistolet.ModeRFID();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //your code here;
            UnSubscribe();
        }

        public void Subscribe()
        {
            MessagingCenter.Subscribe<String>(bindingEvenement_Statut_Scan, App.Subscribe_ScanRFID_Statut, (pStatut) =>
            {
                bindingEvenement_Statut_Scan.Message = pStatut;
            });
            MessagingCenter.Subscribe<String>(bindingEvenement_Statut_NbTagScan, App.Subscribe_ScanRFID_NbTag, (pNbPieceScannees) =>
            {
                bindingEvenement_Statut_NbTagScan.Message = pNbPieceScannees;
            });
            MessagingCenter.Subscribe<String>(this, App.Subscribe_ScanRFIDChange, (pStatut) =>
            {
                this.SubscribeSwitch(pStatut, 0, null);
            });
            MessagingCenter.Subscribe<String, UInt16>(this, App.Subscribe_ScanRFIDChange, (pStatut, pUInt16) =>
            {
                this.SubscribeSwitch(pStatut, pUInt16, null);
            });
            MessagingCenter.Subscribe<String, TagListe>(this, App.Subscribe_ScanRFIDChange, (pStatut, pTagListeTo) =>
            {
                this.SubscribeSwitch(pStatut, 0, pTagListeTo);
            });
        }

        private void SubscribeSwitch(string pStatut, UInt16 pUInt16, TagListe pTagListeTo)
        {
            switch (pStatut)
            {
                case App.Subscribe_ScanRFIDChange_SwitchToAuto:
                    App._pistolet.StartScanRFIDAuto();
                    break;
                case App.Subscribe_ScanRFIDChange_Start:
                    IsScanEnCours = true;

                    btn_Fin.IsEnabled = false;
                    btn_Fin.Text = "Scan en cours...";

                    Task.Run(() => this.Progress(1, pUInt16));
                    break;
                case App.Subscribe_ScanRFIDChange_Stop:
                    IsScanEnCours = false;

                    btn_Fin.IsEnabled = true;
                    btn_Fin.Text = "Valider";

                    Task.Run(() => this.Progress(0, 0));
                    break;
                case App.Subscribe_ScanRFIDChange_NewTagListe:
                    tagliste = pTagListeTo;
                    break;
            }
        }

        private void Progress(Int32 pTo, uint pDuree)
        {
            pgb_NbTagScan.Progress = 0;
            pgb_NbTagScan.ProgressTo(pTo, pDuree, Easing.Linear);
        }

        public void UnSubscribe()
        {

            MessagingCenter.Unsubscribe<String>(bindingEvenement_Statut_Scan, App.Subscribe_ScanRFID_Statut);
            MessagingCenter.Unsubscribe<String>(bindingEvenement_Statut_NbTagScan, App.Subscribe_ScanRFID_NbTag);
            MessagingCenter.Unsubscribe<String>(this, App.Subscribe_ScanRFIDChange);
            MessagingCenter.Unsubscribe<String, UInt16>(this, App.Subscribe_ScanRFIDChange);
            MessagingCenter.Unsubscribe<String, TagListe>(this, App.Subscribe_ScanRFIDChange);

        }
        
        private void onbtn_FinClicked(object sender, EventArgs e)
        {
            if (!IsScanEnCours)
            {
                //await Navigation.PopAsync();
                MessagingCenter.Send(App.Subscribe_pageScanRFIDToList_NewTagListe, App.Subscribe_pageScanRFIDToList_NewTagListe, tagliste);
                Navigation.PopAsync().GetAwaiter();
            }
        }


        protected override bool OnBackButtonPressed()
        {
            if (!IsScanEnCours)
            {
                //IsPopAsync = true;
                MessagingCenter.Send(App.Subscribe_pageScanRFIDToList_NewTagListe, App.Subscribe_pageScanRFIDToList_NewTagListe, tagliste);
                Navigation.PopAsync().GetAwaiter();
                return base.OnBackButtonPressed();
            }
            return false;
        }
    }
}