using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace GPAOnGo.IHM
{
    public partial class ViewToolbarItem : PopupPage
    {
        public ViewToolbarItem()
        {
            InitializeComponent();
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        /*async private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string vErreur = string.Empty;

            if (sender != null)
            {
                ERPObjectTo oERPObjectTo = ((CollectionView)sender).SelectedItem as ERPObjectTo;
                
                if (oERPObjectTo != null)
                {
                    if(!await oERPObjectTo.Action2(Navigation, vErreur))
                    {
                        App._toastManager.ShortMessage(vErreur);
                    }
                    else
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }
                }
            }
        }*/
    }
}