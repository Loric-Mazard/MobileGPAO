using GPAOnGo.RFID;
using GPAOnGo.UTILITAIRE;
using GPAOnGo.WEBSERVICE;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPAOnGo.IHM.RFID
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMScanRFIDContainer : ContentPage
    {
        public Container container;
        public BindingEvenement bindingEvenement_Statut_Scan = new BindingEvenement(string.Empty);
        public BindingEvenement bindingEvenement_Statut_NbTagScan = new BindingEvenement("0");

        bool IsPopAsync = false;
        bool IsScanEnCours = false;

        public IHMScanRFIDContainer()
        {
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));

            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            IsPopAsync = false;

            container = (Container)this.BindingContext;

            Subscribe();

            bindingEvenement_Statut_Scan.Message = string.Empty;
            bindingEvenement_Statut_NbTagScan.Message = "0";

            lbl_Statut_Scan.SetBinding(Label.TextProperty, new Binding("Message", source: bindingEvenement_Statut_Scan, mode: BindingMode.OneWay, stringFormat: "{0}"));

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

        private async void SubscribeSwitch(string pStatut, UInt16 pUInt16, TagListe pTagListeTo)
        {
            switch (pStatut)
            {
                case App.Subscribe_ScanRFIDChange_SwitchToAuto:
                    App._pistolet.StartScanRFIDAuto();
                    break;
                case App.Subscribe_ScanRFIDChange_Start:
                    IsScanEnCours = true;
                    await Task.Run(() => this.Progress(1, pUInt16));
                    break;
                case App.Subscribe_ScanRFIDChange_Stop:
                    IsScanEnCours = false;
                    await Task.Run(() => this.Progress(0, 0));
                    break;
                case App.Subscribe_ScanRFIDChange_NewTagListe:
                    if (!IsPopAsync)
                    {
                        container.TagListe = pTagListeTo;
                        if (container.TagListe.IsConfigured)
                        {
                            if (container.Appairage_Mode_EcartOK && !container.Appairage_Mode_EcartOK_Forcer && (container.QttAttendue != container.TagListe.Count))
                            {
                                container.Appairage_Mode_EcartOK_Forcer = await DisplayAlert("Qtt Attendu <> Qtt Scan", "Voulez-vous continuer?", "Oui", "Non");
                            }

                            if (container.Appairage_Mode_EcartOK && container.Appairage_Mode_EcartOK_Forcer && (container.QttAttendue != container.TagListe.Count) || (container.QttAttendue == container.TagListe.Count))
                            {
                                RestServiceStatut restServiceStatut = container.PostContainer(App._utilisateur);

                                if (restServiceStatut.etat == EnumRestService.STATUT_APP_OK)
                                {
                                    bool answer = await App.Current.MainPage.DisplayAlert("Attention", "Voulez vous valider l'affectation", "Oui", "Non");
                                    if (answer)
                                    {
                                        await restServiceStatut.TraitementStatut(Navigation);
                                    }
                                    IsPopAsync = true;
                                    await Navigation.PopModalAsync();
                                }
                                else
                                {
                                    await restServiceStatut.TraitementStatut(Navigation);
                                }
                            }
                        }
                    }
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

        protected override bool OnBackButtonPressed()
        {
            if (!IsPopAsync && !IsScanEnCours)
            {
                IsPopAsync = true;
                Navigation.PopModalAsync().GetAwaiter();
                return base.OnBackButtonPressed();
            }
            return false;
        }
    }
}