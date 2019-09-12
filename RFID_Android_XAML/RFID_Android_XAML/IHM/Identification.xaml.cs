using Plugin.Permissions.Abstractions;
using RFID_Android_XAML.PISTOLET;
using RFID_Android_XAML.GPAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RFID_Android_XAML
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Identification : ContentPage
	{
        public Identification ()
        {
            InitializeComponent ();

            EntryUtilisateurID.Text = App._utilisateur.Identification;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;

            App._utilisateur = null;
            App._utilisateur = new Utilisateur();
            EntryUtilisateurID.Text = App._utilisateur.Identification;

            MessagingCenter.Subscribe<String>(this, App.Subscribe_Statut, (pStatut) =>
            {
                lblStatut.Text = pStatut;
            });

            MessagingCenter.Subscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag, (pType, pData) =>
            {
                switch (pType)
                {
                    case App.Type_CAB_Utilisateur:
                        CreerUtilisateur(pData);
                        break;
                    default:
                        MessagingCenter.Send("Code Barre de type " + pType, App.Subscribe_Statut);
                        break;
                }
            });

            if (!App._pistolet.IsInitialized)
            {
                Navigation.PopAsync();
            }
            else
            {
                App._pistolet.ModeBarCode();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //your code here;
            MessagingCenter.Unsubscribe<String>(this, App.Subscribe_Statut);
            MessagingCenter.Unsubscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag);
            MessagingCenter.Send("", App.Subscribe_Identification_Utilisateur);
        }

        private void onValiderClicked(object sender, SelectedItemChangedEventArgs e)
        {
            CreerUtilisateur(EntryUtilisateurID.Text.ToUpper());
        }

        public void onScannerClicked(object sender, EventArgs e)
        {
            App._pistolet.ModeBarCode(true);
        }

        async private void CreerUtilisateur(string pIdentification)
        {
            App._utilisateur = null;
            App._utilisateur = await new Utilisateur().CreerUtilisateur (pIdentification);
            EntryUtilisateurID.Text = App._utilisateur.Identification;

            if (App._utilisateur.PeuxAccederApplication)
            {
                App._pistolet.AffecterUtilisateur(App._utilisateur);
                await Navigation.PopModalAsync();
            }
        }
    }
}