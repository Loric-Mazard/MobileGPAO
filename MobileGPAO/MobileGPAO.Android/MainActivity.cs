using System;

using ZXing.Mobile;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;

namespace GPAOnGo.Droid
{
    [Activity(Label = "GPAOnGo", Icon = "@drawable/GPAOicon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Verifie et accepte les autorisations bluetooth
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.BluetoothConnect)
                != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this,
                    new string[] { Manifest.Permission.BluetoothConnect }, 0);
            }

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            MobileBarcodeScanner.Initialize(Application);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

        }
    }
}