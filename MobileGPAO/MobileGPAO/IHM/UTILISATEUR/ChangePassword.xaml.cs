using Plugin.Permissions.Abstractions;
using GPAOnGo.PISTOLET;
using GPAOnGo.RFID;
using GPAOnGo.WEBSERVICE;
using GPAOnGo.NGO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing.Net.Mobile.Forms;

namespace GPAOnGo.IHM.UTILISATEUR
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangePassword : ContentPage
	{
        Utilisateur oUtilisateur;
        bool IsPopAsync = false;
        bool IsSubscribe = false;

        public ChangePassword()
        {
            InitializeComponent ();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;

            IsPopAsync = false;

            Subscribe();

            oUtilisateur = (Utilisateur)this.BindingContext;
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
            }
           
        }

        public void UnSubscribe()
        {
            IsSubscribe = false;
        }

        async private void ChangeUserPassword()
        {
            if(!IsPopAsync)
            {
                if (EntryPassword1.Text.Length == 0 )
                {
                    App._toastManager.LongMessage("Nouveau Password Vide");
                }
                else  if (EntryPassword1.Text!=EntryPassword2.Text)
                {
                    App._toastManager.LongMessage("Saisie Password Differente");
                }
                else
                {
                    RestServiceStatut restServiceStatut = oUtilisateur.ChangeUserPassword(EntryPassword1.Text);
                    bool pTraitementStatut = await restServiceStatut.TraitementStatut(Navigation);

                    if (restServiceStatut.etat== EnumRestService.STATUT_APP_OK )
                    {
                        //
                        IsPopAsync = true;
                        await Navigation.PopModalAsync();
                    }
                }
                   
               
            }
        }
        
        private void onValiderClicked(object sender, EventArgs e)
        {
            ChangeUserPassword();
        }

        protected override bool OnBackButtonPressed()
        {
            IsPopAsync = true;
            return base.OnBackButtonPressed();
        }
    }
}