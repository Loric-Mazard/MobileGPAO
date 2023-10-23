using Plugin.Permissions.Abstractions;
using GPAOnGo.WEBSERVICE;
using GPAOnGo.UTILITAIRE;
using GPAOnGo.PISTOLET;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPAOnGo.IHM
{
	public partial class IHMConnexion : ContentPage
    {
        Pistolet oPistolet;
        List<Pistolet> list_device;
        bool IsPopAsync = false;

        public IHMConnexion()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsPopAsync = false;

            oPistolet = (Pistolet)this.BindingContext;
            list_device = new List<Pistolet>();
            DevicesList.ItemsSource = oPistolet.Bluetooth_getAllPairedKoamtacDevices();
            NewListDevice();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        async private void DevicesList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null && !IsPopAsync)
            {
                string vErreur = string.Empty;

                Pistolet device = ((ListView)sender).SelectedItem as Pistolet;
                if (!device.Connect(ref vErreur))
                {
                    await DisplayAlert("Attention", vErreur, "OK");
                }
                else
                {
                    oPistolet = device;
                    IsPopAsync = true;
                    await Navigation.PopModalAsync();
                    MessagingCenter.Send("", App.Subscribe_Connexion_Pistolet, oPistolet);
                }
            }
            ((ListView)sender).SelectedItem = null;
        }

        private void onRefreshClicked(object sender, EventArgs e)
        {
            NewListDevice();
        }

        private async void NewListDevice()
        {

            list_device.Clear();

            if(oPistolet.Bluetooth_getAllPairedKoamtacDevices().Count==0)
            {
                App._toastManager.LongMessage("Aucun KDCReader Appairé");
            }
            else if (oPistolet.Bluetooth_getAllPairedKoamtacDevices().Count == 1)
            {
                list_device = oPistolet.Bluetooth_getAllPairedKoamtacDevices();
            }
            else if (await Perm.CheckPermissions(Permission.Location))
            {
                list_device = oPistolet.Bluetooth_getAllPairedKoamtacDevices();
            }
            else
            {
                await DisplayAlert("Attention", "Probleme Autorisation", "OK");
            }
        }

        private async void onButtonModePhotoClicked(object sender, EventArgs e)
        {
            if (!IsPopAsync)
            {
                if (!oPistolet.ModeIsSelected)
                {
                    bool answer = await DisplayAlert("Question?", "Continuer Sans Lecteur RFID", "Yes", "No");

                    if (answer)
                    {
                        oPistolet = new Pistolet("XCover4", "00:00:00:00", true);
                        IsPopAsync = true;
                        await Navigation.PopModalAsync();
                        MessagingCenter.Send("", App.Subscribe_Connexion_Pistolet, oPistolet);
                    }
                }
            }
        }

        private void onScannerClicked(object sender, EventArgs e)
        {

        }
    }
}
