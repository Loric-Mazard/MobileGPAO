using GPAOnGo.NGO;
using GPAOnGo.UTILITAIRE;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPAOnGo.IHM.ERP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IHMObjectERPScan : ContentPage
	{
        //ObjectERP oObjectERP { get; set; }
        NGOPage pagetest { get; set; }

        bool IsPopAsync = false;
        bool IsPopModalAsync = false;

        public IHMObjectERPScan()
        {
            CustomNavigationPage.SetHasBackButton(this, false);
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));

            InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //oObjectERP = new ObjectERP();
            pagetest = (NGOPage)this.BindingContext;
            IsPopAsync = false;
            IsPopModalAsync = false;
            Subscribe();
            App._pistolet.ModeBarCode();
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
            /*if(!IsSubscribe)
            {
                IsSubscribe = true;
                MessagingCenter.Subscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag, (pType, pData) =>
                {
                    if (pType == oObjectERP.ObjectERPProfil.CBType_ID)
                    {
                        pagetest.PK = pData;
                        pagetest = pagetest.GetObjectERPFromPK(App._utilisateur).GetAwaiter().GetResult();
                        //oObjectERP = oObjectERP.GetObjectERPFromPK(App._utilisateur).GetAwaiter().GetResult();
                    }
                    else
                    {
                        App._toastManager.LongMessage("Code Barre de type " + pType);
                    }
                           
                });

                MessagingCenter.Subscribe<String>(this, App.Subscribe_ObjectERPRecherche,  pData =>
                {
                    oObjectERP.ROWID = pData;
                    GetObjectERP();
                });
            }*/
        }
        
        public void UnSubscribe()
        {
            MessagingCenter.Unsubscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag);
            MessagingCenter.Unsubscribe<String>(this, App.Subscribe_ObjectERPRecherche);
        }

        /*async public void GetObjectERP()
        {
            if (!IsPopAsync)
            {
                pagetest = await pagetest.GetObjectERP(App._utilisateur);
                GetObjectERP_Traitement();
            }
        }*/

        async public void GetObjectERP_Traitement()
        {
            /*if (!IsPopAsync)
            {
                bool traitementStatut = await oObjectERP.RestServiceStatut.TraitementStatut(Navigation);

                if (oObjectERP.AutorisationOK && oObjectERP.ObjectERPProfil.Princ)
                {   
                    oObjectERP.ObjectERPGroupe = await oObjectERP.ObjectERPGroupe.GetObjectERPForGroupe(oObjectERP.ObjectERPProfil.Princ_GroupeNum, App._utilisateur);
                    bool traitementStatutGroupe = await oObjectERP.ObjectERPGroupe.RestServiceStatut.TraitementStatut(Navigation);
                    if (oObjectERP.ObjectERPGroupe.AutorisationOK)
                    {
                        IsPopAsync = true;
                        IHMERPList oIHMERPList = new IHMERPList();
                        oIHMERPList.BindingContext = oObjectERP;
                        await Navigation.PushAsync(oIHMERPList);
                    }
                    else
                    {
                        oObjectERP = new ObjectERP(oObjectERP);
                    }
                }
                else
                {
                    oObjectERP = new ObjectERP(oObjectERP);
                }
            }*/

                IHMERPList oIHMERPList = new IHMERPList();
                oIHMERPList.BindingContext = pagetest;
                await Navigation.PushAsync(oIHMERPList);

        }

        async private void onValiderClicked(object sender, EventArgs e)
        {
            pagetest = pagetest.GetObjectERPFromPK(App._utilisateur, pagetest.Vues_liste.Collection[0].ListeChamps.Collection[0].NomTable);
            pagetest.Vues_liste.Add(new NGOObject());
            pagetest.Vues_liste.Collection[0].ListeChamps.Add(new NGOChamps());
            pagetest.Vues_liste.Collection[0].ListeChamps.Collection[0].Target = pagetest.Num;
            pagetest.Vues_liste.Collection[0].ListeChamps.Collection[0].TypeTargetPage = pagetest.Type;
            pagetest.Vues_liste.Collection[0].ListeChamps.Collection[0].Rowid = pagetest.Rowid;
            await pagetest.Vues_liste.Collection[0].ListeChamps.Collection[0].Action(Navigation);
        }

        public void onScannerClicked(object sender, EventArgs e)
        {
            App._pistolet.ModeBarCode(true);
        }

        protected override bool OnBackButtonPressed()
        {
            IsPopAsync = true;
            return base.OnBackButtonPressed();
        }

        async private void onRechercherClicked(object sender, EventArgs e)
        {
            if(!IsPopModalAsync)
            {
                /*if (oObjectERP.ObjectERPProfil.Recherche)
                {
                    oObjectERP.ObjectERPGroupe = await oObjectERP.ObjectERPGroupe.GetObjectERPToGroupe(oObjectERP.ObjectERPProfil.Recherche_GroupeNum, App._utilisateur);
                    bool TraitementStatut = await oObjectERP.ObjectERPGroupe.RestServiceStatut.TraitementStatut(Navigation);
                    if (oObjectERP.ObjectERPGroupe.AutorisationOK)
                    {
                        if (oObjectERP.ObjectERPGroupe.ERPObjectGroupeListe.Count == 0)
                        {
                            App._toastManager.LongMessage("Aucun element dans la liste de recherche");
                        }
                        else
                        {
                            IsPopModalAsync = true;
                            App.pageERPListRecherche.BindingContext = pagetest;
                            await Navigation.PushModalAsync(App.pageERPListRecherche);
                        }
                    }
                }*/
                NGOChamps c = new NGOChamps();
                c.Code = pagetest.Vues_liste.Collection[0].ListeChamps.Collection[0].Code;
                pagetest = pagetest.GetObjectERPForGroupe(pagetest.CodePageRecherche,c);
                App.pageERPListRecherche.BindingContext = pagetest;
                await Navigation.PushAsync(App.pageERPListRecherche);
            }
           
        }

        private void OnEntryUnfocused(object sender, FocusEventArgs e)
        {

        }
    }
}