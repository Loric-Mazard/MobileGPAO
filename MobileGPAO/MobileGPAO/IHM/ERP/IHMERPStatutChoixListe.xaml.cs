using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPAOnGo.UTILITAIRE;
using GPAOnGo.WEBSERVICE;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPAOnGo.IHM.ERP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMERPStatutChoixListe : ContentPage
    {

        bool IsPopAsync = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsPopAsync = false;
        }

        public IHMERPStatutChoixListe()
		{
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));

            InitializeComponent ();
        }

        private async void listView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null && !IsPopAsync)
            {
                NGO.NGOStatutChoix eRPStatutChoixSelected = ((ListView)sender).SelectedItem as NGO.NGOStatutChoix;

                if (!eRPStatutChoixSelected.Enable)
                {
                    App._toastManager.ShortMessage(eRPStatutChoixSelected.DisableDescription);
                }
                else
                {
                    RestServiceStatut restServiceStatut = eRPStatutChoixSelected.PostERPStatutChoix(App._utilisateur);

                    if (restServiceStatut.etat == EnumRestService.STATUT_APP_OK)
                    {
                        await restServiceStatut.TraitementStatut(Navigation);
                        IsPopAsync = true;
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await restServiceStatut.TraitementStatut(Navigation);
                    }
                }
            }

            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            IsPopAsync = true;
            return base.OnBackButtonPressed();
        }
    }
}