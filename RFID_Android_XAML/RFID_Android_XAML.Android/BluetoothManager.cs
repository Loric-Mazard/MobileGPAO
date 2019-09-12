using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.Bluetooth;
using Android.Widget;
using RFID_Android_XAML.WEBSERVICE;
using RFID_Android_XAML.UTILITAIRE;
using Koamtac.Kdc.Sdk;
using Android.App;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using RFID_Android_XAML.PISTOLET;

[assembly: Xamarin.Forms.Dependency(typeof(RFID_Android_XAML.Droid.BluetoothManager))]

namespace RFID_Android_XAML.Droid
{
    public class BluetoothManager : Java.Lang.Object,
        IBluetoothManager
    {
        BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;

        private bool getState()
        {
            return adapter.IsEnabled;
        }

        public List<Pistolet> getAllPairedKoamtacDevices()
        {
            try
            {
                if (!getState())
                {
                    App._toastManager.LongMessage("Veuillez activer votre Bluetooth");
                    return new List<Pistolet>();
                }

                List<Pistolet> getAllPairedKoamtacDevices = new List<Pistolet>();
                foreach (BluetoothDevice device in KDCReader.AvailableDeviceList)
                {
                    getAllPairedKoamtacDevices.Add(new Pistolet(device.Name, device.Address, true, false, device));
                }
                return getAllPairedKoamtacDevices;
            }
            catch (System.Exception e)
            {
                App._toastManager.ShortMessage("Problème avec la liste des lecteurs, contactez le service info : \n" + e);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return null;
            }
        }

    }
}