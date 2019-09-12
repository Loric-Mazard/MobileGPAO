using Plugin.Permissions.Abstractions;
using RFID_Android_XAML.RFID;
using RFID_Android_XAML.GPAO;
using RFID_Android_XAML.UTILITAIRE;
using System.Collections.Generic;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RFID_Android_XAML
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartonScan : ContentPage
	{
        private ModeAppairage modeAppairage;
        private Container container = new Container();

        public string vPage_Title { get; set; }
        public string vContainer_ID { get; set; }
        public string vStatut { get; set; }

        public CartonScan()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;

            if (Navigation.NavigationStack.Count == 4)
            {
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 1]);
            }

            this.modeAppairage = (ModeAppairage)this.BindingContext;
     

            container = null;
            container = new Container();

            vPage_Title = modeAppairage.Description;
            vContainer_ID = container.Identificateur;

            PageScanCarton.BindingContext = this;
            EntryContainerID.BindingContext = this;
            PageScanCarton.SetBinding(ContentPage.TitleProperty, "vPage_Title");
            EntryContainerID.SetBinding(Entry.TextProperty, "vContainer_ID");

            this.BindingContext = modeAppairage;

            MessagingCenter.Subscribe<String>(this, App.Subscribe_Statut, (pStatut) =>
            {
                vStatut = pStatut;
            });

            MessagingCenter.Subscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag, (pType, pData) =>
            {
                switch (pType)
                {
                    case App.Type_CAB_Container:
                        CreerContainer(pData);
                        break;
                    default:
                        MessagingCenter.Send("Code Barre de type " + pType, App.Subscribe_Statut);
                        break;
                }
            });

            App._pistolet.ModeBarCode();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //your code here;
            MessagingCenter.Unsubscribe<String>(this, App.Subscribe_Statut);
            MessagingCenter.Unsubscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag);
        }
  
        public void onValiderClicked(object sender, EventArgs e)
        {
            CreerContainer(EntryContainerID.Text.ToUpper());
        }

        public void onScannerClicked(object sender, EventArgs e)
        {
            App._pistolet.ModeBarCode(true);          
        }

        async public void CreerContainer(string pIdentification)
        {
            container = null;
            container = await new Container().CreerContainer(App._utilisateur, modeAppairage, pIdentification);

            vContainer_ID = container.Identificateur;

            if (container.PeuxAccederApplication)
            {
                if (App.page_ScanRFIDContainer==null)
                {
                    App.page_ScanRFIDContainer = new ScanRFIDContainer();
                }
                List<Object> listBindingContext = new List<Object>();
                listBindingContext.Add(modeAppairage);
                listBindingContext.Add(container);

                App.page_ScanRFIDContainer.BindingContext = listBindingContext;
                await Navigation.PushAsync(App.page_ScanRFIDContainer);
            }
        }
    }
}