using GPAOnGo.WEBSERVICE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GPAOnGo.PISTOLET
{
    public interface IBluetoothManager
    {
        List<Pistolet> GetAllPairedKoamtacDevices();
    }
}
