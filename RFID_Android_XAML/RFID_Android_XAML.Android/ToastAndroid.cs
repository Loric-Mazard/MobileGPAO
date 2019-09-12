using Android.App;
using Android.Widget;
using RFID_Android_XAML.Droid;
using RFID_Android_XAML.UTILITAIRE;

[assembly: Xamarin.Forms.Dependency(typeof(ToastAndroid))]

namespace RFID_Android_XAML.Droid
{
    class ToastAndroid : IToast
    {
        public void LongMessage(string msg)
        {
            if (msg != "" && msg != null)
            {
                Toast.MakeText(Application.Context, msg, ToastLength.Long).Show();
            }
        }

        public void ShortMessage(string msg)
        {
            if (msg != "" && msg != null)
            { 
                Toast.MakeText(Application.Context, msg, ToastLength.Short).Show();
            }
        }
    }
}