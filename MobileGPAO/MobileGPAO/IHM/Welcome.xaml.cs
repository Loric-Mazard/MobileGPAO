using System;
using System.Threading.Tasks;
using GPAOnGo.NGO;
using GPAOnGo.PISTOLET;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GPAOnGo.UTILITAIRE;

namespace GPAOnGo.IHM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IHMWelcome : ContentPage
    {
        bool IsInitEnvironement = false;
        AppGPAOnGo oAppGPAOnGo = new AppGPAOnGo();

        public BindingEvenement bindingEvenement_mode_production = new BindingEvenement(Xamarin.Essentials.Preferences.Get("MODE_PRODUCTION", App.CONNEXION_BASE_PROD));
        public BindingEvenement bindingEvenement_version_build;

        public IHMWelcome()
        {
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large,FontAttributes.Bold));

            bindingEvenement_version_build = new BindingEvenement("V" + oAppGPAOnGo.GetApkVersion().ToString());

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            lblModeProduction.SetBinding(Label.TextProperty, new Binding("Message", source: bindingEvenement_mode_production, mode: BindingMode.OneWay, stringFormat: "{0}"));
            lblVersionBuild.SetBinding(Label.TextProperty, new Binding("Message", source: bindingEvenement_version_build, mode: BindingMode.OneWay, stringFormat: "{0}"));
        }

        private async void onIdentificationClicked(object sender, EventArgs e)
        {
            if(!IsInitEnvironement)
            {
                oAppGPAOnGo =  oAppGPAOnGo.GetGPAOnGo();
                bool pTraitementStatut = await oAppGPAOnGo.RestServiceStatut.TraitementStatut(Navigation);

                if (oAppGPAOnGo.ApplicationOK)
                {
                    if (oAppGPAOnGo.GetApkVersion() != oAppGPAOnGo.Version)
                    {
                        App._toastManager.LongMessage("Nouvelle Version Disponible:" + oAppGPAOnGo.Version);
                    }
                    else
                    {
                        IsInitEnvironement = true;
                        await InitEnvironement();
                        IsInitEnvironement = false;
                    }
                }
            }
            
        }

        private void onBaseProd_Clicked(object sender, EventArgs e)
        {
            if (!IsInitEnvironement)
            {
                bindingEvenement_mode_production.Message = App.CONNEXION_BASE_PROD;
                Xamarin.Essentials.Preferences.Set("MODE_PRODUCTION", bindingEvenement_mode_production.Message);
            }

        }

        private void onBaseTest_Clicked(object sender, EventArgs e)
        {
            if (!IsInitEnvironement)
            {
                bindingEvenement_mode_production.Message = App.CONNEXION_BASE_TEST;
                Xamarin.Essentials.Preferences.Set("MODE_PRODUCTION", bindingEvenement_mode_production.Message);
            }

        }

        private async Task InitEnvironement()
        {
            if (App._utilisateur.ApplicationOK && App._pistolet.IsInitialized)
            {
                NGOPage p = new NGOPage();
                p = p.GetObjectERPForGroupe("P000000277",new NGOChamps());
                App.pageERPList.BindingContext = p;
                await Navigation.PushAsync(App.pageERPList);

            }
            else if (!App._pistolet.IsInitialized)
            {
                App._pistolet = (new Pistolet()).autoPlug(Xamarin.Essentials.Preferences.Get("PISTOLET", ""));
                string vErreur = string.Empty;
                if (App._pistolet.IsInitialized && !App._pistolet.IsConnected && !App._pistolet.Connect(ref vErreur))
                {
                    Xamarin.Essentials.Preferences.Set("PISTOLET", "");
                    await DisplayAlert("Conexion Equipement Scan", vErreur,"OK");
                }
                await InitConnexion();
            }
            else if (!App._utilisateur.ApplicationOK)
            {
                App._utilisateur = new Utilisateur(Xamarin.Essentials.Preferences.Get("USER_NAME", ""), string.Empty);
                await InitIdentification();
            }
        }

        private async Task InitIdentification()
        {
            if (!App._utilisateur.ApplicationOK)
            {
                MessagingCenter.Subscribe<string, Utilisateur>(this, App.Subscribe_Identification_Utilisateur, async (pNull, pUtilisateur) =>
                {
                    App._utilisateur = pUtilisateur;
                    App._pistolet.Parametre(ref App._utilisateur);

                    Xamarin.Essentials.Preferences.Set("USER_NAME", App._utilisateur.Identification);

                    if (App._utilisateur.ApplicationOK)
                    {
                        MessagingCenter.Unsubscribe<string, Utilisateur>(this, App.Subscribe_Identification_Utilisateur);
                    }

                    await InitEnvironement();

                });
                App.pageIdentification.BindingContext = App._utilisateur;
                await Navigation.PushModalAsync(App.pageIdentification);
            }
            else
            {
                await InitEnvironement();
            }
        }

        private async Task InitConnexion()
        {
            if (!App._pistolet.IsInitialized)
            {
                MessagingCenter.Subscribe<string, Pistolet >(this, App.Subscribe_Connexion_Pistolet, async (pNull, pPistolet) =>
                {
                    App._pistolet = pPistolet;
                    App._pistolet.Parametre(ref App._utilisateur);

                    string vErreur = string.Empty;
                    if (App._pistolet.IsInitialized && !App._pistolet.IsConnected && !App._pistolet.Connect(ref vErreur))
                    {
                        Xamarin.Essentials.Preferences.Set("PISTOLET", "");
                        await DisplayAlert("Conexion Equipement Scan", vErreur, "OK");
                    }

                    if(App._pistolet.IsConnected && App._pistolet.IsKDCReader)
                    {
                        Xamarin.Essentials.Preferences.Set("PISTOLET", App._pistolet.Address);
                    }

                    if (App._pistolet.IsInitialized)
                    {
                        MessagingCenter.Unsubscribe<string>(this, App.Subscribe_Connexion_Pistolet);
                    }

                    await InitEnvironement();
                });
            
                App.pageConnexion.BindingContext = App._pistolet;
                await Navigation.PushModalAsync(App.pageConnexion);
            }
            else
            {
                await InitEnvironement();
            }
               
        }
    }
}