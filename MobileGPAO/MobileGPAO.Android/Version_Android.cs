using GPAOnGo.UTILITAIRE;
using Android.Content.PM;


[assembly: Xamarin.Forms.Dependency(typeof(GPAOnGo.Droid.Version_Android))]

namespace GPAOnGo.Droid
{
    public class Version_Android : IAppVersion
    {
        public string GetVersion()
        {
            var context = Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        public int GetBuild()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode;
        }
    }
}