using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPAOnGo.UTILITAIRE;
using GPAOnGo.WEBSERVICE;
using GPAOnGo.NGO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPAOnGo.IHM.ERP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMERPStatutListe : ContentPage
    { 
        NGOPage ngoPage;

        public IHMERPStatutListe()
        {
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));
        }

       protected override void OnAppearing()
        {
            base.OnAppearing();

            ngoPage = (NGOPage)this.BindingContext;
            ngoPage.StatutNGO = ((NGOPage)this.BindingContext).StatutNGO.GetObjectNGOStatut();
            this.BindingContext = ngoPage;
            InitializeComponent();
        }

        private void onbtn_backClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }


        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}