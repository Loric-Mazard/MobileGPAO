using RFID_Android_XAML.PISTOLET;
using RFID_Android_XAML.RFID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RFID_Android_XAML
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModeChoix : ContentPage
	{
        public ModeChoix ()
		{
			InitializeComponent ();

            listViewMode.ItemsSource = App._utilisateur.ListeModeAppairage;

            listViewMode.ItemSelected += DeselectItem;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
        }

        async private void listViewMode_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagingCenter.Send("", App.Subscribe_Statut);
            if (((ListView)sender).SelectedItem != null)
            {
                MessagingCenter.Unsubscribe<TagListe>(this, App.Subscribe_ScanRFID_TagListe_FromPage);

                ModeAppairage modeAppairageSelected = listViewMode.SelectedItem as ModeAppairage;
                switch (modeAppairageSelected.Type)
                {
                    case 1:
                        if (!App._utilisateur.TagListeNegatif.IsConfigured)
                        {
                            bool answer = await this.DisplayAlert("Attention", "Vous n'avez pas configurez la Liste de Tag Negatif. Voulez-vous la configurer ?", "Oui", "Non");
                            if(answer)
                            {
                                MessagingCenter.Subscribe<TagListe>(this, App.Subscribe_ScanRFID_TagListe_FromPage, (NewtagListe) =>
                                {
                                    App._utilisateur.TagListeNegatif = NewtagListe;
                                });
                                if (App.page_ScanRFIDToList == null)
                                {
                                    App.page_ScanRFIDToList = new ScanRFIDToList();
                                }
                                App.page_ScanRFIDToList.BindingContext = modeAppairageSelected;
                                await Navigation.PushAsync(App.page_ScanRFIDToList);
                                break;
                            }
                        }
                        if (App.page_CartonScan == null)
                        {
                            App.page_CartonScan = new CartonScan();
                        }
                        App.page_CartonScan.BindingContext = modeAppairageSelected;
                        await Navigation.PushAsync(App.page_CartonScan);
                        break;
                    case 2:
                        if (App.page_ScanRFIDToList==null)
                        {
                            App.page_ScanRFIDToList=new ScanRFIDToList();
                        }
                        App.page_ScanRFIDToList.BindingContext = modeAppairageSelected;
                        await Navigation.PushAsync(App.page_ScanRFIDToList);
                        break;
                    case 3:
                        MessagingCenter.Subscribe<TagListe>(this, App.Subscribe_ScanRFID_TagListe_FromPage, (NewtagListe) =>
                        {
                            App._utilisateur.TagListeNegatif = NewtagListe;
                        });
                        if (App.page_ScanRFIDToList == null)
                        {
                            App.page_ScanRFIDToList = new ScanRFIDToList();
                        }

                        App.page_ScanRFIDToList.BindingContext = (modeAppairageSelected);
                        await Navigation.PushAsync(App.page_ScanRFIDToList);
                        break;
                }
               
            }
        }
        public void DeselectItem(object sender, EventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}