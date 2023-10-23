using Plugin.Permissions.Abstractions;
using GPAOnGo.RFID;
using GPAOnGo.NGO;
using GPAOnGo.UTILITAIRE;
using GPAOnGo.WEBSERVICE;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPAOnGo.IHM.RFID
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IHMCartonScan : ContentPage
	{
        Container oContainer;
        NGOChamps oERPObjectTo;

        bool IsPopAsync = false;
        bool IsSubscribe = false;

        public IHMCartonScan()
        {
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here

            IsPopAsync = false;

            oERPObjectTo = (NGOChamps)this.BindingContext;

            oContainer = null;
            oContainer = new Container("");
            EntryID.SetBinding(Entry.TextProperty, new Binding("Identification", source: oContainer, mode: BindingMode.TwoWay));

            Subscribe();

            App._pistolet.ModeBarCode();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //your code here;
            if (IsPopAsync)
            {
                UnSubscribe();
            }
        }

        public void Subscribe()
        {
            if(!IsSubscribe)
            {
                IsSubscribe = true;
                MessagingCenter.Subscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag, (pType, pData) =>
                {
                    switch (pType)
                    {
                        case App.Type_CAB_Container:
                            oContainer.Identification = pData;
                            CreerContainer();
                            break;
                        default:
                            App._toastManager.LongMessage("Code Barre de type " + pType);
                            break;
                    }
                });
            }
            
        }


        public void UnSubscribe()
        {
            IsSubscribe = false;
            MessagingCenter.Unsubscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag);
        }

        async public  void CreerContainer()
        {
            if(!IsPopAsync)
            {
                oContainer = oContainer.GetContainer(oERPObjectTo.TORFID_Mode, "", App._utilisateur);
                bool traitementStatut = await oContainer.RestServiceStatut.TraitementStatut(Navigation);
                if (oContainer.AutorisationOK)
                {
                    App.pageScanRFIDContainer.BindingContext = oContainer;
                    IsPopAsync = true;
                    await Navigation.PushModalAsync(App.pageScanRFIDContainer);
                }
                else
                {
                    if (oContainer.RestServiceStatut.etat == GPAOnGo.WEBSERVICE.EnumRestService.STATUT_APP_QUESTION_CONTAINERDEJAAPP)
                    {
                        if (await DisplayAlert(oContainer.RestServiceStatut.description, "Voulez-vous modifier?", "Oui", "Non"))
                        {
                            oContainer.Appairage_Mode_DejaEffectue_Forcer = true;
                            CreerContainer();
                        }
                        else
                        {
                            oContainer = null;
                            oContainer = new Container("");
                            EntryID.SetBinding(Entry.TextProperty, new Binding("Identification", source: oContainer, mode: BindingMode.TwoWay));
                        }
                    }
                    else
                    {
                        oContainer = null;
                        oContainer = new Container("");
                        EntryID.SetBinding(Entry.TextProperty, new Binding("Identification", source: oContainer, mode: BindingMode.TwoWay));
                    }
                }
            }
        }

        private void onValiderClicked(object sender, EventArgs e)
        {
            CreerContainer();
        }

        public void onScannerClicked(object sender, EventArgs e)
        {
            App._pistolet.ModeBarCode(true);
        }

        protected override bool OnBackButtonPressed()
        {
            IsPopAsync = true;
            return base.OnBackButtonPressed();
        }
    }
}