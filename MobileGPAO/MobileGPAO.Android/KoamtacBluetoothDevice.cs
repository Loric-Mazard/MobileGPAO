using Android.Bluetooth;
using GPAOnGo.PISTOLET;


[assembly: Xamarin.Forms.Dependency(typeof(GPAOnGo.Droid.KoamtacBluetoothDevice))]

namespace GPAOnGo.Droid
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