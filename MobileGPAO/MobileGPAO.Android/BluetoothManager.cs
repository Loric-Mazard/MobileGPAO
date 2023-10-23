using System.Collections.Generic;
using Android.Bluetooth;
using Koamtac.Kdc.Sdk;
using GPAOnGo.PISTOLET;

[assembly: Xamarin.Forms.Dependency(typeof(GPAOnGo.Droid.BluetoothManager))]

namespace GPAOnGo.Droid
{
    public class BluetoothManager : Java.Lang.Object,
        IBluetoothManager
    {
        BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;

        private bool getState()
        {
            return adapter.IsEnabled;
        }

        public List<Pistolet> GetAllPairedKoamtacDevices()
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
                return null;
            }
        }

    }
}