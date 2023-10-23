using System;
using GPAOnGo.NGO;
using GPAOnGo.PISTOLET;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GPAOnGo.UTILITAIRE;

namespace GPAOnGo.IHM.ERP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMERPList : ContentPage
	{
        bool IsPopAsync = false;
        NGOPage oObjectERP = null;

        public IHMERPList()
        {
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            IsPopAsync = false;

            if(oObjectERP == null)
            {
                oObjectERP = (NGOPage)this.BindingContext;
            }
            else
            {
                oObjectERP = ((NGOPage)this.BindingContext).RefreshPage();
                this.BindingContext = oObjectERP;
            }
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            IsPopAsync = true;
            return base.OnBackButtonPressed();
        }

        async private void OnLofOff_Clicked(object sender, EventArgs e)
        {
            if (!IsPopAsync)
            {
                App._utilisateur = null;
                App._utilisateur = new Utilisateur();
                await Navigation.PopToRootAsync();
            }
        }

        async private void OnRFID_Clicked(object sender, EventArgs e)
        {
            if (!IsPopAsync)
            {
                App._pistolet = null;
                App._pistolet = new Pistolet();
                if (!App._pistolet.IsInitialized)
                {
                    MessagingCenter.Subscribe<string, Pistolet>(this, App.Subscribe_Connexion_Pistolet, (pNull, pPistolet) =>
                    {
                        App._pistolet = pPistolet;
                        App._pistolet.Parametre(ref App._utilisateur);

                        string vErreur = string.Empty;
                        if (App._pistolet.IsInitialized && !App._pistolet.IsConnected && !App._pistolet.Connect(ref vErreur))
                        {
                            Xamarin.Essentials.Preferences.Set("PISTOLET", "");
                            DisplayAlert("Conexion Equipement Scan", vErreur, "OK");
                        }

                        if (App._pistolet.IsConnected && App._pistolet.IsKDCReader)
                        {
                            Xamarin.Essentials.Preferences.Set("PISTOLET", App._pistolet.Address);
                        }

                        if (App._pistolet.IsInitialized)
                        {
                            MessagingCenter.Unsubscribe<string>(this, App.Subscribe_Connexion_Pistolet);
                        }
                    });
                    App.pageConnexion.BindingContext = App._pistolet;
                    await Navigation.PushModalAsync(App.pageConnexion);
                }
            }
        }
    }
}