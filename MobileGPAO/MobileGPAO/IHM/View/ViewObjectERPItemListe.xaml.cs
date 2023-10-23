using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GPAOnGo.NGO;

namespace GPAOnGo.IHM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewObjectERPItemListe : ContentView
    {
        public static readonly BindableProperty NbColProperty =
            BindableProperty.Create("NbCol", typeof(int), typeof(ViewObjectERPItemListe), 1, propertyChanged: OnNbColChanged);

        static void OnNbColChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ThisView = (ViewObjectERPItemListe)bindable;

            /*while (ThisView.oGrid.ColumnDefinitions.Count != 0)
            {
                ThisView.oGrid.ColumnDefinitions.RemoveAt(0);
            }
            for (int vCompteur = 1; vCompteur <= (int)newValue; vCompteur++)
            {
                ColumnDefinition cAdd = new ColumnDefinition();
                cAdd.Width = new GridLength(1, GridUnitType.Star);
                ThisView.oGrid.ColumnDefinitions.Add(cAdd);
            }*/
        }

        public ViewObjectERPItemListe()
        {
            InitializeComponent();
        }


        async private void onTabButtonPressed(object sender, EventArgs e)
        {
            string vErreur = string.Empty;

            if (sender != null)
            {
                NGOChamps champ = ((Grid)sender).BindingContext as NGOChamps;
                await champ.Action(Navigation);
            }
        }
    }
}