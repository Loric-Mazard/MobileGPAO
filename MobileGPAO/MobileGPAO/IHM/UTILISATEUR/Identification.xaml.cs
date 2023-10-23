using Plugin.Permissions.Abstractions;
using GPAOnGo.PISTOLET;
using GPAOnGo.RFID;
using GPAOnGo.WEBSERVICE;
using GPAOnGo.UTILITAIRE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPAOnGo.NGO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing.Net.Mobile.Forms;

namespace GPAOnGo.IHM.UTILISATEUR
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Identification : ContentPage
	{
        Utilisateur oUtilisateur;
        bool IsPopAsync = false;
        bool IsSubscribe = false;

        public Identification()
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
            //EntryID.SetBinding(Entry.TextProperty, new Binding("Identification", source: oUtilisateur, mode: BindingMode.TwoWay));
            //EntryPassword.SetBinding(Entry.TextProperty, new Binding("Password", source: oUtilisateur, mode: BindingMode.TwoWay));

            if (!App._pistolet.IsInitialized)
            {
                Navigation.PopAsync();
            }
            else
            {
                App._pistolet.ModeBarCode();
            }

            // Auto authentification
            oUtilisateur.Identification = "lmaza";
            oUtilisateur.Password = "2508";
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
                    if (!IsPopAsync)
                    {
                        switch (pType)
                        {
                            case App.Type_CAB_Utilisateur:
                                oUtilisateur.Identification = pData.ToUpper();
                                CreerUtilisateur();
                                break;
                            default:
                                App._toastManager.LongMessage("Code Barre de type " + pType);
                                break;
                        }
                    }

                });
            }
        }

        public void UnSubscribe()
        {
            IsSubscribe = false;
            MessagingCenter.Unsubscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag);
        }

        async private void CreerUtilisateur()
        {
            if(!IsPopAsync)
            {
                if(oUtilisateur.Identification.ToString().Length != 0 && oUtilisateur.Password.ToString().Length != 0)
                {
                    oUtilisateur = oUtilisateur.GetUtilisateur();
                    bool pTraitementStatut = await oUtilisateur.RestServiceStatut.TraitementStatut(Navigation);

                    if (oUtilisateur.ApplicationOK)
                    {
                        //
                        IsPopAsync = true;
                        await Navigation.PopModalAsync();
                        MessagingCenter.Send("", App.Subscribe_Identification_Utilisateur, oUtilisateur);
                    }
                    else
                    {
                        if (oUtilisateur.RestServiceStatut.etat == GPAOnGo.WEBSERVICE.EnumRestService.STATUT_APP_QUESTION_USERCHANGEPASSWORD)
                        {
                            if (await DisplayAlert(oUtilisateur.RestServiceStatut.description, "Voulez-vous modifier?", "Oui", "Non"))
                            {
                                App.pageChangePassword.BindingContext = oUtilisateur;
                                await Navigation.PushModalAsync(App.pageChangePassword);
                                EntryPassword.Text = string.Empty;
                            }
                            else
                            {
                                oUtilisateur = null;
                                oUtilisateur = new Utilisateur();
                                EntryID.SetBinding(Entry.TextProperty, new Binding("Identification", source: oUtilisateur, mode: BindingMode.TwoWay));
                                EntryPassword.SetBinding(Entry.TextProperty, new Binding("Password", source: oUtilisateur, mode: BindingMode.TwoWay));
                            }
                        }
                        else
                        {
                            oUtilisateur = null;
                            oUtilisateur = new Utilisateur();
                            EntryID.SetBinding(Entry.TextProperty, new Binding("Identification", source: oUtilisateur, mode: BindingMode.TwoWay));
                            EntryPassword.SetBinding(Entry.TextProperty, new Binding("Password", source: oUtilisateur, mode: BindingMode.TwoWay));
                        } 
                    }
                }
               
            }
        }
        
        private void onValiderClicked(object sender, EventArgs e)
        {
            if (!oUtilisateur.Identification.Contains("$"))
            {
                oUtilisateur.Identification += "$dmnbtb";
            }
            CreerUtilisateur();
        }

        private void onScannerClicked(object sender, EventArgs e)
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