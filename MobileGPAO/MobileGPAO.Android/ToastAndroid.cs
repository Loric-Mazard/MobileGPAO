using Android.App;
using Android.Widget;
using GPAOnGo.UTILITAIRE;

[assembly: Xamarin.Forms.Dependency(typeof(GPAOnGo.Droid.ToastAndroid))]

namespace GPAOnGo.Droid
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