using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPAOnGo.IHM.ERP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IHMNGOModifTitrePage : ContentPage
    {
        public IHMNGOModifTitrePage()
        {
            InitializeComponent();
        }

        private void ValueChangetitre(object sender, EventArgs e)
        {
            App.crea.setNomPage(titreTextBox.Text);
        }
    }
}