using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GPAOnGo.IHM.ERP;
using GPAOnGo.RFID;
using GPAOnGo.UTILITAIRE;


namespace GPAOnGo.IHM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewSfChart_LineSeries : ContentView
	{

        public ViewSfChart_LineSeries()
		{
			InitializeComponent();
        }

        /*private void OnSelectionChanged(object sender, Syncfusion.SfChart.XForms.ChartSelectionEventArgs e)
        {
            var selectedSeries = e.SelectedSeries;
            var dataPointIndex = e.SelectedDataPointIndex;
            var previousSelectedIndex = e.PreviousSelectedIndex;
            var previousSelectedSeries = e.PreviousSelectedSeries;
        }*/
    }
}