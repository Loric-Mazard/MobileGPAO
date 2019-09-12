using RFID_Android_XAML.WEBSERVICE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RFID_Android_XAML.PISTOLET
{
    public interface IKoamtacBluetoothDevice
    {
        object GetBluetoothDevice();

        void SetBluetoothDevice(ref object pbluetoothDevice);
    }
}
