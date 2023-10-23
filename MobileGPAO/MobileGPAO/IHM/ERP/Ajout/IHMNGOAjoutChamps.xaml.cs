using System;
using GPAOnGo.NGO;
using GPAOnGo.PISTOLET;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GPAOnGo.UTILITAIRE;
using System.Collections.Generic;

namespace GPAOnGo.IHM.ERP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMNGOAjoutChamps : ContentPage
	{

        NGOPage oObjectERP;

        public IHMNGOAjoutChamps()
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

            List<string> l1 = new List<string>();
            foreach (NGOChamps c in oObjectERP.Vues_liste.Collection[0].ListeChamps.Collection)
            {
                l1.Add(c.Contenu_liste.Collection[0].Valeur);
            }

            //actionComboBox.ComboBoxSource = l1;

            oObjectERP.Vues_liste.RemoveAt(0);

            List<string> l2 = new List<string>();
            foreach (NGOChamps c in oObjectERP.Vues_liste.Collection[0].ListeChamps.Collection)
            {
                l2.Add(c.Contenu_liste.Collection[0].Valeur);
            }

            //fonctionComboBox.ComboBoxSource = l2;

            oObjectERP.Vues_liste.RemoveAt(0);

            List<string> l3 = new List<string>();
            foreach (NGOChamps c in oObjectERP.Vues_liste.Collection[0].ListeChamps.Collection)
            {
                l3.Add(c.Contenu_liste.Collection[0].Valeur);
            }

            //targetComboBox.ComboBoxSource = l3;

            oObjectERP.Vues_liste.RemoveAt(0);

        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        private void ValueChangetitre(object sender, EventArgs e)
        {
            App.crea.setTitre(titreTextBox.Text);
        }

        /*private void ValueChangeAction(object sender, EventArgs e)
        {
            App.crea.setAction(actionComboBox.Text);
        }

        private void ValueChangeTarget(object sender, EventArgs e)
        {
            App.crea.setNomPageTarget(targetComboBox.Text);
        }

        private void ValueChangeFonction(object sender, EventArgs e)
        {
            App.crea.setFonction(fonctionComboBox.Text);
        }*/
    }
}