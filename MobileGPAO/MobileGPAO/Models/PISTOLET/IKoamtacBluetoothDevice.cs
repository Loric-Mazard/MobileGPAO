using GPAOnGo.WEBSERVICE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GPAOnGo.PISTOLET
{
    public interface IKoamtacBluetoothDevice
    {
        object GetBluetoothDevice();

        void SetBluetoothDevice(ref object pbluetoothDevice);
    }
}
