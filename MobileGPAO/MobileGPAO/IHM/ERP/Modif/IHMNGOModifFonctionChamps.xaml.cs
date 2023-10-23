using System;
using System.Collections.Generic;
using GPAOnGo.NGO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GPAOnGo.UTILITAIRE;

namespace GPAOnGo.IHM.ERP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMNGOModifFonctionChamps : ContentPage
    {
        NGOPage oObjectERP;

        public IHMNGOModifFonctionChamps()
        {
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));

            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            oObjectERP = (NGOPage)this.BindingContext;

            List<string> l2 = new List<string>();
            foreach (NGOChamps c in oObjectERP.Vues_liste.Collection[0].ListeChamps.Collection)
            {
                l2.Add(c.Contenu_liste.Collection[0].Valeur);
            }

            //fonctionComboBox.ComboBoxSource = l2;

            //oObjectERP.Vues_liste.RemoveAt(0);

        }

        private void ValueChangeFonction(object sender, EventArgs e)
        {
            //App.crea.setFonction(fonctionComboBox.Text);
        }
    }
}