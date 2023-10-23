using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GPAOnGo.NGO;

namespace GPAOnGo.IHM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewObjectERPStatutListe : ContentView
	{
        public ViewObjectERPStatutListe()
		{
			InitializeComponent ();
        }
        
        async private void OnSelectedItem(object sender, SelectionChangedEventArgs e)
        {
            if (((CollectionView)sender).SelectedItem != null)
            {
                NGO.NGOStatut eRPStatutSelected = ((CollectionView)sender).SelectedItem as NGO.NGOStatut;

                if (!eRPStatutSelected.Enable)
                {
                    App._toastManager.ShortMessage(eRPStatutSelected.DisableDescription);
                }
                else
                {
                    switch (eRPStatutSelected.Type)
                    {
                        case NGOStatut.enumStatutType.TissusCoupe:
                            break;

                        case NGOStatut.enumStatutType.ListeChoix:
                        case NGOStatut.enumStatutType.Boolean:
                        case NGOStatut.enumStatutType.Pourcentage:
                            App.pageERPStatutChoixListe.BindingContext = eRPStatutSelected;
                            await Navigation.PushModalAsync(App.pageERPStatutChoixListe);
                            break;

                        case NGOStatut.enumStatutType.TextLibre:
                            App.pageERPStatut_TextLibre.BindingContext = eRPStatutSelected;
                            await Navigation.PushModalAsync(App.pageERPStatut_TextLibre);
                            break;

                        case NGOStatut.enumStatutType.PourcentageLibre:
                            App.pageERPStatut_LinearGauge.BindingContext = eRPStatutSelected;
                            await Navigation.PushModalAsync(App.pageERPStatut_LinearGauge);
                            break;

                    }
                }
            }

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}