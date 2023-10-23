﻿using System;
using GPAOnGo.NGO;
using GPAOnGo.UTILITAIRE;
using GPAOnGo.WEBSERVICE;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPAOnGo.IHM.ERP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMERPStatut_LinearGauge : ContentPage
    {
        NGOStatut oERPStatut;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            oERPStatut = (NGOStatut)this.BindingContext;
        }

        public IHMERPStatut_LinearGauge()
		{
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));
            InitializeComponent ();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        /*async private void OnValiderClicked(object sender, EventArgs e)
        {
            NGOStatutChoix oERPStatutChoix = new NGOStatutChoix(oERPStatut.Origine, oERPStatut.ROWID, oERPStatut.Position, ((int)(rangeslider.RangeEnd)).ToString());
            RestServiceStatut restServiceStatut =  oERPStatutChoix.PostERPStatutChoix(App._utilisateur);

            if (restServiceStatut.etat == EnumRestService.STATUT_APP_OK)
            {
                await restServiceStatut.TraitementStatut(Navigation);
                await Navigation.PopModalAsync();
            }
            else
            {
                await restServiceStatut.TraitementStatut(Navigation);
            }
        }*/
    }
}