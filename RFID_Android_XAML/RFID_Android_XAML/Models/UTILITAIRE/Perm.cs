using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RFID_Android_XAML.UTILITAIRE
{
    class Perm
    {
        public static async Task<bool> CheckPermissions(Plugin.Permissions.Abstractions.Permission permission)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
                {
                    var title = $"Autorisation";
                    var question = $"Pour utiliser cette fonctionnalité, l'autorisation " + permission.ToString() + " doit être accordée dans les paramètres.";
                    var positive = "Paramètres";
                    var negative = "Retour";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return false;
                }
            }

            return true;
        }
    }
}
