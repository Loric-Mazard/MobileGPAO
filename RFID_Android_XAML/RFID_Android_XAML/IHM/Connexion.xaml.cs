using Plugin.Permissions.Abstractions;
using RFID_Android_XAML.WEBSERVICE;
using RFID_Android_XAML.UTILITAIRE;
using RFID_Android_XAML.PISTOLET;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;

namespace RFID_Android_XAML
{
	public partial class Connexion : ContentPage
    {
    
        ObservableCollection<Pistolet> list_device;

         public Connexion ()
		{
            InitializeComponent();

            list_device = new ObservableCollection<Pistolet>();
            DevicesList.ItemsSource = list_device;
            NewListDevice();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Send("", App.Subscribe_Connexion_Pistolet);
        }
    
        async private void DevicesList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            string vErreur = string.Empty;
            Pistolet device = DevicesList.SelectedItem as Pistolet;

            if(App._pistolet.IsConnected)
            {
                if (App._pistolet.Address == device.Address)
                {
                    await DisplayAlert("Information", "Connexion Deja Effectuée", "OK");
                }
                else
                {
                    if (!App._pistolet.Disconnect(ref vErreur))
                    {
                        await DisplayAlert("Attention", vErreur, "OK");
                    }
                    else
                    {
                        App._pistolet = null;
                        DevicesList_OnItemTapped(sender, e);
                    }
                }
            }
            else
            {
                Console.WriteLine("Choix : " + device.Name);
                if (!App._pistolet.Connect(ref vErreur))
                {
                    await DisplayAlert("Attention", vErreur, "OK");
                }
                else
                {
                    App._pistolet = device;
                    await Navigation.PopModalAsync();
                } 
            }
        }

        private void onRefreshClicked(object sender, EventArgs e)
        {
            NewListDevice();
        }

        private async void NewListDevice()
        {

            list_device.Clear();

            if(App._pistolet.Bluetooth_getAllPairedKoamtacDevices().Count==0)
            {
                App._toastManager.LongMessage("Aucun Pistolet Appairé");
            }

            if (await Perm.CheckPermissions(Permission.Location))
            {
                foreach(Pistolet pistolet in App._pistolet.Bluetooth_getAllPairedKoamtacDevices())
                {
                    list_device.Add(pistolet);
                }
            }
            else
            {
                await DisplayAlert("Attention", "Probleme Autorisation", "OK");
            }
        }

        private async void onButtonModePhotoClicked(object sender, EventArgs e)
        {
            if (!App._pistolet.ModeIsSelected())
            {
                bool answer = await DisplayAlert("Question?", "Continuer Sans Gun", "Yes", "No");

                if (answer)
                {
                    App._pistolet = new Pistolet("XCover4", "00:00:00:00", true);
                    await Navigation.PopModalAsync();
                }
            }

        }
    }
}
