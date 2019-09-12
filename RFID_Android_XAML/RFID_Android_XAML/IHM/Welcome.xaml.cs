using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFID_Android_XAML.GPAO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RFID_Android_XAML.UTILITAIRE;

namespace RFID_Android_XAML
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Welcome : ContentPage
	{

        public Welcome ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
        }

        private async void onIdentificationClicked(object sender, EventArgs e)
        {
            if (App._utilisateur.PeuxAccederApplication && App._pistolet.IsInitialized)
            {
                if (App.page_ModeChoix == null)
                {
                    App.page_ModeChoix = new ModeChoix();
                }
                await Navigation.PushAsync(App.page_ModeChoix);
            }
            else if (!App._pistolet.IsConnected)
            {
                await InitConnexion();
            }
            else if (!App._utilisateur.PeuxAccederApplication)
            {
                await InitIdentification();
            }
        }

        private async Task InitIdentification()
        {
            if (!App._utilisateur.PeuxAccederApplication)
            {
                MessagingCenter.Subscribe<string>(this, App.Subscribe_Identification_Utilisateur, (pNull) =>
                {
                    if (App._utilisateur.PeuxAccederApplication)
                    {
                        MessagingCenter.Unsubscribe<string>(this, App.Subscribe_Identification_Utilisateur);
                    }

                    if (App._utilisateur.PeuxAccederApplication && App._pistolet.IsInitialized)
                    {
                        if (App.page_ModeChoix == null)
                        {
                            App.page_ModeChoix = new ModeChoix();
                        }
                        Navigation.PushAsync(App.page_ModeChoix);
                    }
                    else
                    {
                        if (!App._utilisateur.PeuxAccederApplication)
                        {
                            if (App.page_Identification == null)App.page_Identification = new Identification();
                            Navigation.PushModalAsync(App.page_Identification);
                        }
                        else if (!App._pistolet.IsConnected)
                        {
                            InitConnexion();
                        }
                    }

                });

                if (App.page_Identification == null)App.page_Identification = new Identification();
                await Navigation.PushModalAsync(App.page_Identification);
            }
        }
        private async Task InitConnexion()
        {
            if (!App._pistolet.IsInitialized)
            {
                MessagingCenter.Subscribe<string>(this, App.Subscribe_Connexion_Pistolet, (pNull) =>
                {
                    if (App._pistolet.IsInitialized)
                    {
                        MessagingCenter.Unsubscribe<string>(this, App.Subscribe_Connexion_Pistolet);
                    }

                    if (App._utilisateur.PeuxAccederApplication && App._pistolet.IsInitialized)
                    {
                        if (App.page_ModeChoix == null)
                        {
                            App.page_ModeChoix = new ModeChoix();
                        }
                        Navigation.PushAsync(App.page_ModeChoix);
                    }
                    else
                    {
                        if (!App._pistolet.IsInitialized)
                        {
                            if (App.page_Connexion == null)App.page_Connexion = new Connexion();
                            Navigation.PushModalAsync(App.page_Connexion);
                        }
                        else if (!App._utilisateur.PeuxAccederApplication)
                        {
                            InitIdentification();
                        }
                    }

                });
                if (App.page_Connexion== null)App.page_Connexion = new Connexion();
                await Navigation.PushModalAsync(App.page_Connexion);
            }
        }
    }
}