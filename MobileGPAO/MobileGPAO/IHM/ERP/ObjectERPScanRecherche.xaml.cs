using System;
using GPAOnGo.NGO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GPAOnGo.UTILITAIRE;
namespace GPAOnGo.IHM.ERP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMERPListRecherche : ContentPage
	{

        NGOPage oObjectERP;
        ListeNGOObject oERPObjectGroupeListe;
        String vStringRecherche;
 

        public IHMERPListRecherche()
        {
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;


            vStringRecherche = string.Empty;
            oObjectERP = (NGOPage)this.BindingContext;
            oViewObjectERPGroupeListe.BindingContext = oObjectERP.Vues_liste;
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        private void OnRechercheTextChanged(object sender, TextChangedEventArgs e)
        {
            if(oSearchBar.Text.Contains(vStringRecherche))
            {
                vStringRecherche = oSearchBar.Text;
                oERPObjectGroupeListe = oObjectERP.Recherche(vStringRecherche);
            }
            else
            {
                vStringRecherche = oSearchBar.Text;
                oERPObjectGroupeListe = oObjectERP.Recherche(vStringRecherche);
            }

            oViewObjectERPGroupeListe.BindingContext = oERPObjectGroupeListe;
        }
    }
}