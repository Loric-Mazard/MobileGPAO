using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.Bluetooth;
using Android.Widget;
using RFID_Android_XAML.PISTOLET;
using RFID_Android_XAML.UTILITAIRE;
using Koamtac.Kdc.Sdk;
using Android.App;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Text;


[assembly: Xamarin.Forms.Dependency(typeof(RFID_Android_XAML.Droid.KoamtacBluetoothDevice))]

namespace RFID_Android_XAML.Droid
{
    public class KoamtacBluetoothDevice : Java.Lang.Object,
        IKoamtacBluetoothDevice
    {
        BluetoothDevice bluetoothDevice;
        public KoamtacBluetoothDevice()
        {
            this.bluetoothDevice = null;
        }

        public object GetBluetoothDevice()
        {
            return bluetoothDevice;
        }

        public void SetBluetoothDevice(ref object pbluetoothDevice)
        {
            bluetoothDevice= (BluetoothDevice)pbluetoothDevice;
        }
    }
}