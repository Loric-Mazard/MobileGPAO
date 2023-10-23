using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPAOnGo.IHM.ERP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMNGOModifTypeObject : ContentPage
    {
        public IHMNGOModifTypeObject()
        {
            InitializeComponent();
        }

        private void ValueChangetitre(object sender, EventArgs e)
        {
            App.crea.setTitre(titreTextBox.Text);
        }
    }
}