using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GPAOnGo.UTILITAIRE;

namespace GPAOnGo.IHM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewObjectERPListeListe : ContentView
	{
        public ViewObjectERPListeListe()
        {
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold));

            InitializeComponent();

        }
    }
}