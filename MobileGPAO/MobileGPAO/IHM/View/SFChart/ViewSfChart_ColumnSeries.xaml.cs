using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GPAOnGo.NGO;
using GPAOnGo.IHM.ERP;
using GPAOnGo.RFID;
using GPAOnGo.UTILITAIRE;
using Rg.Plugins.Popup.Services;


namespace GPAOnGo.IHM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewSfChart_ColumnSeries : ContentView
	{

        public ViewSfChart_ColumnSeries()
		{
			InitializeComponent();

            //ColumnSeries oColumnSeries = new ColumnSeries();
            //oColumnSeries.SetBinding(ColumnSeries.ItemsSourceProperty, new Binding("ERPObjectListeListe"));
            //oColumnSeries.SetBinding(ColumnSeries.XBindingPathProperty, new Binding("ERPObjectListeListe[0].Value_1"));
            //oColumnSeries.SetBinding(ColumnSeries.YBindingPathProperty, new Binding("ERPObjectListeListe[0].Value_2"));
            //oColumnSeries.EnableDataPointSelection = true;
            //oColumnSeries.SelectedDataPointColor = Color.Red;
            //oSfChart.Series.Add(oColumnSeries);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        /*async private void OnSelectionChanged(object sender, Syncfusion.SfChart.XForms.ChartSelectionEventArgs e)
        {
            string vErreur = string.Empty;

            var selectedSeries = e.SelectedSeries;
            var dataPointIndex = e.SelectedDataPointIndex;
            var previousSelectedIndex = e.PreviousSelectedIndex;
            var previousSelectedSeries = e.PreviousSelectedSeries;

            if (dataPointIndex >= 0)
            {
                NGOObject vue = (NGOObject)((SfChart)sender).BindingContext;

                NGOChamps ngoChamp = vue.ListeChamps.ElementAt(dataPointIndex);
                await ngoChamp.Action(Navigation);
            }
        }*/
    }
}