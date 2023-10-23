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
    public partial class IHMNGOModifOrdreObject : ContentPage
    {
        public IHMNGOModifOrdreObject()
        {
            InitializeComponent();
        }

        private void ValueChangeOrdre(object sender, EventArgs e)
        {
            App.crea.setOrdreObject(titreTextBox.Text);
        }
    }
}